using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HighlightPlus;

[RequireComponent(typeof(Interactable))]
[RequireComponent(typeof(HighlightEffect))]
public class InteractableHighlightHandeler : MonoBehaviour
{
    [Header("References")]
    private Interactable interactable;
    private HighlightEffect highlightEffect;

    private void Awake()
    {
        interactable = GetComponent<Interactable>();
        highlightEffect = GetComponent<HighlightEffect>();
    }

    private void Update()
    {
        // setting a highlight effect based on the interactable canInteract bool
        highlightEffect.enabled = interactable.IsInteractable();
    }
}
