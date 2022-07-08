using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AiBase))]
public class AiStateConfig : MonoBehaviour
{
    [Header("References")]
    // this used to change the state of the aiBase
    private AiBase aiBase;
    public delegate void StateChanges();
    // this event invokes when the aiBase transition to this state
    public event StateChanges OnStateStarted;
    // this event invokes when the aiBase transition from this state
    public event StateChanges OnStateCanceled;


    [Header("Settings")]
    // the stateId of the state this stateConfig will request and cancel
    [SerializeField] int aiStateId;
    public int GetAiStateId() => aiStateId;
    // is the current state of the aiBase is the same state as our state
    private bool stateIsCurrentState;
    public bool StateIsCurrentState() => stateIsCurrentState;

    
    private void Start()
    {
        FindPrivateObjects();
        SubscribeToEvents();
    }

    private void Update() => UpdateStateConfig();

    private void OnDestroy() => UnsubscribeFromEvents();

    /// <summary>
    /// this method can be overriden to find different kinds of private objects on start
    /// </summary>
    public virtual void FindPrivateObjects() => aiBase = GetComponent<AiBase>();

    /// <summary>
    /// method for subscribing to different events run on start
    /// </summary>
    public virtual void SubscribeToEvents() => aiBase.OnStateChanges += AiBase_OnStateChanges;

    /// <summary>
    /// method for unsubscribing to different events runs when the object destroyed
    /// </summary>
    public virtual void UnsubscribeFromEvents() => aiBase.OnStateChanges -= AiBase_OnStateChanges;

    /// <summary>
    /// this method is used for running stuff from child classes in the update method
    /// and it is also running the state config and checking for the ai base id
    /// </summary>
    public virtual void UpdateStateConfig()
    {
        HandleStateConfig();
        stateIsCurrentState = aiBase.GetCurrentState().GetId() == aiStateId;
    }

    /// <summary>
    /// this method is handling the ai base state configeration based on the TransitionState and CancelState methods
    /// </summary>
    private void HandleStateConfig()
    {
        // if the TransitionState is true we transition to the config's state
        if (CanTransitionToState())
            aiBase.RequestState(aiStateId);
        // if we are in the TransitionState's state and the transition state bool is not true we reset the state
        else
            aiBase.CancelState(aiStateId);
    }
   
    
    /// <summary>
    /// this method is called when the aiBase state changes
    /// this method helps us deside when the ai is canceling our state and when the ai is switching to our state
    /// </summary>
    private void AiBase_OnStateChanges(AiState oldState, AiState newState)
    {
        if (oldState.GetId() == aiStateId)
            OnStateCanceled?.Invoke();
        else if (newState.GetId() == aiStateId)
            OnStateStarted?.Invoke();
    }

    /// <summary>
    /// when this bool is true we request our state from the aiBase when it is false we cancel our state
    /// this method should be overriden by child classes for deciding when to request the ai state
    /// </summary>
    public virtual bool CanTransitionToState() => true;
}
