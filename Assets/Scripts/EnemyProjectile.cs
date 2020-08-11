using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEditor;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
	private float _speed = 10f;
	private int _damage=1;
	private Player _playerScript;
	private Vector2 _targetPos;

	public GameObject effect;
	private void Start()
	{
		_playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
		_targetPos = _playerScript.transform.position;
	}

	private void Update()
	{
		if ((Vector2)transform.position == _targetPos)
		{
			Instantiate(effect, transform.position, Quaternion.identity);
			Destroy(gameObject);
		}
		else
		{
			transform.position = Vector2.MoveTowards(transform.position, _targetPos, _speed * Time.deltaTime);
		}

	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.tag == "Player")
		{
			_playerScript.TakeDamage(_damage);
			Destroy(gameObject);
		}
	}

}

