using UnityEngine;

public class Scene1Manager : MonoBehaviour
{
    public SpriteRenderer backgroundImageInScene1;  // Drag your SpriteRenderer here in the inspector

    // Default background color: RGB (218, 218, 218, 200)
    private readonly Color defaultBackgroundColor = new Color(218f / 255f, 218f / 255f, 218f / 255f, 200f / 255f);

    void Start()
    {
        // Load the background color from PlayerPrefs, or use the default color
        Color backgroundColor = LoadColorFromPrefs("BackgroundImage", defaultBackgroundColor);

        // Apply the color to the background image in Scene 1
        if (backgroundImageInScene1 != null)
        {
            backgroundImageInScene1.color = backgroundColor;
        }
        else
        {
            Debug.LogError("Background image in Scene 1 not assigned!");
        }
    }

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
            return defaultColor;
        }
    }
}
