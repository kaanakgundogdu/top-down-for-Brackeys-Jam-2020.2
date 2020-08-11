using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : Enemy
{

	private float _stopDistance;
	private float _attackTime;

	public GameObject bullet;
	public Transform shotPoint;
	private Animator _myAnim;

	private void Awake()
	{
		health = 5;
		speed = 17;
		timeBetweenAttack = 4;
		damage = 1;
		_stopDistance = 5;
	}


	public override void Start()
	{
		base.Start();
		_myAnim = GetComponent<Animator>();
	}

	private void Update()
	{

		if ( player !=null)
		{
			if (Vector2.Distance(transform.position , player.position) > _stopDistance)
			{
				transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
			}

			if (Time.time >= _attackTime)
			{
				_attackTime = Time.time + timeBetweenAttack;
				_myAnim.SetTrigger("Attack");
			}

		}

	}

	public void RangedAttack()
	{
		Vector2 _direction = player.position - shotPoint.position;

		float _angle = Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg;

		Quaternion _rotation = Quaternion.AngleAxis(_angle - 90, Vector3.forward);

		shotPoint.rotation = _rotation;

		Instantiate(bullet, shotPoint.position, shotPoint.rotation);
	}


}
