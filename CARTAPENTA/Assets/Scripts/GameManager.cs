using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    //deserialization system
    public Dictionary<string, List<QuizQuestion>> allQuizQuestions;
    Deserializer deserializer = new();
    public TextAsset quizFile;

    //events
    public delegate void QuizMode();
    public event QuizMode OnQuizModeStarted;


    public void Awake()
    {
        if (!Instance)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    private void Start()
    {
        if(quizFile) this.allQuizQuestions = deserializer.GetAllQuizQuestions(quizFile);
    }

    public void LoadNewScene(string name)
    {
        SceneManager.LoadScene(name);
    }

}
