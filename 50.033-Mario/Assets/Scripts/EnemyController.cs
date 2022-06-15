using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public GameConstants gameConstants;

	private Vector2 _velocity;
	private Rigidbody2D _enemyBody;
    private SpriteRenderer _enemySprite;
    private int _moveRight;
	private float _originalX;

    // Start is called before the first frame update
    void Start()
    {
        _enemyBody = GetComponent<Rigidbody2D>();
        _enemySprite = GetComponent<SpriteRenderer>();

        // Get the starting position
        _originalX = transform.position.x;

        // Randomise initial direction
		_moveRight = Random.Range(0, 2) == 0 ? -1 : 1;
		
		// Compute initial velocity
        ComputeVelocity();

        // Subscribe to player event
        GameManager.OnPlayerDeath += EnemyRejoice;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Mathf.Abs(_enemyBody.position.x - _originalX) < gameConstants.maxOffset)
        {
            // Move enemy
            MoveEnemy();
            ChangeEnemySpriteDirection();
        }

        else
        {
            // Change direction
            _moveRight *= -1;
            ComputeVelocity();
            MoveEnemy();
            ChangeEnemySpriteDirection();
        }
    }

    void ComputeVelocity()
    {
        _velocity = new Vector2((_moveRight) * gameConstants.maxOffset / gameConstants.enemyPatrolTime, 0);
    }

    void MoveEnemy()
    {
        _enemyBody.MovePosition(_enemyBody.position + _velocity * Time.fixedDeltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if it collides with Mario
        if (other.gameObject.tag == "Player")
        {
            // Check if collides on top
            float yOffset = (other.transform.position.y - this.transform.position.y);
            if (yOffset > 0.75f)
            {
                KillSelf();
            }

            else
            {
                // Hurt player, implement later
                CentralManager.centralManagerInstance.damagePlayer();
            }
        }
	}

    void KillSelf()
    {
		// Enemy dies
		CentralManager.centralManagerInstance.increaseScore();
		StartCoroutine(Flatten());
		//Debug.Log("Kill sequence ends");
        CentralManager.centralManagerInstance.spawnNewEnemy(); // Spawn new enemy
	}

    IEnumerator Flatten()
    {
		// Debug.Log("Flatten starts");

		int steps = 5;
		float stepper = 1.0f / (float)steps;

		for (int i = 0; i < steps; i++)
        {
			this.transform.localScale = new Vector3(this.transform.localScale.x, this.transform.localScale.y - stepper, this.transform.localScale.z);

			// Make sure enemy is still above ground
			this.transform.position = new Vector3(this.transform.position.x, gameConstants.groundSurface + GetComponent<SpriteRenderer>().bounds.extents.y, this.transform.position.z);
			yield return null;
		}

		// Debug.Log("Flatten ends");
		this.gameObject.SetActive(false);
		// Debug.Log("Enemy returned to pool");
		yield break;
	}

    // Enemy animation when player is dead
    void EnemyRejoice()
    {
        // Debug.Log("Enemy killed Mario");

        // Simple rejoice animation of the enemy turning left and right 5 times
        for (int i = 0; i < 5; i++)
        {
            _moveRight *= -1;
            ChangeEnemySpriteDirection();
        }
    }

    void ChangeEnemySpriteDirection()
    {
        // Enemy moving left
        if (_moveRight == 1)
        {
            // Great! Facing the right direction
            _enemySprite.flipX = false;
        }

        // Enemy moving right
        else
        {
            _enemySprite.flipX = true;
        }
    }
}
