using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCameraX : MonoBehaviour
{
    private readonly float speed = 200.0f;
    private GameObject _player;

    void Start()
    {
        _player = GameObject.Find("Player");       
    }

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        transform.Rotate(Vector3.up, speed * Time.deltaTime * horizontalInput);

        transform.position = _player.transform.position; // Move focal point with _player
    }
}
