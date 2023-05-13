using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PodMovement : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;
    private float shootTimer;
    private float shootCooldown = 3f;
    
    float rotationSpeed = 100f;

    private float xMovementSpeed = 1f;
    private float xMaxDistance = 14f;
    private float maxRotationX = 30;
    private float maxRotationY = 20;

    private float zSpeed = 4f;
    private float zMinSpeed = 4f;
    private float zMaxSpeed = 7f;

    private Rigidbody rb;
    private Camera camera;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        camera = Camera.main;
        shootTimer = 0f;
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
    }

    private void FixedUpdate()
    {
        //float verticalInput = Input.GetAxis("Vertical");
        //verticalInput = Mathf.Clamp(verticalInput, 0f, 1f);
        //Calculate vurrentSpeed
        if (Input.GetKey(KeyCode.W))
        {
            zSpeed += 0.2f;
            zSpeed = Mathf.Clamp(zSpeed, zMinSpeed, zMaxSpeed);
        }
        if (!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S))
        {            
            zSpeed -= 0.05f;
            zSpeed = Mathf.Clamp(zSpeed, zMinSpeed, zMaxSpeed);
        }
        if (Input.GetKey(KeyCode.S))
        {
            zSpeed -= 0.5f;
            zSpeed = Mathf.Clamp(zSpeed, zMinSpeed-2f, zMaxSpeed);
        }
        Debug.Log("zSpeed" + zSpeed);


        // Calculate the new position of the object along the z-axis
        float newZPosition = transform.position.z + (zSpeed * Time.deltaTime);

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
        float targetRotationX = ((xVelocity / xMovementSpeed * maxRotationX) - 90f);
        float targetRotationY = ((xVelocity / xMovementSpeed * maxRotationY) + 90f);
        //Debug.Log("targetRotationX" + targetRotationX);

        // Set the target rotation around the x axis
        Quaternion targetRotation = Quaternion.Euler(targetRotationX, targetRotationY, 0f);

        // Rotate the object towards the target rotation using the rotate function
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);




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
