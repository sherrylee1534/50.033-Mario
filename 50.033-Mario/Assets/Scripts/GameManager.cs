using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text score;
    public delegate void gameEvent();
    public static event gameEvent OnPlayerDeath;
    public static event gameEvent OnIncreaseScore;

	private int _playerScore = 0;

    // Start is called before the first frame update
    void Start()
    {
        // To set and display _playerScore string as 0
        _playerScore = 0;
        score.text = "SCORE: " + _playerScore.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void increaseScore()
    {
		_playerScore += 1;
		score.text = "SCORE: " + _playerScore.ToString();
	}

    public void damagePlayer()
    {
	    OnPlayerDeath();
    }

    public void spawnNewEnemy()
    {
        OnIncreaseScore();
    }
}
