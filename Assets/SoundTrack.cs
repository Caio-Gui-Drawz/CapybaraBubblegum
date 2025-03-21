using UnityEngine;
using FMOD.Studio;
using FMODUnity;
using System.Collections;

public class SoundTrack : MonoBehaviour
{
    public EventReference eventReference;
    private EventInstance instanceFMOD;
    private static SoundTrack instance;

    public static SoundTrack Instance() => instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            instanceFMOD = RuntimeManager.CreateInstance(eventReference);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        StartSoundTrack();
    }

    public void StartSoundTrack()
    {
        if (!instanceFMOD.isValid())
        {
            Debug.LogError("FMOD Event Instance is not valid.");
            return;
        }

        instanceFMOD.setParameterByName("PLAYERISDEAD", 0);
        instanceFMOD.start();
    }

    public void StopSoundTrack()
    {
        if (instanceFMOD.isValid())
        {
            instanceFMOD.setParameterByName("PLAYERISDEAD", 1);
            instanceFMOD.start();
        }
        else
        {
            Debug.LogWarning("FMOD Event Instance is already invalid.");
        }
    }

    private void OnDestroy()
    {
        if (instanceFMOD.isValid())
        {
            instanceFMOD.release();
        }
    }
}
