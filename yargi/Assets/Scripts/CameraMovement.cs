using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform target;
    public float smoothTime = 1f;
    private Vector3 velocity = Vector3.zero;
    private float remainingShakeDuration = 0f;
    private bool isShaking = false;

    private void LateUpdate()
    {
        Vector3 desiredPosition = target.position;
        desiredPosition.y = transform.position.y;
        desiredPosition.x = transform.position.x;
        
        if (isShaking && remainingShakeDuration > 0)
        {
            remainingShakeDuration -= Time.deltaTime;
            transform.position += Random.insideUnitSphere * 0.2f;
        }
        else
        {   
            isShaking = false;
        }
        transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothTime);
    }

    public void CameraShake()
    {   
        if (!isShaking)
        {
            isShaking = true;
            remainingShakeDuration = 0.5f;
        }
    }
}

