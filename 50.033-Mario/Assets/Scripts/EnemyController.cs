using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Vector2 _velocity;
    private Rigidbody2D _enemyBody;
    private float _originalX;
    private float _maxOffset = 5.0f;
    private float _enemyPatroltime = 2.0f;
    private int _moveRight = -1;

    // Start is called before the first frame update
    void Start()
    {
        _enemyBody = GetComponent<Rigidbody2D>();

        // Get the starting position
        _originalX = transform.position.x;
        ComputeVelocity();
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(_enemyBody.position.x - _originalX) < _maxOffset)
        {   // Move Goomba
            MoveGoomba();
        }

        else
        {
            // Change direction
            _moveRight *= -1;
            ComputeVelocity();
            MoveGoomba();
        }
    }

    void ComputeVelocity(){
        _velocity = new Vector2((_moveRight)*_maxOffset / _enemyPatroltime, 0);
    }

    void MoveGoomba(){
        _enemyBody.MovePosition(_enemyBody.position + _velocity * Time.fixedDeltaTime);
    }
}
