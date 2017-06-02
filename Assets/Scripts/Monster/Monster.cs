using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Monster : MonoBehaviour 
{
	public abstract class MonsterPattern : PatternClass
	{
		protected Monster targetMonster;

		public MonsterPattern(Monster monster)
		{
			targetMonster = monster;
		}
	} 

	protected int health = 0;
	protected float speed = 0.0f;
	protected PatternList patterns = null;

	protected virtual void Awake() 
	{
		patterns = new PatternList();
	}

	protected virtual void Start() { }

	protected virtual void Update() 
	{
		patterns.Update();
	}

	public virtual void Reset() {}

	public virtual void Idle()
	 {
		 gameObject.SetActive(false);
		 patterns.Stop();		 
	 }

	private void OnTriggerEnter2D(Collider2D other)
	{
		if(other.CompareTag("Player"))
		{
			other.GetComponent<PlayerController>().Hit();
			Die();
			Debug.Log("Trigger At Monster");
		}
	}

	protected virtual void Die()
	{
		//Destroy(gameObject);
		gameObject.SetActive(false);
		patterns.Stop();
	}

	public virtual void Hit(int damege)
	{
		health -= damege;

		if(health <= 0)
		{
			Die();
		}
	}

	public void AddPattern(MonsterPattern pattern)
	{
		patterns.AddPattern(pattern);
	}

	public int Health
	{
		get
		{
			return health;
		}
		set
		{
			health = value;
		}
	}

	public float Speed
	{
		get
		{
			return speed;
		}
		set
		{
			speed = value;
		}
	}

	public PatternList PatternList
	{
		get
		{
			return patterns;
		}
	}
}
