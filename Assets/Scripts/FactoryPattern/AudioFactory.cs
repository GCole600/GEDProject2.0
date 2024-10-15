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
                    if (clip != null)
                        return new BackgroundMusic(clip);
                    break;
                case "SFX":
                    clip = _audioDatabase.soundEffects.Find(c => c.name == clipName);
                    if (clip != null)
                        return new SoundEffect(clip);
                    break;
            }
            
            Debug.LogWarning("Audio clip not found or invalid type specified.");
            return null;
        }
    }
}