using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class Look : MonoBehaviourPunCallbacks
{
    #region Variables
    public Transform Player;
    public Transform cams;
    public GameObject CamerasParent;
    public Transform weapon;
    public float xsens;
    public float ysens;
    public float maxAngle;
    public static bool CursorLocked = true;
    bool cursorlockcooldown;
     [SerializeField] Camera camera;
    AudioListener listener;

    Quaternion camcenter;
    #endregion
    #region MonoBeh. callbacks
    // Start is called before the first frame update
    void Start()
    {

        if (!photonView.IsMine)
        {
         gameObject.layer = 11;
            
        }
        else
        {
            //camera = GameObject.Find("Cameras/Main Camera").GetComponent<Camera>();
            camera = transform.Find("Cameras/Main Camera").GetComponent<Camera>();
            camera.enabled = true;
            listener =  transform.Find("Cameras/Main Camera").GetComponent<AudioListener>();
            listener.enabled = true;
            Debug.LogWarning("Turning on The camera");
            camcenter = cams.localRotation; //oryginalna rotacja (straight przed nami)
        }

       // CamerasParent.SetActive(photonView.IsMine);
       
    }

    // Update is called once per frame
    void Update()
    {
         
        if (!photonView.IsMine) return;
        setY();
        setX();
         if(!cursorlockcooldown && Input.GetKeyDown(KeyCode.Escape))StartCoroutine(UpdateCursorLock());


    }
    #endregion
    public void UpdateCursorLockFromAnOutsideScript()
    {
        StartCoroutine(UpdateCursorLock());
    }
    void setY()
    {
        float t_input = Input.GetAxis("Mouse Y") * ysens * Time.deltaTime;
        Quaternion t_adjust = Quaternion.AngleAxis(t_input,-Vector3.right);
        Quaternion t_delta = cams.localRotation* t_adjust;

        if (Quaternion.Angle(camcenter, t_delta) < maxAngle)
        {
            cams.localRotation = t_delta;
            weapon.localRotation = t_delta;
         }

       
    }
    void setX()
    {
        float t_input = Input.GetAxis("Mouse X") * xsens * Time.deltaTime;
        Quaternion t_adjust = Quaternion.AngleAxis(t_input, Vector3.up);
        Quaternion t_delta = Player.localRotation * t_adjust;
        Player.localRotation = t_delta;
    }
    IEnumerator UpdateCursorLock()
    {
        cursorlockcooldown = true;
       if(CursorLocked)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            CursorLocked = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            CursorLocked = true;
        }

        yield return new WaitForSeconds(1f);
        cursorlockcooldown = false;
    }
}
