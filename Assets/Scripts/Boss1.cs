using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Boss1 : MonoBehaviour
{
    [Header("#PHASE")]
    public bool phase0; // 보스가 화면 안으로 들어오는 패턴
    public bool phase1; // 화면 안으로 다 오면 미사일을 쏘기 시작하는 패턴
    public bool phase2; // 2개의 손이 다 터졌을 때 나타나는 패턴
    public bool phase3; // 보스의 몸체가 터졌을 때 시작하는 패턴
    public bool phase4; // 보스의 몸체가 다 터지고 나서 어떻게 해야할지 정하는 패턴

    public float countDownToStart;

    [Header("#BOSS_INFO")]
    public GameObject thsBoss;
    public Transform topPosition;
    public Transform bottomPosition;
    public float moveSpeedX;
    public float moveSpeedY;

    public bool moveUp;

    public GameObject laser1;
    public GameObject laser2;

    [Header("#TRANSFORM")]
    public Transform shotPoint1;
    public Transform shotPoint2;
    public Transform shotPoint3;
    public Transform shotPoint4;

    [Header("#DELAY")]
    public float shotDelay;
    private float shotCounter;

    [Header("#HAND")]
    public GameObject topLaserHand;
    public GameObject bottomLaserHand;

    public int topHandHealth; // 위쪽 손 HP
    public int bottomHandHealth; // 아래쪽 손 HP
    public GameObject explosion;
    public GameObject mainExplosion;

    public int mainHealth;

    public float shotDelay2;
    private float shotCounter2;

    public static bool canBeHurt;

    public GameObject levelWinScreen;
    public float timeToLevelWin;

    void Start()
    {
        
    }

    void Update()
    {
        // 위 아래로 움직이는 기본 패턴
        if (!phase0 && !phase1)
        {
            if(!moveUp)
            {
                thsBoss.transform.position = Vector3.MoveTowards(thsBoss.transform.position,
                                     new Vector3(thsBoss.transform.position.x, bottomPosition.transform.position.y, thsBoss.transform.position.z),
                                     moveSpeedY * Time.deltaTime);
                if (thsBoss.transform.position.y <= bottomPosition.transform.position.y)
                    moveUp = true;
            }
            else
            {
                thsBoss.transform.position = Vector3.MoveTowards(thsBoss.transform.position,
                         new Vector3(thsBoss.transform.position.x, topPosition.transform.position.y, thsBoss.transform.position.z),
                         moveSpeedY * Time.deltaTime);
                if (thsBoss.transform.position.y >= topPosition.transform.position.y)
                    moveUp = false;
            }

        }

        shotCounter -= Time.deltaTime;
        if (shotCounter <= 0)
        {
            Instantiate(laser1, shotPoint1.position, shotPoint1.rotation);
            Instantiate(laser1, shotPoint2.position, shotPoint2.rotation);
            AudioManager.instance.PlaySfx(AudioManager.Sfx_1.BossLaser);
            shotCounter = shotDelay;
        }

        if (phase0) // 보스가 화면 안으로 들어오는 패턴
        {
            countDownToStart -= Time.deltaTime;

            if (countDownToStart < 0)
            {
                phase1 = true;
                phase0 = false;
            }
        }

        else if (phase1)
        {
            moveUp = true;
            thsBoss.transform.position = Vector3.MoveTowards(thsBoss.transform.position,
                                                             new Vector3(topPosition.position.x, thsBoss.transform.position.y, thsBoss.transform.position.z),
                                                             moveSpeedX * Time.deltaTime);

            if (thsBoss.transform.position.x == topPosition.position.x)
            {
                phase1 = false;
                phase2 = true;
                canBeHurt = true;
            }
        }

        else if (phase2)
        {
            if (topHandHealth <= 0)
            {
                Instantiate(explosion, topLaserHand.transform.position, topLaserHand.transform.rotation);
                topLaserHand.SetActive(false);
                topHandHealth = 9999;
            }

            if (bottomHandHealth <= 0)
            {
                Instantiate(explosion, bottomLaserHand.transform.position, bottomLaserHand.transform.rotation);
                bottomLaserHand.SetActive(false);
                bottomHandHealth = 9999;
            }

            if (topHandHealth.Equals(9999) && bottomHandHealth.Equals(9999))
            {
                phase2 = false;
                phase3 = true;
            }
        }

        else if (phase3)
        {
            shotCounter2 -= Time.deltaTime;
            if (shotCounter2 <= 0)
            {
                Instantiate(laser2, shotPoint3.position, shotPoint3.rotation);
                Instantiate(laser2, shotPoint4.position, shotPoint4.rotation);
                AudioManager.instance.PlaySfx(AudioManager.Sfx_1.BossLaser);
                shotCounter2 = shotDelay2;
            }

            if (mainHealth <= 0)
            {
                // boss die effect 만들기
                phase3 = false;
                // Invoke("BossDie", 1.7f);
                StartCoroutine(BossDie_2());
                phase4 = true;
                shotCounter = 999;
            }
        }

        else if (phase4)
        {
            timeToLevelWin -= Time.deltaTime;

            if (timeToLevelWin <= 0)
            {
                if (SceneManager.GetActiveScene().name.Equals("PlayEnd"))
                {
                    FindObjectOfType<GameManager>().GameComplete();
                }
                levelWinScreen.SetActive(true);
                gameObject.SetActive(false);
            }
        }
    }

    public void BossDie()
    {
        AudioManager.instance.PlaySfx(AudioManager.Sfx_2.BossExplosion);
        thsBoss.SetActive(false);
    }

    IEnumerator BossDie_2()
    {
        yield return new WaitForSeconds(0f);
        Instantiate(mainExplosion, thsBoss.transform.position, thsBoss.transform.rotation);

        yield return new WaitForSeconds(0.01f);
        Instantiate(explosion, shotPoint3.transform.position, shotPoint3.transform.rotation);

        yield return new WaitForSeconds(0.19f);
        // Instantiate(explosion, shotPoint3.transform.position, shotPoint3.transform.rotation);
        AudioManager.instance.PlaySfx(AudioManager.Sfx_1.Explosion);

        yield return new WaitForSeconds(0.2f);
        // Instantiate(explosion, bottomLaserHand.transform.position, bottomLaserHand.transform.rotation);
        AudioManager.instance.PlaySfx(AudioManager.Sfx_1.Explosion);

        yield return new WaitForSeconds(0.07f);
        // Instantiate(explosion, shotPoint1.transform.position, shotPoint1.transform.rotation);
        AudioManager.instance.PlaySfx(AudioManager.Sfx_1.Explosion);

        yield return new WaitForSeconds(0.13f);
        Instantiate(explosion, shotPoint4.transform.position, bottomLaserHand.transform.rotation);
        // AudioManager.instance.PlaySfx(AudioManager.Sfx_1.Explosion);

        yield return new WaitForSeconds(0.2f);
        // Instantiate(explosion, shotPoint3.transform.position, shotPoint3.transform.rotation);
        AudioManager.instance.PlaySfx(AudioManager.Sfx_1.Explosion);

        yield return new WaitForSeconds(0.6f);
        AudioManager.instance.PlaySfx(AudioManager.Sfx_2.BossExplosion);
        thsBoss.SetActive(false);
    }
}
