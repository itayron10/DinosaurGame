using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.AI;

public class PlayerMovement : MonoBehaviour
{
    [Header("Refernces")]
    [Tooltip("This effect will play when the player lands")]
    [SerializeField] ParticleSystem landEffect; // the particle effect that play when the player lands
    private float currentSpeed; // the mutiplier of the force being applied every frame
    private float currentMaxVelocity; // the mutiplier of the force being applied every frame
    private InputManager inputManager;
    private SoundManager soundManager;
    private PlayerInput playerInput;
    private FootStepsAuioManager footStepsAudioManager;
    private PlayerMovementStatsHandeler playerStatsHandeler;
    private Rigidbody rb;
    private bool grounded; // is the player currently on the ground
    private bool moving; // is the player currently moving
    public bool IsMoving() { return moving; }
    private bool running; // is the player currently running
    public bool IsRunning() { return running; }
    private bool hasLanded; // is the player landed on the floor? 
    private const float MinSlopeSpeedMultiplier = 1f;
    private const float landVelocityThreshold = 1f; // the min y velocity which the player need to be in for the land effects to work when landing

    [Header("Horizontal Movement")]
    [Tooltip("This drag will be the rigidbodt's drag when the player is not moving and is on the ground")]
    [SerializeField] float stoppingDrag;
    [Tooltip("This drag will be the rigidbodt's drag when the player is moving and is on the ground")]
    [SerializeField] float defultDrag;
    [Tooltip("The speed which assaigns to the current speed on defult when moving")]
    [SerializeField] float defultSpeed;
    [Tooltip("The max velocity the rigidbody can reach to in any direction which assaigns on defult when moving")]
    [SerializeField] float defultMaxVelocity;
    [Tooltip("The speed which assaigns to the current speed when running")]
    [SerializeField] float runningSpeed;
    [Tooltip("The max velocity the rigidbody can reach to in any direction that which assaigns to when running")]
    [SerializeField] float runningMaxVelocity;

    [Header("Jump")]
    [Tooltip("The force being added to the rigidbody when pressing the jump button")]
    [SerializeField] float jumpForce;
    [Tooltip("A multiplier of the gravity which applies to the rigidbody when falling")]
    [SerializeField] float fallMultiplier;
    [Tooltip("A multiplier of the gravity which applies to the rigidbody not holding jump after jumping")]
    [SerializeField] float lowJumpMultiplier;
    [Tooltip("The length of the raycast which comes from the player downward to check for walkable terrain")]
    [SerializeField] float groundRayCheckerLength;
    [Tooltip("The layer which the player will consider grounded on")]
    [SerializeField] LayerMask walkableLayer;
    [SerializeField] SoundScriptableObject jumpSoundEffect, landSoundEffect;
    public delegate void OnPlayerJumped();
    public OnPlayerJumped playerJumped;
    
    [Header("Slopes")]
    [Tooltip("How fast the player reaches to the max slope boost when on slopes")]
    [SerializeField] float slopeAccelerationMultiplier; // the multiplier of the slope-player dot product, used to determine how fast the player reaches maxSlopeSpeedMultiplier
    [Tooltip("The max speed which the slope speed multiplier can reach to")]
    [SerializeField] float maxSlopeSpeedMultiplier; // the max value that the currentSlopeSpeedMultiplier can reach to
    private float currentSlopeSpeedMultiplier; // the current slope multiplier of the speed, used to give the speed a boost when player on slopes

    [Header("Shake Screen")]
    private float rigidbodyYVelocityOnLand; // the y velocity of the rigidbody, only updated if the y velocity is greater than 1 
    [SerializeField] float minYLandVelocityMultiplier = 0.1f, maxYLandVelocityMultiplier = 4f;
    [SerializeField] float landShakeStreangth = 6f, landShakeFreequancy = 0.1f, landShakeDuration = 0.3f;

    [Header("Footsteps")]
    [SerializeField] float distanceBetweenSteps;
    private Vector3 lastStepPosition;
    public float GetCurrentVelocitySpeed() { return rb.velocity.magnitude; }


    private void Awake() => SubscribeToInputEvents();

    private void Start()
    {
        FindPrivateObjects();
        SetPrivateValues();
    }

    private void FixedUpdate()
    {
        SetLandVelocity();
        CheckForGround();
        HandleSlopes();
        HandleMovement();
        HandleDragValues();
        HandleFall();
        HandleSteps();
    }

    private void SetLandVelocity()
    {
        float currentYVelocity = Mathf.Abs(rb.velocity.y);
        if (currentYVelocity > 1) rigidbodyYVelocityOnLand = currentYVelocity;
    }

    private void HandleSteps()
    {
        if (!grounded) 
            lastStepPosition = transform.position;
        else
            UpdateFootSteps();
    }

    private void UpdateFootSteps()
    {
        Vector3 lastStepPositionWithFixedY = new Vector3(lastStepPosition.x, transform.position.y, lastStepPosition.z);
        float currentDistanceFromLastStepPosition = Vector3.Distance(transform.position, lastStepPositionWithFixedY);
        if (currentDistanceFromLastStepPosition >= distanceBetweenSteps) Step();
    }

    private void Step()
    {
        //Step
        soundManager.PlaySound(footStepsAudioManager.GetCurrentFootStepSound());
        lastStepPosition = transform.position;
    }

    private void CheckForGround()
    {
        // cancel landing 
        if (!grounded) hasLanded = false;
        Ray groundCheckerRay = new Ray(transform.position, Vector3.down);
        grounded = Physics.Raycast(groundCheckerRay, groundRayCheckerLength, walkableLayer);
        LandCheck();
    }

