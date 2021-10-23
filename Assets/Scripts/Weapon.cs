using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
public class Weapon : MonoBehaviourPunCallbacks
{
    public Gun[] Loadout;
    public Transform WeaponParent;
     GameObject CurrentWeapon;
    public float AimSpeed;
    public GameObject BulletHolePrefab;
    public Camera cam;
    public Transform CamTransform;
    int EquipedID;
    bool Cooldown;
    public LayerMask mask;
    public Rigidbody rb;
    Transform wylotlufy;
    [SerializeField] bool isReloading;
    public Text AmmoText;
    [SerializeField] public Image ScopeImage;
    bool wasTriggerPressed;
    float FOV;
    public AudioSource Sound;
    public float ammo;
    // Start is called before the first frame update
    void Start()
    {
        
        AmmoText = GameObject.Find("Canvas/AmmoText").GetComponent<Text>();

        //AmmoText.text = Loadout[EquipedID].ammo.ToString();
        ScopeImage = GameObject.Find("Canvas/ZoomImg").GetComponent<Image>();
        FOV = cam.fieldOfView;
        if (!photonView.IsMine) return;

    }

    // Update is called once per frame
    void Update()
    {
        if (!photonView.IsMine) return;

        if (Input.GetKeyDown(KeyCode.Alpha1)) photonView.RPC("equip", RpcTarget.All,0);
        else if (Input.GetKeyDown(KeyCode.Alpha2)) photonView.RPC("equip", RpcTarget.All, 1);
        else if (Input.GetKeyDown(KeyCode.Alpha3)) photonView.RPC("equip", RpcTarget.All, 2);
        if (Input.GetMouseButtonUp(0)) photonView.RPC("ReleaseTrigger", RpcTarget.All);
        if (CurrentWeapon!=null)
        {
            if (Input.GetKeyDown(KeyCode.R)) photonView.RPC("Reload", RpcTarget.All, 1.0f);
            //Aim(Input.GetMouseButton(1));
            photonView.RPC("Aim", RpcTarget.All, (Input.GetMouseButton(1)));
            if (Input.GetMouseButton(0) && Loadout[EquipedID].ammo>0) photonView.RPC("Shoot", RpcTarget.All);
        }
       
    }
    [PunRPC]
    void ReleaseTrigger()
    {
        wasTriggerPressed = false;
    }
    [PunRPC]
    void equip(int slot)
    {
        if (CurrentWeapon != null) Destroy(CurrentWeapon);
        if (slot >= Loadout.Length || Loadout[slot]==null) return;
        GameObject newWeapon = Instantiate(Loadout[slot].Prefab, WeaponParent.position,WeaponParent.rotation, WeaponParent);
        newWeapon.transform.localPosition = Vector3.zero;
        newWeapon.transform.localEulerAngles = Vector3.zero;
        CurrentWeapon = newWeapon;
        EquipedID = slot;
        wylotlufy  = CurrentWeapon.transform.Find("Anchor/Resources/wylotlufy");
       AmmoText.text=  Loadout[EquipedID].ammo.ToString();
        StartCoroutine(ShootCooldown(Loadout[EquipedID].EquipTime));
    }
    [PunRPC]
    void Aim(bool isAiming)
    {
        if (CurrentWeapon == null) return;
        Transform t_anchor = CurrentWeapon.transform.GetChild(0);
        Transform t_anchor_hip = CurrentWeapon.transform.GetChild(1).transform.GetChild(0);
        Transform t_anchor_ads = CurrentWeapon.transform.GetChild(1).transform.GetChild(1);
        if (isAiming)
        {
            t_anchor.position = Vector3.Lerp(t_anchor.position, t_anchor_ads.position, Time.deltaTime * AimSpeed);

            if (Loadout[EquipedID].Scope && photonView.IsMine && ScopeImage.enabled==false)
            {
                ScopeImage.enabled = true;
                cam.fieldOfView = 20;
                CurrentWeapon.SetActive(false);
            }
        }
        else
        {
            t_anchor.position = Vector3.Lerp(t_anchor.position, t_anchor_hip.position, Time.deltaTime * AimSpeed);
            if (Loadout[EquipedID].Scope && photonView.IsMine && ScopeImage.enabled == true)
            {
                cam.fieldOfView = FOV;
                ScopeImage.enabled = false;
                CurrentWeapon.SetActive(true);
            }
        }
    }
    [PunRPC]
    void Shoot()
    {
        if(Loadout[EquipedID].ITEM_TYPE == TYPE.FOOD)
        {
           
            
            return;
        }

        if (Cooldown || isReloading) return;
        if (!Loadout[EquipedID].Shooting) return;
        if (!Loadout[EquipedID].automatic && wasTriggerPressed) return;
        wasTriggerPressed = true;
        ammo = Loadout[EquipedID].ammo;

        if (photonView.IsMine)
        {
            Loadout[EquipedID].ammo--;
           
            AmmoText.text = Loadout[EquipedID].ammo.ToString();
        }
        Sound.Play();
        ParticleSystem BulletHitParticles;

        BulletHitParticles = Instantiate(Loadout[EquipedID].ParticlePrefab, wylotlufy.transform.position, Quaternion.LookRotation(cam.transform.forward, Vector3.up));
        BulletHitParticles.Play();
        Destroy(BulletHitParticles.gameObject, 2);
        GameObject BulletHole;
        RaycastHit hit;
         
      
  
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, Loadout[EquipedID].range, mask))
        //if (Physics.Raycast(CamTransform.position, CamTransform.forward, out hit, Loadout[EquipedID].range, mask))
        {
       //if(hit.collider.gameObject.GetPhotonView().IsMine) Physics.Raycast(hit.transform.position, cam.transform.forward, out hit, Loadout[EquipedID].range, mask);


            BulletHitParticles = Instantiate(Loadout[EquipedID].ParticlePrefab, hit.point, Quaternion.LookRotation(-cam.transform.forward, Vector3.up));
          // BulletHitParticles.transform.rotation = Quaternion.LookRotation(-cam.transform.forward, Vector3.up);
            BulletHitParticles.Play();
            Destroy(BulletHitParticles.gameObject, 2);
            BulletHole = Instantiate(BulletHolePrefab, hit.point + hit.normal * 0.01f, Quaternion.identity);
            BulletHole.transform.LookAt(hit.point + hit.normal);
            Destroy(BulletHole.gameObject, 5);
           
            {
                rb = hit.transform.GetComponent<Rigidbody>();
                if (Loadout[EquipedID].PushOnImpact && rb != null)
                {

                    rb.AddForce(cam.transform.forward * Loadout[EquipedID].pushForce);
                    
                }
                else
                {
                   

                }
            }
            if(photonView.IsMine)
            {
                //If we are shooting at a player
                if(hit.collider.transform.gameObject.layer == 11)
                {
                  hit.collider.gameObject.GetPhotonView().RPC("TakeDamage", RpcTarget.All, Loadout[EquipedID].damage);
                }
            }
            
         }
        cam.transform.rotation *= Quaternion.Euler(Loadout[EquipedID].Recoil);
        StartCoroutine(ShootCooldown(1/Loadout[EquipedID].RoF));
        

    }
    IEnumerator ShootCooldown(float waitTime)
    {
        Cooldown = true;
     yield return new WaitForSeconds(waitTime);
        Cooldown = false;
    }
    private void TakeDamage(int damage)
    {
        GetComponent<MovementScript>().TakeDamage(damage);
    }
    [PunRPC]
    void Reload(float waitTime)
    {
        StartCoroutine(ReloadCor(waitTime));
    }
    IEnumerator ReloadCor(float waitTime)
    {
        isReloading = true;
        
        yield return new WaitForSeconds(waitTime);

        if (photonView.IsMine)
        {
            AmmoText.text = Loadout[EquipedID].Maxammo.ToString();
            Loadout[EquipedID].ammo = Loadout[EquipedID].Maxammo;
        }
        isReloading = false;

    }
}
