using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour 
{
	protected float moveSpeed;
    protected bool isIdle = false;
    protected Vector2 direction = Vector2.up; 

    protected virtual void Update()
    {
        if(!isIdle)
        {
            Vector3 movement = direction * moveSpeed * Time.deltaTime;
            transform.Translate(movement);
        }
    }

    public virtual void Idle()
    {
        isIdle = true;
    }

    public bool IsIdle
    {
        get
        {
            return isIdle;
        }
    }

    public Vector2 Direction
    {
        get
        {
            return direction;
        }
        set
        {
            direction = value;
        }
    }

    public float Speed
    {
        get
        {
            return moveSpeed;
        }
        set
        {
            moveSpeed = value;
        }
    }
}
