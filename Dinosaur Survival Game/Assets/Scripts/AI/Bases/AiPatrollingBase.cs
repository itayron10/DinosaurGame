using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AiBase))]
[RequireComponent(typeof(BasicNavmeshMovement))]
public class AiPatrollingBase : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] float patrolingRange = 10f;
    // is the ai will choose random position on the random sphere edge or inside the random sphere
    [SerializeField] bool patrolOnEdge;

    [Header("References")]
    private AiBase aiBase;
    private BasicNavmeshMovement aiMovement;
    private Vector3 randomPatrolPosReference;
    
    [Header("Editor")]
    [SerializeField] Color gizmozColor = Color.white;
    public Color GetGizmozColor() => gizmozColor;

    private void Start() => FindPrivateObjects();

    public virtual void FindPrivateObjects()
    {
        aiBase = GetComponent<AiBase>();
        aiMovement = GetComponent<BasicNavmeshMovement>();
    }

    private void Update()
    {
        // initialize patrol on the default state
        if (aiBase.GetCurrentState() == aiBase.GetDefaultState())
            PatrolRandomly(patrolingRange, transform.position, patrolOnEdge);
    }

    public virtual void PatrolRandomly(float patrolingRange, Vector3 position, bool patrolOnEdge)
    {
        aiMovement.PatrolRandomly(patrolingRange, position, patrolOnEdge, ref randomPatrolPosReference);
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = gizmozColor;
        Gizmos.DrawWireSphere(transform.position, patrolingRange);
    }
#endif
}
