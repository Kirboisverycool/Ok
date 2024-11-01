using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    public static SFXManager instance;

    [SerializeField] AudioSource SFXObject;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;

        }
    }

    public void PlaySFXClip(AudioClip audioClip, Transform spawnTransform, float volume)
    {
        //spawn in gameobject
        AudioSource audioSource = Instantiate(SFXObject, spawnTransform.position, Quaternion.identity);

        //assign the audioClip
        audioSource.clip = audioClip;

        //assign the volume
        audioSource.volume = volume;

        //play sound
        audioSource.Play();

        //get length of the SFX clip
        float clipLength = audioSource.clip.length;

        //destroy the clip when the SFX stops
        Destroy(audioSource.gameObject, clipLength);
    }
}
