using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TargetBehavior : MonoBehaviour
{
    private int currentScene = 0;
    private float distance;
    public Sprite burnedSprite;
    public SpriteRenderer spriteRenderer;
    public PolygonCollider2D myCollider;
    
    public GameObject player;
    public GameObject hidingPlace;
    
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        myCollider = gameObject.GetComponent<PolygonCollider2D>();
        
        //sets the currentScene variable to the build index of the scene the target object is in upon the scene loading
        //since targets aren't singletons and are newly made in every scene they're in, I can put this in Start()!
        currentScene = SceneManager.GetActiveScene().buildIndex;
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector3.Distance (gameObject.transform.position, player.transform.position);
        //if you are in level 1, the targets walk towards you
        if (currentScene == 0)
        {
            if (distance > 3 && gameObject.tag == "unburned")
            {
                transform.position = Vector2.Lerp(gameObject.transform.position, player.transform.position, .001f);
            }
        }
        //by default, targets do not move in level 2.
        
        //if you are in level 3, the targets walk towards invisible spots on the edge of the level
        //(roughly, away from the player)
        if (currentScene == 2 && gameObject.tag == "unburned")
        {
            transform.position = Vector2.Lerp(gameObject.transform.position, hidingPlace.transform.position, .003f);
        }

        if (gameObject.tag == "burned")
        {
            spriteRenderer.sprite = burnedSprite;
            myCollider.enabled = false;
        }
    }
}
