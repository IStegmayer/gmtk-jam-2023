using System;
using UnityEngine;

namespace Game {
/// <summary>
/// Insanely basic audio system which supports 3D sound.
/// Ensure you change the 'Sounds' audio Source to use 3D spatial blend if you intend to use 3D sounds.
/// </summary>
public class AudioSystem : StaticInstance<AudioSystem> {
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource soundsSource;
    [SerializeField] private AudioSource dogStepsSource;
    [SerializeField] private AudioSource NPCSource;
    [SerializeField] private AudioSource clientSource;

    public Sound[] sounds;

    void Start() {
        foreach (Sound s in sounds) {
            s.Source = gameObject.AddComponent<AudioSource>();
            s.Source.clip = s.Clip;
            s.Source.volume = s.Volume;
            s.Source.pitch = s.Pitch;
            s.Source.loop = s.Loop;
        }
    }

    // TODO: event functions
    // private void PlayPlantHarvested(Plant plant) {
    //     PlaySound("plantHarvested", plant.transform.position);
    // }

    public void PlayMusic(string clipName) {
        Sound s = Array.Find(sounds, sound => sound.Name == clipName);
        if (s == null) {
            Debug.LogWarning("Sound: " + clipName + " not found!");
            return;
        }

        musicSource.clip = s.Clip;
        musicSource.loop = s.Loop;
        musicSource.volume = s.Volume;

        musicSource.Play();
    }

    public void StopPlaying() {
        soundsSource.Stop();
    }

    public void PlaySound(string clipName, Vector3 pos, float vol = 1) {
        Sound s = Array.Find(sounds, sound => sound.Name == clipName);
        if (s == null) {
            Debug.LogWarning("Sound: " + clipName + " not found!");
            return;
        }

        soundsSource.transform.position = pos;
        soundsSource.clip = s.Source.clip;
        soundsSource.loop = s.Source.loop;
        soundsSource.volume = s.Source.volume;

        if (soundsSource.loop)
            soundsSource.Play();
        else
            soundsSource.PlayOneShot(soundsSource.clip, vol);
    }

    public void PlayCustomerSound(string clipName, Vector3 pos, float vol = 1) {
        Sound s = Array.Find(sounds, sound => sound.Name == clipName);
        if (s == null) {
            Debug.LogWarning("Sound: " + clipName + " not found!");
            return;
        }

        clientSource.transform.position = pos;
        clientSource.clip = s.Source.clip;
        clientSource.loop = s.Source.loop;
        clientSource.volume = s.Source.volume;

        if (clientSource.loop)
            clientSource.Play();
        else
            clientSource.PlayOneShot(clientSource.clip, vol);
    }

    public void PlayVendorSound(string clipName, Vector3 pos, float vol = 1) {
        Sound s = Array.Find(sounds, sound => sound.Name == clipName);
        if (s == null) {
            Debug.LogWarning("Sound: " + clipName + " not found!");
            return;
        }

        NPCSource.transform.position = pos;
        NPCSource.clip = s.Source.clip;
        NPCSource.loop = s.Source.loop;
        NPCSource.volume = s.Source.volume;

        if (NPCSource.loop)
            NPCSource.Play();
        else
            NPCSource.PlayOneShot(NPCSource.clip, vol);
    }


    #region Events

    // TODO: here we hookup events
    private void OnEnable() {
        // Plant.PlantHarvested += PlayPlantHarvested;
    }

    private void OnDisable() {
        // Plant.PlantHarvested -= PlayPlantHarvested;
    }

    #endregion
}
}