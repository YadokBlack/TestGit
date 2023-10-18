using UnityEngine;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
    public List<AudioClip> audioClips = new List<AudioClip>();
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayAudioByIndex(int index)
    {
        if (audioSource == null) return;

        if (index >= 0 && index < audioClips.Count)
        {
            audioSource.clip = audioClips[index];
            audioSource.Play();
        }
        else
        {
            Debug.LogError("Índice de clip de audio no válido: " + index);
        }
    }
}
