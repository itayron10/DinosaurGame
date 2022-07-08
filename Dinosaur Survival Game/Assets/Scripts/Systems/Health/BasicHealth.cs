using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BasicHealth : MonoBehaviour
{
    [Header("Settings")]
    // the health that the object start with and the max heath the object can get to
    [Tooltip("How much health the object get on default")]
    [SerializeField] float maxHealth;
    // hit sound is the sound that will be played when TakeDamage is called
    // death sound will be played when Die is called
    [SerializeField] SoundScriptableObject hitSound, deathSound;
    [SerializeField] GameObject deathEffectPrefab;

    [Header("References")]
    // reference for the audio source of this health component, used to play the hit and death sounds
    private AudioSource healthAudioSource;
    // reference for the current health amount 
    private float currentHealth;
    // event that invokes when taking damage and it containes the damage amount the object was damaged in
    public delegate void OnHit(float damage);
    public event OnHit OnTakingDamage;

    // set the current health to the max health on default
    private void Awake() => currentHealth = maxHealth;

    private void Start() => FindPrivateObjects();

    /// <summary>
    /// thie method called on start and you can override this method and get private objects
    /// </summary>
    public virtual void FindPrivateObjects()
    {
        // get private objects
    }

    /// <summary>
    /// this method is called for damaging the object and it gets a damage amount
    /// and invokes the OnTakingDamage event 
    /// can be overriden for different kinds of actions when taking damage
    /// </summary>
    public virtual void TakeDamage(float damageAmount)
    {
        // invokes the OnTakingDamage event with the damage amount
        OnTakingDamage?.Invoke(damageAmount);
        // decrease the damageAmount from the currentHealth and clamps the current health between 0f and the max health 
        currentHealth = Mathf.Clamp(currentHealth - damageAmount, 0f, maxHealth);
        // if the current health is 0 or lower initiate the die function
        // if we are not dying then play the hit sound
        if (currentHealth <= 0f) 
            Die();
        else
            SoundManager.instance.PlaySound(hitSound, healthAudioSource);
    }

    /// <summary>
    /// this method is called in the TakeDamage method when the currentHealth is 0 or lower and it can be ovveriden
    /// for different kinds of actions when dying 
    /// </summary>
    public virtual void Die()
    {
        // play sound on death
        SoundManager.instance.PlaySound(deathSound, healthAudioSource);
        // instantiate the death effect
        ParticleManager.InstanciateParticleEffect(deathEffectPrefab, transform.position, Quaternion.identity);
        // destroyes the gameobject
        Destroy(gameObject);
    }

}
