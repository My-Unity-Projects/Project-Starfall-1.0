using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public float rotationSpeed;
    public Animator ani;
    public ParticleSystem effect;

    public int direction;

    [Header("End Game Settings")]
    public bool isDead = false;
    DeathMenu dm;

    [Header("Sound Settings")]
    AudioManager am;

    // Use this for initialization
    void Start () {

        direction = 0;
        dm = GameObject.Find("GameManager").GetComponent<DeathMenu>();
        am = FindObjectOfType<AudioManager>();
	}
	
	// Update is called once per frame
	void Update () {

        Movement();
	}

    void Movement()
    {
        Vector3 currentRotation = transform.eulerAngles;
        Vector3 targetRotation = transform.eulerAngles;

        if ((Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow)) && !isDead)
        {

            effect.Play(); // Play particle effect

            if(!am.isPlaying("PlayerRun"))
            {
                am.Play("PlayerRun"); // Play run sound when the player is in movement
            }
            

            // Left
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                targetRotation = Vector3.Lerp(transform.eulerAngles, transform.eulerAngles + new Vector3(0, 0, 45), Time.deltaTime * rotationSpeed);
                direction = -1;
                ani.SetBool("isRunningLeft", true);
                ani.SetBool("isRunningRight", false);
                
            }

            // Right
            if (Input.GetKey(KeyCode.RightArrow))
            {
                targetRotation = Vector3.Lerp(transform.eulerAngles, transform.eulerAngles + new Vector3(0, 0, -45), Time.deltaTime * rotationSpeed);
                direction = 1;
                ani.SetBool("isRunningLeft", false);
                ani.SetBool("isRunningRight", true);
            }
        }

        else
        {
            ani.SetBool("isRunningLeft", false);
            ani.SetBool("isRunningRight", false);
            direction = 0;
            effect.Stop(); // Stop particle system
            am.Stop("PlayerRun");
        }

        transform.eulerAngles = targetRotation;
    }

    // Collisions
    public void OnCollisionEnter2D(Collision2D collision)
    {
        string tag = collision.gameObject.tag;

        if(tag == "Asteroid_1" || tag == "Asteroid_2" || tag == "Asteroid_3" || tag == "Asteroid_4")
        {
            ani.SetBool("isDead", true);
            isDead = true;
            dm.EndGame();
        }
    }
}
