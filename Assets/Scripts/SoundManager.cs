// Sound Manager Script

using System;
using UnityEngine;
using Random = UnityEngine.Random;

//

public class SoundManager : MonoBehaviour
{
    
    // Singleton 

    private static SoundManager instance;
    
    // Game Variables
    
    [Header("Jump Sources")]
    [SerializeField] private AudioClip[] p1JumpSounds;
    [SerializeField] private AudioClip[] p2JumpSounds;
    [SerializeField] private AudioClip[] p3JumpSounds;
    [SerializeField] private AudioClip[] p4JumpSounds;
    [Space(10f)]
    
    //
    
    [Header("Charge Sources")]
    [SerializeField] private AudioClip[] chargeSounds;
    [Space(10f)]
    
    //
    
    [Header("Death Sources")]
    [SerializeField] private AudioClip[] p1DeathSounds;
    [SerializeField] private AudioClip[] p2DeathSounds;
    [SerializeField] private AudioClip[] p3DeathSounds;
    [SerializeField] private AudioClip[] p4DeathSounds;
    [Space(10f)]
    
    //
    
    [Header("Start Sources")]
    [SerializeField] private AudioClip[] startSounds;
    [Space(10f)]
    
    //
    
    [Header("Score Sources")]
    [SerializeField] private AudioClip scoreSound;
    [Space(10f)]
    
    //

    [Header("Bloc Placement Sources")] 
    [SerializeField] private AudioClip[] placementSounds;
    [Space(10f)]
    
    //
    
    [Header("Spring Sources")] 
    [SerializeField] private AudioClip[] springSounds;
    [Space(10f)]
    
    //
    
    [Header("Explosion Sources")] 
    [SerializeField] private AudioClip[] explosionSounds;
    [Space(10f)]
    
    //
    
    [Header("Ice Sources")] 
    [SerializeField] private AudioClip glassSound;
    [Space(10f)]
    
    //
    
    [Header("Music")]
    [SerializeField] private AudioClip mainMusic;
    [Space(10f)]
    
    //
    
    [Header("Audio Sources")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource soundSource;
    
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
        if (mainMusic == null) 
            return;
        musicSource.clip = mainMusic;
        musicSource.Play();
    }

    //
    
    // Sound Functions
    
    private AudioSource FindEmptyAudioSource()
    {
        if (!soundSource.isPlaying)
        {
            return soundSource;
        }
        AudioSource newSource = gameObject.AddComponent<AudioSource>();
        return newSource;
    }
    
    //

    public void JumpSound(int playerNumber)
    {
        int soundNum = Random.Range(0, 3);
        var soundToPlay = playerNumber switch
        {
            0 => p1JumpSounds[soundNum],
            1 => p2JumpSounds[soundNum],
            2 => p3JumpSounds[soundNum],
            _ => p4JumpSounds[soundNum]
        };
        AudioSource source = FindEmptyAudioSource();
        source.clip = soundToPlay;
        source.Play();
    }
    
    //
    
    public void ChargeSound(int playerNumber)
    {
        var soundToPlay = chargeSounds[playerNumber];
        AudioSource source = FindEmptyAudioSource();
        source.clip = soundToPlay;
        source.Play();
    }
    
    //
    
    public void DeathSound(int playerNumber)
    {
        int soundNum = Random.Range(0, 3);
        var soundToPlay = playerNumber switch
        {
            0 => p1DeathSounds[soundNum],
            1 => p2DeathSounds[soundNum],
            2 => p3DeathSounds[soundNum],
            _ => p4DeathSounds[soundNum]
        };
        AudioSource source = FindEmptyAudioSource();
        source.clip = soundToPlay;
        source.Play();
    }
    
    //

    public void StartingSound()
    {
        int soundNum = Random.Range(0, 2);
        var soundToPlay = startSounds[soundNum];
        AudioSource source = FindEmptyAudioSource();
        source.clip = soundToPlay;
        source.Play();
    }
    
    //

    public void ScoreSound()
    {
        var soundToPlay = scoreSound;
        AudioSource source = FindEmptyAudioSource();
        source.clip = soundToPlay;
        source.Play();
    }
    
    //

    public void PlacementSound()
    {
        int soundNum = Random.Range(0, 3);
        var soundToPlay = placementSounds[soundNum];
        AudioSource source = FindEmptyAudioSource();
        source.clip = soundToPlay;
        source.Play();
    }
    
    // 

    public void ExplosionSound()
    {
        AudioClip[] soundToPlay = { explosionSounds[0],explosionSounds[1]};
        foreach (var elem in soundToPlay)
        {
            AudioSource source = FindEmptyAudioSource();
            source.clip = elem;
            source.Play();
        }
    }
    
    //

    public void SpringSound()
    {
        int soundNum = Random.Range(0, 3);
        var soundToPlay = springSounds[soundNum];
        AudioSource source = FindEmptyAudioSource();
        source.clip = soundToPlay;
        source.Play();
    }
    
    //

    public void GlassBlock()
    {
        var soundToPlay = glassSound;
        AudioSource source = FindEmptyAudioSource();
        source.clip = soundToPlay;
        source.Play();
    }
    
}

// END //
