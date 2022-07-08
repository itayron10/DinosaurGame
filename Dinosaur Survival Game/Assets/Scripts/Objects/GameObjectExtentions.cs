using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectExtentions
{
    /// <summary>
    /// changes the layers of an object recursively (cahanges apply to children)
    /// </summary>
    public static void SetLayerRecursively(Transform rootTransform, LayerMask newLayer)
    {
        rootTransform.gameObject.layer = newLayer;

        for (int i = 0; i < rootTransform.childCount; i++)
        {
            Transform child = rootTransform.GetChild(i);
            SetLayerRecursively(child, newLayer);
        }
    }

    public static void SetGameObjectColliders(GameObject gameObject, bool active)
    {
        foreach (Collider collider in gameObject.GetComponents<Collider>()) collider.enabled = active;
    }

}
