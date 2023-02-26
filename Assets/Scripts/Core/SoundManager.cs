using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance { get; private set; }
    private AudioSource soundSource;
    private AudioSource musicSource;

    [SerializeField] public Transform listenerTransform;

    private void Awake() {
        instance = this;
        soundSource = GetComponent<AudioSource>();
        musicSource = transform.GetChild(0).GetComponent<AudioSource>();

        // Assign initial volumes
        ChangeMusicVolume(0);
        ChangesoundVolume(0);
    }

    public void PlaySound(AudioClip _sound, float volume = 1f) {
        soundSource.PlayOneShot(_sound, volume);
    }

    public float calculateVolume(Vector3 objectPosition, float minDist, float maxDist, float maxSoundPercentage=1f) {

        float dist = Vector3.Distance(objectPosition, listenerTransform.position);

        if(dist < minDist)
        {
           return maxSoundPercentage;
        }
        else if(dist > maxDist)
        {
            return 0;
        }
        else
        {
            return maxSoundPercentage - ((dist - minDist) / (maxDist - minDist));
        }
    }

    public void ChangesoundVolume(float _change) {
        ChangeSourceVolume(1, "soundVolume", _change, soundSource);
    }

    public void ChangeMusicVolume(float _change) {
        ChangeSourceVolume(1f, "musicVolume", _change, musicSource);
    }

    private void ChangeSourceVolume(float baseVolume, string volumeName, float change, AudioSource source) {
        float currentVolume = PlayerPrefs.GetFloat(volumeName, 1);
        currentVolume += change;

        if (currentVolume > 1)
            currentVolume = 0;
        else if (currentVolume < 0)
            currentVolume = 1;

        currentVolume *= baseVolume;
        source.volume = currentVolume;

        PlayerPrefs.SetFloat(volumeName, currentVolume);
    }
}
