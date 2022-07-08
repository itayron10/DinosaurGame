using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class AiAttack
{
    [Header("Attack Params")]
    [Tooltip("The name of the attack")]
    // the name of the attack
    [SerializeField] string name;
    [Tooltip("The time in seconds to wait since the attack animation began before casting the attack")]
    // how much time to wait since the attack animation began before init the attack damage and effects
    public float delay;
    [Tooltip("How many damage to hit for the target if hit")]
    // how many damage to apply if a damagable getting hit from this attack
    public int damage;
    [Tooltip("The radius of the damage casting of this attack")]
    // how big the raius of the damaging sphere
    public float radius; 
    [Tooltip("The attackId is used to active the right attack animation in the animator")]
    // how big the raius of the damaging sphere
    public int attackAnimationId;

    [Header("Attack References")]
    [Tooltip("How many damage to hit for the target if hit")]
    // from where to spawn the effects and cast the damage sphere
    public Transform attackOriginTransform;
    [Tooltip("The prefab of the effect of this attack ")]
    // the prefab of the effect of this attack
    public GameObject attackEffectPrefab;
    [Tooltip("an optional component to the attack used in the ranged attacking")]
    public Projectile projectile;

 
}

