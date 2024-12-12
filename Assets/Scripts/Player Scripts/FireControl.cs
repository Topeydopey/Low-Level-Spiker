using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireControl : MonoBehaviour
{
    public GameObject hellfirePrefab;
    public Transform hardPoint;
    public GameObject chaffPrefab;
    public float cooldown = 5f;
    private float nextUseTime = 0f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(hellfirePrefab, hardPoint.position, hardPoint.rotation); // Spawns missile at the hardpoint with the same rotation
        }
        if (Input.GetKeyDown(KeyCode.F) && Time.time > nextUseTime)
        {
            DeployChaff();
            nextUseTime = Time.time + cooldown; // Start cooldown
        }
    }
    void DeployChaff()
    {
        Instantiate(chaffPrefab, transform.position, Quaternion.identity);
    }
}
