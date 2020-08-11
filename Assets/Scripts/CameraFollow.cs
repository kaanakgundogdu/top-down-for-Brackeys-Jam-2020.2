using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform _playerTransform;
	private float _followSpeed = 0.125f;

	private float _clampedX;
	private float _clampedY;
	private float minX = -12f;
	private float maxX = +11f;
	private float minY = -6.1f;
	private float maxY = 5f;
	

	private void Start()
	{
		transform.position = _playerTransform.position;
	}

	private void Update()
	{
		if (_playerTransform != null)
		{
			_clampedX = Mathf.Clamp(_playerTransform.position.x, minX, maxX);
			_clampedY = Mathf.Clamp(_playerTransform.position.y, minY, maxY);

			//if değeri buradan yukarı alındı
			transform.position = Vector2.Lerp(transform.position, new Vector2 (_clampedX, _clampedY)  , _followSpeed);
		}
		else
		{
			return;
		}
	}


}
