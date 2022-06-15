using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerupManager : MonoBehaviour
{
	public List<GameObject> powerupIcons;

	private List<ConsumableInterface> _powerups;

	// Start is called before the first frame update
	void  Start()
	{
		_powerups = new List<ConsumableInterface>();

		for (int i = 0; i < powerupIcons.Count; i++)
        {
			powerupIcons[i].SetActive(false);
			_powerups.Add(null);
		}
	}

    public void addPowerup(Texture texture, int index, ConsumableInterface i)
    {
        Debug.Log("Adding powerup");

        if (index < powerupIcons.Count)
        {
            powerupIcons[index].GetComponent<RawImage>().texture = texture;
            powerupIcons[index].SetActive(true);
            _powerups[index] = i;
        }
    }

    public void removePowerup(int index)
    {
        if (index < powerupIcons.Count)
        {
            powerupIcons[index].SetActive(false);
            _powerups[index] =  null;
        }
    }

    void cast(int i, GameObject p)
    {
        if (_powerups[i] != null)
        {
            _powerups[i].consumedBy(p); // Interface method
            removePowerup(i);
        }
    }

    public void consumePowerup(KeyCode k, GameObject player)
    {
        switch(k)
        {
            case KeyCode.Z:
                cast(0, player);
                break;
            case KeyCode.X:
                cast(1, player);
                break;
            default:
                break;
        }
    }
}
