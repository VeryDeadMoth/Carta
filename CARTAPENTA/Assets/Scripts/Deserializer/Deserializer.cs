using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deserializer 
{
    public Dictionary<string, List<QuizQuestion>> GetAllQuizQuestions(TextAsset file) => Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, List<QuizQuestion>>>(file.text);


}

//classes that need to be deserialized

[Serializable]
public class QuizQuestion
{
    public string text;
    public bool isCorrectAnswer;
}
