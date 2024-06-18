 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;//using the game engines UI system

public class LevelBar : MonoBehaviour
{



    public static int WinLose = 0;
    public int PlayersLevel = 1;

    public Slider ProgressSlider;//reference to the healthbar slider
    private ParticleSystem LevelBarParticles;//reference to the particle system

    public float LevelBarSpeed = 40f;//speed the bar fills
    public float CurrentProgression = 0;//players current progression at the start of the game

    private void Awake()
    {
        ProgressSlider = gameObject.GetComponent<Slider>();//assigning the slider to the variable
        LevelBarParticles = GameObject.Find("Progress bar effects").GetComponent<ParticleSystem>();//assigning the particles to my variable
    }//end of awake function


    private void Start()
    {
       // PlayerWon();
       // WinLose = 0;
        
    }//end of start function

    private void Update()
    {

        if (ProgressSlider.value < CurrentProgression)//if the players progression on the slider is less than the players current progression then the slider will change to match it
        {
            ProgressSlider.value += LevelBarSpeed * Time.deltaTime;// the progress level bar value will multiply by the bars speed times time to show the bar progressing in real time
            
            if(!LevelBarParticles.isPlaying)
            LevelBarParticles.Play();//this will play it is it is not already playing

        }
        else
        {
            LevelBarParticles.Stop();//this should stop the particle system
        
        }//end of if else statement

        PlayerWon();
        

    }//end of update function


    public void ProgressionIncrease(float PlayersNewProgression)
    {
        CurrentProgression = ProgressSlider.value + PlayersNewProgression;//the players current progression is the same as the sliders value + the players new progression
    }//end of progression function



    public void PlayerWon()//once this int is set to 1 the game adds 10 points to the players progression bar, the Courotine is used to stop the bar from adding on points
    {
        if (WinLose >= 1)
        {
            ProgressionIncrease(+10);
            StartCoroutine(EndOfProgression());
        }

        if (WinLose <= -1)
        {
            ProgressionIncrease(+0);
        }

        else if (WinLose == 0)
        {
            
            ProgressionIncrease(+0);
            
        }//end of points if else statement


       

       
    }//end of point system function


    
    IEnumerator EndOfProgression()//i create dthis to stop the progression bar after 0.25 seconds. if this wasnt here then the progressionof the slider would not reach its intended mark before stopping
    {
        yield return new WaitForSeconds(0.25f);
        WinLose = 0;//if this variable was set to 0 in the starte or update function it would stop the progression bar too early before the +10 mark
        //essentially making players have to play more matches to fill the bar, where this yield prevents the variable from changing too early or too late.

    }//end of yield
    
    
    
   




}//end  of class
