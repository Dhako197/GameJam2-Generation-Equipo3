using UnityEngine;

public class PlayerVfX : MonoBehaviour
{
    public ParticleSystem particleSystem;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       
    }

    public void PlayParticles()
    {
        particleSystem.Stop();
        particleSystem.Play();
    }
}
