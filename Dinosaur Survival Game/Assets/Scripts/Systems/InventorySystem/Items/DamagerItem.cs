using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Item))]
public class DamagerItem : ActionItem
{
    [Header("References")]
    [Tooltip("Each damage type has a different damage variables so that this item will hit different layers in different damages")]
    [SerializeField] List<DamageType> damageTypes = new List<DamageType>();
    [Tooltip("Attack Can Be Defined With Animation And Delay Time To Sync The Damage With The Animation")]
    [SerializeField] List<ItemAttack> itemAttacks = new List<ItemAttack>(); // the list of the item attacks
    private Transform attackerTransform; // the tranform of the player used to raycast from the player origin to detect attacks
    private ItemAnimator itemAnimator; // the animator attached to this item object
    private int currentItemAttackIndex; // the current attack's index
    private float cooldownAttackTimer; // a cool down between attacks timer
    private bool isSwingTimed; // is the swing happens right after the last swing ended
    private Coroutine ActiveSwingTiming; // the coroutine responsable to count the timed swing duration and chang ethe isSwingTimed

    [Header("Damage Casting")]
    // the length of the ray which cast the damage
    [Tooltip("The Max Length the Item Can Attack")]
    [SerializeField] float rayDamageLength = 1.5f;
    // the offset of the origing point of the damage ray
    [Tooltip("The Offset Of The Origin Of The Damage Raycast")]
    [SerializeField] Vector3 damageRayOffset;
    // the delay between the times in which the attacks can init
    [Tooltip("The Cooldown Between Attacks")]
    [SerializeField] float attackCooldown = 1.1f;
    [Tooltip("The chance that the attack will be a critical attack")]
    [SerializeField] [Range(0f, 1f)] float critChance = 0.03f;
    [Tooltip("The window between the end of the swing to the next swing that if the player will attack in this window the attack damage will be increased (reward player for timing swings)")]
    [SerializeField] [Range(0f, 1f)] float swingTimedDuration = 0.3f;



    [Header("Effects")]
    // the shake screen settings will effect on the shake screen when the item hit a damage type
    [SerializeField] ScreenShakeSettingsSO hitScreenShakeSettings;


    [Header("Pop-Up")]
    // the popUpPrefab will be instansiated on the hit position when the item hit a damage type
    [Tooltip("The PopUp Which will be instansiated when the item hits")]
    [SerializeField] GameObject popUpPrefab;
    [SerializeField] Vector3 popUpSpawningOffset;
    // the popUpAliveTime set how much time the popUpTime will stay untill he is destroyed
    [Tooltip("The Time Before The PopUp Will Be Destroyed From The Moment He Was Instansiated")]
    [SerializeField] float popUpAliveTime = 1f;
    // the popUpRandomSphrePosRadius set the sphere radius in which the popUp can spawn(the origin of the sphere in the hit pos)
    [Tooltip("The Radius Of The Random Sphere Around The PopUp Origin Pos")]
    [SerializeField] float popUpRandomSpherePosRadius = 1f;
    [Tooltip("The different color of the pop-up text based on the attack status(normal, timed, critical)")]
    [SerializeField] Color popUpDefultColor = Color.white, popUpTimedColor = Color.yellow, popUpCritColor = Color.red;

    [Header("Animation")]
    [Tooltip("The trigger name responsable for triggering the item swing")]
    [SerializeField] string swingParamName = "Swing";
    [Tooltip("The int paramater name responsable for checking which attack the item is using")]
    [SerializeField] string swingIdParamName = "AttackID";

    
    private void Update() => UpdateCooldownTimer();

    public override void FindPrivateObjects()
    {
        base.FindPrivateObjects();
        itemAnimator = GetComponent<ItemAnimator>();
        // assaign the attacker transfom to be the player
        attackerTransform = PlayerAttackingManager.instance.transform;
    }

    private void UpdateCooldownTimer()
    {
        // when the attack init the coolDownAttackTimer resets and the timer start to update
        if (cooldownAttackTimer < attackCooldown) cooldownAttackTimer += Time.deltaTime;
    }

    public override void Action()
    {
        // only attack if the cooldown is over
        if (cooldownAttackTimer < attackCooldown) { return; }
        // choose a random attack to attack in
        ChooseRandomAttack();
        ItemAttack currentItemAttack = itemAttacks[currentItemAttackIndex];
        // starting the animation of the item
        itemAnimator.SetTrigger(swingParamName);
        itemAnimator.SetInt(swingIdParamName, currentItemAttack.animationID);

        //play swing sound
        SoundManager.instance.PlaySound(currentItemAttack?.attackSound);
        // start the coroutine which responsable for the damaging 
        StartCoroutine(ActiveAttackDamage(currentItemAttack.delayTime));
    }

    private void ChooseRandomAttack()
    {
        // choose a random attack out of all the itemAttacks
        currentItemAttackIndex = Random.Range(0, itemAttacks.Count);
    }

    /// <summary>
    /// start to damage the near objects inside of the damage types
    /// </summary>
    private IEnumerator ActiveAttackDamage(float delay)
    {
        cooldownAttackTimer = 0f; // reseting the cooldownAttackTimer
        //waiting for the delay so the animation and the attacking sync
        yield return new WaitForSeconds(delay);
        // looping all the damage types for this amager item
        LoopAllDamageTypes();
    }

