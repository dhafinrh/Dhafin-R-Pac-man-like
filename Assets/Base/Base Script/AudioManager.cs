using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource playerAudioSource;
    [SerializeField] private List<Audio> playerAudio = new();

    public void PlayAudio(string audioName)
    {
        var audio = playerAudio.Find(item => item.name.Equals(audioName));
        if (audio != null)
        {
            playerAudioSource.clip = audio.audioAsset;
            playerAudioSource.volume = audio.volume;
            playerAudioSource.Play();
        }
        else
        {
            Debug.LogWarning($"Audio '{audioName}' tidak ditemukan!");
        }
    }
}