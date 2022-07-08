using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;

public class AiRangedAttacking : AiAttackingBase
{
    public override void InitAttack(AiAttack attack, Transform currentTarget)
    {
        base.InitAttack(attack, currentTarget);
        SetProjectile(attack, currentTarget);
    }

    private void SetProjectile(AiAttack attack, Transform currentTarget)
    {
        Quaternion projectileDir = Quaternion.LookRotation(currentTarget.position - attack.attackOriginTransform.position);

        Projectile projectileInstance =
            Instantiate(attack.projectile, attack.attackOriginTransform.position, projectileDir);
        
        // setting the projectile params to the attack params
        projectileInstance.SetProjectileRange(attack.radius);
        projectileInstance.SetProjectileDamage(attack.damage);
    }
}
