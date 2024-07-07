using UnityEngine;

namespace Volume
{
    public class SfxVolume : MonoBehaviour
    {
        public float percentageOffset = 1f;
        private AudioSource _audioSource;

        void Start()
        {
            _audioSource = GetComponent<AudioSource>();

            // Set initial volume
            float initialSfxVolume = PlayerPrefs.GetFloat("SFXAudioVolume", 1f);
            _audioSource.volume = initialSfxVolume * percentageOffset;
            Debug.Log($"SfxVolume: Initial volume set to {_audioSource.volume}");
            
        }

        public void UpdateSfxVolume(float newVolume)
        {
            _audioSource.volume = newVolume * percentageOffset;
            Debug.Log($"SfxVolume: Volume updated to {_audioSource.volume}");
        }
    }
}