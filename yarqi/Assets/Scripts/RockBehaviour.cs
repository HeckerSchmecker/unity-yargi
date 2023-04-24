using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockBehaviour : MonoBehaviour
{
    Rigidbody rb;
    private float despawnZ = -20f;
    private float speed = 15f;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = new Vector3(0f, 0f, -speed);
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.z < despawnZ)
        {
            Destroy(gameObject);
        }
    }
}
