using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int targetsBurned = 0;
    public int targetNumber = 3;
    public int targetScene = 0;
    private int currentScene = 0;

    public TextMeshProUGUI screenText;
    
    void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //once you've burned all your targets, goes to the next scene.
        if (targetsBurned == targetNumber)
        {
            targetScene++;
            SceneManager.LoadScene(targetScene);
            targetsBurned = 0;
        }
    }
}
