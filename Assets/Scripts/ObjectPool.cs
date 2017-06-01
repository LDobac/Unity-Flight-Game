using UnityEngine;
using System.Collections.Generic;

public abstract class ObjectPool<T>
{
    public class ObjectData
    {
        private T obj;
        private string key;

        public ObjectData(T obj,string key)
        {
            this.obj = obj;
            this.key = key;
        }

        public T Object
        {
            get
            {
                return obj;
            }
        }

        public string Key
        {
            get
            {
                return key;
            }
            set
            {
                key = value;
            }
        }
    }

    protected List<ObjectData> objectList;
    protected int originCapacity;

    public ObjectPool(int capacity)
    {
        originCapacity = capacity;
        objectList = new List<ObjectData>(originCapacity);
    }

    public virtual void Init() {}

    public abstract T RequestObject();

    public abstract T RequestObjectWithKey(string key);

    public virtual void Drain()
    {
        objectList.Resize(originCapacity);
    }
}