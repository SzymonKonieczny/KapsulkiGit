using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sway : MonoBehaviour
{
    public float smoothe;
    public float intensity;
    Quaternion origin_rotation;

    // Start is called before the first frame update
    void Start()
    {
        origin_rotation = transform.localRotation;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateSway();
    }
    void UpdateSway()
    {
        float t_xMouse = Input.GetAxis("Mouse X");
        float t_yMouse = Input.GetAxis("Mouse Y");
        Quaternion t_xadjust = Quaternion.AngleAxis(intensity * t_xMouse, Vector3.up);
        Quaternion t_yadjust = Quaternion.AngleAxis(intensity * t_yMouse, -Vector3.right);
        Quaternion target_rotation = origin_rotation * t_xadjust* t_yadjust;


        transform.localRotation = Quaternion.Lerp(transform.localRotation, target_rotation, smoothe * Time.deltaTime);
         

    }
}

