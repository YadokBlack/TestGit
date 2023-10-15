using UnityEngine;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
    public List<AudioClip> audioClips = new List<AudioClip>();
    private AudioSource audioSource;

    private void Start()
    {
        // Obt�n el componente AudioSource adjunto a este objeto
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayAudioByIndex(int index)
    {
        if (audioSource == null) return;
        // Aseg�rate de que el �ndice sea v�lido
        if (index >= 0 && index < audioClips.Count)
        {
            // Asigna el clip de audio al AudioSource y reproduce el sonido
            audioSource.clip = audioClips[index];
            audioSource.Play();
        }
        else
        {
            Debug.LogError("�ndice de clip de audio no v�lido: " + index);
        }
    }
}
