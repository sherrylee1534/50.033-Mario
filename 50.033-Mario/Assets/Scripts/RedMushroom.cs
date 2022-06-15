using System.Collections;
using UnityEngine;

public class RedMushroom : MonoBehaviour, ConsumableInterface
{
    public Texture t;

    void Start()
    {

    }
	
	public void consumedBy(GameObject player)
    {
		// Give player jump boost
		player.GetComponent<PlayerController>().upSpeed += 10;
		StartCoroutine(removeEffect(player));
	}

	IEnumerator removeEffect(GameObject player)
    {
		yield return new WaitForSeconds(5.0f);
		player.GetComponent<PlayerController>().upSpeed -= 10;
	}
}
