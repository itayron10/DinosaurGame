using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimationController : MonoBehaviour
{
    [SerializeField] string movementSpeedParam;
    private PlayerMovement playerMovement;
    private Animator animator;


    private void Awake()
    {
        animator = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        UpdateMovementState();
    }

    private void UpdateMovementState()
    {
        animator.SetFloat(movementSpeedParam, playerMovement.GetCurrentVelocitySpeed());
    }
}
