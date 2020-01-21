using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour {

    public GameObject player;
    float t;
    float timetomove = 1;
    Vector3 playerPos;
    Vector3 targetPos;
    Animator ani;
	// Use this for initialization
	void Start () {
        ani = transform.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {

            timetomove = 1;

            playerPos = player.transform.position;

            targetPos = new Vector3(0, 0, transform.position.z);

            if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
            {
                targetPos = new Vector3(playerPos.x, playerPos.y, transform.position.z);
                timetomove = 0.3f;

                ani.SetBool("zoom", true);
            }

            else
            {
                ani.SetBool("zoom", false);
            }
            t = Time.deltaTime / timetomove;

            transform.position = Vector3.Lerp(transform.position, targetPos, t);

            t = 0;
	}
}
