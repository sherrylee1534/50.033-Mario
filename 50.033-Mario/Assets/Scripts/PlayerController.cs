using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public Transform enemyLocation;
    public Text scoreText;
    public MenuController menuController;
    public SpriteRenderer _marioSprite;
    public float speed;
    public float maxSpeed = 10;
    public float upSpeed;
    public float downSpeed;
    public bool isDead = false;


    private Rigidbody2D _marioBody;
    private int _score = 0;
    private float _deathTimer = 3.0f;
    private string _resetMain = "Main";
    private bool _onGroundState = true;
    private bool _faceRightState = true;
    private bool _countScoreState = false;

    // Start is called before the first frame update
    void  Start()
    {
        // Set to be 30 FPS
        Application.targetFrameRate =  30;
        _marioBody = GetComponent<Rigidbody2D>();
        _marioSprite = GetComponent<SpriteRenderer>();

        menuController = GameObject.Find("UI").GetComponent<MenuController>();
    }

    // Update is called once per frame
    void Update()
    {
        // If Menu is still open, no movement is allowed
        if (menuController._isMenuOn) {
            // Do nothing            
        }

        else {
            // Toggle state of direction which Mario is facing
            if (Input.GetKeyDown("a") && _faceRightState){
                _faceRightState = false;
                _marioSprite.flipX = true;
            }

            if (Input.GetKeyDown("d") && !_faceRightState){
                _faceRightState = true;
                _marioSprite.flipX = false;
            }

            // When jumping, and Gomba is near Mario and we haven't registered our score
            if (!_onGroundState && _countScoreState)
            {
                if (Mathf.Abs(transform.position.x - enemyLocation.position.x) < 0.5f)
                {
                    _countScoreState = false;
                    _score++;
                    Debug.Log(_score);
                }
            }

            // If Mario is dead, reset the scene
            if (isDead)
            {
                if (_deathTimer > 0f)
                {
                    // Simple death animation of flipping Mario's sprite 4 times
                    for (int i = 0; i < 4; i++)
                    {
                        _marioSprite.flipX = true;
                    }
                    _deathTimer -= Time.deltaTime;
                }

                if (_deathTimer == 0f)
                {
                    SceneManager.LoadScene(_resetMain);
                }
            }
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

        // Jumping
        if (Input.GetKeyDown("space") && _onGroundState)
        {
            _marioBody.AddForce(Vector2.up * upSpeed, ForceMode2D.Impulse);
            _onGroundState = false;
            _countScoreState = true; // Check if Gomba is underneath when Mario is jumping
        }

        // Falling
        if (!_onGroundState)
        {
            _marioBody.AddForce(Vector2.down * downSpeed, ForceMode2D.Impulse);
        }
    }

    // Called when Mario hits the floor
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Ground"))
        {
            _onGroundState = true; // Back on ground
            _countScoreState = false; // Reset _countScoreState
            scoreText.text = "Score: " + _score.ToString();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Collided with Gomba!");
            isDead = true;
        }
    }

    // void ResetScene()
    // {
    //     while (_isDead && _deathTimer != 0.0f)
    //     {
    //         _marioSprite.flipX = true;
    //         _deathTimer--;
    //         Debug.Log("Death Timer left: " + _deathTimer);
    //     }
        
    //     if (_deathTimer == 0.0f)
    //     {
    //         Debug.Log("Death Timer left: " + _deathTimer);
    //         Debug.Log("Reloading Scene");
    //         SceneManager.LoadScene("Main");
    //     }
    // }
}
