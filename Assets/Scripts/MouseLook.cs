using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class MouseLook : MonoBehaviourPunCallbacks
{
    float MouseX;
    float MouseY;
    public float Sensivity = 100;
    public Transform PlayerBody;
    float xRotation = 0f;
    public Transform Weapon;
    // Start is called before the first frame update
    void Start()
    {
        if (!photonView.IsMine) return;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if (!photonView.IsMine) return;
        MouseX = Input.GetAxis("Mouse X") * Sensivity * Time.deltaTime;
        MouseY = Input.GetAxis("Mouse Y") * Sensivity * Time.deltaTime;
        xRotation -= MouseY;
        xRotation = Mathf.Clamp(xRotation, -90, 90);
        PlayerBody.Rotate(Vector3.up * MouseX);
        Weapon.Rotate(Vector3.up * MouseX);
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

    }
  
}
