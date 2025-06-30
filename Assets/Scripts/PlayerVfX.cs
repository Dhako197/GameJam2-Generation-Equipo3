using UnityEngine;

public class PlayerVfX : MonoBehaviour
{
    private ParticleSystem particleSystem;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        particleSystem = GetComponentInChildren<ParticleSystem>();
    }

    public void PlayParticles()
    {
        particleSystem.Stop();
        particleSystem.Play();
    }
}
