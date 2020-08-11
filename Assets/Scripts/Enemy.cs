using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public int health;
	public float speed;
	public float timeBetweenAttack;
	public int damage;	

	[HideInInspector]
	public Transform player;

	[SerializeField] private int _pickupChance= 30;
	public GameObject[] pickups;

	public int healthPickupChance;
	public GameObject healthPickup;

	public GameObject deathEffect;

	public virtual void Start()
	{
		player = GameObject.FindGameObjectWithTag("Player").transform;
	}

	public void TakeDamage( int damage )
	{
		health -= damage;

		if(health <= 0)
		{
			int randomNumber = Random.Range(0, 101);
			int randHealth = Random.Range(0, 101);
			if (randomNumber < _pickupChance)
			{
				GameObject randomPickup = pickups[Random.Range(0, pickups.Length)];
				Instantiate(randomPickup, transform.position, transform.rotation);
			}

			else if (randHealth < healthPickupChance)
			{
				Instantiate(healthPickup, transform.position, transform.rotation);
			}
			Instantiate(deathEffect, transform.position, Quaternion.identity);
			Destroy(gameObject);
		}
	}
   
}
