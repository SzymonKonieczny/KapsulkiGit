using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun_camera_rotation : MonoBehaviour
{
    public Camera CameraTransform;

    // Update is called once per frame
    void Update()
    {
        Vector3 rot = transform.rotation.eulerAngles;
        rot = new Vector3(CameraTransform.transform.rotation.x, rot.y, rot.z);
        transform.rotation = Quaternion.LookRotation(CameraTransform.transform.forward, Vector3.up);

        
    }
}
