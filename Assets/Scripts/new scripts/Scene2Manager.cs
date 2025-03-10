using UnityEngine;

public class Scene2Manager : MonoBehaviour
{
    public SpriteRenderer backgroundImageInScene2;  // Assign in Inspector

    void Start()
    {
        // Load and apply background color
        Color backgroundColor = LoadColorFromPrefs("BackgroundImage", Color.blue);
        if (backgroundImageInScene2 != null)
        {
            backgroundImageInScene2.color = backgroundColor;
        }
        else
        {
            Debug.LogError("Background image in Scene 2 not assigned!");
        }

        // Load particle colors into PlayerPrefs so ParticleManagers can use them
        LoadColorFromPrefs("XParticleColour", Color.red);
        LoadColorFromPrefs("OParticleColour", Color.blue);
    }

    public static Color LoadColorFromPrefs(string key, Color defaultColor)
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



