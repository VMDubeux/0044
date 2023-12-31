﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyX : MonoBehaviour
{
    internal float _speed = SpawnManagerX._enemySpeed;
    private Rigidbody _enemyRb;
    private GameObject _playerGoal;

    void Start()
    {
        _enemyRb = GetComponent<Rigidbody>();
        _playerGoal = GameObject.Find("Player Goal");
    }

    void Update()
    {
        Vector3 lookDirection = (_playerGoal.transform.position - transform.position).normalized; // Set enemy direction towards _player goal and move there
        _enemyRb.AddForce(_speed * Time.deltaTime * lookDirection);
    }



    void OnCollisionEnter(Collision other) // If enemy collides with either goal, destroy it
    {
        if (other.gameObject.name == "Enemy Goal") 
        {
            Destroy(gameObject);
        }
        else if (other.gameObject.name == "Player Goal")
        {
            Destroy(gameObject);
        }
    }
}
