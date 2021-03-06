using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.UIElements;
#pragma warning disable 0618
[RequireComponent(typeof(UnityEngine.UIElements.Image))]
public class ForcedReset : MonoBehaviour
{
    private void Update()
    {
        // if we have forced a reset ...
        if (CrossPlatformInputManager.GetButtonDown("ResetObject"))
        {
            //... reload the scene
            SceneManager.LoadScene(SceneManager.GetSceneAt(0).name);
        }
    }
}
