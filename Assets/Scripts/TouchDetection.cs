using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchDetection : MonoBehaviour
{
    void Update()
    {
        // Check if there is at least one touch
        if (Input.touchCount > 0)
        {
            Touch touch = Input.touches[0]; // Safely get the first touch

            if (touch.phase == TouchPhase.Began) // Check if the touch just started
            {
                Ray ray = Camera.main.ScreenPointToRay(touch.position); // Convert touch position to a ray
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit)) // Perform the raycast
                {
                    if (hit.collider != null) // Ensure we hit something
                    {
                        // Call the SquareClicked function in GameManager
                        GameObject gameManager = GameObject.Find("Game Manager");
                        if (gameManager != null)
                        {
                            gameManager.SendMessage("SquareClicked", gameObject);
                        }
                        else
                        {
                            Debug.LogWarning("Game Manager not found!");
                        }

                        // Disable SquareSelected component if it exists
                        SquareSelected squareSelected = gameObject.GetComponent<SquareSelected>();
                        if (squareSelected != null)
                        {
                            squareSelected.enabled = false;
                        }

                        // Destroy this script to prevent multiple selections
                        Destroy(this);
                    }
                }
            }
        }
    }
}
