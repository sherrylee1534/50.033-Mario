using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// State the types of objects we want to instantiate in our pool
public enum ObjectType
{
    goombaEnemy = 0,
    greenTurtleEnemy = 1
}

// Helper class to define the data structure of an Object metadata to be spawned in the pool
[System.Serializable]
public class ObjectPoolItem
{
	public int amount;
	public GameObject prefab;
	public bool expandPool;
	public ObjectType type;
}

// Helper class to define the data structure of an Object in the pool
public class ExistingPoolItem
{
	public GameObject gameObject;
	public ObjectType type;

	// Constructor
	public ExistingPoolItem(GameObject gameObject, ObjectType type)
    {
		// Reference input
		this.gameObject = gameObject;
		this.type = type;
	}
}

public class ObjectPooler : MonoBehaviour
{
    public static ObjectPooler SharedInstance;
    public List<ObjectPoolItem> itemsToPool; // Types of different objects to pool
    public List<ExistingPoolItem> pooledObjects; // A list of all objects in the pool, of all types

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Awake()
    {
        SharedInstance = this;

        pooledObjects = new List<ExistingPoolItem>();

        // Instantiate pool
        foreach (ObjectPoolItem item in itemsToPool)
        {
            for (int i = 0; i < item.amount; i++)
            {
                // This 'pickup' a local variable, but Unity will not remove it since it exists in the scene
                GameObject pickup = (GameObject)Instantiate(item.prefab);
                pickup.SetActive(false);
                pickup.transform.parent = this.transform;
                // ExistingPoolItem e = new ExistingPoolItem(pickup, item.type);
                // pooledObjects.Add(e);
                pooledObjects.Add(new ExistingPoolItem(pickup, item.type));
            }
        }
    }

    public GameObject GetPooledObject(ObjectType type)
    {
        // Return inactive pooled object if it matches the type
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].gameObject.activeInHierarchy && pooledObjects[i].type == type)
            {
                return pooledObjects[i].gameObject;
            }
        }

        // This will be called when no more active object is present, item to expand pool if required
        // Drawback with this is that you have to loop through both pooledObjects and itemsToPool whenever this method is called and there’s no available object to return
        foreach (ObjectPoolItem item in itemsToPool)
        {
            if (item.type == type)
            {
                if (item.expandPool)
                {
                    GameObject pickup = (GameObject)Instantiate(item.prefab);
                    pickup.SetActive(false);
                    pickup.transform.parent = this.transform;
                    pooledObjects.Add(new ExistingPoolItem(pickup, item.type));

                    return pickup;
                }
            }
        }

        return null;
    }
}
