using System.Collections;
using System.Collections.Generic;
using Players;
using UnityEngine;

public class OnTrigger : MonoBehaviour {
	private void OnTriggerEnter2D(Collider2D other) {
		if (other.CompareTag("Actor")) {
			((Actor) other.GetComponent(typeof(Actor))).Kill();
		}
	}
}
