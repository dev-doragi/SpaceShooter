using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public bool isShield;
    public bool isSpeed;
    public float moveSpeed;

    void Start()
    {
        
    }

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
                Destroy(gameObject);
            }

            else if (isSpeed)
            {
                FindObjectOfType<GameManager>().ActivateSpeedPower();
                Destroy(gameObject);
            }
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
