using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandHelper : MonoBehaviour
{
    [Header("References")]
    [SerializeField] GameConsoleManager consoleManager;
    
    [Header("Parameters")]
    [SerializeField] string failedCommandMessege;

    public void SetTime(string consoleInputText)
    {
        if (float.TryParse(consoleInputText, out float time))
            DayNightCycle.instance.SetCurrentTime(time);
        else
            consoleManager.AddToConsoleMessegas(failedCommandMessege);
    }

    public void SetHudVisible(string consoleInputText)
    {
        if (bool.TryParse(consoleInputText, out bool isVisible))
            HudManager.instance.GetMainHudCanvas().enabled = isVisible;
        else
            consoleManager.AddToConsoleMessegas(failedCommandMessege);

    }
}
