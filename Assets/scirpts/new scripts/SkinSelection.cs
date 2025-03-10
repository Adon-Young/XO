using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkinSelection : MonoBehaviour
{
    public Color backgroundColour;
    public Color xparticleColour;
    public Color oparticleColour;

    public SpriteRenderer backgroundImageInScene1;
    private Button thisObjButton;

    private void Start()
    {
        thisObjButton = GetComponent<Button>();

        if (thisObjButton != null)
        {
            thisObjButton.onClick.AddListener(ApplyNewSkins);
        }
    }

    public void ApplyNewSkins()
    {
        // Apply this button's specific colors to the scene
        if (backgroundImageInScene1)
        {
            backgroundImageInScene1.color = backgroundColour;
        }

        // Save these specific colors to PlayerPrefs
        SaveColours("BackgroundImage", backgroundColour);
        SaveColours("XParticleColour", xparticleColour);
        SaveColours("OParticleColour", oparticleColour);
    }

    private void SaveColours(string key, Color color)
    {
        PlayerPrefs.SetFloat(key + "_R", color.r);
        PlayerPrefs.SetFloat(key + "_G", color.g);
        PlayerPrefs.SetFloat(key + "_B", color.b);
        PlayerPrefs.SetFloat(key + "_A", color.a);
        PlayerPrefs.Save();
    }
}
