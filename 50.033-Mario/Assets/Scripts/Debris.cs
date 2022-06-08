using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debris : MonoBehaviour
{
    private  Rigidbody2D _rigidBody;
    private  Vector3 _scaler;

    // Start is called before the first frame update
    void Start()
    {
        // We want the object to have a scale of 0 (disappear) after 30 frames. 
        _scaler = transform.localScale / (float) 30;
        _rigidBody = GetComponent<Rigidbody2D>();
        StartCoroutine("ScaleOut");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator ScaleOut()
    {
        Vector2 direction = new Vector2(Random.Range(-1.0f, 1.0f), 1);
        _rigidBody.AddForce(direction.normalized * 10, ForceMode2D.Impulse);
        _rigidBody.AddTorque(10, ForceMode2D.Impulse);
        // Wait for next frame
        yield return null;

        // Render for 0.5 second
        for (int step = 0; step < 30; step++)
        {
            this.transform.localScale = this.transform.localScale - _scaler;
            // Wait for next frame
            yield return null;
        }

        Destroy(gameObject);
    }
}
