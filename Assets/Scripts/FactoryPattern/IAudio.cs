using UnityEngine;

namespace FactoryPattern
{
    public interface IAudio
    {
        void Play(AudioSource audioSource);
    }
}