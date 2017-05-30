using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallMonster : Monster
{

	protected override void Start()
	{
		health = 1;
		speed = 10.0f;
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if(other.CompareTag("Player"))
		{
			
		}
	}
}
