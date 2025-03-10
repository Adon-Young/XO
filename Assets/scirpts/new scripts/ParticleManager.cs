using System.Collections;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    // Public variable to determine if the particle is X or O
    public bool isXParticle; // If true, it will be 'X' particle, if false, it will be 'O'

    // Reference to the particle system
    private ParticleSystem particleSystem;

    // Reference to the main module of the particle system
    private ParticleSystem.MainModule mainModule;

    void Start()
    {
        // Get the ParticleSystem component attached to the particle
        particleSystem = GetComponent<ParticleSystem>();

        // Access the MainModule to change start properties
        mainModule = particleSystem.main;

        // Load the colors from PlayerPrefs
        Color xParticleColor = LoadColorFromPrefs("XParticleColor", Color.blue); // Default to red if not set
        Color oParticleColor = LoadColorFromPrefs("OParticleColor", Color.blue); // Default to blue if not set

        // Set the particle's start color based on the isXParticle flag
        if (isXParticle)
        {
            mainModule.startColor = xParticleColor;
        }
        else
        {
            mainModule.startColor = oParticleColor;
        }

        // Start the destroy timer
        StartCoroutine(WaitAndDestroy());
    }

    // Coroutine to destroy the particle after a delay
    IEnumerator WaitAndDestroy()
    {
        yield return new WaitForSeconds(2);
        Destroy(this.gameObject); // Destroy the particle after 2 seconds
    }

    // Method to load a color from PlayerPrefs
    private Color LoadColorFromPrefs(string key, Color defaultColor)
    {
        if (PlayerPrefs.HasKey(key + "_R"))
        {
            return new Color(
                PlayerPrefs.GetFloat(key + "_R"),
                PlayerPrefs.GetFloat(key + "_G"),
                PlayerPrefs.GetFloat(key + "_B"),
                PlayerPrefs.GetFloat(key + "_A")
            );
        }
        else
        {
            // If no color is saved, return the default color
            return defaultColor;
        }
    }
}
