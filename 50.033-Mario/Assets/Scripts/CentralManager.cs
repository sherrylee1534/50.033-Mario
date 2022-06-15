using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CentralManager : MonoBehaviour
{
    public GameObject gameManagerObject;
	public static CentralManager centralManagerInstance;

    // Add reference to PowerupManager
    public GameObject powerupManagerObject;

	private GameManager _gameManager;
    private PowerupManager _powerUpManager;

    // Start is called before the first frame update
    void Start()
    {
        _gameManager = gameManagerObject.GetComponent<GameManager>();
        _powerUpManager = powerupManagerObject.GetComponent<PowerupManager>();
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

    public void consumePowerup(KeyCode k, GameObject g)
    {
	    _powerUpManager.consumePowerup(k, g);
    }

    public void addPowerup(Texture t, int i, ConsumableInterface c)
    {
        _powerUpManager.addPowerup(t, i, c);
    }
}
