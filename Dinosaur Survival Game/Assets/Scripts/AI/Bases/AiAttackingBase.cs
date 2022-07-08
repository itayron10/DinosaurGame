using System.Collections;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.AI;

[RequireComponent(typeof(AiTargetDetectionBasedConfig))]
public class AiAttackingBase : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] AiAttack[] attacks;
    [Tooltip("The time in seconds which the ai wait before each attack is called")]
    // how much time between attacks the ai waits?
    [SerializeField] float attackingCooldown = 1f;
    // the name of the animator trigger for attacking, set when we attack
    [SerializeField] string attackAnimationTriggerName = "Attack";

    [Header("States")]
    // set to true when the ai is during the attacking (while the attack animation plays and he damage the target)
    // this used for only starting an attack after we finished the attack we started
    private bool isAttacking = false;

    [Header("References")]
    // used to get the target that the attack config found and choose an attack state from the aiBase
    [SerializeField] AiTargetDetectionBasedConfig aiAttackConfig;
    // the current target of the attacking system 
    private Transform currentTarget;
    // used to rotate the ai to the target when attacking
    private BasicNavmeshMovement aiMovement;
    // used to make the ai look at the target when attacking
    private LookAtIK lookAtIk;
    // used to set animations for when attacking
    private AnimationHandler aiAnimationHandler;
    // timer that is used to count the cooldown between attacks
    private float attackingCooldownTimer;
    // used to set the animation to be the current attack's animationbased on the current attack
    private int currentAttackId;
    public int GetCurrentAttackId() { return currentAttackId; }
    


    private void Start() => FindPrivateObjects();

    void Update() => ConfigAttackState();

    /// <summary>
    /// this method finds private object on start and it can be overriden for more objects to find
    /// </summary>
    public virtual void FindPrivateObjects()
    {
        aiMovement = GetComponent<BasicNavmeshMovement>();
        lookAtIk = GetComponent<LookAtIK>();
        aiAnimationHandler = GetComponent<AnimationHandler>();
        aiAttackConfig.OnStateCanceled += ResetAttackState;
        aiAttackConfig.OnStateStarted += StartAttackState;
    }

    /// <summary>
    /// this method decides how to act based on if the ai is angry or not
    /// </summary>
    private void ConfigAttackState()
    {
        currentTarget = aiAttackConfig.GetTarget();
        // if the ai is angry start handling attacking state
        if (aiAttackConfig.StateIsCurrentState() && currentTarget) HandleAttacking(currentTarget.position);
    }

    /// <summary>
    /// this method resets the attack states back to normal and stops the ai from attacking
    /// it is called when the ai base is no longer in angry state
    /// </summary>
    private void ResetAttackState()
    {
        Debug.Log("Stopped Attacking");
        // if the ai is not attacking then the isInAttackMode is false and the isAttacking is false
        isAttacking = false;
        lookAtIk?.SetTargetWeight(0f);
    }

    private void StartAttackState()
    {
        Debug.Log("Start Attacking");
        lookAtIk?.SetTargetWeight(1f);
    }

    private void HandleAttacking(Vector3 targetPos)
    {
        NavMeshAgent agent = aiMovement.GetAgent();
        // resets the agent's path
        if (agent.hasPath) agent.ResetPath();
        // the ai rotates towards the target 
        aiMovement.RotateTowardsContinually(targetPos);
        // makes the ai look to the target
        lookAtIk?.SetTargetPosition(currentTarget.position);
        // only start charging for attack when we finished attacking
        if (!isAttacking) HandleAttackingCooldown();
    }

    private void HandleAttackingCooldown()
    {
        // when you are not attacking (because we don't want repeting attacks every frame) we start
        // checking if we allowed to attack from the cooldown timer prespective if we do we attack
        attackingCooldownTimer += Time.deltaTime;
        if (attackingCooldownTimer >= attackingCooldown) StartCoroutine(Attack());
    }

    /// <summary>
    /// This method can be overridden for ai attacking modules to define their own attacks
    /// </summary>
    public virtual void InitAttack(AiAttack attack, Transform currentTarget)
    {
        // spawns a particle effect
        ParticleManager.InstanciateParticleEffect
            (attack.attackEffectPrefab,
            attack.attackOriginTransform.position,
            Quaternion.identity);
    }

    private IEnumerator Attack()
    {
        // get the animator form the animation handler
        Animator animator = aiAnimationHandler?.GetAnimator();
        // Choose Random Attack
        AiAttack currentChosenAttack = ChooseRandomAttack();
        // start the attack
        isAttacking = true;
        // start the animation
        aiAnimationHandler?.SetTrigger(attackAnimationTriggerName);

        yield return new WaitForSeconds(currentChosenAttack.delay);

        // Attack (casting damage, effects, animation start... )
        if (currentTarget) InitAttack(currentChosenAttack, currentTarget);

        // wait for the animation to end
        if (animator) yield return new WaitForSeconds
                (animator.GetCurrentAnimatorStateInfo(0).length 
                - currentChosenAttack.delay);

        // reset the attack cooldown attacking
        attackingCooldownTimer = 0f;
        // finish the attack
        isAttacking = false;
    }

    private AiAttack ChooseRandomAttack()
    {
        int randomAttackIndex = Random.Range(0, attacks.Length);
        AiAttack attack = attacks[randomAttackIndex];
        currentAttackId = attack.attackAnimationId;
        return attack;
    }
}
