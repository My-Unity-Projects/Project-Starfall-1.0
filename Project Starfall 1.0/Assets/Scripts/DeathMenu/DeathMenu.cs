using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DeathMenu : MonoBehaviour {

    [Header("Score Settings")]
    public Slider orangeSld;
    public Slider purpleSld;
    public Slider greenSld;
    public Slider redSld;
    public Text orangeTxt;
    public Text purpleTxt;
    public Text greenTxt;
    public Text redTxt;
    public Text totalScoreTxt;
    ScoreManager sc;

    [Header("Animation Settings")]
    float myOrangeValue = 0;
    float myPurpleValue = 0;
    float myGreenValue = 0;
    float myRedValue = 0;
    float myTotalValue = 0;
    float i = 0;
    float timer = 1.5f;

    [Header("Transition Settings")]
    public Canvas scoreCnv;
    public Canvas deathCnv;
    public Canvas highScoreCnv;
    public bool isDead = false;
    PlayerMovement pm;
    GameObject gm;

	// Use this for initialization
	void Start () {
        sc = transform.GetComponent<ScoreManager>();
        gm = GameObject.Find("GameManager");
        pm = GameObject.Find("Rotator").GetComponent<PlayerMovement>();
	}
	
	// Update is called once per frame
	void Update () {

        FinalScoreAnimation();
        RestartGame();
        myTotalValue = sc.orangeScore + sc.purpleScore + sc.greenScore + sc.redScore;

        if(myTotalValue > PlayerPrefs.GetFloat("High Score"))
        {
            PlayerPrefs.SetFloat("High Score", myTotalValue);
        }
	}

    public void FinalScoreAnimation()
    {
        if(isDead)
        {
            timer -= Time.deltaTime;

            if(myOrangeValue < sc.orangeScore && timer < 0)
            {
                myOrangeValue ++;
                orangeSld.value = myOrangeValue * 0.4f;
                orangeTxt.text = "x" + myOrangeValue.ToString();
                timer = 0.05f;
            }

            if(myPurpleValue < sc.purpleScore && timer < 0)
            {
                myPurpleValue++;
                purpleSld.value = myPurpleValue * 0.4f;
                purpleTxt.text = "x" + myPurpleValue.ToString();
                timer = 0.05f;
            }

            if (myGreenValue < sc.greenScore && timer < 0)
            {
                myGreenValue++;
                greenSld.value = myGreenValue * 0.4f;
                greenTxt.text = "x" + myGreenValue.ToString();
                timer = 0.05f;
            }

            if (myRedValue < sc.redScore && timer < 0)
            {
                myRedValue++;
                redSld.value = myRedValue * 0.4f;
                redTxt.text = "x" + myRedValue.ToString();
                timer = 0.05f;
            }

            if(i < myTotalValue && timer < 0)
            {
                i++;
                totalScoreTxt.text = "total score\n\n" +  i.ToString();
                timer = 0.05f;
            }
        }
    }

    public void EndGame()
    {

        // Update canvas
        scoreCnv.GetComponent<Canvas>().enabled = false;
        deathCnv.GetComponent<Canvas>().enabled = true;
        highScoreCnv.GetComponent<Canvas>().enabled = false;

        // Stop asteroid spawn
        gm.GetComponent<AsteroidSpawn>().enabled = false;

        // Camera death settings
        Camera.main.GetComponent<Animator>().SetBool("Dead", true);
        Camera.main.transform.parent.GetComponent<Animator>().SetBool("DeadParent", true);
        Camera.main.GetComponent<FollowPlayer>().enabled = false;
        Camera.main.transform.position = new Vector3(0, 0, -1);

        isDead = true;
    }

    public void RestartGame()
    {
        if(isDead && Input.GetKeyDown(KeyCode.Space))
        {
            // Reset scores
            sc.orangeScore = 0;
            sc.purpleScore = 0;
            sc.greenScore = 0;
            sc.redScore = 0;
            i = 0;

            // Reset score txt
            orangeTxt.text = "x0";
            purpleTxt.text = "x0";
            greenTxt.text = "x0";
            redTxt.text = "x0";

            // Reset slider values
            orangeSld.value = 0;
            purpleSld.value = 0;
            greenSld.value = 0;
            redSld.value = 0;

            // Enable score canvas
            scoreCnv.GetComponent<Canvas>().enabled = true;

            // Enable high score canvas
            highScoreCnv.GetComponent<Canvas>().enabled = true;

            // Reset score temp varaibles
            myOrangeValue = 0;
            myPurpleValue = 0;
            myGreenValue = 0;
            myRedValue = 0;

            // Disable death canvas
            deathCnv.GetComponent<Canvas>().enabled = false;

            // Enable asteroid spawn
            gm.GetComponent<AsteroidSpawn>().enabled = true;

            // Enable player movement
            pm.isDead = false;

            // Enable player animations
            pm.ani.SetBool("isDead", false);

            isDead = false;

            // Disable camera death settings
            Camera.main.GetComponent<Animator>().SetBool("Dead", false);
            Camera.main.transform.parent.GetComponent<Animator>().SetBool("DeadParent", false);
            Camera.main.GetComponent<FollowPlayer>().enabled = true;
            Camera.main.transform.parent.GetComponent<Animator>().ResetTrigger("shake");
        }

        if(isDead && Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("SampleScene");
        }
    }
}
