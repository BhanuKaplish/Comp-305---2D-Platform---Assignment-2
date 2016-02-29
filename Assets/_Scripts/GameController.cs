﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameController : MonoBehaviour {

    //Private Instance variables
    private int _scoreValue;
    private int _livesValue;
    //[SerializeField]
    //private AudioSource _gameoverSound;


    //public Access methods
    public int ScoreValue
    {
        get {
            return _scoreValue;
        }
        set {
            
            this._scoreValue = value;
            this.ScoreLabel1.text = "Score: " + this._scoreValue;
        }
    }
    public int livesValue
    {
        get
        {
            return _livesValue;
        }
        set
        {
            //Debug.Log(this._livesValue);
            this._livesValue = value;
            
            if(this._livesValue <= 0)
            {
                this.LivesLabel1.text = "Lives: 0";
                this._endGame();
            }
            else
            {
                this.LivesLabel1.text = "lives: " + this._livesValue;
            }
        }
    }

    //Public Instance Variables
    // public CoinController coin;
    public Text LivesLabel1;
    public Text ScoreLabel1;
    //public Text GameOverLabel;
    //public Text HighscoreLabel;
    //public Button RestartButton;

    // Use this for initialization
    void Start () {
        initialize();
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    //Private method
    private void initialize()
    {
        this._livesValue = 5;
        this._scoreValue = 0;
        //this.GameOverLabel.enabled = false;
        //this.HighscoreLabel.enabled = false;
        //this.RestartButton.gameObject.SetActive(false);
        
    }
    private void _endGame()
    {
        //this.HighscoreLabel.text = "Highscore : " + this._scoreValue;     
        //this.GameOverLabel.enabled = true;
        //this.HighscoreLabel.enabled = true;
        this.LivesLabel1.enabled = false;
        this.ScoreLabel1.enabled = false;
        //this.plane.gameObject.SetActive(false);
        //this.coin.gameObject.SetActive(false);
        //this.RestartButton.gameObject.SetActive(true);
        //this._gameoverSound.Play();
    }
    //Public methods
    //public void RestartButtonClick()
    //{
    //    Application.LoadLevel("Main");
    //}
}
