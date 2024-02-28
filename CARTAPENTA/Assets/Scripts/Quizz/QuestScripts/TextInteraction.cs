using UnityEngine;
using UnityEngine.UI;

public class TextInteraction : MonoBehaviour
{
    private Text textComponent;

    void Start()
    {
        textComponent = GetComponent<Text>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Check for left mouse button click
        {
            // When clicked, make the text disappear
            textComponent.enabled = false;
        }
    }
}
