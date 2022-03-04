using System;
using Pathfinding;
using UnityEngine;




namespace Players
{
    public class Actor : MonoBehaviour
    {
        private bool scored;
        public int points;
        
        private bool moving;
        protected bool winding;
        private bool jumping;
        private bool grounded;

        public LayerMask whatIsGround;
        public Transform footPos;

        //variables
        public float moveSpeed = 3700f;
        public float jumpForce = 600f;
        public float jumpDecay = 1000f;

        public float Health { get; set; }
		public float startHealth = 100;
        private int currentJumps;
        public int maxJump = 1;

        private float framesFlashing = 20f;
        
        public float maxMoveSpeed = 14;

        protected Rigidbody2D rb;

        private SpriteRenderer sprite;
        protected Color actorColor;
        private Vector2 standardScale;


        public bool flashBar;




        //Initial Condition
        protected void Start()
        {
            flashBar = false;
            jumping = false;
            currentJumps = maxJump;
            rb = GetComponent<Rigidbody2D>();
            sprite = GetComponent<SpriteRenderer>();
            actorColor = sprite.material.color;
            standardScale = transform.localScale;
            rb.transform.position = new Vector3(8f, 12f, 0.0f);
            Health = startHealth;

        }

        //Executes every frame
        private void FixedUpdate()
        {
            if (!grounded)
                rb.AddForce(Vector2.down * jumpDecay * Time.deltaTime);

            if (!grounded && jumping && rb.velocity.x > 1f)
                rb.transform.Rotate(0, 0, -8);
            else if (!grounded && jumping && rb.velocity.x < -1f)
                rb.transform.Rotate(0, 0, 8);

   
            //SlowDown();
        }


        //Executes after an action is done
        private void LateUpdate()
        {
            grounded = Physics2D.OverlapCircle(footPos.position, 0.7f, whatIsGround);

            if (currentJumps < maxJump && grounded)
                currentJumps = maxJump;

            if (grounded)
                jumping = false;

            if(transform.localScale.y < standardScale.y)
                transform.localScale = new Vector2(transform.localScale.x, transform.localScale.y + 0.03f);
            if (transform.localScale.x < standardScale.x)
                transform.localScale = new Vector2(transform.localScale.x + 0.03f, transform.localScale.y);

        }

        // Movement
        protected void Move(int dir)
        {
            moving = true;

            float xVel = rb.velocity.x;

            if (xVel < maxMoveSpeed && dir > 0)
                rb.AddForce(Vector2.right * moveSpeed * Time.deltaTime * dir);
            else if (xVel > -maxMoveSpeed && dir < 0)
                rb.AddForce(Vector2.right * moveSpeed * Time.deltaTime * dir);

            // easier turining time
            if (xVel > 0.2f && dir < 0)
                rb.AddForce(-Vector2.right * moveSpeed * Time.deltaTime * 3.2f);

            if (xVel < -0.2f && dir > 0)
                rb.AddForce(Vector2.right * moveSpeed * Time.deltaTime * 3.2f);

            StopMoving();
        }

        private void SlowDown()
        {
            if (moving)
                return;
            //Friction Mechanism            
            //If no key pressed but still moving, slow down player
            if (rb.velocity.x > 0.2f)
                rb.AddForce(moveSpeed * Time.deltaTime * -Vector2.right );
            else if (rb.velocity.x < -0.2f)
                rb.AddForce(moveSpeed * Time.deltaTime * Vector2.right);


        }
        protected void StopMoving()
        {
            moving = false;
        }

        protected void Jump()
        {
            //Health--;
            if (currentJumps < 1)
                return;

            jumping = true;

            rb.velocity = new Vector2(rb.velocity.x, 1);
            rb.AddForce(Vector2.up * jumpForce);

            currentJumps--;
            winding = false;

        }

        private void JumpReset()
        {
            currentJumps = maxJump;
        }

        public void Damage(float damage) {
			Health -= damage;
            flashBar = true;
			if (Health <= 0) {
				Kill();
				// if (gameObject.layer == LayerMask.NameToLayer("Enemy")) {
				// 	if (scored) return;
				// 	scored = true;
				// 	ScoreControllerx.AddScore(points);
				// }
			}
            DamageFlash();
		}

        public void Kill() {
			Health = -1;
			Destroy(gameObject);
            OnDeathAction();
		}

        private void DamageFlash() {
			sprite.material.color = new Color(255, 0, 0, 50);
			Invoke("ResetColor", Time.deltaTime * framesFlashing);
		}
        private void ResetColor() {
			sprite.material.color = actorColor;
		}
		protected virtual void OnDeathAction() {
			Time.timeScale = 0.6f; 
		}

        public Rigidbody2D GetRb()
        {
            return rb;
        }

    }
}


