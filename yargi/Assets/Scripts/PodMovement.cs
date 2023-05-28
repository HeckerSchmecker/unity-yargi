using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PodMovement : MonoBehaviour
{
    
    
    
    
    
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
        
        
        
        noiseOffset = Random.Range(0f, 100f);
        initialY = transform.position.y;
        initialRotation = transform.localRotation;
    }

    // Update is called once per frame
    
    void Update()
    {
        

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



    

    
}
