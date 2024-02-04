using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class WASDController : MonoBehaviour
{
    public static WASDController Instance;
    
    private Rigidbody2D myRB;
    public float forceAmt = 10;
    public TextMeshProUGUI screenText;
    private int currentScene = 0;
    public AudioSource burningSound;
    
    public Sprite burnedSprite;
    public SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        myRB = GetComponent<Rigidbody2D>();
        currentScene = SceneManager.GetActiveScene().buildIndex;
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        burningSound = gameObject.GetComponent<AudioSource>();
        Debug.Log(currentScene);
    }

    // Update is called once per frame
    void Update()
    {
        //only allows WASD movement if the player isn't in Level4 (the 4th scene)
        if (currentScene < 3)
        {
            if (Input.GetKey(KeyCode.W))
            {
                myRB.AddForce(Vector2.up * forceAmt);
            }
        
            if (Input.GetKey(KeyCode.A))
            {
                myRB.AddForce(Vector2.left * forceAmt);
            }
        
            if (Input.GetKey(KeyCode.S))
            {
                myRB.AddForce(Vector2.down * forceAmt);
            }
        
            if (Input.GetKey(KeyCode.D))
            {
                myRB.AddForce(Vector2.right * forceAmt);
            }
        
            //slows the player down
            myRB.velocity *= 0.99f;
        }

        //if the player is in the final level and hits space, changes the sprite + text and starts the audio
        if (currentScene == 3 && Input.GetKey(KeyCode.Space))
        {
            spriteRenderer.sprite = burnedSprite;
            burningSound.Play();
            screenText.text = "";
        }
        
    }

    //if the player is colliding with the targets, which are triggers, text tells them what to do
    //hitting space changes the tag on the target (which changes their sprite in TargetBehavior.cs)
    //and increments the targetsBurned variable which is used in the GameManager to switch scenes
    private void OnTriggerStay2D(Collider2D other)
    {
        screenText.text = "You're close enough now! Press space to warm them up!";
        if (Input.GetKey(KeyCode.Space))
        {
            Debug.Log("hit space");
            if (other.tag == "unburned")
            {
                //set on fire
                GameManager.Instance.targetsBurned++;
                other.tag = "burned";
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        screenText.text = "Help them All!";
    }
}
