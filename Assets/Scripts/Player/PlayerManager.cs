using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] Rigidbody rgBody;
    [SerializeField] PlayerData data;
    [SerializeField] Transform cam;
    [SerializeField] Transform sphere; // first sphere of the player
    [SerializeField] SFXManager _SFXManager;
    float force;
    float density;
    float size = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        InitData();
    }

    void InitData()
    {
        force = data.force;
        density = data.density; // Density of the sphere
        rgBody.mass = data.startMass;
        CalculateNewSize(); // calculate size from mass
        GameManager.instance.cam.Init();
        sphere.transform.localScale = Vector3.one * size;
    }

    public float GetSize()
    {
        return size;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Escape key was pressed");
            GameManager.instance.TogglePause();
        }
    }

    void FixedUpdate()
    {
        float Horizontal = Input.GetAxis("Horizontal") * force;
        float Vertical = Input.GetAxis("Vertical") * force;

        Vector3 forceVector = cam.transform.right * Horizontal + cam.transform.forward * Vertical; // accord direction with current camera rotation
        forceVector.y = 0f; // Never goes in the sky for no reason
        //Debug.Log("Foce: " + forceVector * size * Time.deltaTime);
        rgBody.AddForce(forceVector * rgBody.mass * Time.deltaTime);
    }

    void OnCollisionEnter(Collision collision)
    {
        GameObject collisionObject = collision.gameObject;

        if (collisionObject.tag == "Element")
        {
            Debug.Log("Collision with element");

            Incorporate(collisionObject.GetComponent<Element>());
        }
        else
        {
            Debug.Log("Collision without an element");
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        GameObject collisionObject = collider.gameObject;

        if (collisionObject.tag == "Donut")
        {
            Debug.Log("uhmmm donut");
            GameManager.instance.End();
        }
    }

    void Incorporate(Element element)
    {
        if (element.GetMass() <= rgBody.mass / 5.0f) // Can be incorporated if only 10% of the max mass
        {
            element.transform.parent = transform; // Object will now move with us
            rgBody.mass += element.GetMass();

            element.AutoDestroy(); // Will remove all physical aspect of the element
            CalculateNewSize(); // Calculate new radius for the sphere
            _SFXManager.ChangeState("Plop");
        }
    }

    void CalculateNewSize()
    {
        float radius = 0.0f;
        float volume = rgBody.mass / density;

        radius = (volume / 3.14f) * 0.75f; // V / Pi * 3/4
        radius = Mathf.Pow(radius, 1f / 3f); // r exponent 1/3

        size = radius * 2.0f;
        GameManager.instance.cam.UpdateOffset(size);

        Debug.Log(size);
        sphere.transform.localScale = Vector3.one * size;
    }
}
