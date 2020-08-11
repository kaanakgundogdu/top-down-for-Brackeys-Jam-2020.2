using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointInTime : MonoBehaviour
{
	public Vector2 position;
	public Quaternion rotation;

	public PointInTime(Vector2 _position, Quaternion _rotation)
	{
		position = _position;
		rotation = _rotation;
	}
}
