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

    private Rigidbody2D _marioBody;
    private Animator _marioAnimator;
    private AudioSource _marioAudio;
    private int _score = 0;
    private string _resetMain = "Main";
    private bool _onGroundState = true;
    private bool _onObstacleState = false;
    private bool _faceRightState = true;
    private bool _countScoreState = false;
    private bool _isDead = false;


    // Start is called before the first frame update
    void  Start()
    {
        // Set to be 30 FPS
        Application.targetFrameRate =  30;
        _marioBody = GetComponent<Rigidbody2D>();
        _marioSprite = GetComponent<SpriteRenderer>();

        menuController = GameObject.Find("UI").GetComponent<MenuController>();

        _marioAnimator = GetComponent<Animator>();

        _marioAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        // If Menu is still open, no movement is allowed
        if (menuController._isMenuOn) {
            // Do nothing            
        }

        else {
            _marioAnimator.SetFloat("xSpeed", Mathf.Abs(_marioBody.velocity.x));
            _marioAnimator.SetBool("onGround", _onGroundState);
            //_marioAnimator.SetBool("onObstacle", _onObstacleState);

            // Toggle state of direction which Mario is facing
            if (Input.GetKeyDown("a") && _faceRightState){
                _faceRightState = false;
                _marioSprite.flipX = true;

                if (Mathf.Abs(_marioBody.velocity.x) >  1.0)
                {
	                _marioAnimator.SetTrigger("onSkid");
                }
            }

            if (Input.GetKeyDown("d") && !_faceRightState){
                _faceRightState = true;
                _marioSprite.flipX = false;

                if (Mathf.Abs(_marioBody.velocity.x) >  1.0)
                {
	                _marioAnimator.SetTrigger("onSkid");
                }
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

            if (_isDead)
            {
                SceneManager.LoadScene(_resetMain);
            }
        }
    }

    // FixedUpdate may be called once per frame. See documentation for details.
    void FixedUpdate()
    {
        MarioMovement();
        Debug.Log("_onGroundState: " + _onGroundState + "; _onObstacleState: " + _onObstacleState);

        // Jumping on ground
        if (Input.GetKeyDown("space") && _onGroundState)
        {
            _marioBody.AddForce(Vector2.up * upSpeed, ForceMode2D.Impulse);
            _onGroundState = false;

            _countScoreState = true; // Check if Gomba is underneath when Mario is jumping
        }

        // Jumping on obstacle
        if (Input.GetKeyDown("space") && _onObstacleState)
        {
            _marioBody.AddForce(Vector2.up * upSpeed, ForceMode2D.Impulse);
            _onGroundState = false;
        }

        // Falling in air
        if (!_onGroundState)
        {
            _marioBody.AddForce(Vector2.down * downSpeed, ForceMode2D.Impulse);
        }
    }

        void MarioMovement()
    {
        // Dynamic Rigidbody
        float moveHorizontal = Input.GetAxis("Horizontal");

        if (Mathf.Abs(moveHorizontal) > 0)
        {
            Vector2 movement = new Vector2(moveHorizontal, 0);
            if (_marioBody.velocity.magnitude < maxSpeed)
            {
                _marioBody.AddForce(movement * speed);
            }
        }

        if (Input.GetKeyUp("a") || Input.GetKeyUp("d"))
        {
            // Stop
            _marioBody.velocity = Vector2.zero;
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        // Called when Mario hits the ground
        if (col.gameObject.CompareTag("Ground"))
        {
            Debug.Log("Colliding with: " + col.gameObject.name);
            _onGroundState = true; // Back on ground

            _countScoreState = false; // Reset _countScoreState
            scoreText.text = "Score: " + _score.ToString();

        }

        // Called when Mario hits the obstacle
        if (col.gameObject.CompareTag("Obstacle"))
        {
            Debug.Log("Colliding with: " + col.gameObject.name);
            _onGroundState = true; // Back on "ground" -> obstacle
        }
    }

    // For when Mario steps off the edges and he starts defying the laws of physics and falls very slowly
    void OnCollisionExit2D(Collision2D col)
    {
        _onGroundState = false;
        _onObstacleState = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Collided with Gomba!");
            _isDead = true;
        }
    }

    void PlayJumpSound()
    {
        _marioAudio.PlayOneShot(_marioAudio.clip);
    }
}
