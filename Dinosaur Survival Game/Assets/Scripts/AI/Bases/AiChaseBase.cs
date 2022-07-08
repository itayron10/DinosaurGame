using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AiTargetDetectionBasedConfig))]
public class AiChaseBase : MonoBehaviour
{
    [Header("References")]
    // used to get the target position
    // SerializeField because we can have multiple AiTargetDetectionBasedConfig on one gameobject
    [SerializeField] AiTargetDetectionBasedConfig aiChaseConfig;
    // used to move the ai agent for chasing targets
    private BasicNavmeshMovement aiBaseMovement;
    // used to rotate the head ik to the target
    private LookAtIK lookAtIk;

    private void Start()
    {
        FindPrivateObjects();
        // subscribes the StopChasing method to the OnStateCanceled event
        aiChaseConfig.OnStateCanceled += StopChasing;
    }

    private void Update()
    {
        Transform Target = aiChaseConfig?.GetTarget();
        // if the target is null don't do anything
        if (Target == null) { return; }
        // if we are in the chase state we chase the target
        if (aiChaseConfig.StateIsCurrentState()) ChaseTarget(Target.position);
    }
    
    // unsubscribing the StopChasing from the OnStateCanceled when the object is destroyed
    private void OnDestroy() => aiChaseConfig.OnStateCanceled -= StopChasing;


    /// <summary>
    /// can be overriden to find different private objects
    /// </summary>
    public virtual void FindPrivateObjects()
    {
        aiBaseMovement = GetComponent<BasicNavmeshMovement>();
        lookAtIk = GetComponent<LookAtIK>();
    }

    /// <summary>
    /// this method is called when we are on the chase state and it recieves the target position
    /// this method can be overriden by child classes for different behaviours on chase
    /// </summary>
    public virtual void ChaseTarget(Vector3 targetPos)
    {
        // sets the ai ik to look at the target position
        lookAtIk?.SetTargetPosition(targetPos);
        lookAtIk?.SetTargetWeight(1f);
        // moves the ai to the target
        aiBaseMovement.MoveTo(targetPos);
    }

    /// <summary>
    /// this method is called when we exit the chase state
    /// this method resets the chase state
    /// </summary>
    private void StopChasing()
    {
        Debug.Log("Stopped Chasing");
        // resets the path of the ai agent
        aiBaseMovement.GetAgent().ResetPath();
        // resets the look at ik to stop looking at target position
        lookAtIk?.SetTargetWeight(0f);
    }
}
