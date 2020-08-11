using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Summoner : Enemy
{
	public float minX;
	public float maxX;
	public float minY;
	public float maxY;
	private float _timeBetweenSummon = 5f;
	private float _summonTime;
	private float _stopDistance = 2f;
	private float _attackSpeed = 3f;
	private float _timer = 3f;


	private Vector2 _targetPosition;
	private Animator _myAnim;
	public Enemy enemyToSum;

	public float run = 1f;


	private void Awake()
	{
		health = 10;
		speed = 8;
		timeBetweenAttack = 2;
		damage = 2;
		minX = -15;
		maxX = 15;
		minY = -5;
		maxY = 5;
	}
	public override void Start()
	{
		base.Start();

		float _randomX = Random.Range(minX, maxX);
		float _randomY = Random.Range(minY, maxY);

		_targetPosition = new Vector2(_randomX, _randomY);

		_myAnim = GetComponent<Animator>();

	}


	private void Update()
	{
		if (player != null)
		{

			if (Vector2.Distance(transform.position, _targetPosition) > run)
			{
				transform.position = Vector2.MoveTowards(transform.position, _targetPosition, speed * Time.deltaTime);
				_myAnim.SetBool("isRunning", true);
			}
			else
			{
				_myAnim.SetBool("isRunning", false);

				if (Time.time >= _summonTime)
				{
					_summonTime = Time.time + _timeBetweenSummon;
					_myAnim.SetTrigger("summoning");
				}

			}
			if (Vector2.Distance(transform.position, player.position) < _stopDistance) //Eğer playredan çok uzak isek
			{
				if (Time.time >= _timer)
				{
					//attaack
					_timer = Time.time + timeBetweenAttack;
					StartCoroutine(Attack());
				}
			}
		}
	}

	public void Summon()
	{
		if (player != null)
		{
			Instantiate(enemyToSum, transform.position, transform.rotation);
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
