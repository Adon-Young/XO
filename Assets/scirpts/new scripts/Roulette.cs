//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;

//public class Roulette : MonoBehaviour
//{
//    public GameObject rouletteScreen;  // The roulette UI panel
//    public List<GameObject> skinButtons; // The UI buttons representing skins
//    public Text resultText;            // Display the result (for debugging)

//    private int preDeterminedSkin = -1; // The skin that will be unlocked
//    private List<int> unlockedSkins = new List<int>();

//    void Start()
//    {
//        LoadUnlockedSkins();
//        UpdateUI();
//    }

//    void LoadUnlockedSkins()
//    {
//        string unlockedData = PlayerPrefs.GetString("unlockedSkins", "");
//        if (!string.IsNullOrEmpty(unlockedData))
//        {
//            unlockedSkins = new List<int>(System.Array.ConvertAll(unlockedData.Split(','), int.Parse));
//        }
//    }

//    void SaveUnlockedSkins()
//    {
//        PlayerPrefs.SetString("unlockedSkins", string.Join(",", unlockedSkins));
//        PlayerPrefs.Save();
//    }

//    public void SpendTokenAndOpenRoulette()
//    {
//        // Check if the player has enough tokens (assume TokenSystem is managing this)
//        TokenSystem tokenSystem = FindObjectOfType<TokenSystem>();
//        if (tokenSystem != null && tokenSystem.CanSpendToken())
//        {
//            tokenSystem.SpendTokens();
//            preDeterminedSkin = DetermineSkin();  // Choose skin before opening roulette
//            OpenRouletteScreen();
//        }
//        else
//        {
//            Debug.Log("Not enough tokens!");
//        }
//    }

//    int DetermineSkin()
//    {
//        List<int> availableSkins = new List<int>();

//        // Find locked skins
//        for (int i = 0; i < skinButtons.Count; i++)
//        {
//            if (!unlockedSkins.Contains(i))
//            {
//                availableSkins.Add(i);
//            }
//        }

//        if (availableSkins.Count > 0)
//        {
//            // Unlock a new skin
//            int chosenSkin = availableSkins[Random.Range(0, availableSkins.Count)];
//            unlockedSkins.Add(chosenSkin);
//            SaveUnlockedSkins();
//            return chosenSkin;
//        }
//        else
//        {
//            // All skins unlocked → give duplicate
//            return Random.Range(0, skinButtons.Count);
//        }
//    }

//    void OpenRouletteScreen()
//    {
//        rouletteScreen.SetActive(true);
//        ShowResult();
//        UpdateUI();
//    }

//    void ShowResult()
//    {
//        if (resultText != null)
//        {
//            string resultMessage = unlockedSkins.Contains(preDeterminedSkin)
//                ? $"You unlocked: Skin {preDeterminedSkin}!"
//                : "Duplicate Skin!";
//            resultText.text = resultMessage;
//        }
//    }

//    void UpdateUI()
//    {
//        for (int i = 0; i < skinButtons.Count; i++)
//        {
//            bool isUnlocked = unlockedSkins.Contains(i);
//            skinButtons[i].GetComponent<Button>().interactable = isUnlocked;
//            skinButtons[i].transform.Find("LockImage").gameObject.SetActive(!isUnlocked);
//        }
//    }
//}
