using UnityEngine;
using Effects;
using System.Collections;

public class CloudEffect : MonoBehaviour
{
    [SerializeField] private GameObject ColliderRestriction;
    public int numZone { get; set; } = 0;
    private fadeEffects fadeEffects;

    private void Start()
    {
        fadeEffects = new fadeEffects();

        //Randomize clouds;

        if (ColliderRestriction != null) {
            for (int i = 0; i < ColliderRestriction.transform.childCount; i++)
            {
                for (int j = 0; j < ColliderRestriction.transform.GetChild(i).childCount; j++)
                {
                    int RandRotate = Random.Range(0, 2);

                    float Randx = Random.Range(2f, 3f);
                    float Randy = Random.Range(2f, 4f);
                    if(RandRotate == 0) ColliderRestriction.transform.GetChild(i).GetChild(j).GetComponent<SpriteRenderer>().flipX = true;
                    ColliderRestriction.transform.GetChild(i).GetChild(j).localScale = new Vector3(Randx, Randy, 0);
                }
            }
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown("q") && numZone != -1 && ColliderRestriction != null)
        {
            if (ColliderRestriction.transform.childCount > numZone && ColliderRestriction.transform.GetChild(numZone).gameObject.activeSelf)
            {
                StartCoroutine(SlideEffect(numZone));
            }
            numZone++;
        }
    }


    public IEnumerator FadeEffect(int numeroZone)
    {
        GameObject CloudsGroup = ColliderRestriction.transform.GetChild(numeroZone).gameObject;

        while (!fadeEffects.EndTransitionFadeGroup(CloudsGroup))
        {
            fadeEffects.FadeOutAllObject(CloudsGroup);
            yield return null;
        }
        CloudsGroup.SetActive(false); 
    }
    public IEnumerator FadeEffect(int numeroZone, float SpeedFade)
    {
        GameObject CloudsGroup = ColliderRestriction.transform.GetChild(numeroZone).gameObject;

        while (!fadeEffects.EndTransitionFadeGroup(CloudsGroup))
        {
            fadeEffects.FadeOutAllObject(CloudsGroup, SpeedFade);
            yield return null;
        }
        CloudsGroup.SetActive(false);
    }

    public IEnumerator SlideEffect(int numeroZone)
    {
        GameObject CloudsGroup = ColliderRestriction.transform.GetChild(numeroZone).gameObject;

        StartCoroutine(FadeEffect(numeroZone));
        while (!fadeEffects.EndTransitionDisperse(CloudsGroup))
        {
            fadeEffects.Disperse(CloudsGroup, 0.01f);
            yield return null;
        }
        
    }
    public IEnumerator SlideEffect(int numeroZone, float SpeedDisperse)
    {
        GameObject CloudsGroup = ColliderRestriction.transform.GetChild(numeroZone).gameObject;

        while (!fadeEffects.EndTransitionDisperse(CloudsGroup))
        {
            fadeEffects.Disperse(CloudsGroup, SpeedDisperse);
            yield return null;
        }
        CloudsGroup.SetActive(false);
    }

}
