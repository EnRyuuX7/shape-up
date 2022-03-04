using UnityEngine;

namespace Players
{
    public class Player : Actor
    {

        private new void Start()
        {
            base.Start();
            Health = Game.health;
        }

        private void Update()
        {
            if (Input.GetKey(KeyCode.A))
                Move(-1);
            if (Input.GetKey(KeyCode.D))
                Move(1);
            if (Input.GetKey(KeyCode.Space))
            {
                Jump();
            }

            if (!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
            {
                StopMoving();
                if (winding)
                    Move(0);
            }
            
            // mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        }
    }
}

