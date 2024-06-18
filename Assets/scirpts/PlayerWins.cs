using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerWins : MonoBehaviour
{
    public Image WinScreen;//win screen game variable reference

    public void Start()
    {
        WinScreen.enabled = false;
    }


    public void Update()
    {

        if (WinScreen.enabled == true)
        {
            LevelBar.WinLose = 1;
            StartCoroutine(EndOfProgression());


        }
        else
        {
            LevelBar.WinLose = 0;
        }


    }//end of update function

    IEnumerator EndOfProgression()//i create dthis to stop the progression bar after 0.25 seconds. if this wasnt here then the progressionof the slider would not reach its intended mark before stopping
    {
        yield return new WaitForSeconds(0.25f);
        LevelBar.WinLose = 0;//if this variable was set to 0 in the starte or update function it would stop the progression bar too early before the +10 mark
        //essentially making players have to play more matches to fill the bar, where this yield prevents the variable from changing too early or too late.
        WinScreen.enabled = false;




    }//end of yield
}//end of class
