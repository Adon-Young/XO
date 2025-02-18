using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RouletteButton : MonoBehaviour
{
    public Button  rouletteButton;
    private bool canBePressed;
    private TokenSystem tokenSystemReference;
    public GameObject rouletteWheelPage;
    public TMP_Text cantSpinText;
    private Color canPressColour = new Color(1,1,1,1);
    private Color cantPressColour = new Color(1,1,1,0.5f);
    public GameObject SpritePage;
    private void Start()
    {
        rouletteButton = GetComponent<Button>();//getting the button component of this game object

        canBePressed = false;
        tokenSystemReference = FindAnyObjectByType<TokenSystem>();//Find the reference to tokensystem script
        rouletteWheelPage.SetActive(false);//default off
        rouletteButton.onClick.AddListener(OnButtonClick);//button click listener



    }


    private void Update()
    {
        if(tokenSystemReference.tokens >= 5)
        {
            //the player can press the button to spend tokens/ open the roulette wheel page
            rouletteButton.interactable = true;
            cantSpinText.text = "";
            rouletteButton.GetComponent<Image>().color = canPressColour;
            SpritePage.SetActive(false);//turn it off in the background
        }
        else
        {
            //any other condition the player cant  and the button will be pressed and present text instead...
            //rouletteButton.interactable = false;//keep false
            cantSpinText.text = "Not enough tokens!";
            rouletteButton.GetComponent<Image>().color = cantPressColour;
        }

    }


    private void OnButtonClick()
    {
        if (tokenSystemReference.tokens >= 5)
        {
            // Spend 5 tokens when the button is pressed and player has enough tokens
            tokenSystemReference.SpendTokens();//call the function to spend the 5 tokens and resetting recharge timer


            rouletteWheelPage.SetActive(true);//settng the roulette wheel page to true

            
            rouletteButton.interactable = false;
        }
    }
}






   


