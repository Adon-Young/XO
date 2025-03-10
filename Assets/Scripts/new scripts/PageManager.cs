using UnityEngine;
using UnityEngine.UI;

public class PageManager : MonoBehaviour
{
    public GameObject[] pages; // Array of pages to swap between
    public Button nextButton;
    public Button prevButton;
    public Button resetButton; // New reset button

    private int currentPageIndex = 0;

    void Start()
    {
        UpdatePage();

        // Attach button listeners
        nextButton.onClick.AddListener(GoToNextPage);
        prevButton.onClick.AddListener(GoToPreviousPage);
        resetButton.onClick.AddListener(ResetToFirstPage); // Attach reset function to button

        // Update buttons' initial state
        UpdateButtonStates();
    }

    // Show the current page and hide others
    void UpdatePage()
    {
        for (int i = 0; i < pages.Length; i++)
        {
            pages[i].SetActive(i == currentPageIndex);
        }
    }

    // Go to the next page if available
    void GoToNextPage()
    {
        if (currentPageIndex < pages.Length - 1)
        {
            currentPageIndex++;
            UpdatePage();
            UpdateButtonStates();
        }
    }

    // Go to the previous page if available
    void GoToPreviousPage()
    {
        if (currentPageIndex > 0)
        {
            currentPageIndex--;
            UpdatePage();
            UpdateButtonStates();
        }
    }

    // Reset to the first page
    void ResetToFirstPage()
    {
        currentPageIndex = 0; // Set to the first page index
        UpdatePage();          // Update to show the first page
        UpdateButtonStates();   // Update button states
    }

    // Enable or disable buttons based on the current page
    void UpdateButtonStates()
    {
        prevButton.interactable = currentPageIndex > 0;
        nextButton.interactable = currentPageIndex < pages.Length - 1;
    }
}
