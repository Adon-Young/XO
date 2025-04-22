using UnityEngine;
using UnityEngine.UI;

public class Scene1Manager : MonoBehaviour
{
    public SpriteRenderer backgroundImageInScene1; 
    public Image[] Blockers;
    // Default colours
    private readonly Color defaultBackgroundColor = new Color(218f / 255f, 218f / 255f, 218f / 255f, 200f / 255f);
    private readonly Color defaultOparticleColour = new Color(114f / 255f, 114f / 255f, 114f / 255f, 255f / 255f);
    private readonly Color defaultXparticleColour = new Color(156f / 255f, 156f / 255f, 156f / 255f, 255f / 255f);

    void Start()
    {
        if (!PlayerPrefs.HasKey("HasInitializedDefaults"))
        {
            // First time running the game, set and save default colors
            SaveDefaultColor("BackgroundImage", defaultBackgroundColor);
            SaveDefaultColor("OParticleColour", defaultOparticleColour);
            SaveDefaultColor("XParticleColour", defaultXparticleColour);

            // Mark that defaults have been set
            PlayerPrefs.SetInt("HasInitializedDefaults", 1);
            PlayerPrefs.Save();
        }

        // Load stored colors (whether they are defaults or chosen by the player)
        Color backgroundColor = LoadColorFromPrefs("BackgroundImage", defaultBackgroundColor);
        Color OpartColour = LoadColorFromPrefs("OParticleColour", defaultOparticleColour);
        Color XpartColour = LoadColorFromPrefs("XParticleColour", defaultXparticleColour);

        // Apply the color to the background image in Scene 1
        if (backgroundImageInScene1 != null)
        {
            backgroundImageInScene1.color = backgroundColor;

            foreach (Image blocker in Blockers)
            {
                blocker.color = backgroundColor;
            }


        }
        else
        {
            Debug.LogError("Background image in Scene 1 not assigned!");
        }
    }

    // Helper function to save default colors
    private void SaveDefaultColor(string key, Color color)
    {
        PlayerPrefs.SetFloat(key + "_R", color.r);
        PlayerPrefs.SetFloat(key + "_G", color.g);
        PlayerPrefs.SetFloat(key + "_B", color.b);
        PlayerPrefs.SetFloat(key + "_A", color.a);
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
