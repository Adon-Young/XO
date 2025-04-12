using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spinner : MonoBehaviour
{
    public Image boundaryImage; // The Image object that represents the boundary area
    public GameObject tickingAudioObj;
    private AudioSource rouletteAudioSound;
    public float defaultRadius = 300f;
    public float expandedRadius = 400f;
    public float rotationSpeedIncrement = 800f;
    public float maxSpeed = 1000f;
    public float decelerationRate = 0.98f;
    public Button spinButton;
    public List<Button> skinButtons;
    public float stopSpeed = 100f; // Speed below which the wheel should stop
    public float stopDelay = 1.5f; // Time after the spin button is pressed before starting checks
    private float currentRotationSpeed = 0f;
    private bool isSpinning = false;
    private float stopTimer = 0f;
    private float delayTimer = 0f; // Timer for the delay after spin button is pressed
    private bool hasBeenPressed = false; // Check if spin button has been pressed
    private HashSet<Button> buttonsInsideBoundary = new HashSet<Button>();

    void Start()
    {
        rouletteAudioSound = tickingAudioObj.GetComponent<AudioSource>();
        DisableAllButtons();
        ArrangeButtonsInCircle(); // Arrange buttons initially
        spinButton.onClick.AddListener(Spin); // Attach spin button click event
    }

    void Update()
    {
        foreach (Button button in skinButtons)
        {
            if (IsButtonInsideBoundary(button))
            {
                // If the button is inside the boundary and hasn't been triggered before, play the sound
                if (!buttonsInsideBoundary.Contains(button))
                {
                    rouletteAudioSound.Play(); // Play the sound
                    buttonsInsideBoundary.Add(button); // Add the button to the set
                }
            }
            else
            {
                // If the button is not inside the boundary, remove it from the set
                if (buttonsInsideBoundary.Contains(button))
                {
                    buttonsInsideBoundary.Remove(button);
                }
            }
        }

        if (isSpinning)
        {
            // Countdown the stop timer if it's running
            if (stopTimer > 0f)
            {
                stopTimer -= Time.deltaTime; // Decrease the timer
            }
            else
            {
                // If the stop timer has finished, check conditions to stop the wheel
                if (currentRotationSpeed <= stopSpeed && IsButtonInMiddleOfBoundary())
                {
                    currentRotationSpeed = 0f; // Stop the wheel if conditions are met
                }
            }

            // Countdown the delay timer
            if (delayTimer > 0f)
            {
                delayTimer -= Time.deltaTime; // Decrease the delay timer
            }
        }

        // Rotate the wheel if spinning
        if (Mathf.Abs(currentRotationSpeed) > 0.01f)
        {
            transform.Rotate(0, 0, currentRotationSpeed * Time.deltaTime);
            currentRotationSpeed *= decelerationRate; // Apply smooth deceleration

            if (Mathf.Abs(currentRotationSpeed) < 0.1f)
            {
                currentRotationSpeed = 0f; // Stop when speed is negligible
            }
        }

        UpdateButtonPositions(); // Update button positions
    }

    // Method to trigger the spin (when the button is pressed)
    public void Spin()
    {
        if (hasBeenPressed) return; // Prevent spin button from being pressed multiple times

        hasBeenPressed = true; // Mark the button as pressed
        spinButton.interactable = false; // Disable the spin button after it's pressed

        currentRotationSpeed += rotationSpeedIncrement;
        currentRotationSpeed = Mathf.Clamp(currentRotationSpeed, 0, maxSpeed);
        stopTimer = stopDelay;
        delayTimer = stopDelay; // Start the delay timer when the button is pressed
        isSpinning = true;
    }

    // Arrange buttons in a circle around the wheel
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

    // Update button positions based on their current location relative to the boundary
    void UpdateButtonPositions()
    {
        foreach (Button button in skinButtons)
        {
            button.transform.rotation = Quaternion.identity; // Ensure buttons don't rotate

            // Check if button is inside the boundary
            bool isInsideBoundary = IsButtonInsideBoundary(button);

            // Smoothly transition button positions based on boundary status
            float targetRadius = isInsideBoundary ? expandedRadius : defaultRadius;
            float smoothRadius = Mathf.Lerp(button.transform.localPosition.magnitude, targetRadius, Time.deltaTime * 5f);
            Vector3 direction = (button.transform.localPosition - Vector3.zero).normalized;
            button.transform.localPosition = direction * smoothRadius;

            // Enable or disable buttons based on boundary
            button.interactable = isInsideBoundary;
        }
    }

    // Check if the button is inside the boundary area
    bool IsButtonInsideBoundary(Button button)
    {
        RectTransform boundaryRectTransform = boundaryImage.GetComponent<RectTransform>();
        RectTransform buttonRectTransform = button.GetComponent<RectTransform>();

        // Convert the button's position to the boundary's local space
        Vector2 localButtonPosition = boundaryRectTransform.InverseTransformPoint(buttonRectTransform.position);

        // Check if the button is inside the boundary by comparing the local position
        return boundaryRectTransform.rect.Contains(localButtonPosition);
    }

    // Check if the button is in the middle of the boundary (within the center area)
    bool IsButtonInMiddleOfBoundary()
    {
        foreach (Button button in skinButtons)
        {
            Vector3 screenPos = Camera.main.WorldToScreenPoint(button.transform.position);

            // Convert button position to local space
            RectTransform boundaryRectTransform = boundaryImage.GetComponent<RectTransform>();
            Vector2 localButtonPos = boundaryRectTransform.InverseTransformPoint(button.transform.position);

            // Check if the button is in the middle of the boundary (within a certain x range)
            float middleX = (boundaryRectTransform.rect.xMin + boundaryRectTransform.rect.xMax) / 2;
            float tolerance = 20f; // You can adjust this tolerance based on your needs

            if (Mathf.Abs(localButtonPos.x - middleX) <= tolerance)
            {
                return true;
            }
        }

        return false;
    }

    // Reset the game (buttons and wheel)
    public void Reset()
    {
        // Disable all buttons on the wheel
        foreach (Button button in skinButtons)
        {
            button.interactable = false;
        }

        // Reset the spin button to be interactable
        spinButton.interactable = true;
        hasBeenPressed = false; // Allow the spin button to be pressed again

        // Reset the wheel's rotation to its original position (0 degrees)
        transform.rotation = Quaternion.identity;

        // Optionally, reset any other variables or states you need here.
    }

    // Disable all buttons
    void DisableAllButtons()
    {
        foreach (Button button in skinButtons)
        {
            button.interactable = false; // Disable button
        }
    }

    // Optional: Draw the boundary on the canvas for debugging (this is just for visualization)
    void OnGUI()
    {
        if (boundaryImage != null)
        {
            GUI.color = new Color(1f, 0f, 0f, 0.2f); // Transparent red
            float boxWidth = boundaryImage.rectTransform.rect.width;
            float boxHeight = boundaryImage.rectTransform.rect.height;
            Vector2 screenPos = Camera.main.WorldToScreenPoint(boundaryImage.transform.position);
            GUI.Box(new Rect(screenPos.x, Screen.height - screenPos.y - boxHeight, boxWidth, boxHeight), GUIContent.none);
        }
    }
}
