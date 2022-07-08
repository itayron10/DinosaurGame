using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HotBarSlot : Slot
{
    // a reference for the image that will be enabled when the horbar lot is selected
    [SerializeField] Image highlightImage;
    public Image GetHighlightImage() { return highlightImage; }
}
