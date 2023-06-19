using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SpawnManagerX : MonoBehaviour
{
    //Public Variables:
    [Header("GameObject 1:")]
    public GameObject EnemyPrefab;
    [Header("GameObject 2:")]
    public GameObject PowerupPrefab;

    //Internal Variables:
    public static float _enemySpeed = 250.0f;
    
    //Private Not Serialized Variables:
    private int _enemyCount;
    private int _waveCount = 1;
    private GameObject _player;


    //Private Not Serialized && ReadOnly Variables:
    private readonly float _spawnRangeX = 10.0f;
    private readonly float _spawnZMin = 15.0f; // set min spawn Z
    private readonly float _spawnZMax = 25.0f; // set max spawn Z

    void Start()
    {
        _player = GameObject.Find("Player");
        SpawnEnemyWave(_waveCount);
    }

    void Update()
    {
        _enemyCount = FindObjectsOfType<EnemyX>().Length;

        if (_enemyCount == 0)
        {
            _waveCount++;
            _enemySpeed += 10.0f;
            ResetPlayerPosition(); // put _player back at start
            SpawnEnemyWave(_waveCount);
        }
    }

    Vector3 GenerateSpawnPosition() // Generate random spawn position for powerups and enemy balls
    {
        float xPos = Random.Range(-_spawnRangeX, _spawnRangeX);
        float zPos = Random.Range(_spawnZMin, _spawnZMax);
        return new Vector3(xPos, 0.25f, zPos);
    }

    void SpawnEnemyWave(int waveCount)
    {
        Vector3 powerupSpawnOffset = new(0, 0, -15.0f); // make powerups spawn at _player end

        for (int i = 0; i < waveCount; i++) // Spawn number of enemy balls based on wave number
        {
            Instantiate(EnemyPrefab, GenerateSpawnPosition(), EnemyPrefab.transform.rotation);

            // If no powerups remain, spawn a powerup
            if (GameObject.FindGameObjectsWithTag("Powerup").Length == 0) // check that there are zero powerups
            {
                Instantiate(PowerupPrefab, GenerateSpawnPosition() + powerupSpawnOffset, PowerupPrefab.transform.rotation);
            }
        }
    }

    void ResetPlayerPosition() // Move _player back to position in front of own goal
    {
        _player.transform.position = new Vector3(0, 1, -7);
        _player.GetComponent<Rigidbody>().velocity = Vector3.zero;
        _player.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
    }
}
