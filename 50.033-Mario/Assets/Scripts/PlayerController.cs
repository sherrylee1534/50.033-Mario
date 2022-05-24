using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float maxSpeed = 10;
    public float upSpeed;


    private Rigidbody2D _marioBody;
    private bool _onGroundState = true;
    private SpriteRenderer _marioSprite;
    private bool _faceRightState = true;


    // Start is called before the first frame update
    void  Start()
    {
        // Set to be 30 FPS
        Application.targetFrameRate =  30;
        _marioBody = GetComponent<Rigidbody2D>();

        _marioSprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        // Toggle state of direction which Mario is facing
        if (Input.GetKeyDown("a") && _faceRightState){
            _faceRightState = false;
            _marioSprite.flipX = true;
        }

        if (Input.GetKeyDown("d") && !_faceRightState){
            _faceRightState = true;
            _marioSprite.flipX = false;
        }
    }

    // FixedUpdate may be called once per frame. See documentation for details.
    void FixedUpdate()
    {
        // Dynamic Rigidbody
        float moveHorizontal = Input.GetAxis("Horizontal");
        if (Mathf.Abs(moveHorizontal) > 0){
            Vector2 movement = new Vector2(moveHorizontal, 0);
            if (_marioBody.velocity.magnitude < maxSpeed) {
                _marioBody.AddForce(movement * speed);
            }
        }

        if (Input.GetKeyUp("a") || Input.GetKeyUp("d")){
            // Stop
            _marioBody.velocity = Vector2.zero;
        }

        if (Input.GetKeyDown("space") && _onGroundState)
        {
            _marioBody.AddForce(Vector2.up * upSpeed, ForceMode2D.Impulse);
            _onGroundState = false;
        }
    }

    // Called when Mario hits the floor
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Ground"))
        {
            _onGroundState = true;
        }
    }

      void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Collided with Gomba!");
        }
    }
}
