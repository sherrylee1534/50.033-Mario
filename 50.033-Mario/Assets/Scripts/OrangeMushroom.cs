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
}