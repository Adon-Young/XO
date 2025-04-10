using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spinner : MonoBehaviour
{
    public float xMinBound = 400f;
    public float xMaxBound = 900f;
    public float yMinBound = 200f;
    public float yMaxBound = 500f;
    public float stopSpeed = 100f;
    public float stopDelay = 1f; // Delay before the wheel can stop (in seconds)
    private float stopTimer = 0f; // Timer to track the delay

    public float defaultRadius = 300f;
    public float expandedRadius = 400f;
    public float rotationSpeedIncrement = 250f; // Speed added per button press
    public float maxSpeed = 800f; // Maximum spin speed
    public float decelerationRate = 0.98f; // Smoother gradual slowdown
    public Button spinButton; // Button that triggers the spin
    public List<Button> skinButtons;

    private float currentRotationSpeed = 0f;
    private bool isSpinning = false; // To track if the wheel is spinning
    private bool hasSpinButtonBeenPressed = false; // Track if the spin button has been pressed

    void Start()
    {
        ArrangeButtonsInCircle(); // Arrange buttons initially
        spinButton.onClick.AddListener(Spin); // Attach button click event
        DisableAllButtons(); // Disable all buttons initially
        spinButton.interactable = true; // Make sure the spin button is enabled initially
    }

    void Update()
    {
        // If the wheel is spinning and the delay is over, start checking to stop the wheel
        if (isSpinning)
        {
            // Countdown the stop timer if it's running
            if (stopTimer > 0f)
            {
                stopTimer -= Time.deltaTime; // Decrease the timer
            }
            else
            {
                // Once the delay is over, we check if the speed is low enough and if any button is within bounds
                if (currentRotationSpeed <= stopSpeed && IsButtonInMiddleOfBounds())
                {
                    currentRotationSpeed = 0f; // Stop the wheel
                    // The spin button will remain interactable = false if it's already been pressed.
                }
            }
        }

        // Apply the deceleration
        if (Mathf.Abs(currentRotationSpeed) > 0.01f)
        {
            transform.Rotate(0, 0, currentRotationSpeed * Time.deltaTime);
            currentRotationSpeed *= decelerationRate; // Apply smooth deceleration

            if (Mathf.Abs(currentRotationSpeed) < 0.1f)
            {
                currentRotationSpeed = 0f; // Stop when speed is negligible
            }
        }

        UpdateButtonPositions();
    }

    // Method to trigger the spin (when the button is pressed)
    public void Spin()
    {
        if (hasSpinButtonBeenPressed) return; // Prevent pressing the button again

        currentRotationSpeed += rotationSpeedIncrement; // Increase speed
        currentRotationSpeed = Mathf.Clamp(currentRotationSpeed, 0, maxSpeed); // Cap at max speed
        stopTimer = stopDelay; // Start the delay timer after the button press
        isSpinning = true; // Mark that the wheel is spinning
        spinButton.interactable = false; // Disable the spin button while spinning
        hasSpinButtonBeenPressed = true; // Mark that the spin button has been pressed
    }

    void ArrangeButtonsInCircle()
    {
        int totalButtons = skinButtons.Count;
        float angleStep = 360f / totalButtons;

        for (int i = 0; i < totalButtons; i++)
        {
            float angle = i * angleStep * Mathf.Deg2Rad;
            float x = Mathf.Cos(angle) * defaultRadius;
            float y = Mathf.Sin(angle) * defaultRadius;

            skinButtons[i].transform.localPosition = new Vector3(x, y, 0);
        }
    }

    void UpdateButtonPositions()
    {
        foreach (Button button in skinButtons)
        {
            button.transform.rotation = Quaternion.identity;

            // Convert world position to screen position
            Vector3 screenPos = Camera.main.WorldToScreenPoint(button.transform.position);

            // Check if button is within the "x" and "y" boundaries
            bool isInXBounds = screenPos.x >= xMinBound && screenPos.x <= xMaxBound;
            bool isInYBounds = screenPos.y >= yMinBound && screenPos.y <= yMaxBound;

            float targetRadius = (isInXBounds && isInYBounds) ? expandedRadius : defaultRadius;

            // Smoothly transition button positions
            float smoothRadius = Mathf.Lerp(button.transform.localPosition.magnitude, targetRadius, Time.deltaTime * 5f);
            Vector3 direction = (button.transform.localPosition - Vector3.zero).normalized;
            button.transform.localPosition = direction * smoothRadius;
        }
    }

    // Helper method to check if any button is within the specified y boundaries and in the middle of x boundaries
    bool IsButtonInMiddleOfBounds()
    {
        foreach (Button button in skinButtons)
        {
            Vector3 screenPos = Camera.main.WorldToScreenPoint(button.transform.position);

            // Check if button is within both the x and y boundaries
            if (screenPos.y >= yMinBound && screenPos.y <= yMaxBound)
            {
                // Check if button is in the middle of the x boundaries (within a small margin of the center)
                float middleX = (xMinBound + xMaxBound) / 2f;
                float tolerance = 20f; // Margin of error for "middle" of x boundaries

                if (Mathf.Abs(screenPos.x - middleX) <= tolerance)
                {
                    button.interactable = true; // Enable the button that is in the middle
                    return true; // Button is in the middle of the x boundaries and within y bounds
                }
            }
        }

        // If no button is found in the middle, disable all buttons
        DisableAllButtons();
        return false;
    }

    // Disable all buttons
    void DisableAllButtons()
    {
        foreach (Button button in skinButtons)
        {
            button.interactable = false; // Disable button
        }
    }

    void OnGUI()
    {
        GUI.color = new Color(1f, 0f, 0f, 0.2f); // Transparent red
        float boxWidth = xMaxBound - xMinBound;
        float boxHeight = yMaxBound - yMinBound;

        float topY = Screen.height - yMinBound; // Y flip for GUI space

        // Adjust for GUI’s top-down coordinate system
        GUI.Box(new Rect(xMinBound, topY - boxHeight, boxWidth, boxHeight), GUIContent.none);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = new Color(1f, 0f, 0f, 0.3f); // Semi-transparent red

        Vector3 bottomLeft = Camera.main.ScreenToWorldPoint(new Vector3(xMinBound, yMinBound, 10f));
        Vector3 topRight = Camera.main.ScreenToWorldPoint(new Vector3(xMaxBound, yMaxBound, 10f));

        Vector3 center = (bottomLeft + topRight) / 2f;
        Vector3 size = topRight - bottomLeft;

        Gizmos.DrawCube(center, size);
    }
}
