using Enemies.Generic;

namespace Enemies.Bat
{
    public class Bat : GenericEnemy
    {
        public new void Start()
        {
            IsFacingRight = true;
            base.Start();
        }
    }
}