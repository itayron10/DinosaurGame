using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// NOTE: The aiBase has one goal and one goal only the goal is to decide which state the ai is in
// based on the inputs from other ai modules 

public class AiBase : MonoBehaviour
{
    [Header("References")]
    [SerializeField] AiState defaultState;
    [SerializeField] AiState[] aiStates;
    public delegate void StateChanges(AiState oldState, AiState newState);
    public event StateChanges OnStateChanges;
    private AiState currentState;

    public AiState GetDefaultState() => defaultState;
    public AiState GetCurrentState() => currentState;


    private void Awake() => ChangeState(defaultState);        

    /// <summary>
    /// this method setting the current state to be the requested state based on the requested state id
    /// if the priority of the requested state is bigger then teh priority of the current state 
    /// </summary>
    public void RequestState(int requestedStateId)
    {
        // get the ai state based on it's id
        AiState requestedState = GetAiState(requestedStateId);

        // check if the requested state priority is lower then the current state priority if yes return
        if (currentState.GetStatePriority() >= requestedState.GetStatePriority()) return;

        ChangeState(requestedState);
    }


    /// <summary>
    /// this method resets the ai state back to the default state
    /// called when the ai is no longer need to be in a state
    /// (example: the ai was in some angry state but he killed the enemies so now he should'nt be in the angry state)
    /// </summary>
    public void CancelState(int stateToCancelId)
    {
        if (currentState.GetId() != stateToCancelId) { return; }
        ChangeState(defaultState);
    }

    private void ChangeState(AiState requestedState)
    {
        // check if the requested state is already our current state if yes return
        if (requestedState == currentState) { return; }
        OnStateChanges?.Invoke(currentState, requestedState);
        // assign the current state to the requested state
        currentState = requestedState;
    }

    private AiState GetAiState(int stateId)
    {
        // looping all the states 
        foreach (AiState aiState in aiStates)
            // if the state id matches the state id argument then we return the ai state
            if (aiState.GetId() == stateId) return aiState;
        
        // if we didn't find any ai state that it's id matches we return null
        return null;
    }
}

[System.Serializable]
public class AiState
{
    // the state name, used to describe the state and what is it supposed to mean
    [SerializeField] string stateName;
    
    // the state identifier, used to request states and stuff like that
    [SerializeField] int stateId;
    public int GetId() => stateId;

    // the priority of this state compare to ai states
    [SerializeField] int statePriorirty;
    public int GetStatePriority() => statePriorirty;
}
