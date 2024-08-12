using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public bool isShield;
    public bool isSpeed;
    public bool isDouble;
    public bool isExtraLife;
    public float moveSpeed;

    void Update()
    {
        transform.position = new Vector3(transform.position.x - (moveSpeed * Time.deltaTime),
                                         transform.position.y,
                                         transform.position.z);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (isShield)
            {
                other.gameObject.GetComponent<PlayerController>().shield.SetActive(true);
                AudioManager.instance.PlaySfx(AudioManager.Sfx_2.PowerUp);
                Destroy(gameObject);
            }

            else if (isSpeed)
            {
                FindObjectOfType<GameManager>().ActivateSpeedPower();
                AudioManager.instance.PlaySfx(AudioManager.Sfx_2.PowerUp);
                Destroy(gameObject);
            }

            else if (isDouble)
            {
                other.GetComponent<PlayerController>().doubleShot = true;
                AudioManager.instance.PlaySfx(AudioManager.Sfx_2.PowerUp);
                Destroy(gameObject);
            }

            else if (isExtraLife)
            {
                FindObjectOfType<GameManager>().AdddLife();
                AudioManager.instance.PlaySfx(AudioManager.Sfx_2.LifeUp);
                Destroy(gameObject);
            }
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
