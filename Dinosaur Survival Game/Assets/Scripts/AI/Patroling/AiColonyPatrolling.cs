using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ColonyMember))]
public class AiColonyPatrolling : AiPatrollingBase
{
    [Header("References")]
    private ColonyMember colonyMember;

    public override void FindPrivateObjects()
    {
        base.FindPrivateObjects();
        colonyMember = GetComponent<ColonyMember>();
    }

    public override void PatrolRandomly(float patrolingRange, Vector3 position, bool patrolOnEdge)
    {
        Colony myColony = colonyMember.GetColony();
        base.PatrolRandomly(myColony.GetColonyRange() * patrolingRange, myColony.transform.position, patrolOnEdge);
    }
}
