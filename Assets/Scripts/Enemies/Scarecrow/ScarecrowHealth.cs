using Enemies.Generic;
using UnityEngine;

namespace Enemies.Scarecrow
{
    public class ScarecrowHealth : EnemyHealth
    {
        private static readonly int Hurt = Animator.StringToHash("hurt");

        public override void TakeDamage(int damage)
        {
            var enemy = GetComponent<GenericEnemy>();
            Debug.Log("ScareCrow hit!");
            enemy.Animator.SetTrigger(Hurt);
        }
    }
}