using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OutOfBox : MonoBehaviour
{
    [SerializeField] private GameObject limitMap;
    private Vector3 position = Vector3.zero;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            position = new Vector3(position[0], position[1],0);
            if (limitMap != null && !positionValid(position,limitMap))
            {
                position = this.transform.position;
            }
        }
        this.transform.position = Vector2.MoveTowards(this.transform.position, position, 8 * Time.deltaTime);
    }

    private bool positionValid(Vector3 pos,GameObject limitM)
    {
        bool positionValide = false;
        Transform limitM_T = limitM.transform;
        for (int i = 0; i < limitM_T.transform.childCount; i++)
        {
            if (limitM_T.transform.GetChild(i).gameObject.activeSelf)
            {
                positionValide = positionValide || limitM_T.transform.GetChild(i).GetComponent<SpriteRenderer>().bounds.Contains(pos);
            }
        }
        return positionValide;
    }
}
