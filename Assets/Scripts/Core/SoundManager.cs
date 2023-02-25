using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance { get; private set; }
    private AudioSource source;

    [SerializeField] public Transform listenerTransform;

    private void Awake() {
        instance = this;
        source = GetComponent<AudioSource>();
    }

    public void PlaySound(AudioClip _sound, float volume = 1f) {
        source.PlayOneShot(_sound, volume);
    }

    public float calculateVolume(Vector3 objectPosition, float minDist, float maxDist) {

        float dist = Vector3.Distance(objectPosition, listenerTransform.position);

        if(dist < minDist)
        {
           return 1;
        }
        else if(dist > maxDist)
        {
            return 0;
        }
        else
        {
            return 1 - ((dist - minDist) / (maxDist - minDist));
        }
    }
}
