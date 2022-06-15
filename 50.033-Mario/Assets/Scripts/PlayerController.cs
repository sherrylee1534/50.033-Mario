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
    public SpriteRenderer marioSprite;
    public ParticleSystem dustCloud;
    public float speed;
    public float maxSpeed = 10;
    public float upSpeed;
    public float downSpeed;

    private Rigidbody2D _marioBody;
    private Animator _marioAnimator;
    private string _resetMain = "Main";
    private float _deathTimer = 3.0f;
    private bool _onGroundState = true;
    private bool _onObstacleState = false;
    private bool _faceRightState = true;
    private bool _isDead = false;
    private bool _isInAir = false;


    // Start is called before the first frame update
    void Start()
    {
        // Set to be 30 FPS
        Application.targetFrameRate =  30;
        _marioBody = GetComponent<Rigidbody2D>();
        marioSprite = GetComponent<SpriteRenderer>();

        menuController = GameObject.Find("UI").GetComponent<MenuController>();

        _marioAnimator = GetComponent<Animator>();

        dustCloud = GameObject.Find("DustCloud").GetComponentInChildren<ParticleSystem>();

        GameManager.OnPlayerDeath += PlayerDiesSequence;
    }

    // Update is called once per frame
    void Update()
    {
        // If Menu is still open, no movement is allowed
        if (menuController._isMenuOn) 
        {
            // Do nothing
        }

        else {
            _marioAnimator.SetFloat("xSpeed", Mathf.Abs(_marioBody.velocity.x));
            _marioAnimator.SetBool("onGround", _onGroundState);
            //_marioAnimator.SetBool("onObstacle", _onObstacleState);

            // Toggle state of direction which Mario is facing
            if (Input.GetKeyDown("a") && _faceRightState){
                _faceRightState = false;
                marioSprite.flipX = true;

                if (Mathf.Abs(_marioBody.velocity.x) >  1.0)
                {
	                _marioAnimator.SetTrigger("onSkid");
                }
            }

            if (Input.GetKeyDown("d") && !_faceRightState){
                _faceRightState = true;
                marioSprite.flipX = false;

                if (Mathf.Abs(_marioBody.velocity.x) >  1.0)
                {
	                _marioAnimator.SetTrigger("onSkid");
                }
            }

            if (_isDead)
            {
                StartCoroutine(PlayerDeathTimer());
            }
        }
    }

    // FixedUpdate may be called once per frame. See documentation for details.
    void FixedUpdate()
    {
        MarioMovement();
        //Debug.Log("_onGroundState: " + _onGroundState + "; _onObstacleState: " + _onObstacleState);

        // Jumping on ground
        if (Input.GetKeyDown("space") && _onGroundState)
        {
            _marioBody.AddForce(Vector2.up * upSpeed, ForceMode2D.Impulse);
            _onGroundState = false;

            AudioManager.instance.PlayerJumpSFX();
        }

        // Jumping on obstacle
        if (Input.GetKeyDown("space") && _onObstacleState)
        {
            _marioBody.AddForce(Vector2.up * upSpeed, ForceMode2D.Impulse);
            _onGroundState = false;

            AudioManager.instance.PlayerJumpSFX();
        }

        // Falling in air
        if (!_onGroundState)
        {
            _marioBody.AddForce(Vector2.down * downSpeed, ForceMode2D.Impulse);

            _isInAir = true;
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
        _onGroundState = true;

        if (_isInAir)
        {
            dustCloud.Play(); // Play DustCloud Particle System animation
        }

        _isInAir = false; // Reset _isInAir
    }

    // For when Mario steps off the edges and he starts defying the laws of physics and falls very slowly
    void OnCollisionExit2D(Collision2D col)
    {
        _onGroundState = false;
        _onObstacleState = false;
        _isInAir = true;
    }

    void PlayerDiesSequence()
    {
        // Mario dies
        // Debug.Log("Mario dies");

        // Mario gets yeeted when he dies
        var yeetSpeed = 200f;
        _marioBody.AddForce(Vector2.up * yeetSpeed, ForceMode2D.Impulse);
        _isDead = true;
    }

    IEnumerator PlayerDeathTimer()
    {
        yield return new WaitForSeconds(0.2f);
        _marioBody.isKinematic = true;
	    _marioBody.gravityScale = 0;
        yield return new WaitForSeconds(_deathTimer - 0.2f);
        SceneManager.LoadScene(_resetMain);
    }
}
