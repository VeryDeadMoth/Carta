using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using static drawManager;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    //deserialization system
    public Dictionary<string, List<QuizQuestion>> allQuizQuestions;
    Deserializer deserializer = new();
    public TextAsset quizFile;

    public int QuestProgress { get; private set; } //helps check which NPC is next

    SaveToFile saveSystem = new(); 

    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
            QuizHandler.OnQuizEnded += QuizEnded;
            QuizHandler.OnQuizStarted += QuizStarted;
            drawManager.OnDraw += DrawingEnded;
        }
        else
        {
            Destroy(this);
        }
        
    }

    private void OnDestroy()
    {
        QuizHandler.OnQuizEnded -= QuizEnded;
        QuizHandler.OnQuizStarted -= QuizStarted;
        drawManager.OnDraw -= DrawingEnded;
    }

    private void Start()
    {
        QuestProgress = 0;
        if(quizFile) this.allQuizQuestions = deserializer.GetAllQuizQuestions(quizFile);
    }

    private void QuizStarted()
    {
        this.saveSystem.SaveData("QuizStarted/" + QuestProgress);
    }

    private void QuizEnded(int errors)
    {
        PlayerStateManager.Instance?.SwitchState(PlayerStateManager.Instance.idleState);
        //Handle Quest System Here
        this.saveSystem.SaveData("QuizEnded/"+QuestProgress);
        this.saveSystem.SaveData("QuizErrors/"+errors);
        QuestProgress++;
        //Check if questprogress == max quest value and launch draw line scene
    }

    private void DrawingEnded(int score)
    {
        this.saveSystem.SaveData("Score/" + score);
    }

    public void LoadNewScene(string name)
    {
        //save which new scene has been loaded
        this.saveSystem.SaveData("Session/" + name);
        SceneManager.LoadScene(name);
    }

}
