using System;
using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using Players;
using UnityEngine;
using UnityEngine.Analytics;
public class Enemy : MonoBehaviour
{
    
    public Transform target;

    public float speed = 200f;
    public float nextWaypointDistance = 3f;

    Path path;
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;

    Seeker seeker;
    protected Rigidbody2D rb;

    public float damage =5f;
    private bool gone;

    protected float sessionTime;


    private float framesFlashing = 7f;
    private SpriteRenderer sprite;
    protected Color actorColor;

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.CompareTag("Player")) {
			Actor targetHit = other.gameObject.GetComponent(typeof(Actor)) as Actor;
        if (targetHit != null) {
				targetHit.Damage(damage);
				targetHit.GetRb().AddForce(transform.up * 500f);
			}
        }
    }



    // Start is called before the first frame update
	void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        actorColor = sprite.material.color;
    	seeker = GetComponent<Seeker>();
    	rb = GetComponent<Rigidbody2D>();
        
        if(Game.currentPlayer != null)
        {
            target = Game.currentPlayer.transform;
            InvokeRepeating("UpdatePath", 0f, .5f);
        }

        sessionTime = AnalyticsSessionInfo.sessionElapsedTime;
            
    }
    
    void UpdatePath(){
        if (Game.currentPlayer != null)
            if (seeker.IsDone())
            {
                seeker.StartPath(rb.position, target.position, OnPAthComplete);
            }
                
    }
        

    void OnPAthComplete(Path p)
    {
    	if(!p.error)
    	{
    		path = p;
    		currentWaypoint = 0;
    	}
    }

    // Update is called once per frame
    void Update()
    {

        // Damage Increases over time
        damage = sessionTime * Time.deltaTime * 10;
        AstarPath.active.Scan();
        if(Game.currentPlayer == null)
        {
            return;
        }

        if (path == null)
        return;

        if(currentWaypoint >= path.vectorPath.Count)
        {
        	reachedEndOfPath = true;
        	return;
        } else{
        	reachedEndOfPath = false;
        }

        Vector2 direction =((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
		Vector2 force = direction * speed * Time.deltaTime;
		
		//glitch
		// float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;
        // rb.rotation = angle;
	
		//NON GLITCH WORKING 
		Vector3 look = (target.position - this.rb.transform.position); 
		float angle = Mathf.Atan2(look.y, look.x); 
        //Debug.Log(angle);
        this.rb.transform.rotation = Quaternion.Euler(0f, 0f, (angle * Mathf.Rad2Deg) -90f);

		rb.AddForce(force);
        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

        if (distance < nextWaypointDistance)
        {
        	currentWaypoint++;
        }
    }

    

        public void DamageFlash() {
			sprite.material.color = new Color(255, 0, 0, 50);
			Invoke("ResetColor", Time.deltaTime * framesFlashing);
		}
        private void ResetColor() {
			sprite.material.color = actorColor;
		}

        public Rigidbody2D GetRb()
        {
            return rb;
        }

}
