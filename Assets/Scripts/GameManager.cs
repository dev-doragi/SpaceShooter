using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Transform waveSpawnPoint;

    public GameObject[] waves;

    public float[] waveDelays;
    public bool spawningWaves;

    private int waveTracker;

    public GameObject[] powerUps;
    public int powerUpChance;

    public float powerUpLength;
    private float powerUpCounter;
    private float playerSpeed;
    private PlayerController player;

    public float speedMultiplier;

    void Start()
    {
        player = FindObjectOfType<PlayerController>();
        playerSpeed = player.moveSpeed;
    }

    void Update()
    {
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
                }
            }
        }

        if (powerUpCounter > 0)
        {
            powerUpCounter -= Time.deltaTime;

            if (powerUpCounter <= 0)
            {
                player.moveSpeed = playerSpeed;
            }
        }
    }

    public void DropPowerUp(Vector3 enemyPosition)
    {
        if (Random.Range(0, 100) < powerUpChance)
        {
            Instantiate(powerUps[Random.Range(0, powerUps.Length)], enemyPosition, new Quaternion(0, 0, 0, 0));
        }
    }

    public void ActivateSpeedPower()
    {
        powerUpCounter = powerUpLength;
        player.moveSpeed = playerSpeed * speedMultiplier;
    }
}
