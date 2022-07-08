using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class BasicNavmeshMovement : MonoBehaviour
{
    [Header("Settings")]
    // if the distance from the object to the path end position lower from this distance then the path reseted
    [SerializeField] float minDistanceForResetPath = 2f;
    [Tooltip("The speed which the object is rotating with")]
    [SerializeField] float rotationSpeed = 6f;
    [Tooltip("How fast the object is accelerating")]
    [SerializeField] float accelerationSpeed = 999f;
    [Tooltip("The defult speed of the navmesh agent")]
    [SerializeField] float defultSpeed = 5f;
    [Tooltip("How far to check for nvamesh point to move to when moving to a target position which is not on the navmesh")]
    [SerializeField] float correctionRange = 100f;

    [Header("References")]
    [SerializeField] string movementSpeedAnimationName = "MovementSpeed";
    private NavMeshAgent agent;
    private AnimationHandler animationHandler;

    [Header("Editor")]
    [SerializeField] Color gizmozColor = Color.white;

    public float GetMinDistanceForResetPath() => minDistanceForResetPath;
    public float GetDefultSpeed() => defultSpeed;
    public NavMeshAgent GetAgent() => agent;


    private void Start()
    {
        FindPrivateObjects();
        SetAgent();
    }

    private void Update() => HandleAgentMovement();

    public virtual void FindPrivateObjects()
    {
        agent = GetComponent<NavMeshAgent>();
        animationHandler = GetComponent<AnimationHandler>();
    }
    
    public virtual void HandleAgentMovement()
    {
        SetAnimationsParams();
        HandleAgentPath();
    }

    private void SetAnimationsParams() => animationHandler?.SetFloat(movementSpeedAnimationName, agent.speed);

    private Vector3 GetRandomPointOnSphereEdge(float sphereRadius, Vector3 sphereOriginPos)
    {
        //finding a random pos on the edge of the search radius
        Vector3 randomPointPos = Random.insideUnitCircle.normalized * sphereRadius;
        //rotating the circle 90f degrees to circle the dinosaur horiaontaly
        randomPointPos.z = randomPointPos.y;
        randomPointPos.y = sphereOriginPos.y;

        //moving the circle's origin to the transform's position
        randomPointPos += sphereOriginPos;

        //assaigning the point to the closest point on the navmesh
        return randomPointPos;
    }

    public void PatrolRandomly(float range, Vector3 originPosition, bool onEdge, ref Vector3 patrolRandomPos)
    {
        //set a new random pos only if the navmesh agent reach his destination
        if (agent.hasPath) { return; }

        if (onEdge)
            patrolRandomPos = GetRandomPointOnSphereEdge(range, originPosition);
        else
            patrolRandomPos = Random.insideUnitSphere * range + originPosition;

        if (patrolRandomPos != Vector3.zero)
            //move the navmesh object to the new search pos every frame
            MoveTo(patrolRandomPos);
    }

    public virtual void SetAgent()
    {
        agent.angularSpeed = rotationSpeed * 10f;
        agent.acceleration = accelerationSpeed;
        agent.speed = defultSpeed;
        agent.autoBraking = false;
    }

    public virtual void MoveTo(Vector3 targetPosition)
    {
        agent.SetDestination(NavmeshHandeler.GetClosestPointOnNavmesh(targetPosition, correctionRange));
    }


    private void HandleAgentPath()
    {
        // if the agent is too close to the destination then the path is reseted
        if (Vector3.Distance(agent.destination, agent.transform.position)
            <= GetMinDistanceForResetPath() && agent.hasPath)
        {
            agent.ResetPath();
        }
    }

    public void RotateTowardsContinually(Vector3 targetPos)
    {
        Quaternion targetRotation = Quaternion.LookRotation(targetPos - transform.position);
        //rotate Smoothly towards target
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        //set the x and y rotations to 0 so the object won't glitch with the navmesh agent
        Vector3 eulerAngles = transform.rotation.eulerAngles;
        eulerAngles.x = 0;
        eulerAngles.z = 0;

        // Set the new rotation back
        transform.rotation = Quaternion.Euler(eulerAngles);
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = gizmozColor;
        Gizmos.DrawWireSphere(transform.position, minDistanceForResetPath);
    }
}
