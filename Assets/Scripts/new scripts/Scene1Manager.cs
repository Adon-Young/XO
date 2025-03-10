using UnityEngine;

public class Scene1Manager : MonoBehaviour
{
    public SpriteRenderer backgroundImageInScene2;  // Drag your SpriteRenderer here in the inspector

    // Start is called before the first frame update
    void Start()
    {
        // Load the background color from PlayerPrefs
        Color backgroundColor = LoadColorFromPrefs("BackgroundImage", Color.blue);  // Default to white if no color saved

        // Apply the color to the background image in Scene 2
        if (backgroundImageInScene2 != null)
        {
            backgroundImageInScene2.color = backgroundColor;
        }
        else
        {
            Debug.LogError("Background image in Scene 2 not assigned!");
        }
    }

    // Function to load the color from PlayerPrefs
    private Color LoadColorFromPrefs(string key, Color defaultColor)
    {
        if (PlayerPrefs.HasKey(key + "_R"))
        {
            return new Color(
                PlayerPrefs.GetFloat(key + "_R"),
                PlayerPrefs.GetFloat(key + "_G"),
                PlayerPrefs.GetFloat(key + "_B"),
                PlayerPrefs.GetFloat(key + "_A")
            );
        }
        else
        {
            // If no saved color exists, return the default color
            return defaultColor;
        }
    }
}
