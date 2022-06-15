using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameManager.OnIncreaseScore += SpawnNewEnemy;

        // // Spawn 2 goombaEnemy
        // for (int i = 0; i < 2; i++)
        // {
        //     spawnFromPooler(ObjectType.goombaEnemy);
        //     spawnFromPooler(ObjectType.greenTurtleEnemy);
        // }

        // Spawn 3 greenTurtleEnemy
        for (int i = 0; i < 2; i++)
        {
            spawnFromPooler(ObjectType.greenTurtleEnemy);
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

    public void SpawnNewEnemy()
    {
        var rand = Random.Range(0, 2);
        
        switch(rand)
        {
            case 0:
                spawnFromPooler((ObjectType)0);
                break;
            case 1:
                spawnFromPooler((ObjectType)1);
                break;
            default:
                spawnFromPooler((ObjectType)0);
                break;
        }
    }
}
