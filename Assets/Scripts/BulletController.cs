using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float moveSpeed;
    public GameObject bulletImpact;
    public GameObject dieImpact;

    public bool isEnemy;

    void Update()
    {
        transform.position = new Vector3(transform.position.x + (moveSpeed * Time.deltaTime),
                                         transform.position.y,
                                         transform.position.z);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Instantiate(dieImpact, transform.position, transform.rotation);
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("Player") && isEnemy)
        {
            Destroy(other.gameObject);
            Instantiate(dieImpact, transform.position, transform.rotation);
        }

        Instantiate(bulletImpact, transform.position, transform.rotation);
        Destroy(gameObject);   
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
