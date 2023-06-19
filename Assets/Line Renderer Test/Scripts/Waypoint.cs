using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LineRendererTest
{
    [RequireComponent(typeof(CircleCollider2D))]
    public class Waypoint : MonoBehaviour
    {
        [field: SerializeField] public bool IsActive { get; private set; }
        [SerializeField] Color enabledColor, disabledColor;

        public void EnableWaypoint()
        {
            IsActive = true;
            this.GetComponent<SpriteRenderer>().color = enabledColor;
        }

        public void DisableWaypoint()
        {
            IsActive = false;
            this.GetComponent<SpriteRenderer>().color = disabledColor;
        }
    }
}
