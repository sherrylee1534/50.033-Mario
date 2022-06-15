using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinner : MonoBehaviour
{
    public GameConstants gameConstants;
    public GameObject spinnerCentral;
    public Vector3 axis; // Indicate which axis (x, y or z) it will rotate

    [SerializeField] private Vector3 _radius;

    private Vector3 _rotator;

    // Start is called before the first frame update
    void Start()
    {
        _rotator = new Vector3(0, 0, gameConstants.rotatorRotateSpeed);
    }

     // Update is called once per frame
     void Update ()
     {
        // Gets the position of spinnerCentral and rotates this gameObject around it by a radius, by _rotator magnitude
        transform.RotateAround(spinnerCentral.transform.position + _radius, axis, _rotator.magnitude);
     }

}
