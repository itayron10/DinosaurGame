using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class HudManager : MonoBehaviour
{
    public static HudManager instance;
    private Canvas mainHudCanvas;

    public Canvas GetMainHudCanvas() { return mainHudCanvas; }

    private void Awake() => SetSingelton();
    private void Start() => mainHudCanvas = GetComponent<Canvas>();


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

    /// <summary>
    /// returns the slot under the mouse
    /// </summary>
    public static Slot GetSlotUnderMouse()
    {
        GameObject objectUnderMouse = GetOjectUnderMouse();
        if (objectUnderMouse == null) return null;
        return objectUnderMouse.GetComponent<Slot>();
    }

    /// <summary>
    /// get the gameobject under the mouse (uses the event system)
    /// </summary>
    public static GameObject GetOjectUnderMouse()
    {
        // make a new pointerEventData (like a raycast for ui)
        var pointerEventData = new PointerEventData(EventSystem.current);
        // make the position of the pointer event data as the mouse
        pointerEventData.position = Input.mousePosition;
        // casting the results in a results list
        var results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerEventData, results);
        // if there is no objects under the mouse return null
        if (results.Count < 1) { return null; }
        // if there is return the first gameobject you found
        return results[0].gameObject;
    }

}
