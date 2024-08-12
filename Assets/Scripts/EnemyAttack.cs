using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public float shotDelay; // 레이저 쿨타임
    private float shotCounter;

    public GameObject bullet;
    public Transform bulletPoint;

    void Update()
    {
        shotCounter -= Time.deltaTime;

        if (shotCounter <= 0)
        {
            Instantiate(bullet, bulletPoint.position, bulletPoint.rotation);
            AudioManager.instance.PlaySfx(AudioManager.Sfx_1.EnemyLaser);

            shotCounter = shotDelay;
        }
    }
}
