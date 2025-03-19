using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spinner : MonoBehaviour
{
    public float rotationSpeedIncrement = 250f; // Speed added per button press
    public float maxSpeed = 800f; // Maximum spin speed
    public float decelerationRate = 0.98f; // Smoother gradual slowdown
    public Button spinButton; // Button that triggers the spin
    public List<Button> skinButtons;
    public float defaultRadius = 300f;
    public float expandedRadius = 400f;

    private float currentRotationSpeed = 0f;
    private HashSet<Button> activeButtons = new HashSet<Button>();

    void Start()
    {
        ArrangeButtonsInCircle(); // Arrange buttons initially
        spinButton.onClick.AddListener(Spin); // Attach button click event
    }

    void Update()
    {
        if (Mathf.Abs(currentRotationSpeed) > 0.01f) // Avoid micro rotations
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

    public void Spin()
    {
        currentRotationSpeed += rotationSpeedIncrement; // Increase speed
        currentRotationSpeed = Mathf.Clamp(currentRotationSpeed, 0, maxSpeed); // Cap at max speed
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
        Button lowestButton = null;
        float lowestY = float.MaxValue;

        // Find the button with the lowest screen Y position
        foreach (Button button in skinButtons)
        {
            Vector3 screenPos = Camera.main.WorldToScreenPoint(button.transform.position);
            if (screenPos.y < lowestY)
            {
                lowestY = screenPos.y;
                lowestButton = button;
            }
        }

        // Update button positions
        foreach (Button button in skinButtons)
        {
            button.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
            float radius = (button == lowestButton) ? expandedRadius : defaultRadius;
            Vector3 direction = button.transform.localPosition.normalized;
            button.transform.localPosition = direction * radius;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Button button = other.GetComponent<Button>();
        if (button != null && skinButtons.Contains(button))
        {
            activeButtons.Add(button);
            Debug.Log(button.name + " entered the Hopper!");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Button button = other.GetComponent<Button>();
        if (button != null && skinButtons.Contains(button))
        {
            activeButtons.Remove(button);
            Debug.Log(button.name + " exited the Hopper!");
        }
    }
}
