using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPlayerController : MonoBehaviour
{
    [Header("References")]
    [Tooltip("The follow target set in the Cinemachine Virtual Camera that the camera will follow")]
    [SerializeField] GameObject CinemachineCameraTarget;
    [SerializeField] Transform playerTransform;
    private PlayerInput playerInputActions;
    private float _cinemachineTargetPitch;
    private float _rotationVelocity;

    [Header("Settings")]
    [SerializeField] float sensitivity;
    [SerializeField] float maxCameraXRotation; // used to clamp the camera x rotationãâ


    private void Awake() => SetPlayerInput();
    private void Start() => SetCursorState();


    private void LateUpdate() => HandleRotation();

    private void HandleRotation()
    {
        Vector2 mouseDelta = playerInputActions.Camera.MouseDelta.ReadValue<Vector2>();

        //TODO: Make the camera and player rotation smooth and responsive because the current camera rotaters we used are unresponsive or too jittey
        _cinemachineTargetPitch += -mouseDelta.y * sensitivity * Time.deltaTime;
        _rotationVelocity = mouseDelta.x * sensitivity * Time.deltaTime;

        // clamp our pitch rotation
        _cinemachineTargetPitch = ClampAngle(_cinemachineTargetPitch, -maxCameraXRotation, maxCameraXRotation);

        // Update Cinemachine camera target pitch
        CinemachineCameraTarget.transform.localRotation = Quaternion.Euler(_cinemachineTargetPitch, 0.0f, 0.0f);

        // rotate the player left and right
        playerTransform.Rotate(Vector3.up * _rotationVelocity);
    }

    private static float ClampAngle(float lfAngle, float lfMin, float lfMax)
    {
        if (lfAngle < -360f) lfAngle += 360f;
        if (lfAngle > 360f) lfAngle -= 360f;
        return Mathf.Clamp(lfAngle, lfMin, lfMax);
    }

    private void SetPlayerInput()
    {
        playerInputActions = InputManager.instance.playerInputActions;
        playerInputActions.Camera.Enable();
    }

    private static void SetCursorState()
    {
        // set cursor state
        Cursor.lockState = CursorLockMode.Locked;
    }
}
