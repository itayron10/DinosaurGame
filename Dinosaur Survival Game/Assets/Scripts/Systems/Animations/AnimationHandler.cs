using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
    [Tooltip("The animator that will be used to play all the animation and contain all the paramters")]
    [SerializeField] Animator animator;
    public Animator GetAnimator() => animator;

    private void Start() => FindPrivateObjects();

    public void SetBool(string boolName, bool boolValue) => animator.SetBool(boolName, boolValue);
    public void SetTrigger(string triggerName) => animator.SetTrigger(triggerName);
    public void SetInt(string intName, int intAmount) => animator.SetInteger(intName, intAmount);
    public void SetFloat(string floatName, float floatAmount) => animator.SetFloat(floatName, floatAmount);

    public void ResetAnimator()
    {
        animator.Rebind();
        animator.Update(0f);
    }

    /// <summary>
    /// for overriden and find stuff in the start method
    /// </summary>
    public virtual void FindPrivateObjects()
    {
        // find private objects
    }

}
