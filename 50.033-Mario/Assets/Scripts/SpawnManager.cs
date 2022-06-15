using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // Spawn two goombaEnemy
        for (int j = 0; j < 2; j++)
        {
            spawnFromPooler(ObjectType.goombaEnemy);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void spawnFromPooler(ObjectType i)
    {
        // Static method access
        GameObject item = ObjectPooler.SharedInstance.GetPooledObject(i);
        if (item != null)
        {
            // Set position, and other necessary states
            item.transform.position = new Vector3(Random.Range(item.transform.parent.position.x - 4.5f, item.transform.parent.position.x + 4.5f), item.transform.position.y, 0);
            item.SetActive(true);
        }

        else
        {
            Debug.Log("Not enough items in the pool.");
        }
    }
}
