using UnityEngine;
using Cinemachine;

namespace FixedCamera
{
    [System.Serializable]
    public class CameraTuple
    {
        [field: SerializeField] public Transform camDirection { get; private set; }
        [field: SerializeField] public CinemachineVirtualCamera relatedCam { get; private set; }

        public CameraTuple(Transform camDirection, CinemachineVirtualCamera relatedCam)
        {
            this.camDirection = camDirection;
            this.relatedCam = relatedCam;
        }
    }
}