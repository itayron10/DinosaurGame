using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TODO: acting wierd when the body have a parent
public class IKFootSolver : MonoBehaviour
{
    [Header("References")]
    [Tooltip("The Body Of This Foot")]
    [SerializeField] Transform body;
    [Tooltip("The Parallel Leg Of This Leg")]
    [SerializeField] IKFootSolver parallelLeg;
    [Tooltip("The Floor Layer")]
    [SerializeField] LayerMask floorLayer;

    [Header("General Data")]
    private float distanceFromTargetPoint;
    private Vector3 currentPos, currentGroundPos, targetPos;
    private Vector3 currentNormal, currentGroundNormal, targetNormal;
    private float steppingLerp;


    [Header("Changeable Paramaters")]
    [Tooltip("the distance which the leg steps in")]
    [SerializeField] float distanceForStep = 1f; // the min distance in which the leg steps
    [Tooltip("the speed of leg step")]
    [SerializeField] float stepSpeed = 3f;  // the lerping speed between one grounded pos to another 
    [Tooltip("the height of leg step")]
    [SerializeField] float stepHeight = 0.5f; // the height amount of the step
    [Tooltip("the offset between the raycast casting point and the body")]
    [SerializeField] Vector3 targetPointOffset; // the offset between the raycast casting point and the body
    [Tooltip("the offset between the raycast hit point to the pos of the leg")]
    [SerializeField] Vector3 legPositionOffset; // the offset between the raycast hit point to the pos of the leg


    private void Start() => InitLegValues();

    private void Update()
    {
        SetStepValues();

        if (Physics.Raycast(body.position + body.TransformVector(targetPointOffset),
            Vector3.down, out RaycastHit hit, Mathf.Infinity, floorLayer.value))
        {
            CheckForStep(hit);
        }


        //Step
        if (steppingLerp < 1f)
            Step();
        else
            UpdateLegValues();
    }

    private void InitLegValues()
    {
        currentPos = targetPos = currentGroundPos = transform.position;
        currentNormal = targetNormal = currentGroundNormal = transform.forward;

        steppingLerp = 1f;
    }

    private void CheckForStep(RaycastHit hit)
    {
        targetPos = hit.point + body.TransformVector(legPositionOffset);
        targetNormal = hit.normal.normalized;

        if (Vector3.Distance(currentPos, targetPos) >= distanceForStep && steppingLerp >= 1f && !parallelLeg.IsMoving())
            //Start Step
            steppingLerp = 0f;
    }

    private void UpdateLegValues()
    {
        currentGroundPos = currentPos;
        currentGroundNormal = currentNormal;
    }

    private void Step()
    {
        Vector3 tempPosition = Vector3.Lerp(currentGroundPos, targetPos, steppingLerp);
        Vector3 tempNormal = Vector3.Lerp(currentGroundNormal, targetNormal, steppingLerp);
        tempPosition.y += Mathf.Sin(steppingLerp * Mathf.PI) * stepHeight;

        currentPos = tempPosition;
        currentNormal = tempNormal;

        steppingLerp += Time.deltaTime * stepSpeed;
    }

    private void SetStepValues()
    {
        transform.position = currentPos;
        transform.up = currentNormal;
        transform.forward = body.forward;

        Vector3 newTargetPointPos = new Vector3(targetPos.x, transform.position.y, targetPos.z);
        distanceFromTargetPoint = Vector3.Distance(transform.position, newTargetPointPos);
    }

    public bool IsMoving()
    {
        return steppingLerp < 1;
    }

}
