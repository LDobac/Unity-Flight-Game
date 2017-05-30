using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : Bullet 
{
	public float removeDistance;

	protected int damege = 0;
	protected SpriteRenderer spriteRenderer;

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
			other.GetComponent<Monster>().Hit(damege);
			Idle();
		}	
	}
	public void Init(Vector3 position,Vector2 dir, float vel,int damege)
	{
		base.Init(vel,0.0f,dir,0.0f);

		transform.position = position;

		this.damege = damege;
	}

	public override void Idle()
	{
		base.Idle();

		transform.position = new Vector3(-20.0f,-20.0f,0.0f);
	}
}
