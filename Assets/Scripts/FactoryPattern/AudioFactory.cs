using Unity.Properties;
using UnityEngine;

namespace FactoryPattern
{
    public class AudioFactory
    {
        private readonly AudioDatabase _audioDatabase;

        public AudioFactory(AudioDatabase audioDatabase)
        {
            _audioDatabase = audioDatabase;
        }

        public IAudio CreateAudio(string audioType, string clipName)
        {
            AudioClip clip = null;

            switch (audioType)
            {
                case "Music":
                    clip = _audioDatabase.music.Find(c => c.name == clipName);
                    return new BackgroundMusic(clip);
                case "SFX":
                    clip = _audioDatabase.soundEffects.Find(c => c.name == clipName);
                    return new SoundEffect(clip);
            }
            
            return null;
        }
    }
}