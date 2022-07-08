using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    [Header("References")]
    // the reflaction probe that will be updated in the enviornmentUpdateRate, should cover the entire scene
    public static DayNightCycle instance;
    // the skybox material that will be changed according to the time of day
    [SerializeField] Material Skybox;
    // the reflaction probe that will be updated in the enviornmentUpdateRate, should cover the entire scene
    [SerializeField] ReflectionProbe enviornmentReflectionProbe;
    // reference for the color of the fog in the start of the game
    private Color startFogColor;
    // how much time going to pass every frame, this is used to normalize the full day length to 0 -> 1 value
    private float timeRate;
    // the current time of the day in normalized value between 0 -> 1
    private float currentTime;
    public float GetCurrentTime() => currentTime;
    public void SetCurrentTime(float time) => this.currentTime = time;
    // the rotation of the sun/moon in the middle of theire rotation cycle, used to rotate the sun and the moon
    private Vector3 midRotation = new Vector3(90f, 0f, 0f);
    // if the sun light intensity is bigger then the moon light intensity then it is a day
    private bool isDay() => sunLight.intensity >= moonLight.intensity;

    // when the night becomes morning or the evening becomes night this event invokes
    public delegate void OnDayCahnges(bool isDay);
    public static OnDayCahnges DayCahnged;
    

    [Header("Settings")]
    // the lerping speed in whic the skybox time variable updates to the current time variable
    [SerializeField] float skyboxMaterialTimeLerpingSpeed = 4f;
    // the time of one full day cycle in seconds
    [SerializeField] float fullDayLength;
    // the normalized time that going to be the current time on start
    [SerializeField] [Range(0f, 1f)] float startTime = 0.5f;

    [Header("Lights")]
    // reference for the light of the sun and the moon
    [SerializeField] Light sunLight, moonLight;
    // the gradiant of the sun and moon color that will be changed based on the current time
    [SerializeField] Gradient sunColor, moonColor;
    // an animation curve that will be evaluate with the current time to change the sun\moon light intensity over time
    [SerializeField] AnimationCurve sunIntensity, moonIntensity;
  

    [Header("Other Settings")]
    // if true the reflactions will be updated in the enviornmentUpdateRate
    [SerializeField] bool updateReflactions;
    // the rate in which the enviornment lighting, ambients and reflactions are updated to fit the skybox
    [SerializeField] [Range(0f, 2f)] float enviornmentUpdateRate;
    

    private void Awake() => SetSingelton();
    
    private void Start() => SetPrivateReferences();

    private void Update()
    {
        // get reference for if it is a day at the start of the frame
        bool isDayAtStart = isDay();
        UpdateDayNightCycle();
        // check if the day changed after the day night cycle was updated
        CheckDayChanged(isDayAtStart);
    }

    private void UpdateDayNightCycle()
    {
        UpdateSkybox();
        UpdateCurrentTime();
        UpdateLightRotation();
        UpdateLightIntesity();
        UpdateLightColors();
        UpdateMainSunSource();
        UpdateFogColor();
    }

    private void SetSingelton()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
            Destroy(gameObject);
    }

    private void SetPrivateReferences()
    {
        // get the time rate based on the full day length
        timeRate = 1f / fullDayLength;
        // set the current time to the start time
        currentTime = startTime;
        // set the start fog color to the fog color
        startFogColor = RenderSettings.fogColor;
        // set the skybox time to teh current time
        Skybox.SetFloat("Time", GetCurrentSkyboxTime());
        // start update the EnviormentLighting
        StartCoroutine(UpdateEnviormentLighting());
    }


    /// <summary>
    /// this method checks if the isDay bool has changed and invokes the DayChanged event when it does
    /// </summary>
    private void CheckDayChanged(bool isDayBeforeUpdatedTimer)
    {
        // day/night changes
        if (isDayBeforeUpdatedTimer != isDay()) DayCahnged?.Invoke(isDay());
    }

    /// <summary>
    /// this IEnumerator is a recursive IEnumerator that is called in the start
    /// and it is updates the environment lighting and ambients and the reflactions in a constant intreavels
    /// </summary>
    IEnumerator UpdateEnviormentLighting()
    {
        // update the scene ambients and lighting based on the current skybox look
        DynamicGI.UpdateEnvironment();
        // update the scene reflactions if update reflaction is true based on the current skybox look
        if (updateReflactions) UpdateReflections(enviornmentReflectionProbe);
        // wait some time
        yield return new WaitForSeconds(enviornmentUpdateRate);
        // do it all over again
        StartCoroutine(UpdateEnviormentLighting());
    }

    /// <summary>
    /// this method updates the skybox material time variable based on the current skybox time
    /// </summary>
    private void UpdateSkybox()
    {
        // set the time float of the skybox to be the lerped time
        Skybox.SetFloat("Time", Mathf.Lerp(Skybox.GetFloat("Time"), GetCurrentSkyboxTime(),
            skyboxMaterialTimeLerpingSpeed * Time.deltaTime));
    }

    /// <summary>
    /// this method is called if the updateReflactions is true and it is updating the reflaction probe we give it
    /// for it to work the reflaction probe should cover the whole scene
    /// </summary>
    private void UpdateReflections(ReflectionProbe reflectionProbe)
    {
        // update the reflaction probe
        reflectionProbe.RenderProbe();
    }

    /// <summary>
    /// this method takes the current time and converts it to a nicer value for our skybox shader
    /// </summary>
    private float GetCurrentSkyboxTime()
    {
        // convert the time to be bigger then it is
        float correctTime = GetCurrentTime() * 2f;
        // if the time is bigger then 1.5 loop back to start and return 0 
        if (correctTime >= 1.5f) return 0f;
        // if the time it not bigger then 1.5 return the correct time
        return correctTime;
    }

    /// <summary>
    /// this method is called when the fog color is updated(every frame)
    /// this method lerps the current fog color with a new one based on if it is a day or night
    /// </summary>
    private void UpdateFogColor()
    {
        Color newFogColor = isDay() ? startFogColor : Color.black;
        RenderSettings.fogColor = Color.Lerp(RenderSettings.fogColor, newFogColor, Time.deltaTime);
    }

    /// <summary>
    /// this method updates the main sun source of the scene based on if it is a day or not
    /// </summary>
    private void UpdateMainSunSource()
    {
        Light newLight = isDay() ? sunLight : moonLight;
        RenderSettings.sun = newLight;
    }

    /// <summary>
    /// this method updates the sun and light colors based on the current time
    /// </summary>
    private void UpdateLightColors()
    {
        // update sun color
        sunLight.color = sunColor.Evaluate(currentTime);
        // update moon color
        moonLight.color = moonColor.Evaluate(currentTime);
    }

    /// <summary>
    /// this method updates the sun and moon light intensity based on the current time
    /// </summary>
    private void UpdateLightIntesity()
    {
        // update sun intensity
        sunLight.intensity = sunIntensity.Evaluate(currentTime);
        // update moon intensity
        moonLight.intensity = moonIntensity.Evaluate(currentTime);
    }

    /// <summary>
    /// this method updates the current time of day and loops back when the day has ended
    /// </summary>
    private void UpdateCurrentTime()
    {
        // increment Time
        currentTime += timeRate * Time.deltaTime;
        // loop back to 0 when the day ended
        if (currentTime >= 1f) currentTime = 0f;
    }

    /// <summary>
    /// this method rotates the sun and moon lights based on some math i don't undertand
    /// </summary>
    private void UpdateLightRotation()
    {
        // rotate sun light
        sunLight.transform.eulerAngles = (currentTime - 0.25f) * midRotation * 4f;
        // rotate moon light
        moonLight.transform.eulerAngles = (currentTime - 0.75f) * midRotation * 4f;
    }
}
