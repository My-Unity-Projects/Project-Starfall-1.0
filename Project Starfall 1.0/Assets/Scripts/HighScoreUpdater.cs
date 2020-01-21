using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScoreUpdater : MonoBehaviour {

    public Text highScoreTxt;

	// Use this for initialization
	void Start () {

        highScoreTxt.text = "high score\n\n" + PlayerPrefs.GetFloat("High Score").ToString();
	}
	
	// Update is called once per frame
	void Update () {

        highScoreTxt.text = "high score\n\n" + PlayerPrefs.GetFloat("High Score").ToString();
    }
}
