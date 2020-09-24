using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitToDesktop : MonoBehaviour
{
    public void ExitAI()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
        Application.Quit();
    }
}
