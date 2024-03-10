using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
  public void PlayGame ()
    {
        SceneManager.LoadSceneAsync("Tutorial");
    }

   public void CreditsButton()
    {

    }

    public GameObject soundButton;
    public void SoundButton()
    {
        soundButton.SetActive(true);
    }
    public void CloseSoundButton()
    {
        soundButton.SetActive(false);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}