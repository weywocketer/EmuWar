using UnityEngine;

namespace FubarOps.Terrain
{
    [ExecuteInEditMode]
    public class Obstacle : MonoBehaviour, IEntity
    {
        public float radius = 1;
        public float Radius
        {
            get { return radius; }
        }

        public Transform circleTransform;


        void Start()
        {
            circleTransform = transform.GetChild(0);
        }

        void Update()
        {
            circleTransform.localScale = new Vector3(radius * 2, radius * 2, 0);
        }
    }
}