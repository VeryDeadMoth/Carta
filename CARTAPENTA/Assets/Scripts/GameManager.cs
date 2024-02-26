using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Dictionary<string, List<QuizQuestion>> allQuizQuestions;
    Deserializer deserializer = new();
    public TextAsset quizFile;

    private void Awake()
    {
        if(quizFile) this.allQuizQuestions = deserializer.GetAllQuizQuestions(quizFile);
    }

    public void LoadNewScene(string name)
    {
        SceneManager.LoadScene(name);
    }

}
