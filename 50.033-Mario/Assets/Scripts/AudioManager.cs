using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;


public class AudioManager : MonoBehaviour
{
    public BreakBrick breakBrickScript;

    private AudioMixer _MasterMixer;
    private AudioMixerSnapshot _snapshot1;
    private AudioMixerPlayable _breakBrick;

    // Start is called before the first frame update
    void Start()
    {
        breakBrickScript = GetComponent<BreakBrick>();

        _snapshot1 = _MasterMixer.FindSnapshot("GameAudio");
        Snapshot1(); // Play audio on start
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Snapshot1()
    {
        _snapshot1.TransitionTo(0.0f);
    }

    void PlayBreakBrickSound()
    {
        
    }
}
