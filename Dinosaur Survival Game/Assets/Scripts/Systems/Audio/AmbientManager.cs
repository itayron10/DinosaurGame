using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbientManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] SoundScriptableObject dayAmbient;
    [SerializeField] SoundScriptableObject nightAmbient;

    // listen to when day changes and switch the ambients based on that
    private void Awake() => DayNightCycle.DayCahnged += ChangeAmbientBasedOnDay;


    private void ChangeAmbientBasedOnDay(bool isDay)
    {
        // if it is a day then play the day ambient else play the night ambient
        SoundManager.instance.PlaySound(isDay ? dayAmbient : nightAmbient);
    }
}
