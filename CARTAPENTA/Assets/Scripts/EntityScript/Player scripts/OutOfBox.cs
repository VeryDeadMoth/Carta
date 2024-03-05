using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.PlayerSettings;

public class OutOfBox : MonoBehaviour
{
    [SerializeField] private GameObject ColliderRestriction;
    [SerializeField] private GameObject MapLimit;
    private Vector3 position = Vector2.zero;

    private void Update()
    {
        
        if (Input.GetMouseButtonDown(0))
        {
            position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            position = new Vector3(position[0], position[1],0);

            if (ColliderRestriction != null && !IsPositionValid(position))
            {
                position = this.transform.position;
            }
        }
        this.transform.position = Vector2.MoveTowards(this.transform.position, position, 8 * Time.deltaTime);
    }

    public bool IsPositionValid(Vector3 pos)
    {
        bool result = true;

        if(MapLimit != null)
        {
            result = positionValid(pos,MapLimit);
            if (ColliderRestriction != null)
            {
                result = positionValid(pos, MapLimit, ColliderRestriction);

            }
        }

        return result;
    }


    private bool positionValid(Vector3 pos, GameObject limitM) => limitM.GetComponent<SpriteRenderer>().bounds.Contains(pos);


    private bool positionValid(Vector3 pos,GameObject limitMap, GameObject ColliderRestriction)
    {
        bool positionValide = false;
        Transform limitC_R = ColliderRestriction.transform;
        Transform limitM_T = limitMap.transform;

        for (int i = 0; i < limitC_R.transform.childCount; i++)
        {
            if (limitC_R.GetChild(i).gameObject.activeSelf)
            {
                positionValide = positionValide || 
                    (!(limitC_R.GetChild(i).GetComponent<Collider2D>().bounds.Contains(pos))
                    && limitM_T.GetComponent<SpriteRenderer>().bounds.Contains(pos));
            }
            else
            {
                positionValide = positionValid(pos, limitMap);
            }
        }
        return positionValide;
    }
}
