using System;
using SingletonPattern;
using UnityEngine;
using UnityEngine.Serialization;

namespace FactoryPattern
{
    public class AudioManager : Singleton<AudioManager>
    {
        public AudioSource musicSource;
        public AudioSource sfxSource;
        public AudioDatabase audioDatabase;

        private AudioFactory _audioFactory;

        private void Start()
        {
            _audioFactory = new AudioFactory(audioDatabase);
        }

        public void PlaySfx(string clipName)
        {
            IAudio audioInterface = _audioFactory.CreateAudio("SFX", clipName);

            audioInterface?.Play(sfxSource);
        }
        
        public void PlayMusic(string clipName)
        {
            IAudio audioInterface = _audioFactory.CreateAudio("Music", clipName);

            audioInterface?.Play(musicSource);
        }
    }
}