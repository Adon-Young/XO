using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    public string colorKey; // "XParticleColour" or "OParticleColour"
    private ParticleSystem particleSystem;

    void Start()
    {
        particleSystem = GetComponent<ParticleSystem>();

        if (string.IsNullOrEmpty(colorKey))
        {
            Debug.LogError("Color key not set on " + gameObject.name);
            return;
        }

        // Load and apply particle color
        Color particleColor = Scene2Manager.LoadColorFromPrefs(colorKey, Color.white);
        UpdateParticleColor(particleColor);
    }

    private void UpdateParticleColor(Color newColor)
    {
        if (particleSystem != null)
        {
            var main = particleSystem.main;
            main.startColor = newColor;
        }
        else
        {
            Debug.LogError("Particle system not found on " + gameObject.name);
        }
    }
}


