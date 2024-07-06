using System.Collections;
using Player;
using UnityEngine;
using UnityEngine.SceneManagement;
using Volume; // Required for changing scenes

namespace Other
{
    public class Portal : MonoBehaviour
    {
        private AudioSource[] _audioSources;
        private static readonly int Loading = Animator.StringToHash("loading");

        private void Awake()
        {
            // Get all AudioSource components at start
            _audioSources = GetComponents<AudioSource>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                if(LiveSplitIntegration.Instance != null) LiveSplitIntegration.Instance.PauseRun();
                // Play the audio from the second AudioSource, if available
                if (_audioSources.Length >= 2)
                {
                    AudioSource secondAudioSource = _audioSources[1];
                    secondAudioSource.Play();
                }
                else
                {
                    Debug.LogWarning("Less than two AudioSources found on the Portal.");
                }
                
                PlayerController.Instance.SetLoading(true);
                other.GetComponent<Animator>().SetBool(Loading, true);
                Destroy(other.GetComponent<Rigidbody2D>());
                Destroy(other.GetComponent<BoxCollider2D>());


                // Wait for the audio to finish playing before changing the scene
                StartCoroutine(ChangeSceneAfterAudio());
            }
        }

        private IEnumerator ChangeSceneAfterAudio()
        {
            // Assuming the second AudioSource is the one playing the sound
            if (_audioSources.Length >= 2)
            {
                yield return new WaitForSeconds(_audioSources[1].clip.length);
            }

            // Continue with scene change logic
            string currentSceneName = SceneManager.GetActiveScene().name;
            string baseName = "Game_";
            int currentSceneNumber;

            // Extract the number part of the scene name
            if (int.TryParse(currentSceneName.Replace(baseName, ""), out currentSceneNumber))
            {
                string nextSceneName = baseName + (currentSceneNumber + 1);
                PlayerPrefs.SetString("LastSceneName", currentSceneName);
                Debug.Log("Saving scene name: " + nextSceneName);
                VolumeController.Instance.StopAllCoroutines();

                if (nextSceneName == "Game_5" && LiveSplitIntegration.Instance != null)
                {
                    //LiveSplitIntegration.Instance.ResumeRun();
                    LiveSplitIntegration.Instance.SendSceneChangeEvent();
                }

                SceneManager.LoadScene(nextSceneName);

            }
            else
            {
                Debug.LogError("Current scene name does not follow the 'game_X' naming convention.");
            }
        }
    }
}
