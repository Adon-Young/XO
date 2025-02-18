using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class Spinner : MonoBehaviour, IDragHandler, IPointerDownHandler
{
    public float rotationSpeed = 0.1f;  // Base sensitivity to drag
    public List<Button> skinButtons;
    public float radius = 300f;
    public float deceleration = 0.95f;  // Friction/slowdown factor (0 = instant stop, 1 = no stop)
    public float minimumSpeed = 0.1f;   // Minimum speed before we stop the rotation
    public float maxSpeed = 20f;        // Max speed cap for the spinner
    private float currentRotationSpeed = 0f;  // Current rotational velocity
    private Vector2 lastDragPosition;

    private Button previousBottomButton = null;

    void Update()
    {
        // Apply rotation momentum and decelerate
        if (currentRotationSpeed != 0)
        {
            transform.Rotate(0, 0, currentRotationSpeed * Time.deltaTime);
            currentRotationSpeed *= deceleration;

            if (Mathf.Abs(currentRotationSpeed) > maxSpeed)
            {
                currentRotationSpeed = Mathf.Sign(currentRotationSpeed) * maxSpeed;
            }

            if (Mathf.Abs(currentRotationSpeed) < minimumSpeed)
            {
                currentRotationSpeed = 0f;
            }
        }

        // Track the button with the lowest Y world position
        Button bottomButton = null;
        float lowestY = float.MaxValue;

        foreach (Button buttonObj in skinButtons)
        {
            // Keep button upright
            buttonObj.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);

            // Get world position
            Vector3 worldPos = buttonObj.transform.position;

            // Smooth transition: If within a small range of the lowest button, favor gradual selection
            if (worldPos.y < lowestY + 5f)  // Adjust this range to control smoothness
            {
                lowestY = worldPos.y;
                bottomButton = buttonObj;
            }
        }

        // Reset previous button if it’s not the lowest anymore
        if (previousBottomButton != null && previousBottomButton != bottomButton)
        {
            previousBottomButton.transform.localPosition = previousBottomButton.transform.localPosition.normalized * 300;
        }

        // Assign new lowest button and update its radius
        if (bottomButton != null && bottomButton != previousBottomButton)
        {
            bottomButton.transform.localPosition = bottomButton.transform.localPosition.normalized * 400;
            previousBottomButton = bottomButton; // Store for next frame
        }
    }




    public void OnDrag(PointerEventData eventData)
    {
        // Track the speed of the drag and calculate velocity
        Vector2 dragDelta = eventData.position - lastDragPosition;

        // Calculate speed based on the distance dragged
        currentRotationSpeed = dragDelta.x * rotationSpeed; // Apply drag speed to rotation

        // Cap the rotation speed to maxSpeed
        if (Mathf.Abs(currentRotationSpeed) > maxSpeed)
        {
            currentRotationSpeed = Mathf.Sign(currentRotationSpeed) * maxSpeed;
        }

        lastDragPosition = eventData.position;  // Update last drag position
    }

    // Allow tap to stop spinning immediately
    public void OnPointerDown(PointerEventData eventData)
    {
        // If the player taps anywhere on the screen, stop the spinner immediately
        currentRotationSpeed = 0f;
    }

    void Start()
    {
        ArrangeButtonsInCircle();
    }

    void ArrangeButtonsInCircle()
    {
        int totalButtons = skinButtons.Count;
        float angleStep = 360f / totalButtons;

        for (int i = 0; i < totalButtons; i++)
        {
            float angle = i * angleStep * Mathf.Deg2Rad;
            float x = Mathf.Cos(angle) * radius;
            float y = Mathf.Sin(angle) * radius;

            skinButtons[i].transform.localPosition = new Vector3(x, y, 0);
        }
    }
}
