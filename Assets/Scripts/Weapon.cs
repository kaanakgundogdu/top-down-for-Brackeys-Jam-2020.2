using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Diagnostics;
using UnityEngine;

public class Weapon : MonoBehaviour
{
	private float _angle;
	public float _timeBetweenShots = 0.5f;
	private float _shotTime;

	[SerializeField] private GameObject _projectile;
	[SerializeField] private Transform _shotPoint;
	private Vector2 _direction;
	Animator _cameraAnim;

	private void Start()
	{
		_cameraAnim = Camera.main.GetComponent<Animator>();
	}

	private void Update()
	{
		// ScreenTO world point: normalde mouse position pixel de onu world pointe çeviirip transformla işleme sokabiliyoruz
		_direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;

		_angle = Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg;

		Quaternion _rotation = Quaternion.AngleAxis(_angle - 90 , Vector3.forward);

		transform.rotation = _rotation;

		if(Input.GetMouseButton(0))
		{
			if (Time.time >= _shotTime)
			{
				Instantiate(_projectile, _shotPoint.position, transform.rotation);
				_cameraAnim.SetTrigger("Shake");
				_shotTime = Time.time + _timeBetweenShots;//ateş etmeden once ne kadar süre beklmeli onu tekrardan hesaplıyoz
			}
		}

	}




}
