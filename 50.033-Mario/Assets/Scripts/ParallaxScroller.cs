using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxScroller : MonoBehaviour
{
    // Start is called before the first frame update
	public Renderer[] layers;
	public Transform mario;
	public Transform mainCamera;
	public float[] speedMultiplier;

	private float[] _offset;
	private float _previousXPositionMario;
	private float _previousXPositionCamera;

	void  Start()
	{
		_offset = new float[layers.Length];

		for (int i = 0; i < layers.Length; i++)
        {
			_offset[i] = 0.0f;
		}

		_previousXPositionMario = mario.transform.position.x;
		_previousXPositionCamera = mainCamera.transform.position.x;
	}

    // Update is called once per frame
    void  Update()
    {
        // If camera has moved
        if (Mathf.Abs(_previousXPositionCamera - mainCamera.transform.position.x) > 0.001f)
        {
            for (int i = 0; i < layers.Length; i++)
            {
                if (_offset[i] > 1.0f || _offset[i] < -1.0f)
                {
                    _offset[i] = 0.0f; // Reset offset
                }

                float newOffset = mario.transform.position.x - _previousXPositionMario;
                _offset[i] = _offset[i] + newOffset * speedMultiplier[i];
                layers[i].material.mainTextureOffset = new Vector2(_offset[i], 0);
            }
        }

        // Update previous position
        _previousXPositionMario = mario.transform.position.x;
        _previousXPositionCamera = mainCamera.transform.position.x;
    }
}
