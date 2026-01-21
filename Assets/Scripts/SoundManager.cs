// Sound Manager Script

using System;
using UnityEngine;

//

public class SoundManager : MonoBehaviour
{
    
    // Singleton 

    private static SoundManager instance;
    
    // Game Variables

    [Header("AudioSources")]
    [SerializeField] private AudioSource[] audioSources;
    [SerializeField] private AudioSource[] jumpSources;
    [SerializeField] private AudioSource[] chargeSources;
    [SerializeField] private AudioSource[] deathSources;
    [Space(10f)]
    
    [Header("Music")]
    [SerializeField] private AudioSource mainMusic;
    
    //
    
    // Base Functions 

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }
    
    //

    private void Start()
    {
        mainMusic.Play();
    }

    //
    
    // Sound Functions

    public void JumpSound(int playerNumber)
    {
        jumpSources[playerNumber].Play();
    }
    
    //

    public void ChargeSound(int playerNumber)
    {
        chargeSources[playerNumber].Play();
    }
    
    //
    
    public void DeathSound(int playerNumber)
    {
        deathSources[playerNumber].Play();
    }
    
    //
    
    public void PushedSound()
    {
        audioSources[0].Play();
    }
    
    //

    public void StartingSound()
    {
        audioSources[0].Play();
    }
    
    //

    public void ScoreSound()
    {
        audioSources[0].Play();
    }
    
    //

    public void PlacementSound()
    {
        audioSources[0].Play();
    }
    
    // 

    public void ExplosionSound()
    {
        audioSources[0].Play();
    }
    
    //

    public void SpringSound()
    {
        audioSources[0].Play();
    }
    
    //

    public void GlassBlock()
    {
        audioSources[0].Play();
    }
    
}

// END //
