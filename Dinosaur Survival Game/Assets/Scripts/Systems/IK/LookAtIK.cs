using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class LookAtIK : MonoBehaviour
{
    [SerializeField] MultiAimConstraint aim;
    [Tooltip("How fast the rig is going to look at object")]
    [SerializeField] [Range(0.1f, 20f)] float lookingSpeed = 4f;
    private float targetWeight;
    private Vector3 targetPos;

    public void SetTargetWeight(float targetWeight) { this.targetWeight = targetWeight; }
    public void SetTargetPosition(Vector3 targetPos) { this.targetPos = targetPos; }



    private void Update()
    {
        aim.weight = Mathf.Lerp(aim.weight, targetWeight, lookingSpeed * Time.deltaTime);
        aim.data.sourceObjects[0].transform.position = targetPos;
    }



}   
