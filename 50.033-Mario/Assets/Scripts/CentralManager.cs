using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CentralManager : MonoBehaviour
{
    public GameObject gameManagerObject;
	public static CentralManager centralManagerInstance;

	private GameManager _gameManager;

    // Start is called before the first frame update
    void Start()
    {
        _gameManager = gameManagerObject.GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	void Awake()
    {
		centralManagerInstance = this;
    }

    public void increaseScore()
    {
		_gameManager.increaseScore();
	}

    public void damagePlayer()
    {
	    _gameManager.damagePlayer();
    }

    public void spawnNewEnemy()
    {
        _gameManager.spawnNewEnemy();
    }
}