    /// <summary>
    /// loop all the damage types and checks for collisions with all of them
    /// </summary>
    private void LoopAllDamageTypes()
    {
        for (int i = 0; i < damageTypes.Count; i++)
        {
            DamageType damageType = damageTypes[i];
            //casting a sphere, checking for damagable objects
            CheckForCollisionWithDamageType(damageType);
        }

    }

    /// <summary>
    /// check if the damager item hit a damage type object
    /// </summary>
    private void CheckForCollisionWithDamageType(DamageType damageType)
    {
        if (Physics.Raycast(attackerTransform.position + damageRayOffset,
            transform.forward, out RaycastHit hit, rayDamageLength, damageType.damageLayer))
        {
            if (hit.collider.TryGetComponent<BasicHealth>(out BasicHealth health))
            {
                //checking for the health component and taking damage
                ActiveAttack(damageType, hit, health);
            }
        }
    }

    /// <summary>
    /// this method actives the attack and is called when the damager item hit something hitable
    /// </summary>
    private void ActiveAttack(DamageType damageType, RaycastHit hit, BasicHealth health)
    {
        PlayAttackEffects(out Color popUpColor, damageType, hit);
        HandleAttackDamage(health, damageType, popUpColor, hit);
    }

    private void HandleAttackDamage(BasicHealth health, DamageType damageType, Color popUpColor, RaycastHit hit)
    {
        // choose random base damage amount
        float randomizeDamageAmount = Random.Range(damageType.minDamage, damageType.maxDamage);
        // check for timed attack
        SetAttackIncreaseDamage(isSwingTimed && damageType.swingTimedDamageIncreaseAmount > 0, ref popUpColor, popUpTimedColor, ref randomizeDamageAmount, damageType.swingTimedDamageIncreaseAmount);
        // add critical chance
        SetAttackIncreaseDamage(Random.Range(0f, 1f) < critChance && damageType.critDamageIncreaeAmount > 0, ref popUpColor, popUpCritColor, ref randomizeDamageAmount, damageType.critDamageIncreaeAmount);
        // spawn popUp
        SpawnPopUp(popUpColor, hit.point, randomizeDamageAmount);
        // take damage
        health.TakeDamage(randomizeDamageAmount);

        // handle swing timing
        if (ActiveSwingTiming != null) StopCoroutine(ActiveSwingTiming);
        ActiveSwingTiming = StartCoroutine(ActiveSwingTimed(attackCooldown + swingTimedDuration));
    }

    /// <summary>
    /// instantiate a popup effect in certain pos and assaings a damage amount to his text
    /// </summary>
    private void SpawnPopUp(Color popUpColor, Vector3 Pos, float damageAmount)
    {
        // init the popUpEffect on attack
        PopUpUtilities.InstantiatePopUp(damageAmount, popUpColor, popUpPrefab, Pos + transform.InverseTransformVector(popUpSpawningOffset),
            popUpRandomSpherePosRadius, popUpAliveTime);
    }

    /// <summary>
    /// sets the attack damage amount and colors if the toIncrease bool is true when this method is called
    /// </summary>
    private void SetAttackIncreaseDamage(bool toIncrease, ref Color popUpColor, Color newPopUpColor, ref float damageAmount, float addedDamgeAmount)
    {
        if (!toIncrease) { return; }
        popUpColor = newPopUpColor;
        damageAmount += addedDamgeAmount;
    }

    /// <summary>
    /// activate the attack effects when hiting object 
    /// </summary>
    private void PlayAttackEffects(out Color popUpColor, DamageType damageType, RaycastHit hit)
    {
        // setting the defult color for the popUpColor
        popUpColor = popUpDefultColor;
        // init the particle effect on the hit point on attacking
        if (damageType.hitEffect) 
            ParticleManager.InstanciateParticleEffect
                (damageType.hitEffect, hit.point, Quaternion.LookRotation(hit.normal));
        // init the camera shake screen on attacking
        CinemachineShake.instance.Shake(hitScreenShakeSettings);
    }

    /// <summary>
    /// coroutine responsable for opening the timedSwing window for a specific duration
    /// </summary>
    private IEnumerator ActiveSwingTimed(float duration)
    {
        isSwingTimed = true;
        yield return new WaitForSeconds(duration);
        isSwingTimed = false;
    }
}

/// <summary>
/// each different item attack marks different attack animation
/// </summary>
[System.Serializable]
[SerializeField] class ItemAttack
{
    [SerializeField] string attackName; // the name of the attack (used just to make cleaner inspector)
    public int animationID; // use to play the different animations for different attacks
    public float delayTime; // use to sync the attack damage casting and the attack animation 
    public SoundScriptableObject attackSound; // used to play a sound on the tart of attacking with this item attack
}

/// <summary>
/// each damage type is a reference for a different layer which this damage item can damage
/// </summary>
[System.Serializable]
[SerializeField] struct DamageType
{
    public string damageTypeName; // the name of the damage type
    public GameObject hitEffect; // the hit effect will be played when the damager item hit this damage type
    public LayerMask damageLayer; // what layer this damage type will target
    public int minDamage, maxDamage; // how much damage to apply to this damage type min/max
    public int critDamageIncreaeAmount; // how much to add to the damage amount if the attack is crit 
    public int swingTimedDamageIncreaseAmount; // how much to add to the damage amount if the swing is timed
}
