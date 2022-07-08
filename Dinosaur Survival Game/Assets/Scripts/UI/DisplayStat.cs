using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayStat : MonoBehaviour
{
    [Header("References")]
    [SerializeField] string statName = "Stat Name";
    [SerializeField] Image statImage;
    private Stat Stat;

    [Header("Settings")]
    [SerializeField] bool Lerp = true;
    [SerializeField] float fillLerpingSpeed = 1f;


    private void Start()
    {
        Stat = PlayerStats.instance.GetStat(statName);
        statImage.fillAmount = Stat.GetNormalizedStatAmount();
    }

    private void Update()
    {
        UpdateStatDisplay();
    }

    private void UpdateStatDisplay()
    {
        float fillAmount;
        if (Lerp) fillAmount = Mathf.Lerp(statImage.fillAmount, Stat.GetNormalizedStatAmount(), Time.deltaTime * fillLerpingSpeed);
        else fillAmount = Stat.GetNormalizedStatAmount();
        statImage.fillAmount = fillAmount;
    }
}
