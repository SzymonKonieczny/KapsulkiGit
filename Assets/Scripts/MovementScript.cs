using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
public class MovementScript : MonoBehaviourPunCallbacks
{
    public LayingItem item;
    public CharacterController controller;
    public Weapon weapon_script;
    public Camera cam;
    public LayerMask WeapoPickMask;
    public float speed = 10;
    public float SprintMultiplier=2;
    float SprintMultiplier_with_check=1;
    public float Gravity = -1.81f;
    public Transform GroundCheck;
    public float GroundDistancs = 0.5f;
    public LayerMask groundMask;
    public Text HPText;
    public int maxHP;
    public bool isSliding;
    public ParticleSystem DashParticle;
    Menager menager;
    int HP;
    bool isGrounded;
    float x;
    Vector3 velocity;
    float z;
    Vector3 move;
    Inventory inventory;
    // Update is called once per frame
    private void Start()
    {
        menager = GameObject.Find("Menager").GetComponent<Menager>();
        HP = maxHP;
        HPText = GameObject.Find("Canvas/HPText").GetComponent<Text>();
        if (!photonView.IsMine) return;
      inventory= GameObject.Find("Canvas/Inventory").GetComponent<Inventory>();
            inventory.LocalPlayerReference = this.gameObject;
    }
    void Update()
    {
        if (!photonView.IsMine) return;
        isGrounded = Physics.CheckSphere(GroundCheck.position, GroundDistancs, groundMask);
        if(isGrounded && velocity.y <0)
            {
            velocity.y = 0f;
            }

        if(!isSliding)
        {
        x = Input.GetAxisRaw("Horizontal");
        z = Input.GetAxisRaw("Vertical");
        move = transform.right * x + transform.forward * z;
        }


       RaycastHit hit;

       if (Input.GetKeyDown(KeyCode.F))
       { 
            if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, 100, WeapoPickMask))
        {
            
            item = hit.transform.GetComponent<LayingItem>();
            if (item != null)
            {


                    inventory.AddItem(item.item_identity);

                   /* foreach (EQslot s in inventory.Slots)
                    {
                        if(s.item==null)
                        {
                        s.SetItem(item.item_identity);
                        break;
                        }
                        
                    } */

                
            }
                Debug.Log("RAyShot");
                if (hit.transform.GetComponent<ItemCrate>()!=null)
                {
                    Debug.Log("RayHitACrate");
                    hit.transform.GetComponent<ItemCrate>().Open();
                }
        }
       }

            SprintMultiplier_with_check = 1;
        if (Input.GetKey(KeyCode.LeftShift) && (z>0 && x ==0))
        {
            if(Input.GetKey(KeyCode.LeftControl)) ///Dashs
            {
                photonView.RPC("DashCall", RpcTarget.All, 1.0f);
            }
            else SprintMultiplier_with_check = SprintMultiplier;
            
        }

        if(isSliding) controller.Move(move*speed* SprintMultiplier_with_check*Time.deltaTime*3);
        else controller.Move(move * speed * SprintMultiplier_with_check * Time.deltaTime);

        if (isGrounded && Input.GetKey(KeyCode.Space)) velocity.y = 5f;

        velocity.y += Gravity * Time.deltaTime ; 

       controller.Move(velocity * Time.deltaTime);
    }
    [PunRPC]
    public void TakeDamage(int p_damage)
    {
        if(photonView.IsMine)
        {
        HP -= p_damage;
        HPText.text = HP.ToString();
            if(HP<=0)
            {
                menager.Spawn();
                PhotonNetwork.Destroy(gameObject);
                
            }
        }
        
    }
    [PunRPC]
    void DashCall(float waitTime)
    {
        
        StartCoroutine(Dash(waitTime));
    }
    IEnumerator Dash(float waitTime)
    {
        isSliding = true;
        DashParticle.Play();
        yield return new WaitForSeconds(waitTime);
        isSliding = false;
    }
}
