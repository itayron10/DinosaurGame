using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager
{
    public static void InstanciateParticleEffect(GameObject particlePrefab, Vector3 position, Quaternion rotation, float aliveTime = 3f, Transform parant = null)
    {
        if (particlePrefab == null) { return; }
        GameObject particleInstance = MonoBehaviour.Instantiate(particlePrefab, position, rotation, parant);
        MonoBehaviour.Destroy(particleInstance, aliveTime);
        if (!particleInstance.TryGetComponent<ParticleSystem>(out ParticleSystem particleSystem)) { Debug.LogError("No Particle System has found"); return; }
        StartParticle(particleSystem);
    }

    public static void StartParticle(ParticleSystem particleSystem, bool controlColor = false, Color color = new Color())
    {
        if (controlColor) particleSystem.startColor = color;
        particleSystem.Play();
    }

    public static void StopParticle(ParticleSystem particleSystem) { particleSystem.Stop(); }

}
