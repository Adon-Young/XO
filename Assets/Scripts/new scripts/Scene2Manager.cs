using UnityEngine;
using System;

public class Scene2Manager : MonoBehaviour
{
    public SpriteRenderer backgroundImageInScene2;  // Assign in Inspector
    public TokenSystem tokenSystem;
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

        LoadTokenData(); // Load the token data when entering Scene 2
        tokenSystem.UpdateCountdownTimer();
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
            tokenSystem.tokens = tokens;
            tokenSystem.lastTokenTime = lastTime; // Assign the loaded last token time
        }
    }
}



