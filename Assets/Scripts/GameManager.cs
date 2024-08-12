using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool isLive;
    public Transform waveSpawnPoint;

    public GameObject[] waves;

    public float[] waveDelays;
    public bool spawningWaves;

    private int waveTracker;

    public GameObject[] powerUps;
    public int powerUpChance;

    public float powerUpLength;
    private float powerUpCounter;
    private float playerSpeed; // �÷��̾� �̵��ӵ�
    private PlayerController player;

    public float speedMultiplier;

    public int playerLives;
    public Transform playerSpawnPoint;

    public float spawnDelay; // ���� ��Ÿ��

    public GameObject dieImpact;

    public GameObject VHSFilter;
    public GameObject gameOverScreen;

    public Text liveText;
    public Text scoreText;

    public GameObject bossBattle;

    public int currentScore; // ���� ����
    public int currentHiScore; // ���� �ְ� ����

    public Text hiScoreText;
    public Text completeText;
    public Text gameOverText;
    public Text restartText;

    public string mainMenu;

    void Start()
    {
        isLive = true;
        VHSFilter.SetActive(true);
        player = FindObjectOfType<PlayerController>();
        playerSpeed = player.moveSpeed;
        liveText.text = "LIVES: " + playerLives;
        scoreText.text = "SCORE: " + currentScore;
        AudioManager.instance.PlayBgm(true);
    }

    void Update()
    {
        // ���̺� ����
        if (spawningWaves)
        {
            waveDelays[waveTracker] -= Time.deltaTime;
            if( waveDelays[waveTracker] < 0)
            {
                Instantiate(waves[waveTracker], waveSpawnPoint.position, waveSpawnPoint.rotation);
                waveTracker++;
                if (waveTracker >= waveDelays.Length)
                {
                    spawningWaves = false;

                    if (bossBattle != null)
                    {
                        bossBattle.SetActive(true);
                    }
                }
            }
        }

        // ���� ���ӽð�
        if (powerUpCounter > 0)
        {
            powerUpCounter -= Time.deltaTime;

            if (powerUpCounter <= 0)
            {
                player.moveSpeed = playerSpeed; // �⺻ �̵��ӵ��� ����
            }
        }

        // ���� ���� �� �����
        if (!isLive)
        {
            StartCoroutine(Restart());
        }
    }

    // �÷��̾� ���
    public void KillPlayer()
    {
        playerLives--;
        liveText.text = "LIVES: " + playerLives;

        // �������� 0���� Ŭ ��
        if (playerLives > 0)
        {
            //spawnCounter = spawnDelay;
            Instantiate(dieImpact, player.transform.position, player.transform.rotation);
            AudioManager.instance.PlaySfx(AudioManager.Sfx_1.Explosion);
            player.gameObject.SetActive(false);
            player.moveSpeed = playerSpeed;
            player.doubleShot = false;

            StartCoroutine(Respawn(spawnDelay));
            //player.transform.position = playerSpawnPoint.position;
            //player.gameObject.SetActive(true);
            //player.shield.SetActive(true);

            // To do. �÷��̾� ������ ������
            //if (spawnCounter > 0)
            //{
            //    spawnCounter -= Time.deltaTime;
            //    if (spawnCounter <= 0)
            //    {
            //        player.transform.position = playerSpawnPoint.position;
            //        player.gameObject.SetActive(true);
            //        player.shield.SetActive(true);

            //        spawnCounter = spawnDelay;
            //    }
            //}
        }

        // GameOver
        else 
        {
            isLive = false;
            liveText.text = "LIVES ALL LOST!";
            AudioManager.instance.PlayBgm(false);
            Instantiate(dieImpact, player.transform.position, player.transform.rotation);
            AudioManager.instance.PlaySfx(AudioManager.Sfx_1.GameOver);
            player.gameObject.SetActive(false);
            gameOverText.text = "" + currentScore;
            gameOverScreen.SetActive(true);

            // ���� ����
            if (currentScore > currentHiScore)
            {
                PlayerPrefs.SetInt("Hiscore", currentScore);
            }

            StartCoroutine(SoundOff());
            Time.timeScale = 0.2f;
        }
    }

    public void AdddLife()
    {
        playerLives++;
        liveText.text = "LIVES: " + playerLives;
    }

    public void AddScore(int scoreToAdd)
    {
        currentScore += scoreToAdd;
        scoreText.text = "SCORE: " + currentScore;
    }

    public void DropPowerUp(Vector3 enemyPosition)
    {
        if (Random.Range(0, 100) < powerUpChance)
        {
            Instantiate(powerUps[Random.Range(0, powerUps.Length)], enemyPosition, new Quaternion(0, 0, 0, 0));
        }
    }

    public void ActivateSpeedPower() // �̼� ���� Activate
    {
        powerUpCounter = powerUpLength;
        player.moveSpeed = playerSpeed * speedMultiplier;
    }

    public void GameComplete()
    {
        completeText.text = "" + currentScore;
    }

    public void GotoMain()
    {
        SceneManager.LoadScene(mainMenu);
    }

    IEnumerator Respawn(float spawnDelay)
    {
        yield return new WaitForSeconds(spawnDelay);

        player.transform.position = playerSpawnPoint.position;
        player.gameObject.SetActive(true);
        // player.shield.SetActive(true);

        FindObjectOfType<PlayerController>().OnDamaged();
    }

    IEnumerator SoundOff()
    {
        yield return new WaitForSeconds(1.0f);
        // Debug.Log("Sound Off");
        AudioManager.instance.StopSfx();
    }

    IEnumerator Restart()
    {
        yield return new WaitForSeconds(1.0f);
        restartText.text = "PRESS LEFT BUTTON TO RESTART";

        if (Input.GetMouseButton(0))
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene("Play1");
        }
    }
}
