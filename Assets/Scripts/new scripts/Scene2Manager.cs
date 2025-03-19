using UnityEngine;
using System;

public class Scene2Manager : MonoBehaviour
{
    public SpriteRenderer backgroundImageInScene2;
    public SpriteRenderer[] squareButtons;
    

    // Default colors
    private readonly Color defaultBackgroundColor = new Color(218f / 255f, 218f / 255f, 218f / 255f, 200f / 255f);
    private readonly Color defaultXParticleColor = new Color(156f / 255f, 156f / 255f, 156f / 255f, 255f / 255f);
    private readonly Color defaultOParticleColor = new Color(114f / 255f, 114f / 255f, 114f / 255f, 255f / 255f);

    void Start()
    {
        // Load and apply background color
        Color backgroundColor = LoadColorFromPrefs("BackgroundImage", defaultBackgroundColor);
        if (backgroundImageInScene2 != null)
        {
            backgroundImageInScene2.color = backgroundColor;
            foreach(SpriteRenderer sprite in squareButtons)
            {
                sprite.color = backgroundColor;
            }
            

            
            

        }
        else
        {
            Debug.LogError("Background image in Scene 2 not assigned!");
        }

        // Load particle colors into PlayerPrefs so ParticleManagers can use them
        LoadColorFromPrefs("XParticleColour", defaultXParticleColor);
        LoadColorFromPrefs("OParticleColour", defaultOParticleColor);

        LoadTokenData(); // Load the token data when entering Scene 2
        
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

    void LoadTokenData()
    {
        // Reload token data from PlayerPrefs, so it's consistent across scenes
        int tokens = PlayerPrefs.GetInt("tokens", 0);
        string lastTimeStr = PlayerPrefs.GetString("lastTokenTime", "");

        if (!string.IsNullOrEmpty(lastTimeStr))
        {
            DateTime lastTime = DateTime.Parse(lastTimeStr);
           
        }
    }
}


