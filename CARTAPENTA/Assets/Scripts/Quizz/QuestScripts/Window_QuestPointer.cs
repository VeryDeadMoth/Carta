using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class Window_QuestPointer : MonoBehaviour
{
    [SerializeField] private Camera uiCamera;
    private Vector3 targetPosition;
    private RectTransform pointerRectTransform;

    private GameObject[] npcs; // Array to store all NPCs
    private int currentNPCIndex = 0; // Index of the current NPC

    private void Awake()
    {
        pointerRectTransform = transform.Find("Pointer").GetComponent<RectTransform>();
        npcs = GameObject.FindGameObjectsWithTag("NPC"); // Find all NPCs in the scene
        UpdateTargetPosition(); // Set initial target position
    }

    /*
        private void Awake() {
        targetPosition = GameObject.Find("NPC"+GameManager.Instance.QuestProgress).transform.position; // TODO: find using gamemanager which quest we need!!!
        pointerRectTransform = transform.Find("Pointer").GetComponent<RectTransform>();
    }
    */

    private void Update()
    {
        Vector3 toPosition = targetPosition;
        Vector3 fromPosition = GameObject.Find("Player").transform.position;
        fromPosition.z = 0f;
        Vector3 dir = (toPosition - fromPosition).normalized;
        float angle = UtilsClass.GetAngleFromVectorFloat(dir);
        pointerRectTransform.localEulerAngles = new Vector3(0, 0, angle);

        float borderSize = 50f;
        Vector3 targetPositionScreenPoint = Camera.main.WorldToScreenPoint(targetPosition);
        bool isOffScreen = targetPositionScreenPoint.x <= borderSize || targetPositionScreenPoint.x >= Screen.width - borderSize || targetPositionScreenPoint.y <= borderSize || targetPositionScreenPoint.y >= Screen.height - borderSize;

        if (isOffScreen)
        {
            Vector3 cappedTargetScreenPosition = targetPositionScreenPoint;
            if (cappedTargetScreenPosition.x <= borderSize) cappedTargetScreenPosition.x = borderSize;
            if (cappedTargetScreenPosition.x >= Screen.width - borderSize) cappedTargetScreenPosition.x = Screen.width - borderSize;
            if (cappedTargetScreenPosition.y <= borderSize) cappedTargetScreenPosition.y = borderSize;
            if (cappedTargetScreenPosition.y >= Screen.height - borderSize) cappedTargetScreenPosition.y = Screen.height - borderSize;

            Vector3 pointerWorldPosition = uiCamera.ScreenToWorldPoint(cappedTargetScreenPosition);
            pointerRectTransform.position = pointerWorldPosition;
            pointerRectTransform.localPosition = new Vector3(pointerRectTransform.localPosition.x, pointerRectTransform.localPosition.y, 0f);

        }
        else
        {
            Vector3 pointerWorldPosition = uiCamera.ScreenToWorldPoint(targetPositionScreenPoint);
            pointerRectTransform.position = pointerWorldPosition;
            pointerRectTransform.localPosition = new Vector3(pointerRectTransform.localPosition.x - 3f, pointerRectTransform.localPosition.y + 0.5f, 0f);
            pointerRectTransform.localEulerAngles = new Vector3(0, 0, -100);
        }
    }

    private void UpdateTargetPosition()
    {
        if (currentNPCIndex < npcs.Length)
        {
            targetPosition = npcs[currentNPCIndex].transform.position;
        }
        else
        {
            //TODO: Handle case where there are no more NPCs
            Debug.LogWarning("No more NPCs left to point to.");
            targetPosition = Vector3.zero;
        }
    }

    // Called when the player collides with an NPC
    public void OnPlayerCollidedWithNPC()
    {
        currentNPCIndex++;
        UpdateTargetPosition();
    }
}
