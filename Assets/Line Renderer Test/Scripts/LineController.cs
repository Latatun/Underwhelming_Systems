using System.Collections.Generic;
using UnityEngine;

namespace LineRendererTest
{
    public static class LineController
    {
        public static void InitializeLineRenderer(this LineRenderer lineRenderer, Material lineMaterial)
        {
            lineRenderer.enabled = true;
            lineRenderer.positionCount = 2;
            lineRenderer.numCapVertices = 16;
            lineRenderer.material = lineMaterial;
            lineRenderer.enabled = false;
        }

        public static void DrawLine(this LineRenderer lineRenderer, Vector2 origin, Vector2 destination)
        {
            EdgeCollider2D coll;
            if (!lineRenderer.gameObject.TryGetComponent<EdgeCollider2D>(out coll))
                coll = lineRenderer.gameObject.AddComponent<EdgeCollider2D>();

            coll.edgeRadius = 1;
            coll.isTrigger = true;
            coll.enabled = true;

            lineRenderer.enabled = true;
            lineRenderer.SetPosition(0, origin);
            lineRenderer.SetPosition(1, destination);

            var edges = new List<Vector2>();
            edges.Add(coll.transform.InverseTransformPoint(lineRenderer.GetPosition(0)));
            edges.Add(coll.transform.InverseTransformPoint(lineRenderer.GetPosition(1)));
            coll.SetPoints(edges);
        }

        public static void DisableLineRenderer(this LineRenderer lineRenderer)
        {
            if (lineRenderer.gameObject.TryGetComponent<EdgeCollider2D>(out var coll))
                coll.enabled = false;
            lineRenderer.enabled = false;
        }
    }
}
