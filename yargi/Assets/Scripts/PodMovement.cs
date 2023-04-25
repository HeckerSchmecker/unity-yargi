using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PodMovement : MonoBehaviour
{
    float rotationSpeed = 100f;

    private float xMovementSpeed = 4f;
    private float xMaxDistance = 14f;
    private float maxRotation = 30;

    private float zMinSpeed = 3f;
    private float zMaxSpeed = 5f;

    private Rigidbody rb;
    private Camera camera;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        camera = Camera.main;
    }

    // Update is called once per frame
    
    void Update()
    {
        
        

    }

    private void FixedUpdate()
    {

        float verticalInput = Input.GetAxis("Vertical");
        verticalInput = Mathf.Clamp(verticalInput, 0f, 1f);
        //Calculate vurrentSpeed
        float zCurrentSpeed = verticalInput * (zMaxSpeed - zMinSpeed) + zMinSpeed;

        // Calculate the new position of the object along the z-axis
        float newZPosition = transform.position.z + (zCurrentSpeed * Time.deltaTime);

        // Set the position of the object
        transform.position = new Vector3(transform.position.x, transform.position.y, newZPosition);


        //----------------Set x-Velocity ----------------
        float horizontalInput = Input.GetAxis("Horizontal");
        Vector3 velocity = rb.velocity;
        velocity.x = horizontalInput * xMovementSpeed;
        rb.velocity = velocity;

        

        //-------------------Clamp X-Distance ----------------

        // Get the current position of the object
        Vector3 currentPosition = transform.position;

        // Calculate the new position of the object based on the horizontal input and movement speed
        float newXPos = currentPosition.x + (horizontalInput * xMovementSpeed * Time.deltaTime);

        // Clamp the x position based on maxDistance
        newXPos = Mathf.Clamp(newXPos, -xMaxDistance, xMaxDistance);

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

    

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Obstacle")
        {
            Debug.Log("Felsen getroffen!");
            camera.GetComponent<CameraMovement>().CameraShake();
            Destroy(other.gameObject);
        }
    }
}
