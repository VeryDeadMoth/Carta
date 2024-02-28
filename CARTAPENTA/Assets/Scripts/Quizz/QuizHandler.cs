using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuizHandler : MonoBehaviour
{
    string baseText = "J'en déduis que...";

    void Awake()
    {
        //TODO : subscribe to delegate or be called by gamemanager
        GameManager.Instance.OnQuizModeStarted += StartQuizMode;
    }

    void GenerateUI()
    {

    }

    void StartQuizMode()
    {
        
    }

    void EndQuizMode()
    {

    }
}
