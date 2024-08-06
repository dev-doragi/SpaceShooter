using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOvertime : MonoBehaviour
{
    public float lifeTime;

    private void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    //public float lifeTime;

    //void Update()
    //{
    //    lifeTime -= Time.deltaTime;

    //    if (lifeTime < 0)
    //    {
    //        Destroy(gameObject);
    //    }
    //}

    //private void OnBecameInvisible()
    //{
    //    Destroy(gameObject);
    //}
}
