using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using fadeMethods;


public class drawManager : MonoBehaviour
{
    [SerializeField] private GameObject listGoodPlacement;
    [SerializeField] private GameObject NewMap;
    [SerializeField] private GameObject LineCollider;
    [SerializeField] private float timeInterval;
    public GameManager GameManager;
    public bool isInPlace { get; set; }
    private bool End;
    public int numberCollider { get; set; }
    public int percentage { get; private set; }
    private float timeChrono;
    public List<GameObject> ObjectChecked { get; set; }

    public delegate void DrawEvent(int i);
    public static event DrawEvent OnDraw;

    private void Start()
    {
        isInPlace = false;
        End = false;
        timeChrono = 0f;
        timeInterval = 1f;
        LineCollider.GetComponent<ColliderDraw>().ColliderGoodPlace += PlacementCollided;
        ObjectChecked = new List<GameObject>(listGoodPlacement.transform.childCount);
    }

    private void Update()
    {
        if (Input.GetMouseButton(0) && !(GetComponent<DrawObject>().timeDrag > GetComponent<DrawObject>().lengthMax))
        {
            LineCollider.transform.position = GetComponent<DrawObject>().GetMousePosition();
        }

        if (End)
        {
            percentage = 100;
            LineCollider.SetActive(false);
            if (NewMap != null)
            {
                this.GetComponent<DrawObject>().enabled = false;

                FadeMethods FM = new FadeMethods();

                // Fade away
                Color newMapColor = NewMap.GetComponent<Image>().color;
                NewMap.GetComponent<Image>().color = FM.Fade(newMapColor, false,timeChrono);

                Color newLineColor = this.GetComponent<MeshRenderer>().material.color;
                this.GetComponent<Renderer>().material.color = FM.Fade(newLineColor, true,timeChrono);

                timeChrono = FM.ChronoUp(timeChrono,timeInterval);
                if (newMapColor.a >= 1)
                {
                    timeChrono = 0;
                    numberCollider = 0;
                    GameManager.LoadNewScene("Final_scene");

                }
            }
            else
            {
                numberCollider = 0;
                GameManager.LoadNewScene("Final_scene");

            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            percentage = numberCollider*100/ listGoodPlacement.transform.childCount;
            OnDraw?.Invoke(percentage);
            ObjectChecked.Clear();
            if(isInPlace)
            {
                End = true;
            }
        }
    }


    public void PlacementCollided(object sender, EventArgsCollider e)
    {
        bool isExistant = false;
        
        foreach(GameObject go in ObjectChecked)
        {
            if(GameObject.ReferenceEquals(go, e.GO_ColliderLine))
            {
                isExistant = true;
            }
        }
        if (!isExistant)
        {
            numberCollider++;
            ObjectChecked.Add(e.GO_ColliderLine);
        }
        isInPlace = numberCollider == listGoodPlacement.transform.childCount;
    }
}
