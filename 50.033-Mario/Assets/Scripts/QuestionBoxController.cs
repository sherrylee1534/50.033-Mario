using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionBoxController : MonoBehaviour
{
    public Rigidbody2D rigidBody;
    public SpringJoint2D springJoint;
    public List<GameObject> consummablePrefabs; // The spawned mushroom prefab
    public SpriteRenderer spriteRenderer;
    public Sprite usedQuestionBox; // The sprite that indicates empty box instead of a question mark

    private bool _hit =  false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player") &&  !_hit){
            _hit  =  true;
            // Ensure that we move this object sufficiently 
		    rigidBody.AddForce(new Vector2(0, rigidBody.mass * 20), ForceMode2D.Impulse);

            var rand = Random.Range(0, 2); // Get a random number to spawn a random mushroom

            // Spawn the mushroom prefab slightly above the box
            Instantiate(consummablePrefabs[rand], new Vector3(this.transform.position.x, this.transform.position.y  +  1.0f, this.transform.position.z), Quaternion.identity);

            // Begin check to disable object's spring and rigidbody
            StartCoroutine(DisableHittable());
        }
    }

    bool ObjectMovedAndStopped()
    {
	    return Mathf.Abs(rigidBody.velocity.magnitude) < 0.01;
    }

    IEnumerator DisableHittable()
    {
        if (!ObjectMovedAndStopped())
        {
            yield return new WaitUntil(() => ObjectMovedAndStopped());
        }

        // Continues here when the ObjectMovedAndStopped() returns true
        spriteRenderer.sprite = usedQuestionBox; // Change sprite to be "used-box" sprite
        rigidBody.bodyType = RigidbodyType2D.Static; // Make the box unaffected by Physics

        springJoint.enabled = false; // Disable spring
    }
}
