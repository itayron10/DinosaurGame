using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [Header("Reference")]
    public static PlayerStats instance;
    
    [Header("Settings")]
    [SerializeField] Stat[] playerStats; // array of all the player stats

    private void Awake() => SetSingelton();

    private void Start() => SetStatsToMaxAmount();

    private void SetStatsToMaxAmount()
    {
        // loop all the player stats
        for (int i = 0; i < playerStats.Length; i++)
        {
            // get the current stat
            Stat Stat = playerStats[i];
            // set the stat to max amount
            Stat.SetStatAmount(Stat.GetMinMaxStatAmount().Item2); 
        }
    }

    private void SetSingelton()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }

    // get a stat field based on the stat field name
    public Stat GetStat(string statFieldName)
    {
        // loop all the player stats
        for (int i = 0; i < playerStats.Length; i++)
        {
            // get the current stat
            Stat Stat = playerStats[i];
            // if the stat names are the same then return the stat
            if (Stat.GetStatName().ToLower() == statFieldName.ToLower()) return Stat;      
        }

        // if we didn't find any stats return null
        return null;
    }
}


[System.Serializable]
public class Stat
{
    [SerializeField] string statName = "New Stat"; // the stat name used to identify the stat
    [SerializeField] float maxStatAmount = 1f, minStatAmount = 0f; // the stat min value it can go down and max value it can go up
    private float currentStatAmount = 0.5f; // the current stat amount

    public string GetStatName() { return statName; } // gets the stat name
    public float GetStatAmount() { return currentStatAmount; } // gets the current stat amount
    public (float, float) GetMinMaxStatAmount() { return (minStatAmount, maxStatAmount); } // get the min and max stat amounts
    public float GetNormalizedStatAmount() { return (currentStatAmount - minStatAmount) / (maxStatAmount - minStatAmount); } // get the normalized amount (btw 0 to 1) of the current stat amount reletive to the min and max stat amounts
    // sets the stat amount and clamping the set value between the min and max amounts
    public void SetStatAmount(float statAmount)
    {
        (float min, float max) = GetMinMaxStatAmount();
        currentStatAmount = Mathf.Clamp(statAmount, min, max);
    }
}

