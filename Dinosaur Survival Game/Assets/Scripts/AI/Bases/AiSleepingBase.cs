using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AiSleepingConfig))]
public class AiSleepingBase : MonoBehaviour
{
    [Header("References")]
    // this used to reset the agent path when sleeping
    private BasicNavmeshMovement movement;
    // this used to set the sleeping animation when sleeping
    private AnimationHandler animationHandler;
    // this used to detect when to start sleep and when to stop
    private AiSleepingConfig sleepingConfig;

    [Header("Settings")]
    // this is the bool parameter name in the animator of the animation handler that will be true when sleeping
    [SerializeField] string sleepingBoolAnimationName = "IsSleeping";
    // this effect will be enabled when we sleep
    [SerializeField] ParticleSystem sleepEffectInstance;

    private void Start()
    {
        FindPrivateObject();
        SubscribeToConfigEvents();
    }

    private void OnDisable() => UnsubscribeToConfigEvents();

    /// <summary>
    /// this method called on start and can be overriden by child clsses to find private objects
    /// </summary>
    public virtual void FindPrivateObject()
    {
        movement = GetComponent<BasicNavmeshMovement>();
        animationHandler = GetComponent<AnimationHandler>();
        sleepingConfig = GetComponent<AiSleepingConfig>();
    }


    /// <summary>
    /// this method is called on start and it's subscribing the Sleep and Wake methods
    /// to the events of the sleep config
    /// </summary>
    private void SubscribeToConfigEvents()
    {
        sleepingConfig.OnStateStarted += Sleep;
        sleepingConfig.OnStateCanceled += WakeUp;
    }

    /// <summary>
    /// this method is calle when the object is destroyed and it's unsubscribing
    /// the Sleep and Wake methods from the sleepConfig events
    /// </summary>
    private void UnsubscribeToConfigEvents()
    {
        sleepingConfig.OnStateStarted -= Sleep;
        sleepingConfig.OnStateCanceled -= WakeUp;
    }

    /// <summary>
    /// this method called when we transition out of the sleeping state
    /// </summary>
    private void WakeUp()
    {
        // stop the sleep animation
        animationHandler.SetBool(sleepingBoolAnimationName, false);
        // stop the sleep effect particles
        ParticleManager.StopParticle(sleepEffectInstance);
    }

    /// <summary>
    /// this method called when we transition into the sleeping state
    /// </summary>
    private void Sleep()
    {
        Debug.Log("Sleep");
        // stop the ai movement
        movement.GetAgent().ResetPath();
        // start playing the sleep effect particles
        ParticleManager.StartParticle(sleepEffectInstance);
        // start playing the sleep animation
        animationHandler.SetBool(sleepingBoolAnimationName, true);
    }

}