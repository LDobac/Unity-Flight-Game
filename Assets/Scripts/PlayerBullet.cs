using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : Bullet 
{
	protected int damege = 0;
	protected Boundary idleBoundary;
	protected SpriteRenderer spriteRenderer;

	private void Start()
	{
		spriteRenderer = GetComponent<SpriteRenderer>();
	}

	protected override void Update()
	{
		if(!isIdle)
		{
			base.Update();

			if(transform.position.x > idleBoundary.maxX || transform.position.x < idleBoundary.minX ||
				transform.position.y > idleBoundary.maxY || transform.position.y < idleBoundary.minY)
			{
				Idle();
			}
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
	public void Init(Vector3 pos,Vector2 dir, float vel,int dmg,Boundary boundary)
	{
		base.Init(vel,0.0f,dir,1.0f);

		transform.position = pos;

		damege = dmg;

		idleBoundary = boundary;
	}

	public override void Idle()
	{
		base.Idle();

		transform.position = new Vector3(-20.0f,-20.0f,0.0f);
	}
}
