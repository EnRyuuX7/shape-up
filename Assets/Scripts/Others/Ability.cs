using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Players;

public class Ability : MonoBehaviour
{

    private Sprite mySprite;
    private SpriteRenderer sr;

    //public float damage = 10f;


    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.CompareTag("Enemy")) {
            Enemy targetHit = other.gameObject.GetComponent(typeof(Enemy)) as Enemy;

            Debug.Log(targetHit);    

            if (targetHit != null) {
                    targetHit.DamageFlash();
                    Score.AddScore(500);
                }
        }
    }

    void Awake()
    {
    }

}