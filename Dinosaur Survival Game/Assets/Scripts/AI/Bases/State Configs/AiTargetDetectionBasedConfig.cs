using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiTargetDetectionBasedConfig : AiStateConfig
{
    [Header("References")]
    // used to detect the targets in range and getting the closest one
    private AiDetectionBase aiDetection;
    // the closest target that the aiDetection found in our range 
    private Transform currentTarget;
    public Transform GetTarget() => currentTarget;
    // the distance from the transfrom position to the target position
    private float currentDistanceFromTarget;


    [Header("Settings")]
    // the range in which the ai will detect targets in
    // targets whithin the minDetectionRange won't be detected
    // as well as targets that are not whithin the maxDetectionRange
    [SerializeField] float minDetectionRange, maxDetectionRange;
    // the layer in which we detect targets
    [SerializeField] LayerMask detectionLayer;

    [Header("Editor")]
    // the color of the visual gizmoz (Editor only)
    [SerializeField] Color gizmozColor = Color.white;


    public override void FindPrivateObjects()
    {
        base.FindPrivateObjects();
        aiDetection = GetComponent<AiDetectionBase>();
    }

    /// <summary>
    /// this overriden method asigns the currentTarget to the nearest target from all the target we detected
    /// and sets the currentDistanceFromTarget
    /// </summary>
    public override void UpdateStateConfig()
    {
        base.UpdateStateConfig();
        currentTarget = aiDetection.GetNearestTarget(maxDetectionRange, transform.position, detectionLayer);
        currentDistanceFromTarget = currentTarget ? Vector3.Distance(currentTarget.position, transform.position) : 0f;
    }

    /// <summary>
    /// this overriden method returns true when the the distance from target is bigger than the minRange
    /// and smaller than the maxRange
    /// </summary>
    public override bool CanTransitionToState()
    {
        return currentDistanceFromTarget > minDetectionRange && currentDistanceFromTarget < maxDetectionRange;
    }


#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        // set the gizmoz color based on the gizmozColor
        Gizmos.color = gizmozColor;
        // draws a wireSphere gizmos in the radius of the maxDetection range
        Gizmos.DrawWireSphere(transform.position, maxDetectionRange);
        // draws a wireSphere gizmos in the radius of the minDetectionRange range
        Gizmos.DrawWireSphere(transform.position, minDetectionRange);
    }
#endif
}
