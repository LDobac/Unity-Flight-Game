using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : Bullet 
{
	public float removeDistance;

	private SpriteRenderer spriteRenderer;

	private void Start()
	{
		spriteRenderer = GetComponent<SpriteRenderer>();
	}

	protected override void Update()
	{
		base.Update();

		if(transform.position.y >= removeDistance)
		{
			Idle();
		}
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if(other.CompareTag("Monster"))
		{
			Idle();
		}	
	}
	public void Init(Vector3 position,Vector3 dir,float speed)
	{
		transform.position = position;

		direction = dir;

		moveSpeed = speed;

		isIdle = false;
	}

	public override void Idle()
	{
		base.Idle();

		transform.position = new Vector3(-20.0f,-20.0f,0.0f);
	}
}
