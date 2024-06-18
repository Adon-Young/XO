using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchDetection : MonoBehaviour
{





    //private void OnMouseUp()
     // {

    //        GameObject.Find("Game Manager").SendMessage("SquareClicked", gameObject);//this sends a message to the game manager script telling that script that a certain square has been selected
    //        Destroy(this);//destroys the script preventing the player from selecting the same square more than once during the game 
    //        gameObject.GetComponent<SquareSelected>().enabled = false;
    


     //}//end of mouse up function


    

     void Update()
    {

        // if (Input.touchCount > 0)//number of fingers/touches on the screen
        {

        if (Input.touches[0].phase == TouchPhase.Began)//the touches array, allowing me to access the first touch on the screen. when the touch first begins
        //this happens when the touch is happening on the first frame of the screen.
        {
         Ray ray = Camera.main.ScreenPointToRay(Input.touches[0].position);//this find the screen point position for our ray

         RaycastHit hit;//this raycast hit variable



         if (Physics.Raycast(ray, out hit))//if the physicls of the raycast shoots out the position and direction chosen from above then itll call its function
         {
           if (hit.collider != null)//if it hit its collider then itll run the function below
           {


             //blahblahlbah
        
          }



         }

        //i am converting the screen touches raycast from the screen position to the world position.
        //i am using the raycast from the finger touch to detect which game object e.g game square it comes into contact with.
         }

         }





    }
}


