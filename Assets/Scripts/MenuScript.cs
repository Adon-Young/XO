using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{

    
    public void Exit()//this is the function to close the game/ tell us its closed.
    {
        StartCoroutine(WaitForEnd());
        
    

    }//end of function

    IEnumerator WaitForEnd()//this coroutine allows the player to press the exit button and itll wait 1 second before closing down the app, this prevents the button audio from being cut off
    {
        Debug.Log("Timer Started");
        yield return  new WaitForSeconds(1f);
        Debug.Log("Timer Ended");
        Hold();
        
    }


    public void Hold()
    {
        Debug.Log("ExitGame");//an indication to tell us if the game will close when the button is pressed while in unity
        Application.Quit();//this closes the application down
    }
   
}//end of class
