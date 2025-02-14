using System;
using UnityEngine;
using UnityEngine.UI;

public class TokenSystem : MonoBehaviour
{
    private const int maximumNumberOfTokens = 5; // Max tokens
    private const float numberOfHoursToRegenerateTokens = 0.1f; // 6 mins per token for testing

    private int tokens; // Current token count

    public Text timerDisplay; // UI Text for the countdown timer
    public Image[] tokenSprites; // Array for token circle images

    private readonly Color activeTokenColor = new Color(1f, 0.84f, 0f, 1f); // Gold
    private readonly Color inactiveTokenColor = Color.black;

    void Start()
    {
        LoadTokens();
        UpdateTokenRegen();
        UpdateUI();
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
            SaveTokens();
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

