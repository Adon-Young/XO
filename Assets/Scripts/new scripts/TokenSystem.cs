using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TokenSystem : MonoBehaviour
{
    private const int maximumNumberOfTokens = 5; // Max tokens
    private const float numberOfHoursToRegenerateTokens = 24.0f; 

    public int tokens; // Current token count
    public DateTime lastTokenTime; // Add this field to store the last token time

    public TMP_Text timerDisplay; // UI Text for the countdown timer
    public Image[] tokenSprites; // Array for token circle images

    private readonly Color activeTokenColor = new Color(1f, 0.84f, 0f, 1f); // Gold
    private readonly Color inactiveTokenColor = Color.white;
    public Sprite xSprite;
    public Sprite oSprite;


    void Start()
    {
        LoadTokens();
        UpdateTokenRegen();
        UpdateUI();

        //resetting playe prefs for testing, comment out or remove when complete

        //PlayerPrefs.DeleteAll();
        //PlayerPrefs.Save();
    }

    void Update()
    {
        UpdateCountdownTimer();
    }

    void LoadTokens()
    {
        tokens = PlayerPrefs.GetInt("tokens", 0);
        string lastTimeStr = PlayerPrefs.GetString("lastTokenTime", "");

        if (!string.IsNullOrEmpty(lastTimeStr))
        {
            if (!DateTime.TryParse(lastTimeStr, out lastTokenTime))
            {
                Debug.LogWarning("Invalid lastTokenTime in PlayerPrefs, resetting to current time.");
                lastTokenTime = DateTime.UtcNow; // Set to current time if invalid
            }
        }
        else
        {
            lastTokenTime = DateTime.UtcNow; // Set to current time if empty
        }

        SaveTokens();
    }


    void SaveTokens()
    {
        PlayerPrefs.SetInt("tokens", tokens);
        PlayerPrefs.SetString("lastTokenTime", lastTokenTime.ToString()); // Save lastTokenTime
        PlayerPrefs.Save();
    }

    void UpdateTokenRegen()
    {
        if (tokens >= maximumNumberOfTokens) return;

        TimeSpan timePassed = DateTime.UtcNow - lastTokenTime;

        // Prevent negative or absurdly large values
        if (timePassed.TotalSeconds < 0 || timePassed.TotalDays > 365)
        {
            Debug.LogError($"[UpdateTokenRegen] Invalid timePassed: {timePassed}, resetting lastTokenTime.");
            lastTokenTime = DateTime.UtcNow;  // Reset to current time
            SaveTokens();
            return;
        }

        int tokensToRegen = (int)(timePassed.TotalHours / numberOfHoursToRegenerateTokens);

        Debug.Log($"[UpdateTokenRegen] Time Passed: {timePassed}, Tokens to Regen: {tokensToRegen}");

        if (tokensToRegen > 0)
        {
            tokens = Mathf.Min(tokens + tokensToRegen, maximumNumberOfTokens);
            double leftoverSeconds = timePassed.TotalSeconds % (numberOfHoursToRegenerateTokens * 3600);
            lastTokenTime = DateTime.UtcNow.AddSeconds(-leftoverSeconds);
            SaveTokens();
            UpdateUI();
        }
    }


    public void UpdateCountdownTimer()
    {
        if (tokens >= maximumNumberOfTokens)
        {
            timerDisplay.text = "Tokens Full";
            return;
        }

        if (lastTokenTime.Year < 2000) // Instead of DateTime.MinValue
        {
            timerDisplay.text = "Next token: --:--";
            return;
        }

        TimeSpan timeSinceLast = DateTime.UtcNow - lastTokenTime;
        TimeSpan timeUntilNext = TimeSpan.FromHours(numberOfHoursToRegenerateTokens) - timeSinceLast;

        if (timeUntilNext.TotalSeconds > 0)
        {
            // If there's at least an hour left, show hours and minutes
            if (timeUntilNext.TotalHours >= 1.0)
            {
                int hours = Mathf.FloorToInt((float)timeUntilNext.TotalHours);
                int minutes = Mathf.FloorToInt((float)(timeUntilNext.TotalMinutes - hours * 60));
                timerDisplay.text = $"Next token in: {hours:D2}:{minutes:D2}";
            }
            // If less than an hour left, show minutes and seconds
            else
            {
                int minutes = Mathf.FloorToInt((float)timeUntilNext.TotalMinutes);
                int seconds = Mathf.FloorToInt((float)(timeUntilNext.TotalSeconds - minutes * 60));
                timerDisplay.text = $"Next token in: {minutes:D2}:{seconds:D2}";
            }
        }
        else
        {
            timerDisplay.text = "Token Ready!";
            UpdateTokenRegen(); // Try regenerating
            UpdateUI();         // Force UI update here
        }
    }



    void UpdateUI()
    {
        for (int i = 0; i < tokenSprites.Length; i++)
        {
            tokenSprites[i].color = (i < tokens) ? activeTokenColor : inactiveTokenColor;
            Transform childImage = tokenSprites[i].transform.GetChild(0);//get the 1st chiild object of the token
            Image childSprite = childImage.GetComponent<Image>();//gettinig the sprite renderer of the child object

            if (childSprite != null)
            {
                childSprite.sprite = (i < tokens) ? oSprite : xSprite;
            }
            //similar to colour changing for active and inactive colour but instead itll be on the child object and spriite swapping
        }
    }
    public void SpendTokens()
    {
        // Check if the player has at least 5 tokens
        if (tokens >= 5)
        {
            tokens -= 5;  // Spend 5 tokens
            SaveTokens();  // Save the new token count
            UpdateUI();    // Update the UI to reflect the new token count
        }
        else
        {
            Debug.Log("Not enough tokens! You need at least 5 tokens to play.");
        }
    }
}

