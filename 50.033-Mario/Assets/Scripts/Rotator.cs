using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    public GameConstants gameConstants;

	private Vector3 _rotator;

	// Start is called before the first frame update
	void Start()
	{
		_rotator = new Vector3(0, gameConstants.rotatorRotateSpeed, 0);
	}

	// Update is called once per frame
	void Update()
	{
		// Rotate
		this.transform.rotation = Quaternion.Euler(this.transform.eulerAngles - _rotator);
	}
}
