using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Element : MonoBehaviour
{
    [SerializeField] Collider[] colliders;
    [SerializeField] Rigidbody rgBody;
    [SerializeField] float mass; // Independant of rgBody, it will be the mass given to PlayerManager

    void Awake()
    {
        gameObject.tag = "Element"; // Security in case object is untagged
    }

    void Start()
    {
        if (rgBody != null)
        { // Some elements don't have a rgBody
            rgBody.mass = mass; // Prevent typing two times the mass
        }
    }

    public float GetMass()
    {
        return mass;
    }

    public void AutoDestroy()
    {
        tag = "Untagged";

        if (rgBody != null)
        { // Some elements don't have a rgBody
            Destroy(rgBody);
        }

        foreach (Collider item in colliders)
        {
            Destroy(item);
        }

        Destroy(this);
    }

    public void InvokeAutoDestroy()
    {

    }
}
