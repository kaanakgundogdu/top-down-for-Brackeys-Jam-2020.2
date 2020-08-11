using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeBody : MonoBehaviour
{

	bool isRewinding = false;

	//public Text myText;

	public float recordTime = 5f;

	public float delayRewind = 5f;
	public float _timeBetweenRewind= 10f;

	List<PointInTime> pointsInTime;

	Rigidbody2D rb;

	void Start()
	{

		pointsInTime = new List<PointInTime>();
		rb = GetComponent<Rigidbody2D>();
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.R) && Time.time >= delayRewind)
		{
			//myText.text = "Rewind Time";
			StartRewind(); 
			delayRewind = Time.time + _timeBetweenRewind;
			
		}
		/*else if (Time.time < delayRewind)
			{
			myText.text = "Wait for 5 seconds to Use or ReUse Rewind";
			}*/
		if (Input.GetKeyUp(KeyCode.R))
			StopRewind();
	}

	void FixedUpdate()
	{
		if (isRewinding)
			Rewind();
		else
			Record();
	}

	void Rewind()
	{
		if (pointsInTime.Count > 0)
		{
			PointInTime pointInTime = pointsInTime[0];
			transform.position = pointInTime.position;
			transform.rotation = pointInTime.rotation;
			pointsInTime.RemoveAt(0);
		}
		else
		{
			StopRewind();
		}

	}

	void Record()
	{
		if (pointsInTime.Count > Mathf.Round(recordTime / Time.fixedDeltaTime))
		{
			pointsInTime.RemoveAt(pointsInTime.Count - 1);
		}

		pointsInTime.Insert(0, new PointInTime(transform.position, transform.rotation));
	}

	public void StartRewind()
	{
		isRewinding = true;
		rb.isKinematic = true;
	}

	public void StopRewind()
	{
		isRewinding = false;
		rb.isKinematic = false;
	}
}