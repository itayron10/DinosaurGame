using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiDetectionBase : MonoBehaviour
{
    /// <summary>
    /// this method is called when we want to get the near targets for something like enemy detection or food detection
    /// this method returns the list of all the targets it detected
    /// NOTE: for objects to be detected in this function they must have a collider
    /// </summary>
    public List<Transform> GetNearTargets(float detectionRange, Vector3 detectionOrigin, LayerMask detectionLayer)
    {
        // get all the colliders in the detection range which are in the detection layer
        Collider[] collidersDetected =
            Physics.OverlapSphere(detectionOrigin, detectionRange, detectionLayer);
        
        // make a list of transforms for later use
        List<Transform> transformsDetected = new List<Transform>();

        // adding each transform of a collider in the colliders detected list to the transforms list
        for (int i = 0; i < collidersDetected.Length; i++)
        {
            Collider collider = collidersDetected[i];
            // avoiding adding the self to the list
            if (collider.gameObject != gameObject) transformsDetected.Add(collider.transform);
        }

        // returning the targets list
        return transformsDetected;
    }

    
    public Transform GetNearestTarget(float detectionRange, Vector3 detectionOrigin, LayerMask detectionLayer)
    {
        return DetectionHelper.GetClosest
            (GetNearTargets(detectionRange, detectionOrigin, detectionLayer), detectionOrigin);
    }

}
