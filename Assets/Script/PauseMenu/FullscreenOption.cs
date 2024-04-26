using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FullscreenOption : MonoBehaviour
{
    public void Change()
    {
        Screen.fullScreen = !Screen.fullScreen;
        Debug.Log("Fullscreen change");
    }
}
