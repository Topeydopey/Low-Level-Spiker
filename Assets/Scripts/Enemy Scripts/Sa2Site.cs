using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sa2Site : MonoBehaviour
{
    public GameObject missilePrefab;
    public Transform hardPoint;
    public float fireRate = 6f;
    private Transform player;
    public float detectionRange = 10f; // Maximum range to detect the player

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        InvokeRepeating(nameof(TryFireAtPlayer), fireRate, fireRate);
    }

    void Update()
    {
        // Rotate towards the player if within range
        if (player != null && Vector2.Distance(transform.position, player.position) <= detectionRange)
        {
            Vector2 direction = player.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }

    void TryFireAtPlayer()
    {
        if (player == null) return;

        // Fire only if the player is within detection range
        if (Vector2.Distance(transform.position, player.position) <= detectionRange)
        {
            Instantiate(missilePrefab, hardPoint.position, hardPoint.rotation);
        }
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}
