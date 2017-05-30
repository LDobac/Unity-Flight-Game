using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour 
{
	protected float velocity;
    protected float acceleration;
    protected float angle;
    protected float angleAcceleration;
    
    //이 시간이 지나면 총알을 삭제
    protected float idleTime = 0.0f;
    protected float idleTimer = 0.0f;
    protected bool isIdle = false;
    protected Vector2 direction = Vector2.up; 

    protected virtual void Update()
    {
        if(!isIdle)
        {
            velocity += acceleration;
            angle += angleAcceleration;

            direction.Rotate(angleAcceleration);

            Vector3 movement = direction * velocity * Time.deltaTime;
            transform.Translate(movement);

            if(idleTime != 0.0f)
            {
                idleTimer += Time.deltaTime;

                if(idleTimer >= idleTime)
                {
                    Idle();
                }
            }
        }
    }

    //각도 받는것에서 방향벡터로 바꿈 업데이트에서 방향벡터를 각가속도에 따라 회전
    public virtual void Init(float vel,float acc,Vector2 dir,float angAcc,float removeTime = 0.0f)
    {
        isIdle = false;

        idleTimer = 0.0f;

        velocity = vel;
        acceleration = acc;
        direction = dir;

        angle = Mathf.Atan2(direction.y,direction.x);
        angleAcceleration = angAcc;

        idleTime = removeTime;
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

}

public static class Vector2Extension 
{
     public static Vector2 Rotate(this Vector2 v, float degrees)
    {
         float radians = degrees * Mathf.Deg2Rad;
         float sin = Mathf.Sin(radians);
         float cos = Mathf.Cos(radians);
         
         float tx = v.x;
         float ty = v.y;
 
         return new Vector2(cos * tx - sin * ty, sin * tx + cos * ty);
     }
 }