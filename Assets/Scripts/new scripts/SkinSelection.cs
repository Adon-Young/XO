using UnityEngine;
using UnityEngine.UI;

public class SkinSelection : MonoBehaviour
{
    public Color backgroundColour;
    public Color xparticleColour;
    public Color oparticleColour;

    public SpriteRenderer backgroundImageInScene1;
    private Button thisObjButton;
    private ButtonColors buttonColorManager; // Reference to ButtonColorManager
    public Image[] Blockers;

    private void Start()
    {
        thisObjButton = GetComponent<Button>();

        // Find ButtonColorManager in the scene
        buttonColorManager = FindObjectOfType<ButtonColors>();

        if (thisObjButton != null)
        {
            thisObjButton.onClick.AddListener(ApplyNewSkins);
        }
    }

    public void ApplyNewSkins()
    {
        // Apply the new background color immediately
        if (backgroundImageInScene1)
        {
            backgroundImageInScene1.color = backgroundColour;
          
            foreach (Image blocker in Blockers)
            {
                blocker.color = backgroundColour;
            }

        }

        // Save these specific colors to PlayerPrefs
        SaveColours("BackgroundImage", backgroundColour);
        SaveColours("XParticleColour", xparticleColour);
        SaveColours("OParticleColour", oparticleColour);

        // Call ButtonColorManager to update all buttons in the list
        if (buttonColorManager != null)
        {
            buttonColorManager.UpdateButtonColors();
        }
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

