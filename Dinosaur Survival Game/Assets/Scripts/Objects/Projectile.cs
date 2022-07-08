using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Header("Params")]
    [SerializeField] float speed;
    [Tooltip("the allowed range for the projectile to go in before destroyed if 'destroyOutOfRange' is true")]
    [SerializeField] float range;
    [Tooltip("the damage amount the projectile will take")]
    [SerializeField] float damage;
    [Tooltip("is the projectile get destroyedonce he is out of range")]
    [SerializeField] bool destroyOutOfRange; // if true the projectile is destroyed once he leaves the range
    
    [Header("References")]
    private Rigidbody rb;
    private Vector3 startPos; // the position of the projectile in the start

    public void SetProjectileRange(float range) { this.range = range; }
    public void SetProjectileDamage(float damage) { this.damage = damage; }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        startPos = transform.position;
        rb.velocity = transform.forward * speed;
    }

    private void Update()
    {
        if (destroyOutOfRange)
            if (Vector3.Distance(startPos, transform.position) > range) 
                Destroy(gameObject); 
    }

    private void OnTriggerEnter(Collider other)
    {
        // check if we close to the start point so the projectile won't get destroyed if hit the shooter collider
        if (Vector3.Distance(startPos, transform.position) < 0.2f) { return; }

        // if we hit someone with health we damage hit and self destroy
        if (other.TryGetComponent<BasicHealth>(out BasicHealth damagable))
            damagable.TakeDamage(damage);

        Destroy(gameObject);
    }
}
