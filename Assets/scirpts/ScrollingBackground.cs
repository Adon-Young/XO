
using UnityEngine;

public class ScrollingBackground : MonoBehaviour
{
[Range(-1f,1f)]//this is used to make sure that the image moves in the correct direction
//for example if the vale goes into the negative itll fix itself to scroll and continue to scroll in the correct / opposite direction.
    public float speed = 0.5f;//this is the variable that allows me to adjust the speed of the scrolling background image i made. and it is assigned a default value of 0.5
    private float offset;
    private Material Background;
    // Start is called before the first frame update
    void Start()
    {
        Background = GetComponent<Renderer>().material;
    }//end of start

    // Update is called once per frame
    void Update()
    {

        offset += (Time.deltaTime * speed);//this is calculating the speed of the image and having it follow the offset of the cameras view.
        Background.SetTextureOffset("_MainTex", new Vector2(0, offset));//this is the direction that our background image will be moving and repeating, it will move on the y axit upwards
        //rather than on the x axis horizontally.

    }//end of update
}//end of class
