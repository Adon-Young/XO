using UnityEngine;
using UnityEngine.UI;

public class ButtonColors : MonoBehaviour
{
    public Button[] buttonColorList; // Manually populated list of buttons to update

    private void Start()
    {
        UpdateButtonColors(); // Initial update when the scene starts
    }

    public void UpdateButtonColors()
    {
        Color backgroundColor = LoadColor("BackgroundImage", new Color(218f / 255f, 218f / 255f, 218f / 255f, 200f / 255f));
        Color xparticleColor = LoadColor("XParticleColour", new Color(156f / 255f, 156f / 255f, 156f / 255f, 255f / 255f));
        Color oparticleColor = LoadColor("OParticleColour", new Color(114f / 255f, 114f / 255f, 114f / 255f, 255f / 255f));

        foreach (Button btn in buttonColorList)
        {
            ColorBlock cb = btn.colors;
            cb.normalColor = oparticleColor; // Default color
            cb.highlightedColor = xparticleColor; // Highlighted color
            cb.disabledColor = backgroundColor; // Disabled color
            cb.selectedColor = xparticleColor;
            btn.colors = cb;
        }
    }

    // Helper function to load a color from PlayerPrefs
    private Color LoadColor(string key, Color defaultColor)
    {
        float r = PlayerPrefs.GetFloat(key + "_R", defaultColor.r);
        float g = PlayerPrefs.GetFloat(key + "_G", defaultColor.g);
        float b = PlayerPrefs.GetFloat(key + "_B", defaultColor.b);
        float a = PlayerPrefs.GetFloat(key + "_A", defaultColor.a);
        return new Color(r, g, b, a);
    }
}




