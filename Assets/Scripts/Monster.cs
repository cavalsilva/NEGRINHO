using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    //References
    Candle candle;
    PlayerMovement player;
    GameManager gm;

    //Movement
    public float speed;
    public float amplitude;
    public float frequency;

    private float startTime;
    private Vector3 direction;
    private Vector3 orthogonal;

    void Start()
    {
        candle = FindObjectOfType<Candle>();
        player = FindObjectOfType<PlayerMovement>();
        gm = FindObjectOfType<GameManager>();

        gm.monstersAlive.Add(this.gameObject);

        startTime = Time.time;
    }

    void Update()
    {
        float radius = candle.radius;
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        if (distanceToPlayer <= radius || distanceToPlayer < 2.5f)
            Die();
    }

    private void FixedUpdate()
    {
        //movement
        direction = (player.transform.position - transform.position).normalized;
        orthogonal = new Vector3(-direction.z, 0, direction.x);

        float t = Time.time - startTime;

        float distance = Vector3.Distance(player.transform.position, transform.position);

        transform.position += direction * speed + orthogonal * amplitude * Mathf.Sin(frequency * t) * Time.deltaTime;

    }


    void Die()
    {
        //TODO feedback the death
        gm.monstersAlive.Remove(this.gameObject);
        Destroy(gameObject);
    }
}
