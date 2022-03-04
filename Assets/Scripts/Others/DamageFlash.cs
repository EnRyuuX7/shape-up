using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class DamageFlash : MonoBehaviour
{
    
/////////////// test damage flash 

    private SpriteRenderer sprite;
    protected Color aColor;
	private float framesFlashing = 5f;

    public bool flashHealthBar; 



    // Start is called before the first frame update
    void Start()
    {
        //flashHealthBar = GameObject.Find("Player").GetComponent<Actor>().flashBar = false;
        
        sprite = GetComponent<SpriteRenderer>();
    	aColor = sprite.color;
        
    }

    private void function() {
			sprite.color = new Color(255, 1, 1, 20);
			Invoke("color", Time.deltaTime * framesFlashing);
		}
        private void color() {
			sprite.color = aColor;
		}

    // Update is called once per frame
    void Update()
    {
        if(Game.currentPlayer != null)
        {
            flashHealthBar = Game.currentPlayerScript.flashBar;
            if(flashHealthBar==true)
            {
                function();
                Game.currentPlayerScript.flashBar = false;
            }
        }

    }
}
