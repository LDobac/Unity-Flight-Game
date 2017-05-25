using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour 
{
	[System.Serializable]
	public class Boundary
	{
		public float minX;
		public float minY;
		public float maxX;
		public float maxY;
	}

	public float moveSpeed;
	public float shotDelay;
	public float bulletSpeed;
	public Boundary moveLimit;

	private float shotDelayTimer = 0.0f;
	private GameObject bulletSpawnPlace;
	private PlayerBulletMemPool bulletPool;

	private void Start()
	{
		bulletSpawnPlace = transform.GetChild(0).gameObject;

		bulletPool = GetComponent<PlayerBulletMemPool>();
	}
	private void Update()
	{
		FlightMove();

		shotDelayTimer += Time.deltaTime;
		if(shotDelayTimer >= shotDelay)
		{
			if(Input.GetKey(KeyCode.Space))
			{
				ShotBullet();

				shotDelayTimer = 0.0f;
			}
		}
	}

	private void FlightMove()
	{
		float verControl = Input.GetAxis("Vertical");
		float horControl = Input.GetAxis("Horizontal");

		Vector3 move = (new Vector3(horControl,verControl,0.0f) * moveSpeed) * Time.deltaTime;
		transform.Translate(move);

		transform.position = new Vector3(
			Mathf.Clamp(transform.position.x,moveLimit.minX,moveLimit.maxX),
			Mathf.Clamp(transform.position.y,moveLimit.minY,moveLimit.maxY),
			0.0f);
	}

	private void ShotBullet()
	{
		PlayerBullet bullet = bulletPool.RequestIdleBullet();
		bullet.Init(bulletSpawnPlace.transform.position,Vector3.up,bulletSpeed);
	}
}
