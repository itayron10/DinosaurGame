using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Interactable))]
public class InteractableCanvasHighlight : MonoBehaviour
{
    [Header("References")]
    private Interactable interactable;
    [SerializeField] Canvas highlightCanvas;

    private void Awake()
    {
        interactable = GetComponent<Interactable>();
    }

    private void Update()
    {
        // setting a canvas enabled based on the interactable canInteract bool
        highlightCanvas.enabled = interactable.IsInteractable();
    }
}
