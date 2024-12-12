using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sa2Site : MonoBehaviour
{
    public GameObject missilePrefab;
    public Transform hardPoint;
    public float fireRate = 6f;
    private Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        InvokeRepeating(nameof(FireMissile), fireRate, fireRate);
    }

    void Update()
    {
        if (player != null)
        {
            Vector2 direction = player.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }

    void FireMissile()
    {
        Instantiate(missilePrefab, hardPoint.position, hardPoint.rotation);
    }
}
