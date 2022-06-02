using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] float height;
    [SerializeField] float distance;
    [SerializeField] float turnSpeed;
    private Vector3 offsetX;
    private Vector3 offsetY;
    private float lastSize = 1.0f; // prevent miscalcul with 0.0f

    // Start is called before the first frame update
    void Start()
    {
        offsetX = new Vector3(0, height, distance);
    }

    public void Init() // Init size only after player start
    {
        offsetX = new Vector3(0, height, distance);
        lastSize = GameManager.instance.player.GetSize();
        Debug.Log(lastSize);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (Time.timeScale == 0) return; // Pause
        offsetX = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * turnSpeed, Vector3.up) * offsetX;

        transform.position = player.position + offsetX;
        transform.LookAt(player.position);
    }

    public void UpdateOffset(float size)
    {
        offsetX *= size / lastSize;
        lastSize = size;
    }
}
