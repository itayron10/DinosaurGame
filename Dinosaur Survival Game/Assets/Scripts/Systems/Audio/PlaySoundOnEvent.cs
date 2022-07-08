using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlaySoundOnEvent : MonoBehaviour
{
    // NOTE: this script is used mainly to play sound on unity events / c# events like on animator event or button click

    /// <summary>
    /// this method is used to play a certain sound it can be called on events
    /// </summary>
    public void PlaySound(SoundScriptableObject sound)
    {
        SoundManager.instance.PlaySound(sound);
    }
}
