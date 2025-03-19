using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickableSquares : MonoBehaviour
{
    //public intiger called square number
    public int squareNumber = 1;
  
  

    private void OnMouseDown()
    {

        gameObject.GetComponent<SpriteRenderer>().color = Color.cyan;
        gameObject.GetComponent<AudioSource>().Play();//plays the audio



    }//end of mouse up function


    private void OnMouseUp()
     {

            GameObject.Find("Game Manager").SendMessage("SquareClicked", gameObject);//this sends a message to the game manager script telling that script that a certain square has been selected
            Destroy(this);//destroys the script preventing the player from selecting the same square more than once during the game 
            gameObject.GetComponent<SquareSelected>().enabled = false;
            gameObject.GetComponent<SpriteRenderer>().color = Color.white;
            gameObject.GetComponent<AudioSource>().Pause();//pauses the audio

    }//end of mouse up function


  
   

    
}//end of class


