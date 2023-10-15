using UnityEngine;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
    public List<AudioClip> audioClips = new List<AudioClip>();
    private AudioSource audioSource;

    private void Start()
    {
        // Obtén el componente AudioSource adjunto a este objeto
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayAudioByIndex(int index)
    {
        if (audioSource == null) return;
        // Asegúrate de que el índice sea válido
        if (index >= 0 && index < audioClips.Count)
        {
            // Asigna el clip de audio al AudioSource y reproduce el sonido
            audioSource.clip = audioClips[index];
            audioSource.Play();
        }
        else
        {
            Debug.LogError("Índice de clip de audio no válido: " + index);
        }
    }
}
