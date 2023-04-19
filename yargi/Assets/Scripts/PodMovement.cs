using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PodMovement : MonoBehaviour
{
    Rigidbody rigidbody;
    float torque = 0.05f;
    float moveSpeed = 5f;
    float rotationSpeed = 50f;
    private float horizontal;
    private float maxRotation = 0.3f;
    

    
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
    }

    private void FixedUpdate()
    {
        Debug.Log(transform.rotation);
        if (Input.GetKey(KeyCode.A) && transform.rotation.z < maxRotation)
        {
            transform.Rotate(new Vector3(0, 0, 1) * rotationSpeed * Time.deltaTime);
        }
        else if (Input.GetKey(KeyCode.D) && transform.rotation.z > -maxRotation)
        {
            transform.Rotate(new Vector3(0, 0, 1) * -rotationSpeed * Time.deltaTime);
        }
        else
        {
            if (transform.rotation.z > 0.001)
            {
                transform.Rotate(new Vector3(0, 0, 1) * -rotationSpeed * Time.deltaTime);
            }
            else if(transform.rotation.z < 0.001)
            {
                transform.Rotate(new Vector3(0, 0, 1) * rotationSpeed * Time.deltaTime);
            }
        }

    }
}
