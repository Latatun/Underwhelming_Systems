using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace FixedCamera
{
    public class FixedCameraMovement : MonoBehaviour
    {
        [SerializeField] CharacterController characterController;
        [SerializeField] RuntimeCameras runtimeCams;
        [SerializeField] float speed = 4.0f;
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
                MoveCharacter(Time.deltaTime);
        }

        private void MoveCharacter(float dt)
        {
            var movement = new Vector3(dir.x, Vector2.zero.y, dir.y);
            var transformedDir = currentCamDir.TransformDirection(movement);
            characterController.Move(transformedDir * dt * speed);
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
}
