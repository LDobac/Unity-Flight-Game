using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletMemPool : MonoBehaviour
{
	public GameObject bulletPrefab; 

	List<PlayerBullet> playerBullet;

	private void Awake()
	{
		playerBullet = new List<PlayerBullet>(10);
	}

	public PlayerBullet RequestIdleBullet()
	{
		for(int i = 0; i < playerBullet.Count ; i++)
		{
			if(playerBullet[i].IsIdle)
			{
				playerBullet[i].gameObject.SetActive(true);

				return playerBullet[i];
			}
		}

		GameObject newBullet = Instantiate(bulletPrefab);
		playerBullet.Add(newBullet.GetComponent<PlayerBullet>());

		return newBullet.GetComponent<PlayerBullet>();
	}
}
