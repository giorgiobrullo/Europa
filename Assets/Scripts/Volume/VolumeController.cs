using System;
using System.Collections;
using UnityEngine;

namespace Volume
{
    public class VolumeController : MonoBehaviour
    {
        private AudioSource _audioSrc;
        private float _audioVolume = 1f;
        [SerializeField] private float fadeDuration = 3f;

        public static VolumeController Instance { get; private set; }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }
        
        void Start()
        {
            _audioSrc = GetComponent<AudioSource>();
            _audioVolume = PlayerPrefs.GetFloat("AudioVolume", 1f);
            _audioSrc.volume = 0;
            FadeVolume(_audioVolume, fadeDuration);
        }

        public void SetVolume(float vol)
        {
            if (!Mathf.Approximately(_audioVolume, vol))
            {
                // Stop coroutine if it's running
                StopAllCoroutines();

                // Debug.Log("Setting Volume to " + vol);
                _audioVolume = vol;
                _audioSrc.volume = _audioVolume;
                PlayerPrefs.SetFloat("AudioVolume", _audioVolume);
                PlayerPrefs.Save();
            }
        }

        public void SetSfxVolume(float vol)
        {
            //Debug.Log("Setting SFX volume to " + vol);
            PlayerPrefs.SetFloat("SFXAudioVolume", vol);
            PlayerPrefs.Save();
            
            // Find all existing SFXVolume objects and update their volume
            SfxVolume[] sfxVolumes = FindObjectsOfType<SfxVolume>();
            foreach (SfxVolume sfxVolume in sfxVolumes)
            {
                sfxVolume.UpdateSfxVolume(vol);
            }
        }

        private void FadeVolume(float targetVolume, float duration)
        {
            StartCoroutine(FadeVolumeCoroutine(targetVolume, duration));
        }

        private IEnumerator FadeVolumeCoroutine(float targetVolume, float duration)
        {
            float startVolume = _audioSrc.volume;
            float time = 0;

            while (time < duration)
            {
                // Debug.Log("Set volume to " + Mathf.Lerp(startVolume, targetVolume, time / duration));
                _audioSrc.volume = Mathf.Lerp(startVolume, targetVolume, time / duration);
                time += Time.deltaTime;
                yield return null;
            }

            _audioSrc.volume = targetVolume;

            PlayerPrefs.SetFloat("AudioVolume", targetVolume);
            PlayerPrefs.Save();
        }
    }
}
