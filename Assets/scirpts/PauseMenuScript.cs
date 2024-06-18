using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuScript : MonoBehaviour
{

    public GameObject gameCubes;


    public void Start()
    {
        gameCubes.SetActive (true);
    }




    public  void PauseGame()//this is the pause game code
    {

        
        Time.timeScale = 0f;//this changes the game time to 0 setitng it at a standstill. we can even slow down time by editing this code.
        gameCubes.SetActive(false);



    }//end of pause code

   public void UnPauseGame()
    {
       
        Time.timeScale = 1f;//this changes the game time to 0 setitng it at a standstill. we can even slow down time by editing this code.
        gameCubes.SetActive(true);
    }


}//end of class
