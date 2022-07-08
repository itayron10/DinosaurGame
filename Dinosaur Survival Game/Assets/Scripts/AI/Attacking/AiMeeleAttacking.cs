using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiMeeleAttacking : AiAttackingBase
{
    public override void InitAttack(AiAttack attack, Transform currentTarget)
    {
        base.InitAttack(attack, currentTarget);
        // casting damage on the target if the target within the attack range
        if (Vector3.Distance(currentTarget.position, attack.attackOriginTransform.position) > attack.radius) { return; }
        // check if the target has a health component, if yes take from it damage
        if (currentTarget.TryGetComponent<BasicHealth>(out BasicHealth damagable)) damagable.TakeDamage(attack.damage);
    }
}
