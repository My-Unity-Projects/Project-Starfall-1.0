using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawn : MonoBehaviour {


    [Header("Asteroid kinds")]
    public GameObject asteroid_1;
    public GameObject asteroid_2;
    public GameObject asteroid_3;
    public GameObject asteroid_4;

    [Header("Asteroid Spawn Settings")]
    public GameObject[] asteroidSpawns;
    float time;
    PlayerMovement pm;

    [Header("Score Settings")]
    int totalScore = 0;
    ScoreManager sm;

	// Use this for initialization
	void Start () {
        pm = asteroidSpawns[0].transform.parent.GetComponent<PlayerMovement>();
        time = 0.5f;
        sm = GameObject.Find("GameManager").GetComponent<ScoreManager>();
    }
	
	// Update is called once per frame
	void Update () {

        Spawn();

   	}

    void Spawn()
    {
        totalScore = sm.orangeScore + sm.purpleScore + sm.greenScore + sm.redScore;
        GameObject planet = GameObject.Find("Planet");

        int minSpawn = 0;
        int maxSpawn = 0;


        if(pm.direction == 1)
        {
            minSpawn = 3;
            maxSpawn = 8;
        }

        if (pm.direction == -1)
        {
            minSpawn = 8;
            maxSpawn = 11;
        }

        int choseenSpawn = Random.Range(0, 3);
        int choseenSpawn2 = Random.Range(minSpawn, maxSpawn);

        while(choseenSpawn == choseenSpawn2)
        {
            choseenSpawn = Random.Range(0, 3);
        }

        GameObject spawn = asteroidSpawns[choseenSpawn];
        GameObject spawn2 = asteroidSpawns[choseenSpawn2];
        

        time -= Time.deltaTime;

        if(time <= 0)
        {
            GameObject asteroid = (GameObject)Instantiate(chooseAsteroid());
            asteroid.transform.position = spawn.transform.position;
            asteroid.transform.rotation = Quaternion.Euler(0, 0, spawn.transform.parent.eulerAngles.z + spawn.transform.localEulerAngles.z);
            Vector2 direction = planet.transform.position - asteroid.transform.position;
            asteroid.GetComponent<MyAsteroid>().SetDirection(direction);

            GameObject asteroid2 = (GameObject)Instantiate(chooseAsteroid());
            asteroid2.transform.position = spawn2.transform.position;
            asteroid2.transform.rotation = Quaternion.Euler(0, 0, spawn2.transform.parent.eulerAngles.z + spawn2.transform.localEulerAngles.z);
            Vector2 direction2 = planet.transform.position - asteroid2.transform.position;
            asteroid2.GetComponent<MyAsteroid>().SetDirection(direction2);

            time = 3f;

            if(totalScore >= 10)
            {
                time = 2.5f;

                if(totalScore >= 20)
                {
                    time = 2f;

                    if(totalScore >= 30)
                    {
                        time = 1.5f;

                        if(totalScore >= 35)
                        {
                            time = 1.2f;

                            if(totalScore >= 30)
                            {
                                time = 0.9f;
                                
                                if(totalScore >= 50)
                                {
                                    time = 0.6f;
                                }
                            }
                        }
                    }
                }
            }
        }        
    }


    GameObject chooseAsteroid()
    {
        int a = Random.Range(0, 4);

        if(a == 1)
        {
            return asteroid_1;
        }

        if (a == 2)
        {
            return asteroid_2;
        }

        if (a == 3)
        {
            return asteroid_3;
        }

        else
            return asteroid_4;
    }
}
