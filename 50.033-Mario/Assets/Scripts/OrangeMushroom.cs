using System.Collections;
using UnityEngine;

public class OrangeMushroom : MonoBehaviour, ConsumableInterface
{
    public Texture t;

    void Start()
    {

    }

	public void consumedBy(GameObject player)
    {
		// Give player jump boost
		player.GetComponent<PlayerController>().maxSpeed *= 2;
		StartCoroutine(removeEffect(player));
	}

	IEnumerator removeEffect(GameObject player)
    {
		yield return new WaitForSeconds(5.0f);
		player.GetComponent<PlayerController>().maxSpeed /= 2;
	}

	void OnCollisionEnter2D(Collision2D col)
	{
		if (col.gameObject.CompareTag("Player"))
		{
			// Update UI
			CentralManager.centralManagerInstance.addPowerup(t, 0, this);
			GetComponent<Collider2D>().enabled = false;
		}
	}
}