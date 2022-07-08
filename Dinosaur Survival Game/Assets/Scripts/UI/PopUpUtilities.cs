using UnityEngine;
using TMPro;

public class PopUpUtilities : MonoBehaviour
{
    public static TextMeshPro InstantiatePopUp(float damage, Color popUpColor, GameObject popUpPrefab = null, Vector3 popUpStartPos = default, float popUpRandomSphereRadius = 1f, float popUpAliveTime = 1f)
    {
        if (popUpPrefab == null) { return null; }
        // choose random spawn position from the origing
        Vector3 randomSpawnPos = popUpStartPos + Random.insideUnitSphere * popUpRandomSphereRadius;
        // instantiate the pop up effect
        GameObject popUpInstance = Instantiate(popUpPrefab, randomSpawnPos, Quaternion.identity);
        // destroy the effect after some time
        Destroy(popUpInstance, popUpAliveTime);
        // records teh popUpText
        TextMeshPro popUpText = popUpInstance.GetComponentInChildren<TextMeshPro>();
        // set the text color 
        popUpText.color = popUpColor;
        Debug.Log($"the text color {popUpText.color} \nand the color is {popUpColor}");
        // set the text to the amount of damage being damaged
        popUpText.text = damage.ToString();
        return popUpText;
    }
}
