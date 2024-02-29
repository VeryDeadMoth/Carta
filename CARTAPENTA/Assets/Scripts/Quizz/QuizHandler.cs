using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Globalization;

public class QuizHandler : MonoBehaviour
{
    List<string> baseTextList = new List<string> { "J'en d�duis que...","Hm... Je ne suis pas sure...","Hm... Peut-�tre pas...","Hm... Peut-�tre autre chose...?"};
    [SerializeField]
    GameObject panel;
    [SerializeField]
    List<GameObject> buttonList;
    [SerializeField]
    GameObject textPanel;

    string whichNPC; //set from event

    public delegate void QuizEvent();
    public static event QuizEvent OnQuizEnded;

    private void Awake()
    {
        //subscribe to an event
        NPC.OnDialogueEnded += StartQuizMode;
    }

    private void Start()
    {
        
        for(int i = 0; i < buttonList.Count; i++)
        {
            int copy_i = i;
            buttonList[i].GetComponent<Button>().onClick.AddListener(() => { Debug.Log("clicked on : " + copy_i);  CheckAnswer(buttonList[copy_i], copy_i); });
        }

        //test
        //StartQuizMode("NPC1");
        
    }

    private void OnDestroy()
    {
        NPC.OnDialogueEnded -= StartQuizMode;
        //maybe removelistener from buttons ?
    }

    void StartQuizMode(string npc)
    {
        this.whichNPC = npc;
        print(this.textPanel == null);
        this.textPanel.GetComponent<TextMeshProUGUI>().text = baseTextList[0];

        this.panel.SetActive(true);
        for(int i = 0; i < buttonList.Count; i++)
        {
            buttonList[i].GetComponentInChildren<TextMeshProUGUI>().text = GameManager.Instance.allQuizQuestions[this.whichNPC][i].text;
            buttonList[i].GetComponent<Button>().interactable = true;
            buttonList[i].SetActive(true);
        }
    }

    //called by onClick of each buttons
    public void CheckAnswer(GameObject target, int buttonIndex)
    {
        if (GameManager.Instance.allQuizQuestions[this.whichNPC][buttonIndex].isCorrectAnswer)
        {
            //animate, kill quiz mode
            EndQuizMode();
        }
        else
        {
            //turn red, animate, make it impossible to click again
            target.GetComponent<Button>().interactable = false;
            this.textPanel.GetComponent<TextMeshProUGUI>().text = baseTextList[Random.Range(1, baseTextList.Count)];
        }
    }

    void EndQuizMode()
    {
        this.panel.SetActive(false);
        foreach(GameObject button in buttonList)
        {
            button.SetActive(false);
        }

        //get player out of locked mode here. (out of listening state through event)
        OnQuizEnded?.Invoke();
    }
}