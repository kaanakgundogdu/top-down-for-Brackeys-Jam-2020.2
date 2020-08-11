using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleEnemy : Enemy
{

    private float _stopDistance;
	private float _attackTime;
	private float _attackSpeed;


	private void Awake()
	{
		health = 5;
		speed = 3f;
		timeBetweenAttack = 3f;
		_stopDistance = 1.4f;
		_attackSpeed = 3f;
		damage = 2;

	}


	private void Update()
	{
		if(player != null)
		{
			if(Vector2.Distance(transform.position , player.position) > _stopDistance) //Eğer playredan çok uzak isek
			{
				transform.position = Vector2.MoveTowards( transform.position , player.position , speed *Time.deltaTime )	;
			}

			else
			{
				if(Time.time >= _attackTime)
				{
					//attaack
					StartCoroutine(Attack());
					_attackTime = Time.time + timeBetweenAttack;
				}
			}
		}
	}

	IEnumerator Attack()
	{
		player.GetComponent<Player>().TakeDamage(damage);

		Vector2 originalPosition = transform.position;

		Vector2 targetPosition = player.position;

		float _percent = 0;

		while (_percent <= 1)
		{
			_percent += Time.deltaTime * _attackSpeed;

			float _formula = (-Mathf.Pow(_percent, 2) + _percent) * 4;

			transform.position = Vector2.Lerp(originalPosition, targetPosition, _formula);

			yield return null;
		}

	}



}
