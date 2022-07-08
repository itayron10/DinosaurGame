using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    [Header("References")]
    public static InputManager instance;
    public PlayerInput playerInputActions;


    private void Awake()
    {
        // creates a new player input every time we start the game
        playerInputActions = new PlayerInput();
        SetSingelton();
        ActiveAllInputActions();
    }

    private void Update() => HandleMouseLockState();

    private void ActiveAllInputActions()
    {
        // activate all the input actions in the game
        foreach (var action in playerInputActions.asset.actionMaps) SetInputActionMap(action, true);
    }

    private void SetSingelton()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }


    private void HandleMouseLockState()
    {
        // only if the camera controlls are active we want to lock the mouse to the center
        if (playerInputActions.Camera.enabled)
            Cursor.lockState = CursorLockMode.Locked;
        else
            Cursor.lockState = CursorLockMode.None;
    }

    /// <summary>
    /// this method returns the x and y mouse delta position (x delta position, y delta position)
    /// </summary>
    public static (float, float) GetMouseDeltaPosXandY()
    {
        Vector2 mouseDelta = instance.playerInputActions.Camera.MouseDelta.ReadValue<Vector2>();
        return (mouseDelta.x, mouseDelta.y);
    }

    /// <summary>
    /// this method returns the current mouse screen position
    /// </summary>
    /// <returns></returns>
    public static Vector2 GetMousePosition()
    {
        return Mouse.current.position.ReadValue();
    }

    public static float GetScrollAmount() => instance.playerInputActions.UI.HotBarScroll.ReadValue<float>();

    /// <summary>
    /// this method takes the player input and converts the 2d movement input direction into a 3d world direction
    /// </summary>
    public static Vector3 ConvertPlayerMovementInputToMovementDirection(PlayerInput playerInput)
    {
        Vector2 movementDirInput = playerInput.Movement.MoveHorizontaly.ReadValue<Vector2>();
        return new Vector3(movementDirInput.x, 0f, movementDirInput.y);
    }

    /// <summary>
    /// this method is enables/disables an action map based on the active param it recives
    /// </summary>
    public void SetInputActionMap(InputActionMap actionMap, bool active)
    {
        if (active) actionMap.Enable();
        else actionMap.Disable();
    }
}
