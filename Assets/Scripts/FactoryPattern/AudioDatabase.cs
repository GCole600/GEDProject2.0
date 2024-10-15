using System.Collections.Generic;
using UnityEngine;

namespace FactoryPattern
{
    [CreateAssetMenu(fileName = "AudioDatabase", menuName = "Audio/Database")]
    public class AudioDatabase : ScriptableObject
    {
        public List<AudioClip> music;
        public List<AudioClip> soundEffects;
    }
}