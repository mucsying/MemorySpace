using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemoryFairyView : MonoBehaviour
{
    public float viewField = 180f;
    public bool isPlayerFound = false;
    public Vector3 playerLastPos;
    public float patrolRad = 7.0f;
    public float trackRad = 10.0f;

    private CapsuleCollider col;
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        col = GetComponent<CapsuleCollider>();
        col.radius = patrolRad;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Vector3 dir = other.transform.position - transform.position;
            float angle = Vector3.Angle(dir, transform.forward);

            if (angle < viewField * 0.5)
            {
                isPlayerFound = true;
                col.radius = trackRad;
                playerLastPos = player.transform.position;
            }
        }
    }

    private void Update()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
}
