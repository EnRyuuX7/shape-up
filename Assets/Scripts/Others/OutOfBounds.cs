using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Players;

public class OutOfBounds : MonoBehaviour
{
    // Start is called before the first frame update
    	
	private int damage = 10000;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player")) {
			Actor targetHit = other.gameObject.GetComponent(typeof(Actor)) as Actor;
        if (targetHit != null) {
				targetHit.Damage(damage);
			}
        }
    }
}
