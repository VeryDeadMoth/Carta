using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GotoMainGame : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadSceneAsync("Main_Menu");
    }
}