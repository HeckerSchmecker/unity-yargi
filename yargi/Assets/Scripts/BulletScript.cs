using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    Rigidbody rb;
    private float shootForce = 1000f;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(Vector3.forward * shootForce);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            Debug.Log("Hit Enenmy");
            Destroy(gameObject);
            Destroy(other.gameObject);
        }
        if (other.tag == "Obstacle")
        {
            Debug.Log("Hit Obstacle");
            Destroy(gameObject);
        }
        if (other.tag == "ObstacleSmall")
        {
            Debug.Log("Hit small Obstacle");
            Destroy(other.gameObject);
        }
    }
}
