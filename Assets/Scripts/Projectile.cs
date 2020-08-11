using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
	public float _speed ;
	public float _lifeTime;
	public int _damage;

	[SerializeField] GameObject _vfx;

	public GameObject soundObj;

	private void Start()
	{
		Invoke("DestroyProjectile", _lifeTime);
		Instantiate(soundObj , transform.position , transform.rotation);
	}

	private void Update()
	{
		transform.Translate(Vector2.up * _speed * Time.deltaTime);
	}

	private void DestroyProjectile()
	{
		Instantiate(_vfx, transform.position, Quaternion.identity);
		Destroy(gameObject);
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{

		if ( collision.tag == "Enemy")
		{
			collision.GetComponent<Enemy>().TakeDamage(_damage);
			DestroyProjectile();
		}
		if (collision.tag == "Boss")
		{
			collision.GetComponent<Boss>().TakeDamage(_damage);
			DestroyProjectile();
		}
	}

}
