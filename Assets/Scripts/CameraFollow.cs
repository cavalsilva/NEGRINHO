using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    PlayerMovement player;
    public Vector3 distance;

    public float lerpTime = 1f;
    float currentLerpTime;

    void Start()
    {
        player = FindObjectOfType<PlayerMovement>();

        if (player == null)
            Debug.LogError("Não há o player na cena");
    }

    void FixedUpdate()
    {
        Vector3 target = player.transform.position + distance;

        if (transform.position != target)
        {
            currentLerpTime = 0f;
        }

        currentLerpTime += Time.deltaTime;
        if (currentLerpTime > lerpTime)
        {
            currentLerpTime = lerpTime;
        }

        float t = currentLerpTime / lerpTime;
        transform.position = Vector3.Lerp(transform.position, target, t);
    }
}
