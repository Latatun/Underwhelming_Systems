using System.Collections.Generic;
using UnityEngine;
using System;
using Cinemachine;

[CreateAssetMenu(fileName = "RuntimeCameras", menuName = "UWS/Fixed Camera System/Runtime Cameras", order = 0)]
public class RuntimeCameras : ScriptableObject
{
    public CameraTuple ActiveCamera => sceneCameras.Find(t => t.relatedCam.enabled);
    public Action OnCameraChange;
    [SerializeField] List<CameraTuple> sceneCameras;

    public void AddRuntimeCamera(CameraTuple tuple)
    {
        if (sceneCameras.Contains(tuple)) return;

        if (sceneCameras.Count == 0)
        {
            tuple.relatedCam.enabled = true;
            sceneCameras.Add(tuple);
            return;
        }

        if (IsHigherPriority(tuple.relatedCam))
        {
            tuple.relatedCam.enabled = true;
            ActiveCamera.relatedCam.enabled = false;
        }
        sceneCameras.Add(tuple);
    }

    public void SwitchCamera(CameraTuple switchTuple)
    {
        if (ActiveCamera.Equals(switchTuple) || !sceneCameras.Contains(switchTuple)) return;

        ActiveCamera.relatedCam.enabled = false;
        switchTuple.relatedCam.enabled = true;
        OnCameraChange?.Invoke();
    }

    private void OnEnable()
    {
        sceneCameras = new List<CameraTuple>();
    }

    private bool IsHigherPriority(CinemachineVirtualCamera cam)
    {
        return cam.Priority > ActiveCamera.relatedCam.Priority;
    }
}
