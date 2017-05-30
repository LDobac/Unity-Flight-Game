using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour 
{
	public class MonsterPattern : PatternClass
	{
		protected Monster targetMonster;

		public void SetTargetMonster(Monster monster)
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

	protected virtual void Start() {}

	protected virtual void Update() 
	{
		patterns.Update();
	}

	protected virtual void Die()
	{
		Destroy(gameObject);
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
}
