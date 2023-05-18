using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PodScript : MonoBehaviour
{
    public ParticleSystem particleSystemDamage;
    public ParticleSystem particleSystemMotorDamage;

    private Camera camera;

    private int health;
    private int maxHealth = 100;
    private int damageSmallObstacle = 20;

    private float emissionRateOverTimeMaxValue = 200f;

    void Start()
    {
        camera = Camera.main;
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            PodMovement podMovement = GetComponent<PodMovement>();
            podMovement.enabled = false;
            Debug.Log("Pod ist zerstört!");
            Destroy(gameObject);
        }
        if (health <= (0.2 * maxHealth))
        {
            particleSystemMotorDamage.Play();
        }
        else
        {
            particleSystemMotorDamage.Stop();
        }
        if (health <= (0.6 * maxHealth))
        {
            var emissionModule = particleSystemDamage.emission;
            emissionModule.rateOverTime = (1f - ((float)health / maxHealth)) * emissionRateOverTimeMaxValue;
        }
        else
        {
            var emissionModule = particleSystemDamage.emission;
            emissionModule.rateOverTime = 0f;
        }

        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Obstacle")
        {
            Debug.Log("Felsen getroffen!");
            camera.GetComponent<CameraMovement>().CameraShake(0.5f, 0.1f);
            Destroy(other.gameObject);
        }
        if (other.tag == "ObstacleSmall")
        {
            Debug.Log("Kleinen Felsen getroffen!");
            camera.GetComponent<CameraMovement>().CameraShake(0.4f, 0.05f);
            Destroy(other.gameObject);
            health -= damageSmallObstacle;
        }
    }
}
