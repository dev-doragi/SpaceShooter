using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtPlayer : MonoBehaviour
{
    public GameObject dieImpact;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Instantiate(dieImpact, transform.position, transform.rotation);
            Destroy(other.gameObject);
        }

        else if (other.gameObject.CompareTag("Shield"))
        {
            other.gameObject.SetActive(false);
            Destroy(this.gameObject);
        }
    }
}
