using UnityEngine;

namespace Volume
{
    public class SfxVolume : MonoBehaviour
    {
        public float percentageOffset = 1f;
        void Start()
        {
            GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("SFXAudioVolume", 1f) * percentageOffset;
        }
    }
}