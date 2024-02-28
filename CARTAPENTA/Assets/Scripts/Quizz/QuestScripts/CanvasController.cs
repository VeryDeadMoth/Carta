using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CanvasController : MonoBehaviour
{
    public GameObject originalImage;
    public GameObject originalText;
    public GameObject originalButton;

    public GameObject newImage;
    public GameObject newText;
    public GameObject newDisclaimer;

    private void Start()
    {
        // Initialize your canvas elements
        originalImage.SetActive(true);
        originalText.SetActive(true);
        originalButton.SetActive(true);

        newImage.SetActive(false);
        newText.SetActive(false);
        newDisclaimer.SetActive(false);
    }

    public void OnButtonClick()
    {
        // Hide original elements
        originalImage.SetActive(false);
        originalText.SetActive(false);
        originalButton.SetActive(false);

        // Show new elements
        newImage.SetActive(true);
        newText.SetActive(true);
        newDisclaimer.SetActive(true);
    }
}
