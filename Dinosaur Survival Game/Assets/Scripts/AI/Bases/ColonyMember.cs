using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColonyMember : MonoBehaviour
{
    private Colony myColony;

    public Colony GetColony() { return myColony; }
    public void SetColony(Colony colony) { this.myColony = colony; }
    public bool InMyColony() { return Vector3.Distance(transform.position, GetColony().transform.position) < GetColony().GetColonyRange(); }
    public Vector3 GetRandomPosInMyColony() { return (GetColony().GetColonyRange() * Random.insideUnitSphere + GetColony().transform.position); }

}
