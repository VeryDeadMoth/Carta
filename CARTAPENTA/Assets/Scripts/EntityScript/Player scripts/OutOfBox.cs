using UnityEngine;


public class OutOfBox : MonoBehaviour
{
    [SerializeField] private GameObject ColliderRestriction;
    [SerializeField] private GameObject MapLimit;


    public bool IsPositionValid(Vector3 pos)
    {
        bool result = true;

        if (MapLimit != null) result = positionValidLimiteMap(pos, MapLimit);

        if (ColliderRestriction != null) result = positionValidCollider(pos, ColliderRestriction);

        if (MapLimit != null && ColliderRestriction != null) result = positionValid(pos, MapLimit, ColliderRestriction);

        return result;
    }


    private bool positionValidLimiteMap(Vector3 pos, GameObject limitM) => limitM.GetComponent<Renderer>().bounds.Contains(pos);

    private bool positionValidCollider(Vector3 pos, GameObject ColliderRestriction)
    {
        bool positionValide = true;
        Transform limitC_R = ColliderRestriction.transform;

        for (int i = 0; i < limitC_R.transform.childCount; i++)
        {
            positionValide = positionValide && !limitC_R.GetChild(i).GetComponent<Collider2D>().bounds.Contains(pos);
        }
        return positionValide;
    }

    private bool positionValid(Vector3 pos, GameObject limitMap, GameObject ColliderRestriction) => positionValidCollider(pos, ColliderRestriction) && positionValidLimiteMap(pos, limitMap);
}
