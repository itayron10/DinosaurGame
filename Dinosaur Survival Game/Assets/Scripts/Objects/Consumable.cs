using System.Collections.Generic;
using UnityEngine;

public class Consumable : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] [Range(10f, 60f)] float satisfactionCoolDown = 20f;
    
    public float GetSatisfactionCoolDown() => satisfactionCoolDown;

}
