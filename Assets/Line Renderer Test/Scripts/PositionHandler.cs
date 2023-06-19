using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace LineRendererTest
{
    public class PositionHandler : MonoBehaviour
    {
        [SerializeField] LineRenderer lineRenderer;
        [SerializeField] Camera cam;
        [SerializeField] Material lineMaterial;
        Waypoint currentWaypoint;
        Coroutine drawCoroutine;

        private void Start()
        {
            lineRenderer.InitializeLineRenderer(lineMaterial);
        }

        public void OnMouseClick(InputAction.CallbackContext ctx)
        {
            if (ctx.canceled)
            {
                currentWaypoint = null;
                lineRenderer.DisableLineRenderer();
                return;
            }
            if (!ctx.performed) return;


            var mousePos = cam.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            var hit = Physics2D.Raycast(mousePos, Vector2.up, Mathf.Infinity);

            if (hit.collider)
            {
                if (!hit.collider.TryGetComponent<Waypoint>(out var waypoint) || !waypoint.IsActive) return;
                currentWaypoint = waypoint;
                drawCoroutine = StartCoroutine(DrawLine());
            }
            else
            {
                currentWaypoint = null;
                drawCoroutine = null;
            }
        }

        IEnumerator DrawLine()
        {
            while (currentWaypoint != null)
            {
                lineRenderer.DrawLine(currentWaypoint.transform.position, cam.ScreenToWorldPoint(Mouse.current.position.ReadValue()));
                yield return null;
            }
            lineRenderer.DisableLineRenderer();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent<Waypoint>(out var waypoint))
            {
                currentWaypoint?.DisableWaypoint();
                waypoint.EnableWaypoint();
                currentWaypoint = waypoint;
            }
        }
    }
}
