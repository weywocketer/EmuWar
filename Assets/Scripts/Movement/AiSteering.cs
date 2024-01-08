using System.Collections.Generic;
using UnityEngine;
using FubarOps.Terrain;
using FubarOps.Combat;
using FubarOps.Core;
using FubarOps.GridNS;

namespace FubarOps.Movement
{
    /// <summary>
    /// Responsible for calculating velocity based on active steering behaviours.
    /// </summary>
    public class AiSteering : MonoBehaviour, ISteering
    {
        [Header("General")]
        public float maxSpeed = 5;
        [SerializeField] bool debugOn = false;
        public float radius = 0.25f;

        public float Radius
        {
            get { return radius; }
        }

        [SerializeField] int groupId;

        Vector3 velocity = Vector3.zero;
        float xBorder;
        float yBorder;
        float borderMargin;
        GridPositionTracker gridPositionTracker;

        [Header("Behaviour weights")]
        [SerializeField] float weightSeek = 0;
        [SerializeField] float weightFlee = 0;
        [SerializeField] float weightArrive = 0;
        [SerializeField] float weightObstacleAvoidance = 0;
        [SerializeField] float weightWander = 10;
        [SerializeField] float weightFlocking = 0;
        [SerializeField] float weightHide = 0;
        [SerializeField] float weightOffsetPursuit = 0;

        [Header("Seek/Flee/Arrive")]
        [SerializeField]
        public Vector2 targetPosition;
        Transform targetTransform = null;
        public Transform TargetTransform
        {
            get
            {
                return targetTransform;
            }
            set
            {
                if (targetTransform != null)
                {
                    if (targetTransform.gameObject.TryGetComponent(out Health oldTargetsHealth))
                    {
                        oldTargetsHealth.OnDeath -= ResetTarget;
                    }
                }

                targetTransform = value;

                if (targetTransform != null)
                {
                    if (targetTransform.gameObject.TryGetComponent(out Health newTargetsHealth))
                    {
                        newTargetsHealth.OnDeath += ResetTarget;
                    }
                }
            }
        }

        [Header("Obstacle avoidance")]
        public float minDetectionBoxLength = 2;
        private GameObject detectionBox;
        [SerializeField] private float breakingWeight = 0.2f;
        public Color detectionBoxClear = new Color(0, 1, 0, 0.5f);
        public Color detectionBoxActive = new Color(1, 0, 0, 0.5f);

        [Header("Wander")]
        [SerializeField] float wanderRadius = 1;
        [SerializeField] float wanderDistance = 1;
        [SerializeField] float wanderJitter = 1;
        Vector3 wanderTarget;
        GameObject wanderTargetDebug;

        [Header("Flocking")]
        [SerializeField] float neighbourhoodRadius = 3;
        float neighbourhoodRadiusSquared;

        [Header("Hide")]
        [SerializeField] float hideDistance = 1;

        [Header("Offset pursuit")]
        public Vector2 positionInFormation;


        void Awake()
        {
            // General
            gridPositionTracker = GetComponent<GridPositionTracker>();

            // ObstacleAvoidance
            detectionBox = transform.Find("DetectionBox").gameObject;

            // Wander
            wanderTarget = transform.position;
            wanderTargetDebug = transform.Find("WanderTargetDebug").gameObject;

            // Flocking
            neighbourhoodRadiusSquared = neighbourhoodRadius * neighbourhoodRadius;
        }

        void Start()
        {
            // General
            borderMargin = GameController.Instance.borderMargin;
            xBorder = GameController.Instance.mapSize.x - borderMargin;
            yBorder = GameController.Instance.mapSize.y - borderMargin;
        }

        void OnDisable()
        {
            if (targetTransform != null)
            {
                if (targetTransform.gameObject.TryGetComponent(out Health health))
                {
                    health.OnDeath -= ResetTarget;
                }
            }
        }

        void ResetTarget()
        {
            // The target should remove it's OnDeath subscribers if it dies, so we don't have to use the TargetTransform setter here.
            targetTransform = null;
            
            // TODO: This should not be here! Move to FSM or somewhere else.
        }

        public Vector3 CalculateVelocity()
        {
            Vector3 steeringForce = CalculateSteeringForce();

            velocity += steeringForce * Time.fixedDeltaTime;
            velocity = Vector3.ClampMagnitude(velocity, maxSpeed);

            ClampToBorders();

            return velocity;
        }

        void ClampToBorders()
        {
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, borderMargin, xBorder), Mathf.Clamp(transform.position.y, borderMargin, yBorder), 0);

            if (transform.position.x >= xBorder || transform.position.x <= borderMargin)
            {
                velocity = new Vector3(-velocity.x, velocity.y, 0);
            }

