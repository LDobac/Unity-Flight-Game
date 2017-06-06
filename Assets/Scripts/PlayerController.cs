using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour 
{
	[System.Serializable]
	private class BulletData
	{
		public int damege = 0;
		public float shotDelay = 0.0f;
		public float velocity = 0.1f;
		public float acceleration = 0.0f;
		public float angleAcceleration = 0.0f;
		public Vector2 direction = Vector2.up;
		public Boundary moveLimit = new Boundary(0.0f,0.0f,0.0f,0.0f); 
	}
	private class PlayerBulletPool : ObjectPool<PlayerBullet>
	{
		private GameObject bulletPrefab = null;
		
		public PlayerBulletPool()
			:base(100)
		{
			bulletPrefab = Resources.Load("Prefab/Player Bullet",typeof(GameObject)) as GameObject;
			if(bulletPrefab == null)
			{
				Debug.Log("Load Player Bullet Failed!");
			}
		}

		public override PlayerBullet RequestObject()
		{
			for(int i = 0 ; i < objectList.Count ; i++)
			{
				if(objectList[i].Object.IsIdle)
				{
					objectList[i].Object.gameObject.SetActive(true);

					return objectList[i].Object;
				}
			}

			GameObject newBullet = Instantiate(bulletPrefab);
			objectList.Add(new ObjectData(newBullet.GetComponent<PlayerBullet>(),""));

			return newBullet.GetComponent<PlayerBullet>();
		}

		public override PlayerBullet RequestObjectWithKey(string key)
		{
			return null;
		}

		public override void Drain()
		{
			for(int i = 0 ; i < objectList.Count ; i++)
			{
				Destroy(objectList[i].Object.gameObject);
				objectList[i] = null;
			}

			base.Drain();
		}
	}

	public float moveSpeed;
	public Boundary moveLimit;

	private bool controlPlayer = true; 
	private int life = 0;
	private float shotDelayTimer = 0.0f;
	private GameObject bulletSpawnPlace;
	private PlayerBulletPool bulletPool;

	[SerializeField]
	private BulletData bulletData;
	

	private void Start()
	{
		bulletSpawnPlace = transform.GetChild(0).gameObject;

		bulletPool = new PlayerBulletPool();

		life = 5;
	}
	private void Update()
	{
		if(controlPlayer)
		{
			if(life > 0)
			{
				FlightMove();

				shotDelayTimer += Time.deltaTime;
				if(shotDelayTimer >= bulletData.shotDelay)
				{
					if(Input.GetKey(KeyCode.Space))
					{
						ShotBullet();

						shotDelayTimer = 0.0f;
					}
				}
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
		PlayerBullet bullet = bulletPool.RequestObject();
		bullet.Init(
			bulletSpawnPlace.transform.position,
			bulletData.direction,
			bulletData.velocity,
			bulletData.acceleration,
			bulletData.angleAcceleration,
			bulletData.damege,
			bulletData.moveLimit);
	}

	public void Hit()
	{
		life--;

		Debug.Log("Player Hit ! " + life.ToString() + " / " + "5");
	}

	public void SetControlPlayer(bool b)
	{
		controlPlayer = b;
	}
}
