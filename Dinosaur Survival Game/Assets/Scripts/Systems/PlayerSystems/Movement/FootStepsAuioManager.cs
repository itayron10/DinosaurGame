using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootStepsAuioManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] FootStepSoundLayer[] footStepSoundLayers; // all the diffenrent foot step's sounds layers
    private SoundScriptableObject currentFootStepSound; // reference for the current sound being played on foot step
    private TerrainTextureDetector terrainTextureDetector; // reference for the script responsablefor detecting the current terrain layer
    public SoundScriptableObject GetCurrentFootStepSound() { return currentFootStepSound; }

    /// <summary>
    /// struct that stores a sound and the terrain layer index, when the player walks on the terrain layer with this index
    /// the foot step sound will be the sound
    /// </summary>
    [System.Serializable]
    [SerializeField] struct FootStepSoundLayer
    {
        [SerializeField] string layerName; // just for a better inspector
        [Tooltip("This Sound will be played when the player steps on the terrain layer")]
        public SoundScriptableObject sound; // the foot step sound that will be associated with this terrain layer
        [Tooltip("This Terrain layer is the layer that when the player steps on the sound will be played")]
        public int terrainTextureLayerIndex; // the terrain layer index that when the player walks on changes the sound to the struct's sound
    }

    // finds the terrain texture detector
    private void Start() => terrainTextureDetector = TerrainTextureDetector.instance;

    // assaigns the currentFootStepSound to the sound being found based on the terrain layer
    private void Update() => currentFootStepSound = GetFootStepSoundBaseOnLayer();


    /// <summary>
    /// get the correct foot step sound based on the terrain layer which the player walks on
    /// </summary>
    private SoundScriptableObject GetFootStepSoundBaseOnLayer()
    {
        // loops all the foot step sound layers
        for (int i = 0; i < footStepSoundLayers.Length; i++)
        {
            // get the current foot step sound layer
            FootStepSoundLayer footStepSoundLayer = footStepSoundLayers[i];
         
            // checks if the footStepSoundLayer's terrain layer index is the terrain layer index which the player walks on
            if (footStepSoundLayer.terrainTextureLayerIndex ==
                terrainTextureDetector.GetTerrainTextureIndexAtPosition(transform.position))
                return footStepSoundLayer.sound;
        }

        return null;
    }
}

