using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform target;
    public float smoothTime = 1f;
    private Vector3 velocity = Vector3.zero;
    private float remainingShakeDuration = 0f;
    private float shakeIntensity;
    private bool isShaking = false;

    private void LateUpdate()
    {
        Vector3 desiredPosition = target.position;
        desiredPosition.y = transform.position.y;
        desiredPosition.x = transform.position.x;
        desiredPosition.z -= 40;
        
        if (isShaking && remainingShakeDuration > 0)
        {
            remainingShakeDuration -= Time.deltaTime;
            transform.position += Random.insideUnitSphere * shakeIntensity;
        }
        else
        {   
            isShaking = false;
        }
        transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothTime);
    }

    public void CameraShake(float shakeTime, float intensity)
    {   
        if (!isShaking)
        {
            isShaking = true;
            remainingShakeDuration = shakeTime;
            shakeIntensity = intensity;
        }
    }
}

