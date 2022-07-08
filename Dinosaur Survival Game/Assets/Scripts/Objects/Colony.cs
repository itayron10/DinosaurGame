using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Colony : MonoBehaviour
{
    [SerializeField] List<ColonyMember> colonyMembers;
    [SerializeField] float colonyRange;

    public float GetColonyRange() { return colonyRange; }
    public List<ColonyMember> GetColonyMembers() { return colonyMembers; }

    private void Awake()
    {
        SetDinosaursColony();
    }

    public void SetDinosaursColony()
    {
        for (int i = 0; i < colonyMembers.Count; i++)
        {
            ColonyMember colonyMember = colonyMembers[i];

            if (colonyMember == null) { colonyMembers.Remove(colonyMember); continue; }

            colonyMember.SetColony(this);
        }
    }



#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, colonyRange);
    }
#endif
}
