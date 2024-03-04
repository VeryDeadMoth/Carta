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

    public int QuestProgress { get; private set; } //helps check which NPC is next

    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
            QuizHandler.OnQuizEnded += QuizEnded;
        }
        else
        {
            Destroy(this);
        }
        
    }

    private void Start()
    {
        QuestProgress = 0;
        if(quizFile) this.allQuizQuestions = deserializer.GetAllQuizQuestions(quizFile);
    }

    private void QuizEnded()
    {
        PlayerStateManager.Instance?.SwitchState(PlayerStateManager.Instance.idleState);
        //Handle Quest System Here
        QuestProgress++;
        //Check if questprogress == max quest value and launch draw line scene
    }

    public void LoadNewScene(string name)
    {
        SceneManager.LoadScene(name);
    }

}