            if (transform.position.y >= yBorder || transform.position.y <= borderMargin)
            {
                velocity = new Vector3(velocity.x, -velocity.y, 0);
            }
        }

        Vector3 CalculateSteeringForce()
        {
            Vector3 steeringForce = Vector3.zero;

            if (weightSeek != 0)
            {
                if (targetTransform != null)
                {
                    targetPosition = targetTransform.position;
                }

                steeringForce += Seek(targetPosition) * weightSeek;
            }

            if (weightFlee != 0)
            {
                steeringForce += Flee() * weightFlee;
            }

            if (weightArrive != 0)
            {
                steeringForce += Arrive(TargetTransform.position) * weightArrive;
            }

            if (weightObstacleAvoidance != 0)
            {
                steeringForce += ObstacleAvoidance() * weightObstacleAvoidance;
            }

            if (weightWander != 0)
            {
                steeringForce += Wander() * weightWander;
            }

            if (weightFlocking != 0)
            {
                steeringForce += Flocking() * weightFlocking;
            }

            if (weightHide != 0)
            {
                steeringForce += Hide() * weightHide;
            }

            if (weightOffsetPursuit != 0)
            {
                steeringForce += OffsetPursuit() * weightOffsetPursuit;
            }


            return steeringForce;
        }

        public void SetBehaviourWeights( (float seek, float flee, float arrive, float obstacleAvoidance, float wander, float flocking, float hide, float offsetPursuit) weights )
        {
            weightSeek = weights.seek;
            weightFlee = weights.flee;
            weightArrive = weights.arrive;
            weightObstacleAvoidance = weights.obstacleAvoidance;
            weightWander = weights.wander;
            weightFlocking = weights.flocking;
            weightHide = weights.hide;
            weightOffsetPursuit = weights.offsetPursuit;
        }

        // Recalculate neighbourhoodRadiusSquared if case of neighbourhoodRadius value change.
        void OnValidate()
        {
            neighbourhoodRadiusSquared = neighbourhoodRadius * neighbourhoodRadius;
        }


        // Steering behaviours:
        Vector3 Seek(Vector3 targetPosition) // The targetPosition parameter is necessary here, as other behaviours also use Seek() with different targets.
        {
            Vector3 desiredVelocity = Vector3.Normalize(targetPosition - transform.position) * maxSpeed;

            if (debugOn)
            {
                Debug.DrawRay(transform.position, desiredVelocity - velocity, Color.blue);
            }
            return (desiredVelocity - velocity);
        }

        Vector3 Flee()
        {
            Vector3 desiredVelocity = Vector3.Normalize(transform.position - TargetTransform.position) * maxSpeed;

            return (desiredVelocity - velocity);
        }

        Vector3 Arrive(Vector3 targetPosition) // The targetPosition parameter is necessary here, as other behaviours also use Arrive() with different targets.
        {
            float deceleration = 2;
            float distance = Vector3.Distance(transform.position, targetPosition);

            if (distance > 0)
            {
                float speed = distance / deceleration;
                speed = Mathf.Min(speed, maxSpeed);

                Vector3 desiredVelocity = (targetPosition - transform.position) * speed / distance;

                if (debugOn)
                {
                    Debug.DrawRay(transform.position, desiredVelocity - velocity, Color.green);
                }
                return (desiredVelocity - velocity);
            }

            return new Vector3(0, 0, 0);
        }

        Vector3 ObstacleAvoidance() // TODO: Rewrite ObstacleAvoidance so it uses 2D colliders and tags instead of predefined obstacle list.
        {
            float detectionBoxLength = minDetectionBoxLength + (velocity.magnitude / maxSpeed) * minDetectionBoxLength;
            List<GameObject> obstaclesInRange = new List<GameObject>();


            foreach (GameObject obstacle in GameController.Instance.obstacles)
            {
                if (Vector3.Distance(transform.position, obstacle.transform.position) <= detectionBoxLength)
                {
                    obstaclesInRange.Add(obstacle);
                }
            }

            float distanceToClosestObstacle = Mathf.Infinity;
            GameObject closestObstacle = null;
            Vector3 localPosOfClosestObstacle = new Vector3();

            foreach (GameObject obstacle in obstaclesInRange)
            {
                Vector3 localPos = transform.InverseTransformPoint(obstacle.transform.position);

                if (localPos.x >= 0)
                {

                    float expandedRadius = obstacle.GetComponent<Obstacle>().radius + radius;

                    if (Mathf.Abs(localPos.y) < expandedRadius)
                    {


                        // The center of the circle is represented by (cX, cY). The intersection points are given by the formula x = cX +/-sqrt(r^2-cY^2) for y=0.
                        float sqrtPart = Mathf.Sqrt(expandedRadius * expandedRadius - localPos.y * localPos.y);
                        float intersectionPointX = localPos.x - sqrtPart;

                        if (intersectionPointX <= 0)
                        {
                            intersectionPointX = localPos.x + sqrtPart;
                        }

                        if (intersectionPointX < distanceToClosestObstacle)
                        {
                            distanceToClosestObstacle = intersectionPointX;
                            closestObstacle = obstacle;
                            localPosOfClosestObstacle = localPos;
                        }

                    }
                }

            }

            if (debugOn)
            {
                detectionBox.transform.localScale = new Vector3(detectionBox.transform.localScale.x, detectionBoxLength, detectionBox.transform.localScale.z);
                if (closestObstacle != null)
                {
                    detectionBox.GetComponent<SpriteRenderer>().color = detectionBoxActive;
                }
                else
                {
                    detectionBox.GetComponent<SpriteRenderer>().color = detectionBoxClear;
                }
            }


            Vector3 steeringForce = new Vector3(0, 0, 0);

            if (closestObstacle != null)
            {
                steeringForce.y = (closestObstacle.GetComponent<Obstacle>().radius - localPosOfClosestObstacle.y) * (1 + (detectionBoxLength - localPosOfClosestObstacle.x) / detectionBoxLength);


                steeringForce.x = (closestObstacle.GetComponent<Obstacle>().radius - localPosOfClosestObstacle.x) * breakingWeight;
            }


            steeringForce = transform.TransformVector(steeringForce);

            if (debugOn)
            {
                Debug.DrawRay(transform.position, steeringForce, Color.red);
            }

            return steeringForce;
        }

        Vector3 Wander()
        {
            wanderTarget += new Vector3(Random.Range(-1f, 1f) * wanderJitter, Random.Range(-1f, 1f) * wanderJitter, 0);
            wanderTarget = Vector3.Normalize(wanderTarget) * wanderRadius;
            Vector3 desiredVelocity = transform.TransformPoint(wanderTarget + new Vector3(wanderDistance, 0, 0)) - transform.position;

            if (debugOn)
            {
                wanderTargetDebug.transform.localPosition = wanderTarget + new Vector3(wanderDistance, 0, 0);
                Debug.DrawRay(transform.position, desiredVelocity, Color.cyan);
            }

            return desiredVelocity;
        }

        Vector3 Flocking()
        {
            int neighbourCount = 0;
            Vector3 steeringForce;
            Vector3 separationForce = new Vector3(0, 0, 0);
            Vector3 cohesionForce = new Vector3(0, 0, 0);
            Vector3 alignmentForce = new Vector3(0, 0, 0);
            Vector3 centerOfMass = new Vector3(0, 0, 0);

            foreach (GameObject boid in gridPositionTracker.GetListOfNearbyBoids(gridPositionTracker.gridUsedByThisBoid))
            {
                if (boid != gameObject)
                {
                    Vector3 fromBoid = transform.position - boid.transform.position;
                    float squaredMagnitude = fromBoid.sqrMagnitude;

                    if (squaredMagnitude <= neighbourhoodRadiusSquared)
                    {
                        neighbourCount++;

                        // Separation force
                        if (squaredMagnitude > 0)
                        {
                            // Separation is a normalized vector fromBoid, moreover divided by its length (thus division by squared magnitude [for the sake of optimization])
                            separationForce += fromBoid / squaredMagnitude;
                        }

                        centerOfMass += boid.transform.position;

                        // Alignment is based on velocity (Reynolds) not heading (Buckland), TODO: Test with heading.
                        alignmentForce += (Vector3)boid.GetComponent<Rigidbody2D>().velocity;
                    }
                }
            }

            if (neighbourCount > 0)
            {
                centerOfMass /= (float)neighbourCount;
                cohesionForce = Seek(centerOfMass);

                alignmentForce /= (float)neighbourCount;
            }

            if (debugOn)
            {
                Debug.DrawRay(transform.position, separationForce, Color.yellow);
                Debug.DrawRay(transform.position, cohesionForce, Color.white);
                Debug.DrawRay(transform.position, alignmentForce, Color.magenta);
            }

            steeringForce = separationForce + cohesionForce + alignmentForce;

            return steeringForce;
        }

        Vector3 Hide()
        {
            Vector3 bestHidingPosition = new Vector3();
            float squaredDistanceToClosest = Mathf.Infinity;

            foreach (GameObject obstacle in GameController.Instance.obstacles)
            {
                Vector3 hidingPosition = Vector3.Normalize(obstacle.transform.position - TargetTransform.position) * (obstacle.GetComponent<Obstacle>().radius + hideDistance) + obstacle.transform.position;
                float squaredDistance = Vector3.SqrMagnitude(hidingPosition - transform.position);

                if (squaredDistance < squaredDistanceToClosest)
                {
                    squaredDistanceToClosest = squaredDistance;
                    bestHidingPosition = hidingPosition;
                }
            }

            if (debugOn)
            {
                Debug.DrawLine(transform.position, bestHidingPosition, Color.magenta);
            }

            return Seek(bestHidingPosition);
        }

        Vector3 OffsetPursuit()
        {
            Vector2 destination = targetTransform.TransformPoint(positionInFormation); // Convert offset position from squad leader's space to world space.

            return Arrive(destination);

            //float lookAheadTime = (destination - new Vector2(transform.position.x, transform.position.y)).magnitude / (maxSpeed + targetTransform.GetComponent<Rigidbody2D>().velocity.magnitude);
            //return Arrive(destination + targetTransform.GetComponent<Rigidbody2D>().velocity * lookAheadTime);
        }
    }
}

