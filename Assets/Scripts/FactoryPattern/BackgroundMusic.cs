using UnityEngine;

namespace FactoryPattern
{
    public class BackgroundMusic : IAudio
    {
        private readonly AudioClip _audioClip;

        public BackgroundMusic(AudioClip audioClip)
        {
            _audioClip = audioClip;
        }

        public void Play(AudioSource audioSource)
        {
            audioSource.clip = _audioClip;
            audioSource.loop = true;
            audioSource.Play();
        }
    }
}