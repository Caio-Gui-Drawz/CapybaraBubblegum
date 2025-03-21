using UnityEngine;
using FMOD.Studio;
using FMODUnity;

public class SoundTrack : MonoBehaviour
{
    public EventReference eventReference;
    private FMOD.Studio.EventInstance instanceFMOD;

    private static SoundTrack instance;
    public static SoundTrack Instance() => instance;


    private void Awake()
    {
        instance = this;
        instanceFMOD = FMODUnity.RuntimeManager.CreateInstance(eventReference);
    }

    public void StartSoundTrack()
    {
        instanceFMOD.setParameterByName("PLAYERISDEAD", 0);
        instanceFMOD.start();
    }

    public void StopSoundTrack()
    {
        instanceFMOD.setParameterByName("PLAYERISDEAD", 1);
    }
}