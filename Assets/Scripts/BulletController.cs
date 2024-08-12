using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float moveSpeed;
    public GameObject laserImpact;
    public GameObject dieImpact;
    public GameObject shieldBreak;

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
            FindObjectOfType<GameManager>().AddScore(other.GetComponent<PointValue>().value);

            Instantiate(dieImpact, transform.position, transform.rotation);
            AudioManager.instance.PlaySfx(AudioManager.Sfx_1.Explosion);
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }

        else if (other.CompareTag("Player"))
        {
            if (isEnemy)
            {
                //Instantiate(dieImpact, other.transform.position, other.transform.rotation);
                FindObjectOfType<GameManager>().KillPlayer();
                Destroy(this.gameObject);
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
            Instantiate(shieldBreak, other.transform.position, other.transform.rotation);
            AudioManager.instance.PlaySfx(AudioManager.Sfx_2.ShieldBreak);
            FindObjectOfType<PlayerController>().OnDamaged();
            Destroy(this.gameObject);
            other.gameObject.SetActive(false);
        }

        if (other.CompareTag("BossHandTop") && Boss1.canBeHurt)
        {
            FindObjectOfType<Boss1>().topHandHealth--;
        }

        if (other.CompareTag("BossHandBottom") && Boss1.canBeHurt)
        {
            FindObjectOfType<Boss1>().bottomHandHealth--;
        }

        if (other.CompareTag("BossMain") && Boss1.canBeHurt)
        {
            FindObjectOfType<Boss1>().mainHealth--;
        }

        Instantiate(laserImpact, transform.position, transform.rotation);
        AudioManager.instance.PlaySfx(AudioManager.Sfx_1.LaserImpact);
        Destroy(this.gameObject);
    }

    private void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }
}