    private void LandCheck()
    {
        float yVelocity = Mathf.Clamp(rigidbodyYVelocityOnLand * 0.1f, minYLandVelocityMultiplier, maxYLandVelocityMultiplier);

        if (grounded && !hasLanded)
        {
            // landed
            hasLanded = true;
            if (yVelocity > landVelocityThreshold) ActivateLandEffects(yVelocity);
        }
    }

    private void ActivateLandEffects(float rbYVelocity)
    {
        CinemachineShake.instance.Shake
            (landShakeStreangth * rbYVelocity, landShakeFreequancy, landShakeDuration * rbYVelocity);
        ParticleManager.StartParticle(landEffect);
        soundManager.PlaySound(landSoundEffect);
    }

    private void HandleDragValues()
    {
        if (!moving && grounded)
            rb.drag = stoppingDrag;
        else if (grounded)
            rb.drag = defultDrag;
        else
            rb.drag = 0f;

    }

    private void FindPrivateObjects()
    {
        rb = GetComponent<Rigidbody>();
        soundManager = SoundManager.instance;
        footStepsAudioManager = GetComponent<FootStepsAuioManager>();
        playerStatsHandeler = GetComponent<PlayerMovementStatsHandeler>();
    }

    private void HandleFall()
    {
        float jumpPressingValue = playerInput.Movement.Jump.ReadValue<float>();
        float roundedYVelocity = Mathf.Round(rb.velocity.y); 
        if (roundedYVelocity < 0f)
            rb.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1f) * Time.deltaTime;
        else if (roundedYVelocity > 0f && jumpPressingValue <= 0f)
            rb.velocity += Vector3.up * Physics.gravity.y * (lowJumpMultiplier - 1f) * Time.deltaTime;
    }

    private void HandleSlopes()
    {
        if (OnSlope(out RaycastHit slopeCheckerHit))
        {
            float slopeDotProduct = Vector3.Dot(transform.forward, slopeCheckerHit.normal);
            if (slopeDotProduct < 0f)
            {
                // climbing on slope
                // set the slope speed multiplier by the slopeDotPrpduct and clamp it
                currentSlopeSpeedMultiplier = Mathf.Clamp(Mathf.Abs(slopeDotProduct * slopeAccelerationMultiplier),
                    MinSlopeSpeedMultiplier, maxSlopeSpeedMultiplier);
            }
        }
        else
        {
            // resets the slopeSpeedMultiplier if not on slope
            currentSlopeSpeedMultiplier = MinSlopeSpeedMultiplier;
        }

    }

    private bool OnSlope(out RaycastHit slopeCheckerHit)
    {
        if (Physics.Raycast(transform.position, Vector3.down, out slopeCheckerHit, groundRayCheckerLength, walkableLayer))
            return Vector3.Distance(slopeCheckerHit.normal, Vector3.up) > 0.1f;
        return false;
    }

    private void SetPrivateValues()
    {
        SetCurrentMaxVelocity(defultMaxVelocity);
        SetCurrentSpeed(defultSpeed);
        lastStepPosition = transform.position;
    }

    private void SubscribeToInputEvents()
    {
        inputManager = InputManager.instance;
        playerInput = inputManager.playerInputActions;
        playerInput.Movement.IncreaseSpeed.performed += OnIncreaseSpeedPressed;
        playerInput.Movement.IncreaseSpeed.canceled += OnIncreaseSpeedCanceled;
        playerInput.Movement.Jump.performed += OnJumpPressed;
    }

    private void HandleMovement()
    {
        Vector3 moveDir = InputManager.ConvertPlayerMovementInputToMovementDirection(playerInput);
        moving = moveDir.magnitude > 0f;
        if (moving) Move(moveDir);

    }

    private void Move(Vector3 moveDir)
    {
        // checks if the stamina is greater than the min stamina amount if not then the player can't move
        moving = true;
        Vector3 movementForce = transform.TransformDirection(moveDir.normalized) * currentSpeed * currentSlopeSpeedMultiplier;


        // adding rigidbody forces in the movement direction
        rb.AddForce(movementForce, ForceMode.Force);
        // if running make sure that you can run
        if (running)
        {
            if (!playerStatsHandeler.HasStamina()) SetPlayerRunningState(false);
        }

        // limiting the velocity by applying forces on negetive direction when the maxVelocity is bigger than the rb velocity
        if (rb.velocity.magnitude > currentMaxVelocity)
            rb.AddForce(-movementForce, ForceMode.Force);
    }


    private void SetCurrentSpeed(float speedAmount) { this.currentSpeed = speedAmount; }
    private void SetCurrentMaxVelocity(float maxVelocity) { this.currentMaxVelocity = maxVelocity; }

    private void OnIncreaseSpeedCanceled(InputAction.CallbackContext contex) { SetPlayerRunningState(false); }

    private void OnIncreaseSpeedPressed(InputAction.CallbackContext contex) { SetPlayerRunningState(true); }

    private void SetPlayerRunningState(bool active)
    {
        running = active;
        if (active && playerStatsHandeler.HasStamina())
        {
            SetCurrentSpeed(runningSpeed);
            SetCurrentMaxVelocity(runningMaxVelocity);
        }
        else
        {
            SetCurrentMaxVelocity(defultMaxVelocity);
            SetCurrentSpeed(defultSpeed);
        }
    }

    private void OnJumpPressed(InputAction.CallbackContext context)
    {
        float jumpNormalizedStaminaAmount = playerStatsHandeler.GetJumpStaminaNormalizedConsumeAmount();
        if (!grounded || !playerStatsHandeler.HasStamina(jumpNormalizedStaminaAmount)) { return; }
        Jump();
    }

    private void Jump()
    {
        playerJumped?.Invoke();
        rb.AddForce(jumpForce * Vector3.up, ForceMode.Impulse);
        soundManager.PlaySound(jumpSoundEffect);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * groundRayCheckerLength);
    }
}