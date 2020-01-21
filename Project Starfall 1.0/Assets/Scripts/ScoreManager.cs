using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ScoreManager : MonoBehaviour {

    [Header("Score Settings")]
    public int orangeScore = 0;
    public int purpleScore = 0;
    public int greenScore = 0;
    public int redScore = 0;

    [Header("UI Settings")]
    public Text orangeScoreText;
    public Text purpleScoreText;
    public Text greenScoreText;
    public Text redScoreText;

    // Use this for initialization
    void Start () {

        orangeScoreText.text = "x" + orangeScore.ToString();
        purpleScoreText.text = "x" + purpleScore.ToString();
        greenScoreText.text = "x" + greenScore.ToString();
        redScoreText.text = "x" + redScore.ToString();
    }

    // Update is called once per frame
    void Update () {

        orangeScoreText.text = "x" + orangeScore.ToString();
        purpleScoreText.text = "x" + purpleScore.ToString();
        greenScoreText.text = "x" + greenScore.ToString();
        redScoreText.text = "x" + redScore.ToString();
    }
}
