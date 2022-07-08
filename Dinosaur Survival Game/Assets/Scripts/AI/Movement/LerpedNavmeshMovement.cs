using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpedNavmeshMovement : BasicNavmeshMovement
{
    [Header("Settings")]
    [Tooltip("The min speed which the object can move in")]
    [SerializeField] float minSpeed = 3.5f;
    [Tooltip("The max speed which the object can move in")]
    [SerializeField] float maxSpeed = 10f;
    [Tooltip("multiplayer of the speed and the distance to get a nice speed value")]
    [SerializeField] float speedMultiplayer = 0.15f;
    private float targetSpeed;



    public override void HandleAgentMovement()
    {
        base.HandleAgentMovement();
        HandleAgentSpeed();
    }

    private void HandleAgentSpeed()
    {
        GetAgent().speed = Mathf.Lerp(GetAgent().speed, targetSpeed, Time.deltaTime);

        if (GetAgent().hasPath)
        {
            float currentSpeed = Vector3.Distance(transform.position, GetAgent().pathEndPosition)
            * speedMultiplayer * GetDefultSpeed();
            targetSpeed = Mathf.Clamp(currentSpeed, minSpeed, maxSpeed);
        }
        else targetSpeed = 0f;
    }
}
