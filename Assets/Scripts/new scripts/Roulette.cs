using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class LootItem
{
    public GameObject itemObject;
    public float weightValue;
}

public class Roulette : MonoBehaviour
{
    public List<LootItem> lootItemList;

    private void Start()
    {
        LoadUnlockedSkins();
    }

    public void PickASkinButton()
    {
        GameObject chosenItem = PickRandomLootItem();
        Debug.Log("Roulette Button Was Pressed");

        if (chosenItem != null)
        {
            // Check if the item is already unlocked
            if (PlayerPrefs.GetInt("Unlocked_" + chosenItem.name, 0) == 1)
            {
                HandleDuplicatePick(chosenItem);
                return; // Stop further execution to prevent re-unlocking
            }

            DisableUIImageIfNeeded(chosenItem);

            // Save unlocked state
            PlayerPrefs.SetInt("Unlocked_" + chosenItem.name, 1);
            PlayerPrefs.Save();
            Debug.LogWarning("Skin Unlocked: " + chosenItem.name + " is now unlocked!");
        }
    }

    void HandleDuplicatePick(GameObject obj)
    {
        Debug.LogWarning("Duplicate Pick: " + obj.name + " has already been unlocked!");
    }

    GameObject PickRandomLootItem()
    {
        float totalWeightInObjectList = 0f;
        Debug.Log("Picking An Item From The List");

        foreach (var lootItem in lootItemList)
        {
            totalWeightInObjectList += lootItem.weightValue;
        }

        float randomWeightPick = Random.Range(0f, totalWeightInObjectList);
        float cumulativeWeight = 0f;

        foreach (var lootItem in lootItemList)
        {
            cumulativeWeight += lootItem.weightValue;

            if (randomWeightPick <= cumulativeWeight)
            {
                return lootItem.itemObject;
            }
        }

        return null;
    }

    void DisableUIImageIfNeeded(GameObject obj)//if already selected does not remove lock ui as player already unlocked that cosmetic-upgrades can be done here to display to the player
    {
        Debug.Log("UI disabled & ButtonComponent Enabled");

        if (obj.transform.childCount >= 2)
        {
            Transform thirdChild = obj.transform.GetChild(4);

            if (thirdChild != null)
            {
                thirdChild.gameObject.SetActive(false);
            }
        }
        else
        {
            Debug.LogWarning("Not enough child objects in " + obj.name);
        }

        Button buttonComponent = obj.GetComponent<Button>();
        if (buttonComponent != null && buttonComponent.interactable == false)
        {
            buttonComponent.interactable = true;
        }
    }

    private void LoadUnlockedSkins()//make sure to keep the unlocked skins unlocked when players return to the page
    {
        foreach (var lootItem in lootItemList)
        {
            if (PlayerPrefs.GetInt("Unlocked_" + lootItem.itemObject.name, 0) == 1)
            {
                DisableUIImageIfNeeded(lootItem.itemObject);
            }
        }
    }
}
