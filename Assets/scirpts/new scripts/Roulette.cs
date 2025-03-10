using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
//this class contains the insofmration that the button game object hold ie the weight of the skins i.e. rarity and the reference to the object itself
public class LootItem
{
    public GameObject itemObject; // The GameObject representing the loot
    public float weightValue; // The weight (rarity) of this item
}

public class Roulette : MonoBehaviour
{
    public List<LootItem> lootItemList;
    //list containing all skin buttons.

    //making it a public function so i can add it to the button in game
    public void PickASkinButton()
    {
        GameObject chosenItem = PickRandomLootItem(); //calculates the skin chosen
        Debug.Log("Roulette Button Was Pressed");
        if (chosenItem != null)
        {
            DisableUIImageIfNeeded(chosenItem);
            //if the object has been chosen it removed the lock ui allowin players to press the button.
        }
    }


    //not a public void as it returns a gameobject and thats what populates the chosenItem variable above...
    GameObject PickRandomLootItem()
    {
        float totalWeightInObjectList = 0f;
        Debug.Log("Picking AnItem From The List");
        /*the total sum of all weights can be hard coded but keeping it dynamic by calculatin all weights allows me to add and
         * remove buttons from the list and still keep the correct/accurate chance of picking each rarity item*/ 
        foreach (var lootItem in lootItemList)
        {
            totalWeightInObjectList += lootItem.weightValue;
        }

        
        float randomWeightPick = Random.Range(0f, totalWeightInObjectList);
        float cumulativeWeight = 0f;

        //looping through the list of items to select the correct skin based on the weights and cumulative weights...
        foreach (var lootItem in lootItemList)
        {
            cumulativeWeight += lootItem.weightValue;

            if (randomWeightPick <= cumulativeWeight)
            {
                return lootItem.itemObject; //returning the item from the list of skins
            }
        }

        return null; //ie if nothing in list even if all skins unlocked this wont happen as all buttons will still be part of the list
    }

    void DisableUIImageIfNeeded(GameObject obj)
    {
        Debug.Log("UI disabled & ButtonComponent Enabled");

        // Ensure the object has at least 3 children
        if (obj.transform.childCount >= 2)
        {
            // Get the 3rd child's Image component
            Transform thirdChild = obj.transform.GetChild(1); // Index 2 = 3rd child

            if (thirdChild != null)
            {
                thirdChild.gameObject.SetActive(false);//setting the entire obj to false not jus image component. allowint button pressing
            }
        }
        else
        {
            Debug.LogWarning("Not enough child objects in " + obj.name);
        }

        // Enable the button on the parent
        Button buttonComponent = obj.GetComponent<Button>();
        if (buttonComponent != null && buttonComponent.interactable == false)
        {
            buttonComponent.interactable = true;
        }
    }

}
