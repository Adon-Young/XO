using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class StartGame : MonoBehaviour
{
   

    public GameObject Sq1;
    public GameObject Sq2;
    public GameObject Sq3;
    public GameObject Sq4;
    public GameObject Sq5;
    public GameObject Sq6;
    public GameObject Sq7;
    public GameObject Sq8;
    public GameObject Sq9;

    


    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
       
            int waitforturn = 1;


            while (waitforturn == 1)
            {


                int RandomNumber = Random.Range(1, 9);




                if (RandomNumber == 1)
                {
                    Debug.Log("number is (1)" + RandomNumber);
                    GameObject.Find("Game Manager").SendMessage("SquareClicked", Sq1);//this sends a message to the game manager script telling that script that a certain square has been selected
                    Destroy(Sq1.GetComponent<ClickableSquares>());
                    Sq1.GetComponent<SquareSelected>().enabled = false;
                    waitforturn = 0;
                    if (RandomNumber == 1 && Sq1.GetComponent<SquareSelected>().enabled == false)
                    {

                    }



                }

                if (RandomNumber == 2)
                {
                    Debug.Log("number is (2)" + RandomNumber);
                    GameObject.Find("Game Manager").SendMessage("SquareClicked", Sq2);//this sends a message to the game manager script telling that script that a certain square has been selected
                    Destroy(Sq2.GetComponent<ClickableSquares>());
                    Sq2.GetComponent<SquareSelected>().enabled = false;
                    waitforturn = 0;
                    if (RandomNumber == 2 && Sq2.GetComponent<SquareSelected>().enabled == false)
                    {

                    }
                }

                if (RandomNumber == 3)
                {
                    Debug.Log("number is (3)" + RandomNumber);
                    GameObject.Find("Game Manager").SendMessage("SquareClicked", Sq3);//this sends a message to the game manager script telling that script that a certain square has been selected
                    Destroy(Sq3.GetComponent<ClickableSquares>());
                    Sq3.GetComponent<SquareSelected>().enabled = true;
                    waitforturn = 0;
                    if (RandomNumber == 3 && Sq3.GetComponent<SquareSelected>().enabled == false)
                    {

                    }
                }

                if (RandomNumber == 4)
                {
                    Debug.Log("number is (4)" + RandomNumber);
                    GameObject.Find("Game Manager").SendMessage("SquareClicked", Sq4);//this sends a message to the game manager script telling that script that a certain square has been selected
                    Destroy(Sq4.GetComponent<ClickableSquares>());
                    Sq4.GetComponent<SquareSelected>().enabled = false;
                    waitforturn = 0;
                    if (RandomNumber == 4 && Sq4.GetComponent<SquareSelected>().enabled == false)
                    {

                    }

                }

                if (RandomNumber == 5)
                {
                    Debug.Log("number is (5)" + RandomNumber);
                    GameObject.Find("Game Manager").SendMessage("SquareClicked", Sq5);//this sends a message to the game manager script telling that script that a certain square has been selected
                    Destroy(Sq5.GetComponent<ClickableSquares>());
                    Sq5.GetComponent<SquareSelected>().enabled = false;
                    waitforturn = 0;
                    if (RandomNumber == 5 && Sq5.GetComponent<SquareSelected>().enabled == false)
                    {

                    }

                }

                if (RandomNumber == 6)
                {
                    Debug.Log("number is (6)" + RandomNumber);
                    GameObject.Find("Game Manager").SendMessage("SquareClicked", Sq6);//this sends a message to the game manager script telling that script that a certain square has been selected
                    Destroy(Sq6.GetComponent<ClickableSquares>());
                    Sq6.GetComponent<SquareSelected>().enabled = true;
                    waitforturn = 0;
                    if (RandomNumber == 6 && Sq6.GetComponent<SquareSelected>().enabled == false)
                    {

                    }

                }

                if (RandomNumber == 7)
                {
                    Debug.Log("number is (7)" + RandomNumber);
                    GameObject.Find("Game Manager").SendMessage("SquareClicked", Sq7);//this sends a message to the game manager script telling that script that a certain square has been selected
                    Destroy(Sq7.GetComponent<ClickableSquares>());
                    Sq7.GetComponent<SquareSelected>().enabled = false;
                    waitforturn = 0;
                    if (RandomNumber == 7 && Sq7.GetComponent<SquareSelected>().enabled == false)
                    {

                    }

                }

                if (RandomNumber == 8)
                {
                    Debug.Log("number is (8)" + RandomNumber);
                    GameObject.Find("Game Manager").SendMessage("SquareClicked", Sq8);//this sends a message to the game manager script telling that script that a certain square has been selected
                    Destroy(Sq8.GetComponent<ClickableSquares>());
                    Sq8.GetComponent<SquareSelected>().enabled = false;
                    waitforturn = 0;
                    if (RandomNumber == 8 && Sq8.GetComponent<SquareSelected>().enabled == false)
                    {

                    }

                }

                if (RandomNumber == 9)
                {
                    Debug.Log("number is (9)" + RandomNumber);
                    GameObject.Find("Game Manager").SendMessage("SquareClicked", Sq9);//this sends a message to the game manager script telling that script that a certain square has been selected
                    Destroy(Sq9.GetComponent<ClickableSquares>());
                    Sq9.GetComponent<SquareSelected>().enabled = false;
                    waitforturn = 0;
                    if (RandomNumber == 9 && Sq9.GetComponent<SquareSelected>().enabled == false)
                    {

                    }

                }



            }
       



    }



}//end of class
