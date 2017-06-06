using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Boundary
{
	public float minX;
	public float minY;
	public float maxX;
	public float maxY;

    public Boundary()
    {
        maxX = maxY = minX = minY = 0.0f;
    }
    public Boundary(float minX,float minY,float maxX,float maxY)
    {
        this.maxX = maxX;
        this.minX = minX;
        this.maxY = maxY;
        this.minY = minY;
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

public static class ListExtension 
{
    public static void Resize<T>(this List<T> list, int size)
    {
        int count = list.Count;

        if (size < count)
        {
            list.RemoveRange(size, count - size);
        }
        else if (size > count)
        {
            if (size > list.Capacity)
            {
                list.Capacity = size;
            }

            list.AddRange(new T[size - count]);
        }
    }
}