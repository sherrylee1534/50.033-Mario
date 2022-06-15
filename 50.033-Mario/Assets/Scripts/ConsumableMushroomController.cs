using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumableMushroomController : MonoBehaviour
{
    private Rigidbody2D _consumableMushroomBody;
    private Animator _mushroomAnimator;
    private float _speed = 5.0f;
    private float _randomFloat;
    private float _originalX;
    private bool _collected = false;

    // Start is called before the first frame update
    void Start()
    {
        _consumableMushroomBody = GetComponent<Rigidbody2D>();

        _originalX = transform.position.x; // Get the starting position
        _randomFloat = Random.Range(-1.0f, 1.0f); // Get a random Integer to decide direction

        // To check
        //_randomInt = Random.Range(0.0f, 1.0f); // Always spawns right
        //_randomInt = Random.Range(-1.0f, 0.0f); // Always spawns left
        //Debug.Log("Random Int: " + _randomInt);

        _consumableMushroomBody.AddForce(Vector2.up * 10, ForceMode2D.Impulse);

        _mushroomAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // Move right
        if (_randomFloat >= 0 && _randomFloat <= 1)
        {
            MoveConsumableMushroom(Mathf.Sign(_randomFloat));
        }

        // Move left
        else
        {
            MoveConsumableMushroom(Mathf.Sign(_randomFloat));
        }

        if (_collected)
        {
            _mushroomAnimator.SetTrigger("collected");
            StartCoroutine(DestroyMushroom());
        }
    }

    void MoveConsumableMushroom(float direction)
    {
        _consumableMushroomBody.velocity = new Vector2(direction * _speed, _consumableMushroomBody.velocity.y);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        // Flip direction when it hits Pipe
        if (col.gameObject.CompareTag("Pipe"))
        {
            // Change direction
            _randomFloat *= -1;

            // Move right
            if (_randomFloat >= 0 && _randomFloat <= 1)
            {
                MoveConsumableMushroom(Mathf.Sign(_randomFloat));
            }

            // Move left
            else
            {
                MoveConsumableMushroom(Mathf.Sign(_randomFloat));
            }
        }

        // Stop the mushroom movement when it hits Mario
        if (col.gameObject.CompareTag("Player"))
        {
            _collected = true;
        }
    }

    // Mushroom disappears when no longer in camera view
    void OnBecameInvisible()
    {
	    Destroy(gameObject);	
    }

    IEnumerator DestroyMushroom()
    {
        //_speed = 0; // Stop mushroom movement
        yield return new WaitForSeconds(0.25f);
        Destroy(gameObject);
    }
}
