using System;
using SingletonPattern;
using UnityEngine;

namespace FactoryPattern
{
    public class AudioManager : Singleton<AudioManager>
    {
        public AudioSource musicSource;
        public AudioSource SFXSource;
        public AudioDatabase audioDatabase;

        private AudioFactory _audioFactory;

        private void Start()
        {
            _audioFactory = new AudioFactory(audioDatabase);
        }

        public void PlaySfx(string clipName)
        {
            IAudio audioInterface = _audioFactory.CreateAudio("SFX", clipName);

            audioInterface?.Play(SFXSource);
        }
        
        public void PlayMusic(string clipName)
        {
            IAudio audioInterface = _audioFactory.CreateAudio("Music", clipName);

            audioInterface?.Play(musicSource);
        }
    }
}