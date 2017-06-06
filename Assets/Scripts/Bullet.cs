using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour 
{
	protected float velocity;
    protected float acceleration;
    protected float dgrAngle;
    protected float dgrAngleAcceleration;
    protected bool isIdle = false;
    protected Vector2 direction = Vector2.up; 

    protected virtual void Update()
    {
        if(!isIdle)
        {
            velocity += acceleration;

            direction = direction.Rotate(dgrAngleAcceleration * Time.deltaTime);

            //Atan2를 사용시 x : 1, y : 0 일경우 0도, x : 0 , y : 1 일경우 0도 처럼 행동 하기 위해 90도를 빼줌
            dgrAngle = (Mathf.Atan2(direction.y,direction.x) * Mathf.Rad2Deg) - 90;

            Vector3 movement = direction * velocity * Time.deltaTime;
            //transform.Translate(movement); transform.foward를 기준으로 움직임
            transform.position = movement + transform.position;
            transform.rotation = Quaternion.Euler(0.0f,0.0f,dgrAngle);
        }
    }

    public virtual void Init(float vel,float acc,Vector2 dir,float angAccDgr)
    {
        isIdle = false;

        velocity = vel;
        acceleration = acc;
        direction = dir;

        dgrAngle = Mathf.Atan2(direction.y,direction.x) * Mathf.Rad2Deg;

        //직관성을 위해 시계 방향 회전을 하기 위해 부호를 바꿈
        dgrAngleAcceleration = -angAccDgr;
    }

    public virtual void Idle()
    {
        isIdle = true;
        velocity = 0.0f;
        acceleration = 0.0f;
        dgrAngle = 0.0f;
        dgrAngleAcceleration = 0.0f;
        direction = Vector2.up; 

        transform.position = Vector3.zero;
        transform.rotation = Quaternion.identity;
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

