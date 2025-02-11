using System;
using UnityEngine;

public class TokenSystem : MonoBehaviour
{
    private const int MAX_TOKENS = 5;
    private const int TOKEN_REGEN_TIME_HOURS = 24;
    private int tokens;

    void Start()
    {
        LoadTokens();
        UpdateTokenRegen();
    }

    void LoadTokens()
    {
        tokens = PlayerPrefs.GetInt("tokens", MAX_TOKENS); // Default to max if first launch
    }

    void SaveTokens()
    {
        PlayerPrefs.SetInt("tokens", tokens);
        PlayerPrefs.SetString("lastTokenTime", DateTime.UtcNow.ToString()); // Save current time
        PlayerPrefs.Save();
    }

    void UpdateTokenRegen()
    {
        if (tokens >= MAX_TOKENS) return; // No need to regen if at max

        // Get last token update time
        string lastTimeStr = PlayerPrefs.GetString("lastTokenTime", "");
        if (string.IsNullOrEmpty(lastTimeStr)) return; // No previous record

        DateTime lastTime = DateTime.Parse(lastTimeStr);
        TimeSpan timePassed = DateTime.UtcNow - lastTime;

        int tokensToRegen = (int)(timePassed.TotalHours / TOKEN_REGEN_TIME_HOURS);

        if (tokensToRegen > 0)
        {
            tokens = Mathf.Min(tokens + tokensToRegen, MAX_TOKENS); // Cap at 5
            SaveTokens(); // Save updated token count
        }
    }

    public bool CanStartRoulette()
    {
        return tokens >= MAX_TOKENS;
    }

    public void SpendTokens()
    {
        if (tokens >= MAX_TOKENS)
        {
            tokens -= MAX_TOKENS; // Use 5 tokens
            SaveTokens();
        }
        else
        {
            Debug.Log("Not enough tokens!");
        }
    }
}
