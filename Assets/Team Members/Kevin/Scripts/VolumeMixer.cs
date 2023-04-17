using System.Collections;
using System.Collections.Generic;
using FMODUnity;
using UnityEngine;

public class VolumeMixer : MonoBehaviour
{
    private FMOD.Studio.VCA vca;

    private FMODEventMixerBehaviour fmodMixer;
    //private FMOD.Studio.Bank bank;
    
    [SerializeField] [Range(-80f, 10f)] 
    private float vcaVolume;
    // Start is called before the first frame update
    void Start()
    {
        vca = FMODUnity.RuntimeManager.GetVCA("events:/MUSIC");
    }

    // Update is called once per frame
    void Update()
    {
        vca.setVolume(DecibelToLinear(vcaVolume));
    }

    private float DecibelToLinear(float dB)
    {
        float linear = Mathf.Pow(10.0f, dB / 20f);
        return linear;
    }
}
