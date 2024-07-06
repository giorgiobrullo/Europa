using System;
using Unity.VisualScripting;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Items
{
    public abstract class GenericItem : MonoBehaviour
    {
        [SerializeField] protected GameObject pickupSound;
        protected bool Activated;

        protected virtual void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                PlayPickupSound();
                OnPlayerTrigger(other);
            }
        }

        protected virtual void OnCollisionEnter2D(Collision2D other)
        {
            if (other.collider.CompareTag("Player"))
            {
                PlayPickupSound();
                Console.WriteLine("Player collided with " + gameObject.name);
                OnPlayerTrigger(other.collider);
            }
        }

        protected abstract void OnPlayerTrigger(Collider2D player);

        protected void PlayPickupSound()
        {
            if (pickupSound && !Activated)
            {
                Object soundObj = Instantiate(pickupSound, transform.position, Quaternion.identity);
                AudioSource audioSource = soundObj.GetComponent<AudioSource>();

                if (audioSource != null)
                {
                    audioSource.Play();
                    Destroy(soundObj, audioSource.clip.length);
                }
                else
                {
                    Destroy(soundObj);
                }
            }
        }

    }
}