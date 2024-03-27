using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Globalization;

public class QuizHandler : MonoBehaviour
{
    List<string> baseTextList = new List<string> { "J'en deduis que...","Hm... Je ne suis pas sure...","Hm... Peut-etre pas...","Hm... Peut-etre autre chose...?"};
    [SerializeField]
    GameObject panel;
    [SerializeField]
    List<GameObject> buttonList;
    [SerializeField]
    GameObject textPanel;

    string whichNPC; //set from event

    [SerializeField] private CloudEffect cloudEffect;
    [SerializeField] private Camera _Cameraplayer;

    public delegate void QuizIntEvent(int i);
    public delegate void QuizEvent();
    public static event QuizIntEvent OnQuizEnded;
    public static event QuizEvent OnQuizStarted;

    int numberOfError;


    private void Start()
    {
        
        for(int i = 0; i < buttonList.Count; i++)
        {
            int copy_i = i;
            buttonList[i].GetComponent<Button>().onClick.AddListener(() => { Debug.Log("clicked on : " + copy_i);  CheckAnswer(buttonList[copy_i], copy_i); });
        }

        //test
        //StartQuizMode(NPC.currentNPC.name);
        
    }

    public void StartQuizMode(string npc)
    {
        this.panel.SetActive(true);
        this.whichNPC = npc;
        this.numberOfError = 0;
        this.textPanel.GetComponent<TextMeshProUGUI>().text = baseTextList[0];

        this.panel.SetActive(true);
        for(int i = 0; i < buttonList.Count; i++)
        {
            buttonList[i].GetComponentInChildren<TextMeshProUGUI>().text = GameManager.Instance.allQuizQuestions[this.whichNPC][i].text;
            buttonList[i].GetComponent<Button>().interactable = true;
            buttonList[i].SetActive(true);
        }
        OnQuizStarted?.Invoke();
    }

    //called by onClick of each buttons
    public void CheckAnswer(GameObject target, int buttonIndex)
    {
        // Access the current NPC GameObject
        GameObject currentNPC = NPC.currentNPC;

        if (GameManager.Instance.allQuizQuestions[this.whichNPC][buttonIndex].isCorrectAnswer)
        {
            //animate, kill quiz mode
            if (currentNPC != null)
            {
                NPC npcScript = currentNPC.GetComponent<NPC>();
                if (npcScript != null && npcScript.checkMark != null)
                {
                    EndQuizMode(npcScript.checkMark);
                }
            }
        }
        else
        {
            //turn red, animate, make it impossible to click again
            target.GetComponent<Button>().interactable = false;
            this.numberOfError++;
            this.textPanel.GetComponent<TextMeshProUGUI>().text = baseTextList[Random.Range(1, baseTextList.Count)];
        }
    }


    public void EndQuizMode(GameObject checkMark)
    {
        this.panel.SetActive(false);
        foreach (GameObject button in buttonList)
        {
            button.SetActive(false);
        }
        if (checkMark != null)
            checkMark.SetActive(true);
        //get player out of locked mode here. (out of listening state through event)
        OnQuizEnded?.Invoke(this.numberOfError);

        if (_Cameraplayer != null)
        {
            
            Vector3 CameraDirection = _Cameraplayer.transform.parent.position * (-1);
            CameraDirection.x *= _Cameraplayer.transform.localScale.x;
            CameraDirection.y *= _Cameraplayer.transform.localScale.y;
            CameraDirection.z = -10;
            CameraTransition C1 = new CameraTransition(_Cameraplayer,cloudEffect);
            StartCoroutine(C1.CamMovementToCenter(CameraDirection, 20f, 2f, 0.3f));
        }

        if (whichNPC == "NPC4")
        {
            GameManager.Instance.LoadNewScene("TracageTrait");

        }
    }

}
