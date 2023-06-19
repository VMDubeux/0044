using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerX : MonoBehaviour
{
    //Public Variables:
    [Header("Complementar GameObject 1:")]
    public GameObject powerupIndicator;
    [Header("Complementar GameObject 2:")]
    public ParticleSystem Particles;

    //Private Not Serialized Variables:
    private Rigidbody playerRb;
    private GameObject focalPoint;
    private bool hasPowerup = false;

    //Private Not Serialized && ReadOnly Variables:
    private readonly float speed = 500.0f;
    private readonly float bustedSpeed = 1000.0f;
    private readonly float normalStrength = 10; // how hard to hit enemy without powerup
    private readonly float powerupStrength = 25; // how hard to hit enemy with powerup
    private readonly int powerUpDuration = 5;

    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("Focal Point");
    }

    void Update()
    {
        float verticalInput = Input.GetAxis("Vertical"); // Add force to _player in direction of the focal point (and camera)
        
        playerRb.AddForce(speed * Time.deltaTime * verticalInput * focalPoint.transform.forward);

        if (Input.GetKey(KeyCode.Space))
        {
            playerRb.AddForce(bustedSpeed * Time.deltaTime * focalPoint.transform.forward);
            Particles.Play();
        } 
       
        powerupIndicator.transform.position = transform.position + new Vector3(0, -0.5f, 0);// Set powerup indicator position to beneath _player
    }
    
    void OnTriggerEnter(Collider other) // If Player collides with powerup, activate powerup
    {
        if (other.CompareTag("Powerup"))
        {
            Destroy(other.gameObject);
            hasPowerup = true;
            powerupIndicator.SetActive(true);
            StartCoroutine(PowerupCooldown(powerUpDuration));
        }
    }

    IEnumerator PowerupCooldown(int powerUpDuration) // Coroutine to count down powerup duration
    {
        yield return new WaitForSeconds(powerUpDuration);
        hasPowerup = false;
        powerupIndicator.SetActive(false);
    }
    
    private void OnCollisionEnter(Collision other) // If Player collides with enemy
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Rigidbody enemyRigidbody = other.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = other.gameObject.transform.position - transform.position;

            if (hasPowerup) // if have powerup hit enemy with powerup force
            {
                enemyRigidbody.AddForce(awayFromPlayer * powerupStrength, ForceMode.Impulse);
            }
            else // if no powerup, hit enemy with normal strength 
            {
                enemyRigidbody.AddForce(awayFromPlayer * normalStrength, ForceMode.Impulse);
            }


        }
    }



}
