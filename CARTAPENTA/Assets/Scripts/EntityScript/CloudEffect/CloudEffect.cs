using UnityEngine;
using Effects;

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
        if (numZone != -1)
        {
            if (ColliderRestriction != null)
            {
                if (ColliderRestriction.transform.childCount > numZone && ColliderRestriction.transform.GetChild(numZone).gameObject.activeSelf)
                {
                    GameObject CloudsGroup = ColliderRestriction.transform.GetChild(numZone).gameObject;
                    
                    if (!fadeEffects.EndTransitionFadeGroup(CloudsGroup))
                    {
                        fadeEffects.FadeOutAllObject(CloudsGroup, 0);
                    }
                    else
                    {
                        CloudsGroup.SetActive(false);
                    }
                }
            }
        }
    }

}
