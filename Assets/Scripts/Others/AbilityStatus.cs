using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityStatus : MonoBehaviour
{
    
   	private SpriteRenderer sprite;
    protected Color actorColor;
    // Start is called before the first frame update  
    void Start(){
        sprite = GetComponent<SpriteRenderer>();
        actorColor = sprite.material.color;
    }

    void FixedUpdate()
    {
        if(Game.abilityAwake == false)
        {
            sprite.material.color = new Color(255, 0, 0, 50);

        }
        else{
            sprite.material.color = actorColor;
        }
    }
}