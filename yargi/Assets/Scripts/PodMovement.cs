using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PodMovement : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;
    private float shootTimer;
    private float shootCooldown = 3f;
    


    private float xMovementSpeed = 1f;
    private float xMaxDistance = 14f;
    private float maxRotationX = 30;
    private float maxRotationY = 20;

    private float zSpeed = 4f;
    private float zMinSpeed = 4f;
    private float zMaxSpeed = 7f;

    private Rigidbody rb;
    private Camera camera;

    private float amplitude = 0.2f;
    private float frequency = 1f;
    private float initialY;
    private float noiseScale = 0.2f; // Scale of the Perlin noise
    private float noiseSpeed = 1f; // Speed of the Perlin noise

    private float noiseOffset;

    private float maxLeanAngle = 30f;
    private float maxRotationAngle = 50f;
    private float leanSpeed = 2f;
    private float leanReturnSpeed = 0.2f;
    private float rotationSpeed = 1f;
    private float rotationReturnSpeed = 0.25f;

    private float targetLeanAngle = 0f;
    private Quaternion initialRotation;

    private float fixedSpeed = 5f;
    private float minSpeed = 3f;
    private float maxSpeed = 10f;
    private float speedChangeRate = 2f;

    private float currentSpeed = 10f;



    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        camera = Camera.main;
        shootTimer = 0f;
        noiseOffset = Random.Range(0f, 100f);
        initialY = transform.position.y;
        initialRotation = transform.localRotation;
        Debug.Log(initialRotation);
    }

    // Update is called once per frame
    
    void Update()
    {
        shootTimer -= Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Space) && shootTimer < 0f)
        {
            Shoot();
            shootTimer = shootCooldown;
        }

        float verticalInput = Input.GetAxisRaw("Vertical");

        // Decrease speed when "S" key is pressed
        if (verticalInput < 0f)
        {
            currentSpeed = Mathf.Lerp(currentSpeed, minSpeed, speedChangeRate * Time.deltaTime);
        }
        // Increase speed when "W" key is pressed
        else if (verticalInput > 0f)
        {
            currentSpeed = Mathf.Lerp(currentSpeed, maxSpeed, speedChangeRate * Time.deltaTime);
        }
        // No input, maintain fixed speed
        else
        {
            currentSpeed = Mathf.Lerp(currentSpeed, fixedSpeed, speedChangeRate * Time.deltaTime);
        }

        // Move the parent object in the z-direction based on the current speed
        float zMovement = currentSpeed * Time.deltaTime;
        transform.parent.Translate(0f, 0f, zMovement);
    }

    private void FixedUpdate()
    {
        




        float horizontalInput = Input.GetAxisRaw("Horizontal");


        // Rotate the motorcycle object around its local X-axis for leaning
        targetLeanAngle = Mathf.Clamp(horizontalInput * maxLeanAngle, -maxLeanAngle, maxLeanAngle);
        targetLeanAngle -= 90f;
        Quaternion targetLeanRotation = Quaternion.Euler(targetLeanAngle, 90f, 0f);
        transform.localRotation = Quaternion.Lerp(transform.localRotation, targetLeanRotation, leanSpeed * Time.deltaTime);

        // Set the target rotation for the parent object around the global Z-axis
        Quaternion targetRotation = Quaternion.Euler(0f, Mathf.Clamp(horizontalInput * maxRotationAngle, -maxRotationAngle, maxRotationAngle), 0f);
        transform.parent.rotation = Quaternion.Lerp(transform.parent.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        // If no horizontal input, return to initial rotation for both objects
        if (horizontalInput == 0f)
        {
            transform.localRotation = Quaternion.Lerp(transform.localRotation, initialRotation, leanReturnSpeed * Time.deltaTime);
            //transform.parent.rotation = Quaternion.RotateTowards(transform.parent.rotation, Quaternion.identity, rotationReturnSpeed * Time.deltaTime);
            transform.parent.rotation = Quaternion.Lerp(transform.parent.rotation, Quaternion.identity, rotationReturnSpeed * Time.deltaTime);
        }


        ////----------------Set x-Velocity ----------------
        //float horizontalInput = Input.GetAxis("Horizontal");
        //Vector3 velocity = rb.velocity;
        //velocity.x = horizontalInput * xMovementSpeed;
        //rb.velocity = velocity;



        ////-------------------Clamp X-Distance ----------------

        //// Get the current position of the object
        //Vector3 currentPosition = transform.position;

        //// Calculate the new position of the object based on the horizontal input and movement speed
        //float newXPos = currentPosition.x + (horizontalInput * xMovementSpeed * Time.deltaTime);

        //// Clamp the x position based on maxDistance
        //newXPos = Mathf.Clamp(newXPos, -xMaxDistance, xMaxDistance);

        //// Set the position of the object
        //transform.position = new Vector3(newXPos, currentPosition.y, currentPosition.z);


        /////-----Rotation of the Pod -------------

        //float xVelocity = rb.velocity.x;

        //// Calculate the target rotation based on the x velocity
        //float targetRotationX = ((xVelocity / xMovementSpeed * maxRotationX) - 90f);
        //float targetRotationY = ((xVelocity / xMovementSpeed * maxRotationY) + 90f);
        ////Debug.Log("targetRotationX" + targetRotationX);

        //// Set the target rotation around the x axis
        //Quaternion targetRotation = Quaternion.Euler(targetRotationX, targetRotationY, 0f);

        //// Rotate the object towards the target rotation using the rotate function
        //transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);


        //---------------Up- and Down-Movement------------------------
        // Calculate the sinusoidal movement
        float sinusoidalValue = Mathf.Sin(Time.time * frequency) * amplitude;

        // Calculate the Perlin noise
        float perlinNoise = Mathf.PerlinNoise(Time.time * noiseSpeed, noiseOffset) * 2f - 1f; // Scale the Perlin noise to range [-1, 1]

        // Apply the noise to the sinusoidal movement
        float finalValue = sinusoidalValue + perlinNoise * noiseScale;

        // Update the object's position
        transform.position = new Vector3(transform.position.x, initialY + finalValue, transform.position.z);

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
        }
    }

    private void Shoot()
    {
        Debug.Log("Shoot");
        GameObject bulletObject = Instantiate(bulletPrefab, rb.position, Quaternion.identity);
    }
}
