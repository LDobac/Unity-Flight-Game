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

	public override void Reset()
	{
		base.Reset();

		health = 1;
		speed = 10.0f;

		gameObject.SetActive(true);
	}
}
