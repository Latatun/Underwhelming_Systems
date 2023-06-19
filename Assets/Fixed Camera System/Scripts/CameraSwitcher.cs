using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

[RequireComponent(typeof(BoxCollider))]
public class CameraSwitcher : MonoBehaviour
{
    [SerializeField] BoxCollider coll;
    [SerializeField] CameraTuple camTuple;
    [SerializeField] RuntimeCameras runtimeCameras;

    private void Awake()
    {
        coll.isTrigger = true;
        SetCamProperties();
        runtimeCameras.AddRuntimeCamera(camTuple);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(TagUtils.PLAYER_TAG))
            runtimeCameras.SwitchCamera(camTuple);
    }

    private void SetCamProperties()
    {
        var character = GameObject.FindGameObjectWithTag(TagUtils.PLAYER_TAG);
        camTuple.relatedCam.LookAt = character.transform;
        camTuple.relatedCam.enabled = false;
    }
}
