using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TokenSystem : MonoBehaviour
{
    private const int maximumNumberOfTokens = 5; // Max tokens
    private const float numberOfHoursToRegenerateTokens = 0.005f; // 6 mins per token for testing

    private int tokens; // Current token count

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
        SaveTokens();
    }

    void SaveTokens()
    {
        PlayerPrefs.SetInt("tokens", tokens);
        PlayerPrefs.SetString("lastTokenTime", DateTime.UtcNow.ToString());
        PlayerPrefs.Save();
    }

    void UpdateTokenRegen()
    {
        if (tokens >= maximumNumberOfTokens) return;

        string lastTimeStr = PlayerPrefs.GetString("lastTokenTime", "");
        if (string.IsNullOrEmpty(lastTimeStr)) return;

        DateTime lastTime = DateTime.Parse(lastTimeStr);
        TimeSpan timePassed = DateTime.UtcNow - lastTime;

        int tokensToRegen = (int)(timePassed.TotalHours / numberOfHoursToRegenerateTokens);
        if (tokensToRegen > 0)
        {
            tokens = Mathf.Min(tokens + tokensToRegen, maximumNumberOfTokens);

            // Calculate leftover time that wasn’t enough for a full token
            double leftoverSeconds = timePassed.TotalSeconds % (numberOfHoursToRegenerateTokens * 3600);//converting hours to seconds *3600
            DateTime newLastTokenTime = DateTime.UtcNow.AddSeconds(-leftoverSeconds); // Subtract leftover
            //this makes sure to save the used time ie prevents the player from resetting the countdown timer every time they join the game
            //in theory this should allow the timer to essentially continue regenerating tokens for the player even when not in the game

            // Save updated tokens and adjusted last token time
            PlayerPrefs.SetInt("tokens", tokens);
            PlayerPrefs.SetString("lastTokenTime", newLastTokenTime.ToString());
            PlayerPrefs.Save();

            UpdateUI();
        }
    }

    void UpdateCountdownTimer()
    {
        if (tokens >= maximumNumberOfTokens)
        {
            timerDisplay.text = "Tokens Full";
            return;
        }

        string lastTimeStr = PlayerPrefs.GetString("lastTokenTime", "");
        if (string.IsNullOrEmpty(lastTimeStr))
        {
            timerDisplay.text = "Next token: --:--";
            return;
        }

        DateTime lastTime = DateTime.Parse(lastTimeStr);
        TimeSpan timeSinceLast = DateTime.UtcNow - lastTime;
        TimeSpan timeUntilNext = TimeSpan.FromHours(numberOfHoursToRegenerateTokens) - timeSinceLast;

        if (timeUntilNext.TotalSeconds > 0)
        {
            int minutes = Mathf.FloorToInt((float)timeUntilNext.TotalMinutes);
            int seconds = Mathf.FloorToInt((float)timeUntilNext.TotalSeconds % 60);
            timerDisplay.text = $"Next token in: {minutes:D2}:{seconds:D2}";
        }
        else
        {
            timerDisplay.text = "Token Ready!";
            UpdateTokenRegen(); // Try regenerating
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
        if (tokens > 0)
        {
            tokens--;
            SaveTokens();
            UpdateUI();
        }
        else
        {
            Debug.Log("Not enough tokens!");
        }
    }
}

