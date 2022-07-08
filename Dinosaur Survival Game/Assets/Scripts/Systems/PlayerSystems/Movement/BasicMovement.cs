using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class BasicMovement : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] float defultSpeed; // the defult mutiplier of the force being applied every frame
    [SerializeField] float boostSpeed; // the faster mutiplier of the force being applied every frame (active once increase speed performs)
    [SerializeField] float stoppingDrag; // the drag will be applied once the player stop moving 
    [SerializeField] float sensitivity;

    [Header("References")]
    private PlayerInput playerInputActions;
    private bool canRotate = false;
    private float currentSpeed; // the mutiplier of the force being applied every frame
    private PlayerInput playerInput;
    private Rigidbody rb;

    void Awake() => SubscribeToInputEvents();

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        SetCurrentSpeed(defultSpeed);
    }

    private void FixedUpdate()
    {
        if (canRotate)
            HandleRotation();
        HandleMovement();
    }

    private void SubscribeToInputEvents()
    {
        playerInputActions = InputManager.instance.playerInputActions;
        playerInputActions.Movement.IncreaseSpeed.performed += OnIncreaseSpeedPressed;
        playerInputActions.Movement.IncreaseSpeed.canceled += OnIncreaseSpeedCanceled;
        playerInputActions.Camera.StartRotating.performed += StartRotating;
        playerInputActions.Camera.StartRotating.canceled += StopRotating;
    }

    private void StopRotating(InputAction.CallbackContext contex) { canRotate = false; }
    private void StartRotating(InputAction.CallbackContext contex) { canRotate = true; }
    private void OnIncreaseSpeedCanceled(InputAction.CallbackContext contex) { SetCurrentSpeed(defultSpeed); }
    private void OnIncreaseSpeedPressed(InputAction.CallbackContext contex) { SetCurrentSpeed(boostSpeed); }

    private void SetCurrentSpeed(float speedAmount)
    {
        currentSpeed = speedAmount;
    }

    private void HandleMovement()
    {
        Vector2 horizontalInputDir = playerInput.Movement.MoveHorizontaly.ReadValue<Vector2>();
        float verticalInput = playerInput.Movement.MoveVerticly.ReadValue<float>();
        Vector3 moveDir = new Vector3(horizontalInputDir.x, verticalInput, horizontalInputDir.y);

        if (moveDir.magnitude > 0f)
            Move(moveDir);
        else
            rb.drag = stoppingDrag;
    }

    private void HandleRotation()
    {
        Vector2 mouseDelta = playerInput.Camera.MouseDelta.ReadValue<Vector2>();
        Vector3 rotation = new Vector3(-mouseDelta.y * sensitivity, mouseDelta.x * sensitivity, 0f); 
        transform.eulerAngles = Vector3.Lerp(transform.eulerAngles, (transform.eulerAngles + rotation), 5f * Time.deltaTime);
    }

    private void Move(Vector3 moveDir)
    {
        rb.drag = 0f;
        rb.AddForce(transform.TransformDirection(moveDir) * currentSpeed, ForceMode.Force);
    }
}
