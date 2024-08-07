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
            FindObjectOfType<GameManager>().DropPowerUp(other.transform.position);

            Instantiate(dieImpact, transform.position, transform.rotation);
            Destroy(other.gameObject);
        }

        else if (other.CompareTag("Player"))
        {
            if (isEnemy)
            {
                Destroy(other.gameObject);
                Instantiate(dieImpact, transform.position, transform.rotation);
            }

        }

        //else if (other.CompareTag("Bullet"))
        //{
        //    Debug.Log("Hit");
        //    Destroy(other.gameObject);
        //}

        else if (other.CompareTag("Shield"))
        {
            other.gameObject.SetActive(false);
        }

        else
        {
            Instantiate(bulletImpact, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
