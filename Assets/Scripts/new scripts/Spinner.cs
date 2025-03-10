using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Spinner : MonoBehaviour, IDragHandler, IPointerDownHandler
{
    public float rotationSpeed = 0.1f;  // Base sensitivity to drag
    public List<Button> skinButtons;
    public float defaultRadius = 300f;
    public float expandedRadius = 400f;
    public float deceleration = 0.95f;  // Friction/slowdown factor
    public float minimumSpeed = 0.1f;   // Minimum speed before stopping
    public float maxSpeed = 20f;        // Max speed cap for the spinner
    private float currentRotationSpeed = 0f;  // Current rotational velocity
    private Vector2 lastDragPosition;

    private HashSet<Button> activeButtons = new HashSet<Button>(); // Tracks buttons inside the Hopper

    void Start()
    {
        ArrangeButtonsInCircle();  // Arrange buttons initially in a circle
    }


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

        // Update button positions based on whether they're inside the Hopper
        foreach (Button button in skinButtons)
        {
            // Keep button upright
            button.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);

            // Change radius if it's inside the Hopper
            float radius = activeButtons.Contains(button) ? expandedRadius : defaultRadius;
            Vector3 direction = button.transform.localPosition.normalized;
            button.transform.localPosition = direction * radius;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        // Track the speed of the drag and calculate velocity
        Vector2 dragDelta = eventData.position - lastDragPosition;

        // Calculate speed based on the distance dragged
        currentRotationSpeed = dragDelta.x * rotationSpeed;

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
        currentRotationSpeed = 0f;
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        Button button = other.GetComponent<Button>();
        if (button != null && skinButtons.Contains(button))
        {
            activeButtons.Add(button);  // Mark button as inside the Hopper
            Debug.Log(button.name + " entered the Hopper!");  // Debug log
        }
        else
        {
            Debug.Log("Collider entered, but it's not a button or it's not in the skinButtons list.");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Button button = other.GetComponent<Button>();
        if (button != null && skinButtons.Contains(button))
        {
            activeButtons.Remove(button);  // Remove from active buttons
            Debug.Log(button.name + " exited the Hopper!");  // Debug log
        }
        else
        {
            Debug.Log("Collider exited, but it's not a button or it's not in the skinButtons list.");
        }
    }

}

