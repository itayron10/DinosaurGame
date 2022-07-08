using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiSleepingConfig : AiStateConfig
{
    [Header("References")]
    // used to get the time of day
    private DayNightCycle dayNightCycle;

    [Header("Settings")]
    // in what time the ai goes to sleep?
    [SerializeField] [Range(0f, 1f)] float sleepingTime = 0.8f;
    // in what time the ai wake up?
    [SerializeField] [Range(0f, 1f)] float wakingTime = 0.3f;


    public override void FindPrivateObjects()
    {
        base.FindPrivateObjects();
        dayNightCycle = DayNightCycle.instance;
    }

    /// <summary>
    /// this method returns true when the current world time is between the sleeping time to the waking time
    /// </summary>
    public override bool CanTransitionToState()
    {
        // get the current time of day
        float currentTime = dayNightCycle.GetCurrentTime();
        // return true if the time is bigger than the sleepig time or smaller than the waking time
        return currentTime >= sleepingTime || currentTime <= wakingTime;
    }

}
