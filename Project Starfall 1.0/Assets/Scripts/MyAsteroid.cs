using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyAsteroid : MonoBehaviour {

    [Header("Particle Settings")]
    public GameObject effect;

    [Header("Asteroid Settings")]
    float speed;
    Vector2 idirection;
    bool isReady;

    [Header("Score Settings")]
    ScoreManager sm;

    [Header("Audio Settings")]
    AudioManager am;

    /*[Header("Sound Settings")]
    AudioSource audioData;
    public AudioClip asteroidDestroy;*/

    void Awake()
    {
        speed = 25f;
        isReady = false;
        am = FindObjectOfType<AudioManager>();
    }

    void Start()
    {
        if(transform.tag == "Asteroid_1")
        {
            speed = 25;
        }

        if (transform.tag == "Asteroid_2")
        {
            speed = 30;
        }

        if (transform.tag == "Asteroid_3")
        {
            speed = 37.5f;
        }

        if (transform.tag == "Asteroid_4")
        {
            speed = 40f;
        }

        sm = GameObject.Find("GameManager").GetComponent<ScoreManager>();
        // audioData = transform.GetComponent<AudioSource>();
    }

    public void SetDirection(Vector2 direction)
    {
        idirection = direction.normalized;
        isReady = true;
    }

    void Update()
    {
        Movement();
    }

    void Movement()
    {
        if (isReady)
        {
            Vector2 position = transform.position;
            position += idirection * speed * Time.deltaTime;

            transform.position = position;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Planet")
        {
            Instantiate(effect, transform.position, Quaternion.identity);
            Camera.main.transform.parent.GetComponent<Animator>().SetTrigger("shake");

            if(gameObject.tag == "Asteroid_1")
            {
                sm.orangeScore++;
            }

            if(gameObject.tag == "Asteroid_2")
            {
                sm.purpleScore++;
            }

            if(gameObject.tag == "Asteroid_3")
            {
                sm.greenScore++;
            }

            if(gameObject.tag == "Asteroid_4")
            {
                sm.redScore++;
            }

            if(am.isPlaying("AsteroidCollision"))
            {
                am.Stop("AsteroidCollision");              
                Debug.Log("Play collision");
            }

            am.Play("AsteroidCollision");

            Destroy(gameObject);
        }

        if(collision.gameObject.tag == "Player")
        {
            am.Play("PlayerDeath");
            Destroy(gameObject);
        }
    }
}
