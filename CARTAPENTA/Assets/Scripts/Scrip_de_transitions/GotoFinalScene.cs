using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 using UnityEngine.SceneManagement;


public class GotoFinalScene : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadSceneAsync("Final_scene");
    }
}