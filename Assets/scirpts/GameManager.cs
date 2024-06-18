using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{



    public GameObject Circle;//o sprites game object reference
    public GameObject Cross;//x sprite game object reference
    public GameObject Win;
    public GameObject Lose;
    public GameObject Draw;
    public GameObject CrossParticles;
    public GameObject CircleParticles;
    
   



    int turn = 1;//this variable is used for the turns 1 = o and 2 = x's turn.
    int[] squares = new int[10];//multiple intigers within the same variable "squares" because there is a total of 9 squares on the board
    //there will be 9 numbers for the script to collect in order to figure out who has won the game

    public int winner = 0;
    public int numberOfClicks = 0;//this integer is for the number of times a square is clicked, so if all squares have been clicked and theres no winner by the end of the game we can set it to a draw.


    public void Start()
    {
        Win.SetActive(false);
        Lose.SetActive(false);
        Draw.SetActive(false);

      

    }


    public void SquareClicked(GameObject square)
    {
        //get the square number

        int squareNumber = square.GetComponent<ClickableSquares>().squareNumber;
        //this line of code is used to take the int number from the ClickableSquare script after the player clicks on a square and takes that squares number value and puts it into the new squareNumber variable in this script. basically transferring the number from one script to another
        //so that this script knows what box the player has selected in the game
        numberOfClicks += 1;//adds on 1 every time the square is clicked
        SpawnPlayersPrefab(square.transform.position);//this refers to the function below (using the position of the square to spawn the token)
        squares[squareNumber] = turn;//this assigns each square clicked in the game with either 1 or 2 so that we know if its the player or the ai who took the turn


        CalculateWinner();

        NextTurn();//calling the next turn function

        DisplayWinLoseScreen();




    }//end of public void function







    //SPAWN TOKEN//


    void SpawnPlayersPrefab(Vector3 tokenPosition)//this function is used to spawn the players token where they pressed the square box
                                                  //its going to spawn the token at the location of this variable "token position" which will hold the vale given when the player selects one of the numbered squares

    {
        tokenPosition.z = -1;//to make sure the tokens spawn in fromt of the game board


        if (turn == 1)//if the turn = 1 on the int then the circle AI token is spawned in 
        {
            Instantiate(Circle, tokenPosition, Quaternion.identity);
            Instantiate(CircleParticles, tokenPosition, Quaternion.identity);
            //gameObject.GetComponent<AudioSource>().Play();
        }

        else if (turn == 2)//if the turn = 2 on the int then the players token the cross will be spawned
        {
            Instantiate(Cross, tokenPosition, Quaternion.identity);
            Instantiate(CrossParticles, tokenPosition, Quaternion.identity);
            //gameObject.GetComponent<AudioSource>().Play();
        }


    }//end of function







    void NextTurn()
    {
        //this function allows the AI to go first and then adds 1 to the int, once the int goes to 2 then its the players turn
        //once the players turn is over the int will go to 3 reverting it back to 1 making it the AIs turn again and so on...
        turn += 1;

        if (turn == 3)
        {
            turn = 1;//setting the turn int back to 1




        }


     



    }//end of function







    void CalculateWinner()//this is the interestin part where the game has to figure out who has won, in order to do this the game will
                          //use if statments to see which squares are selected in a line of 3. for example if player 1 selects box 1,2 and 3 then they win...

    {
        //if box 1 2 and 3 are selected by the player or ai with value of 1 on their turn then they win the game


        for (int player = 1; player <= 2; player++)//this for loop is used to change the player variable switching between the ai and the player
                                                   //so instead of having just the ai or player being able to win it swaps in the correct player and shows that in the consol...
        {
            //winning in rows

            if (squares[1] == player && squares[2] == player && squares[3] == player)//if the player or AI get a combo of 123 then they win the game
            {
                EndOfGame();
                Debug.Log(player + "Wins!");
                winner = player;//whichever player scores the combo is classes as the winner
            }

            else if (squares[4] == player && squares[5] == player && squares[6] == player)//if the player or AI get a combo of 456 then they win the game
            {
                EndOfGame();
                Debug.Log(player + "Wins!");
                winner = player;//whichever player scores the combo is classes as the winner
            }

            else if (squares[7] == player && squares[8] == player && squares[9] == player)//if the player or AI get a combo of 789 then they win the game
            {
                EndOfGame();
                Debug.Log(player + "Wins!");
                winner = player;//whichever player scores the combo is classes as the winner
            }

            //winnning in columns

            else if (squares[1] == player && squares[4] == player && squares[7] == player)//if the player or AI get a combo of 147 then they win the game
            {
                EndOfGame();
                Debug.Log(player + "Wins!");
                winner = player;//whichever player scores the combo is classes as the winner
            }

            else if (squares[2] == player && squares[5] == player && squares[8] == player)//if the player or AI get a combo of 258 then they win the game
            {
                EndOfGame();
                Debug.Log(player + "Wins!");
                winner = player;//whichever player scores the combo is classes as the winner
            }

            else if (squares[3] == player && squares[6] == player && squares[9] == player)//if the player or AI get a combo of 369 then they win the game
            {
                EndOfGame();
                Debug.Log(player + "Wins!");
                winner = player;//whichever player scores the combo is classes as the winner
            }



            //winning diagonally

            else if (squares[1] == player && squares[5] == player && squares[9] == player)//if the player or AI get a combo of 159 then they win the game
            {
                EndOfGame();
                Debug.Log(player + "Wins!");
                winner = player;//whichever player scores the combo is classes as the winner
            }

            else if (squares[3] == player && squares[5] == player && squares[7] == player)//if the player or AI get a combo of 357 then they win the game
            {
                EndOfGame();
                Debug.Log(player + "Wins!");
                winner = player;//whichever player scores the combo is classes as the winner
            }

            if (numberOfClicks == 9 && winner == 0)//if the number of clicks reaches 9 and there is no winner then the game ends in a draw
            {
                winner = 3;//winner is set to 3 which is a draw
            }

        }//end of for loop

    }//end of function








    void EndOfGame()
    {
        //at the end of the game this will destroy the clickable squares preventing the playert to keep playing the game after they win/lose

        foreach (ClickableSquares square in GameObject.FindObjectsOfType<ClickableSquares>())
        {
            Destroy(square);//this destroys the squares on each game object speventing the game objects from being clickable by the player
        }

    }//end of end game function








    void DisplayWinLoseScreen()//this will display the win screen or the lose screen depending on who won, the player or the AI

    {

        if (winner == 1)//if the AI "O" is the winner then the player loses the game displaying the lose popup
        {
            Win.SetActive(false);
            Lose.SetActive(true);
            Draw.SetActive(false);

        }

        else if (winner == 2)//if the winner is player 2 eg "X" then they win the game, displaying the winner pop up
        {
            Win.SetActive(true);
            Lose.SetActive(false);
            Draw.SetActive(false);

        }

        else if (winner == 3)//if there is no winner then the game is a draw and the draw pop up is displayed
        {
            Win.SetActive(false);
            Lose.SetActive(false);
            Draw.SetActive(true);

        }




    }//end of DisplayWinLoseScreen function






}//end of class

