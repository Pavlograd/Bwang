using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    [SerializeField] Transform arrival;

    void OnTriggerEnter(Collider collider)
    {
        Transform collisionObject = collider.transform;

        if (collisionObject.tag == "Player")
        {
            Debug.Log("Collision with player");
            collisionObject.parent.transform.position = arrival.position;
            collisionObject.parent.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero; // Stop player to prevent loop with physic
        }
    }
}
