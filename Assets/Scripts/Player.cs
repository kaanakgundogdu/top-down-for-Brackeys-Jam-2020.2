using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
	private float _speed = 5f;
	private float _horizontal;
	private float _vertical;
	private int _health= 5;

	public GameObject[] hearts;
	public Sprite fullHeart;
	public Sprite emptyHeart;

	private Rigidbody2D _myRig;

	private Vector2 _moveAmount;

	private Animator _anim;

	private SceneTransition _loseScene;

	private void Start()
	{
		_myRig = GetComponent<Rigidbody2D>();
		_anim = GetComponent<Animator>();
		_loseScene = FindObjectOfType<SceneTransition>();
	}

	private void Update()
	{
		_horizontal = Input.GetAxisRaw("Horizontal");
		_vertical = Input.GetAxisRaw("Vertical");
		Vector2 moveInput = new Vector2(_horizontal, _vertical);
		_moveAmount = moveInput.normalized * _speed; // normalized çok hızlı olmasın diye

		if (moveInput != Vector2.zero)
		{
			_anim.SetBool("isRunning",true);
		}
		else
		{
			_anim.SetBool("isRunning", false);
		}

	}

	private void FixedUpdate()
	{
		_myRig.MovePosition(_myRig.position + _moveAmount * Time.fixedDeltaTime);


	}


	public void TakeDamage(int damage)
	{
		_health -= damage;
		UpdateHealthUI(_health);

		if (_health <= 0)
		{
			Destroy(gameObject);
			_loseScene.LoadScene("Lose");
		}
	}

	public void ChangeWeapon(Weapon weaponToEquip)
	{
		
		Destroy(GameObject.FindGameObjectWithTag("Weapon"));
		Instantiate(weaponToEquip, transform.position + new Vector3(0.288f,0,0), Quaternion.identity, transform);
	}

	void UpdateHealthUI(int currentHealth)
	{

		for (int i = 0; i < hearts.Length; i++)
		{

			if (i < currentHealth)
			{
				hearts[i].GetComponent<Image>().sprite = fullHeart;
			}
			else
			{
				hearts[i].GetComponent<Image>().sprite = emptyHeart;
			}

		}

	}
	public void Heal(int healAmount)
	{
		if (_health + healAmount > 5)
		{
			_health = 5;
		}
		else
		{
			_health += healAmount;
		}
		UpdateHealthUI(_health);
	}


}
