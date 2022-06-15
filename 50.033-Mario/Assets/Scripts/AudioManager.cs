using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    
    [SerializeField] List<AudioClip> audioClips = new List<AudioClip>();
    [SerializeField] AudioSource themeSource;
    [SerializeField] AudioSource playerJumpSource;
    [SerializeField] AudioSource breakBrickSource;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;

            DontDestroyOnLoad(gameObject);
        }

        else
        {
            Destroy(gameObject); // So we don't have multiple AudioManagers
        }
    }

    public void ThemeSFX()
    {
        AudioClip clip = audioClips[0];
        themeSource.Play();
    }

    public void PlayerJumpSFX()
    {
        AudioClip clip = audioClips[1];
        playerJumpSource.PlayOneShot(clip);
    }

    public void BreakBrickSFX()
    {
        AudioClip clip = audioClips[2];
        breakBrickSource.PlayOneShot(clip);
    }
}
