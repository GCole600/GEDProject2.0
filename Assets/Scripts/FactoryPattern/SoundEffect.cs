using UnityEngine;

namespace FactoryPattern
{
    public class SoundEffect : IAudio
    {
        private readonly AudioClip _audioClip;

        public SoundEffect(AudioClip audioClip)
        {
            _audioClip = audioClip;
        }

        public void Play(AudioSource audioSource)
        {
            audioSource.PlayOneShot(_audioClip);
        }
    }
}
