using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundOnEnable : MonoBehaviour
{
    [Header("References")]
    [SerializeField] SoundScriptableObject sound; // this sound will be played on enable

    // play a sound when the object is enabeled
    private void OnEnable() => SoundManager.instance.PlaySound(sound);
}
