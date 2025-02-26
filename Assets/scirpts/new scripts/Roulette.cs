using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections.Generic;


public class Roulette : MonoBehaviour
{
    public List<GameObject> skinLocks = new List<GameObject>();
    //list of game object i.e. the locks on the skins that need to be removed...
    //can add publically from 1->15
    //disable lock ui over buttons
    public int[] valuePerItem;
    int randomValue = Random.Range(1,101);//random range between 1->100 or should it be 0->101
    

    /*
     weights per item for each of the 15 items
    1-
    2-
    3-
    4-
    ------------------------
    5-
    6-
    7-
    8-
    ------------------------
    9-
    10-
    11-
    12-
    ------------------------
    13-
    14-
    15-
    (16->already unlocked i.e. default)
     */

    public void Start()
    {

    }
    
    
    
    
    //roulette button. this script is attached to the button that takes the player to te roulette wheel
    public void RouletteButton()
    {
      



    }







}
