using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    //public GameObject playerDieImpact;
    public GameObject enemyDieImpact;

    public float moveSpeedX;
    public float moveSpeedY;
    public float rotateSpeed;

    private Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(-moveSpeedX, moveSpeedY);
    }

    void Update()
    {
        transform.rotation = Quaternion.Euler(0f, 0f, transform.rotation.eulerAngles.z + (Random.Range(rotateSpeed / 5f, rotateSpeed) * Time.deltaTime));

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            FindObjectOfType<GameManager>().KillPlayer();
        }

        if (other.gameObject.CompareTag("Enemy"))
        {
            Instantiate(enemyDieImpact, other.transform.position, other.transform.rotation);
            AudioManager.instance.PlaySfx(AudioManager.Sfx_1.Explosion);
            Destroy(other.gameObject);
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
