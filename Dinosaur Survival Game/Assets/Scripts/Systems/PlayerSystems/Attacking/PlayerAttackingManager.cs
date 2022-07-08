using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttackingManager : MonoBehaviour
{
    [Header("Refernecs")]
    public static PlayerAttackingManager instance;
    private PlayerInput playerInput;
    
    public delegate void PlayerAttackAction();
    public static event PlayerAttackAction OnPlayerAction;


    void Awake()
    {
        SetSingelton();
        SubscribeToInput();
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

    private void SubscribeToInput()
    {
        playerInput = InputManager.instance.playerInputActions;
        playerInput.Combat.Attack.performed += OnAttackPerformed;
    }

    private void OnAttackPerformed(InputAction.CallbackContext context) => OnPlayerAction?.Invoke();
}
