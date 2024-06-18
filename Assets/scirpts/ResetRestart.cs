using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetRestart : MonoBehaviour
{

public void ResetGameScreen()
    {

        Debug.Log("Game should go back to MM and reset the game screen");

        SceneManager.LoadScene("SampleScene");




    }//end of function/button

    public void Restart()
    {

        Debug.Log("restart the scene");
        SceneManager.LoadScene("PVP");



    }//end of restart function



    public void PlayGame()
    {
        SceneManager.LoadScene("PVP");
        Debug.Log("In Game Against Friend");

    }//end of play game function


    public void PlayAgainstAI()
    {
        SceneManager.LoadScene("PVAI");
        Debug.Log("In Game Against AI");
    }




}//end of class
