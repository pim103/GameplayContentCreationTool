using UnityEngine;
using System.Collections;

public class PlayerCamera: MonoBehaviour
{
    [SerializeField] private float speed = 2;
    [SerializeField] private float sensitivity = 1;
    [SerializeField] private GameObject player;

    private float yaw = 0.0f;
    private float pitch = 0.0f;

    void Update()
    {
        if(speed < 0)
        {
            speed = 0;
        }

        yaw += sensitivity * Input.GetAxis("Mouse X");
        pitch -= sensitivity * Input.GetAxis("Mouse Y");

        transform.eulerAngles = new Vector3(pitch, transform.eulerAngles.y, 0.0f);
        player.transform.eulerAngles = new Vector3(0.0f, yaw, 0.0f);

        if (Input.GetAxis("Mouse ScrollWheel") > 0f) // forward
        {
            speed += 0.1f;
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f) // backwards
        {
            speed -= 0.1f;
        }
    }
}


  