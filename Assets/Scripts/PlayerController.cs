using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour 
{
	public float speed;

	private void Start()
	{

	}
	private void Update()
	{
		FlightMove();
	}

	private void FlightMove()
	{
		float verControl = Input.GetAxis("Vertical");
		float horControl = Input.GetAxis("Horizontal");

		Vector3 move = transform.position + new Vector3(horControlverControl,,0.0f) * speed;
		transform.position = move;
	}
}
