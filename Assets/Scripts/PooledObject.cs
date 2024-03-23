using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PooledObject : MonoBehaviour
{
    private ObjectPool pool;
    public ObjectPool Pool { get => pool; set => pool = value; }
    public void Release()
    {
        pool.ReturnToPool(this);
        
    }

    private void Update()
    {
        if (transform.position.y < -2)
        {
            pool.ReturnToPool(this);
        }
    }
}
