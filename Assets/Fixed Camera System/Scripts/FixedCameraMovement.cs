using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FixedCameraMovement : MonoBehaviour
{
    [SerializeField] CharacterController characterController;
    [SerializeField] RuntimeCameras runtimeCams;
    bool isWalking;
    Vector2 dir;
    Transform currentCamDir;

    public void SetCharacterDirection(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            isWalking = true;
            dir = ctx.ReadValue<Vector2>();
        }
        else isWalking = false;
    }

    private void Start()
    {
        currentCamDir = runtimeCams.ActiveCamera.camDirection;
    }

    private void Update()
    {
        if (isWalking)
            MoveCharacter();
    }

    private void MoveCharacter()
    {
        var movement = new Vector3(dir.x, Vector2.zero.y, dir.y);
        var transformedDir = currentCamDir.TransformDirection(movement);
        characterController.Move(transformedDir);
    }

    private IEnumerator ChangeCamDirection()
    {
        while (isWalking)
        {
            yield return null;
        }
        currentCamDir = runtimeCams.ActiveCamera.camDirection;
    }

    private void SetCamDirection()
    {
        StartCoroutine(ChangeCamDirection());
    }

    private void OnEnable()
    {
        runtimeCams.OnCameraChange += SetCamDirection;
    }

    private void OnDisable()
    {
        runtimeCams.OnCameraChange -= SetCamDirection;
    }
}
