using UnityEngine;
using System;
using UnityEngine.UI;

public class Scene2Manager : MonoBehaviour
{
    //similar to scene1Manager assigning the colours
    public SpriteRenderer backgroundImageInScene2;
    public SpriteRenderer[] squareButtons;
    public SpriteRenderer boardSprite;
    public Image[] Blockers;
    

    // Default colors
    private readonly Color defaultBackgroundColor = new Color(218f / 255f, 218f / 255f, 218f / 255f, 200f / 255f);
    private readonly Color defaultXParticleColor = new Color(156f / 255f, 156f / 255f, 156f / 255f, 255f / 255f);
    private readonly Color defaultOParticleColor = new Color(114f / 255f, 114f / 255f, 114f / 255f, 255f / 255f);

    void Start()
    {
        // Load and apply background color
        Color backgroundColor = LoadColorFromPrefs("BackgroundImage", defaultBackgroundColor);
        Color squareBackgroundColor = LoadColorFromPrefs("XParticleColour", defaultXParticleColor);
        Color boardBackgroundColor = LoadColorFromPrefs("OParticleColour", defaultOParticleColor);
        if (backgroundImageInScene2 != null)
        {
            backgroundImageInScene2.color = backgroundColor;
            boardSprite.color = boardBackgroundColor;


            foreach (SpriteRenderer sprite in squareButtons)
            {
                sprite.color = squareBackgroundColor;

            }

            foreach (Image blocker in Blockers)
            {
                blocker.color = backgroundColor;
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
        int tokens = PlayerPrefs.GetInt("tokens", 0);//this is so when the player leaves the main menu (similar to leaving the game) the countdown for tokens continues to count while they are away
        string lastTimeStr = PlayerPrefs.GetString("lastTokenTime", "");

        if (!string.IsNullOrEmpty(lastTimeStr))
        {
            DateTime lastTime = DateTime.Parse(lastTimeStr);
           
        }
    }
}


