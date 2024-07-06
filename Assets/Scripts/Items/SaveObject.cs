using Other;
using Player;
using UnityEngine;

namespace Items // Ensure this namespace matches that of GenericItem
{
    public class SaveObject : GenericItem
    {
        public GameObject pFlagTop; // Assign this in the editor with the child GameObject
        public Sprite greenFlagSprite; // Assign the green flag sprite in the editor
        public GameObject respawn;
        public Stats stats;
        public bool split;
        public int id;

        protected void Start()
        {
            if (GetComponent<Collider>() == null && GetComponent<Collider2D>() == null)
            {
                Debug.LogError("SaveObject requires a Collider or Collider2D component marked as Trigger.");
            }
        }

        protected override void OnPlayerTrigger(Collider2D player)
        {
            if (Activated) return;
            
            if (LiveSplitIntegration.Instance != null) LiveSplitIntegration.Instance.CheckpointReached(id);
            stats.FullHeal();
            ChangeFlagTopSprite();
            PlayPickupSound();
            SaveGame();

            if (respawn != null)
            {
                // Put it slightly above the flag top
                respawn.transform.position = new Vector3(pFlagTop.transform.position.x, pFlagTop.transform.position.y + 0.5f, pFlagTop.transform.position.z);
            }

            Activated = true;
            Debug.Log("Activated save object.");
        }

        void ChangeFlagTopSprite()
        {
            // Get the SpriteRenderer component from pFlagTop and change its sprite
            SpriteRenderer spriteRenderer = pFlagTop.GetComponent<SpriteRenderer>();
            if (spriteRenderer != null)
            {
                spriteRenderer.sprite = greenFlagSprite;
            }
            else
            {
                Debug.LogError("P_FlagTop does not have a SpriteRenderer component.");
            }
        }
        
        void SaveGame()
        {
            // Assuming ES3AutoSaveMgr is part of your project's saving mechanism
            ES3AutoSaveMgr.Current.Save();
        }
    }
}