using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtPlayer : MonoBehaviour
{
    public GameObject shieldBreak;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //Instantiate(dieImpact, other.transform.position, other.transform.rotation);
            FindObjectOfType<GameManager>().KillPlayer();
            Destroy(this.gameObject);
        }

        else if (other.gameObject.CompareTag("Shield"))
        {
            other.gameObject.SetActive(false);
            Instantiate(shieldBreak, other.transform.position, other.transform.rotation);
            AudioManager.instance.PlaySfx(AudioManager.Sfx_2.ShieldBreak);
            Destroy(this.gameObject);
        }
    }
}
