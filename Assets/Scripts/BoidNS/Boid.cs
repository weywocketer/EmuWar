using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using FubarOps.Movement;
using FubarOps.Combat;
using FubarOps.Terrain;
using FubarOps.GridNS;

namespace FubarOps.BoidNS
{
    /// <summary>
    /// Handles the high level unit's movement and gameObject's destruction.
    /// </summary>
    [RequireComponent(typeof(Rigidbody2D))]
    public class Boid : MonoBehaviour
    {
        [Header("General")]

        bool canMove = true;
        float timeWhenMovementEnabled = 0;
        public ISteering steering;
        Health health;
        public GridPositionTracker gridPositionTracker;
        Rigidbody2D rb;
        [SerializeField] float terrainSpeedModifier = 1f;
        [SerializeField] List<TerrainArea> collidingTerrainAreas = new();
        int terrainLayer = 6;
        Transform footprintHierarchyParent;

        protected virtual void Awake()
        {
            steering = GetComponent<ISteering>();
            health = GetComponent<Health>();
            gridPositionTracker = GetComponent<GridPositionTracker>();
            rb = GetComponent<Rigidbody2D>();
            footprintHierarchyParent = GameObject.FindGameObjectWithTag("FootprintParent").transform;
        }

        void OnEnable()
        {
            health.OnDeath += DestroyBoid;

            if (TryGetComponent(out DistanceAttack distanceAttack))
            {
                distanceAttack.OnWeaponFired += DisableMovement;
            }
        }

        void OnDisable()
        {
            health.OnDeath -= DestroyBoid;

            if (TryGetComponent(out DistanceAttack distanceAttack))
            {
                distanceAttack.OnWeaponFired -= DisableMovement;
            }
        }

        protected virtual void FixedUpdate()
        {
            if (canMove)
            {
                Vector3 velocity = steering.CalculateVelocity();
                rb.velocity = velocity * terrainSpeedModifier;
                if (Mathf.Abs(velocity.x) >= 0.5f || Mathf.Abs(velocity.y) >= 0.5f)
                {
                    transform.eulerAngles = new Vector3(0, 0, Vector3.SignedAngle(Vector3.right, velocity, Vector3.forward));
                }
            }
            else
            {
                if (Time.time >= timeWhenMovementEnabled)
                {
                    EnableMovement();
                }
            }

            gridPositionTracker.UpdateGridPosition();
        }

        void UpdateTerrainSpeedModifier()
        {

            if (collidingTerrainAreas.Count > 0)
            {
                terrainSpeedModifier = collidingTerrainAreas.Max(terrainArea => terrainArea.speedModifier);
            }
            else
            {
                terrainSpeedModifier = 1f;
            }
        }

        void DisableMovement(float movementDisabledDuration)
        {
            canMove = false;
            rb.velocity = Vector3.zero;

            // Set new timeUntillMovementEnabled value if its higher than the current one.
            float newTimeWhenMovementEnabled = Time.time + movementDisabledDuration;
            timeWhenMovementEnabled = Mathf.Max(timeWhenMovementEnabled, newTimeWhenMovementEnabled);
        }

        void EnableMovement()
        {
            canMove = true;
        }

        void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.layer == terrainLayer)
            {
                collidingTerrainAreas.Add(collision.gameObject.GetComponent<TerrainArea>());
            }
            UpdateTerrainSpeedModifier();
        }

        void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.layer == terrainLayer)
            {
                collidingTerrainAreas.Remove(collision.gameObject.GetComponent<TerrainArea>());
            }
            UpdateTerrainSpeedModifier();
        }

        public virtual void DestroyBoid()
        {
            gridPositionTracker.RemoveBoidFromGrid();

            // Detach footprint particle system, so it stays in the scene.
            Transform footprintParticleSystem = transform.Find("FootprintParticle");
            if (footprintParticleSystem)
            {
                footprintParticleSystem.SetParent(footprintHierarchyParent);
                // TODO: consider adding some script to remove detached particle systems after some time, when they are not visible anyway.
            }

            Destroy(gameObject);
        }

        public float GetSquaredDistanceTo(Transform targetTransform)
        {
            return Vector2.SqrMagnitude(targetTransform.position - transform.position);
        }

    }
}