using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{

    [Header("Movement Settings")]
    public float rotationSpeed;
    public Animator ani;
    public ParticleSystem effect;
    public int direction;
    Vector3 targetRotation = new Vector3(0, 0, 0);
    Vector3 currentRotation = new Vector3(0, 0, 0);

    [Header("UI Settings")]
    public Text playTxt;
    public Text exitTxt;

    [Header("Scene Transition Settings")]
    public bool mainMenu = true;
    public GameObject gameManager;
    public GameObject mainCamera;
    public Canvas mainMenuCnv;
    public Canvas scoreCnv;

    [Header("Sound Settings")]
    AudioManager am;
    bool soundPlayed = false;
    

    // Use this for initialization
    void Start()
    {
        am = FindObjectOfType<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();
    }

    // Player movement

    void PlayerMovement()
    {
        if (mainMenu)
        {
            // Left
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                targetRotation = new Vector3(0, 0, 60);
                direction = -1;
                ani.SetBool("isRunningLeft", true);
                ani.SetBool("isRunningRight", false);            
                effect.Play();
            }

            // Right
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                targetRotation = new Vector3(0, 0, -60);
                direction = 1;
                ani.SetBool("isRunningLeft", false);
                ani.SetBool("isRunningRight", true);
                effect.Play();
            }

            // Player stop
            if (currentRotation == targetRotation)
            {
                ani.SetBool("isRunningLeft", false);
                ani.SetBool("isRunningRight", false);
                direction = 0;
                effect.Stop();

            }

            // Select option effects
            if (currentRotation.z == 60)
            {
                playTxt.color = Color.magenta;
                playSwitch();
                am.Stop("PlayerRun");
            }

            else if (currentRotation.z == -60)
            {
                exitTxt.color = Color.magenta; 
                playSwitch();
                am.Stop("PlayerRun");
            }

            else 
            {
                playTxt.color = Color.red;
                exitTxt.color = Color.red;
                resetSound();
                
                if(!am.isPlaying("PlayerRun") && currentRotation.z != 0)
                {
                    am.Play("PlayerRun");
                }

            }


            // Select option
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (currentRotation == new Vector3(0, 0, 60))
                {
                    startGame();
                }

                if (currentRotation == new Vector3(0, 0, -60))
                {
                    exitGame();
                }
            }

            currentRotation = Vector3.MoveTowards(currentRotation, targetRotation, rotationSpeed);
            transform.eulerAngles = currentRotation;
        }
    }


    // Start game

    public void startGame()
    {
        gameManager.GetComponent<AsteroidSpawn>().enabled = true; // Enable the spawn of the asteroids
        mainCamera.GetComponent<FollowPlayer>().enabled = true; // Enable the camera following script
        mainMenuCnv.GetComponent<Canvas>().enabled = false; // Disable the main menu canvas
        scoreCnv.GetComponent<Canvas>().enabled = true; // Enable the score canvas

        transform.GetComponent<PlayerMovement>().enabled = true;

        mainMenu = false;
    }

    // Exit game

    public void exitGame()
    {
        Application.Quit();
    }


    // Selection sounds

    public void playSwitch()
    {
        if (!soundPlayed)
        {
            soundPlayed = true;
            am.Play("LightSwitch");
        }         
    }

    public void resetSound()
    {
        soundPlayed = false;
    }
}
