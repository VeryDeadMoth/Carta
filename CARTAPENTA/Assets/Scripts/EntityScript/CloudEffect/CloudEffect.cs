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
    }

    private void Update()
    {
/*        if (Input.GetMouseButtonDown(0) && numZone != -1 && ColliderRestriction != null)
        {
            if (ColliderRestriction.transform.childCount > numZone && ColliderRestriction.transform.GetChild(numZone).gameObject.activeSelf)
            {
                StartCoroutine(FadeEffect(numZone));
            }
            numZone++;
        }*/
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

        while (!fadeEffects.EndTransitionDisperse(CloudsGroup))
        {
            fadeEffects.Disperse(CloudsGroup, 0.01f);
            yield return null;
        }
        CloudsGroup.SetActive(false);
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
