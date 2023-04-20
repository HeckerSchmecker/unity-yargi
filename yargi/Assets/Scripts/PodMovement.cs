using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PodMovement : MonoBehaviour
{
    float rotationSpeed = 100f;

    private float movementSpeed = 4f;
    private float maxDistance = 14f;
    private float maxRotation = 30;

    private Rigidbody rb;





    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        
    }

    // Update is called once per frame
    
    void Update()
    {
        
        

    }

    private void FixedUpdate()
    {
        
        //----------------Set x-Velocity ----------------
        float horizontalInput = Input.GetAxis("Horizontal");
        Vector3 velocity = rb.velocity;
        velocity.x = horizontalInput * movementSpeed;
        rb.velocity = velocity;

        

        //-------------------Clamp X-Distance ----------------

        // Get the current position of the object
        Vector3 currentPosition = transform.position;

        // Calculate the new position of the object based on the horizontal input and movement speed
        float newXPos = currentPosition.x + (horizontalInput * movementSpeed * Time.deltaTime);

        // Clamp the x position based on maxDistance
        newXPos = Mathf.Clamp(newXPos, -maxDistance, maxDistance);

        // Set the position of the object
        transform.position = new Vector3(newXPos, currentPosition.y, currentPosition.z);


        ///-----Rotation of the Pod -------------

        float xVelocity = rb.velocity.x;

        // Calculate the target rotation based on the x velocity
        float targetRotationX = Mathf.Lerp(-maxRotation, maxRotation, (xVelocity + 5f) / 10f); // Maps xVelocity from range [-5, 5] to range [0, 1]

        // Get the current rotation of the object
        Quaternion currentRotation = transform.rotation;

        // Set the target rotation around the x axis
        Quaternion targetRotation = Quaternion.Euler(targetRotationX, currentRotation.eulerAngles.y, currentRotation.eulerAngles.z);

        // Rotate the object towards the target rotation using the rotate function
        transform.rotation = Quaternion.RotateTowards(currentRotation, targetRotation, rotationSpeed * Time.deltaTime);

    }
}
