using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavmeshHandeler
{
    public static Vector3 GetClosestPointOnNavmesh(Vector3 pos, float correctionRange = Mathf.Infinity)
    {
        if (NavMesh.SamplePosition(pos, out NavMeshHit hit, correctionRange, NavMesh.AllAreas))
        {
            return hit.position;
        }

        return Vector3.zero;
    }

    public static Vector3 GetNormal(Vector3 pos, Vector3 direction)
    {
        if (Physics.Raycast(pos, direction, out RaycastHit hit))
        {
            return hit.normal;
        }

        return default;
    }


}
