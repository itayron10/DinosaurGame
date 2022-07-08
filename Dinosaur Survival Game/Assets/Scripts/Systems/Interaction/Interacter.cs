using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Interacter : MonoBehaviour
{
    // NOTE: this script should be on the player and it is kind of the "interaction manager"
    // but also it is very important that it will be on the player transform for the active interactable to work

    [Header("Refernecs")]
    // the layer mask of the interactable objects
    [SerializeField] LayerMask interactionLayerMask;
    private PlayerInput playerInput;
    // reference for the closest interactable, this interactable will be interacted when we interact
    private Interactable activeInteractable;
    public Interactable GetActiveInteractable() { return activeInteractable; }
    public static Interacter instance;

    [Header("Settings")]
    // how far away we can interact with interactables
    [SerializeField] float interactionRadius;


    private void Awake()
    {
        SetSingelton();
        SubscribeToInput();
    }

    private void Update() => CheckForInteractables();

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
        // set the player input based on the input manager
        playerInput = InputManager.instance.playerInputActions;
        // subscribes to the interact action
        playerInput.Interaction.Interact.performed += Interact_performed;
    }
    
    /// <summary>
    /// this method checks the interactabels in the interaction radius and set the closest one to be the active interactable
    /// </summary>
    private void CheckForInteractables()
    {
        // get the list of all the colliders in range
        Collider[] colliders = Physics.OverlapSphere(transform.position, interactionRadius, interactionLayerMask);
        // convert the colliders list to interactables list
        List<Interactable> Interactables = HandleInteractablesColliders(colliders);
        if (Interactables.Count < 1) { activeInteractable = null; return; }
        // set the active interactable as the closest interactable
        activeInteractable = GetClosestInterabtable(Interactables);
    }

    /// <summary>
    /// this method takes the list of colliders in the interaction radius and converts it to a list of interactables
    /// </summary>
    private List<Interactable> HandleInteractablesColliders(Collider[] colliders)
    {
        List<Interactable> interactables = new List<Interactable>();
        foreach (var collider in colliders)
        {
            if (collider.gameObject.TryGetComponent<Interactable>(out Interactable interactable))
                interactables.Add(interactable);
        }
        return interactables;
    }

    /// <summary>
    /// this method returns the closest interactable out 
    /// of interactables list by converting the list to transforms and return the closest one
    /// </summary>
    private Interactable GetClosestInterabtable(List<Interactable> Interactables)
    {
        // converts the interactables list to transforms list
        List<Transform> interactablesTransforms = new List<Transform>();
        foreach (Interactable interactable in Interactables) interactablesTransforms.Add(interactable.transform);
        // get the closest transform and return the interactable of this transform
        return DetectionHelper.GetClosest(interactablesTransforms, transform.position).GetComponent<Interactable>();
    }

    private void Interact_performed(InputAction.CallbackContext obj)
    {
        activeInteractable?.Interacte();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, interactionRadius);
    }
}
