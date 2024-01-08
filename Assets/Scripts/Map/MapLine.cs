using System.Collections.Generic;
using UnityEngine;

namespace FubarOps.Map
{
    public class MapLine : MonoBehaviour
    {
        LineRenderer lineRenderer;
        EdgeCollider2D edgeCollider;
        List<Vector2> points = new List<Vector2>();
        float minimalSegmentLengthSquared = 0.25f;

        void Awake()
        {
            lineRenderer = GetComponent<LineRenderer>();
            edgeCollider = GetComponent<EdgeCollider2D>();
        }

        public void SetLineParameters(Color color, float width)
        {
            lineRenderer.startColor = color;
            lineRenderer.endColor = color;
            lineRenderer.startWidth = width;
            lineRenderer.endWidth = width;
            edgeCollider.edgeRadius = width / 2;
        }

        public void AddPoint(Vector2 point)
        {
            if (points.Count == 0)
            {
                points.Add(point);
                lineRenderer.positionCount = points.Count;
                lineRenderer.SetPosition(points.Count - 1, point);
            }
            else if (Vector2.SqrMagnitude(point - points[^1]) >= minimalSegmentLengthSquared)
            {
                points.Add(point);
                lineRenderer.positionCount = points.Count;
                lineRenderer.SetPosition(points.Count - 1, point);
                edgeCollider.SetPoints(points);
            }
        }

        public void DestroyIfTooShort()
        {
            if (points.Count < 2)
            {
                Destroy(gameObject);
            }
        }
    }
}