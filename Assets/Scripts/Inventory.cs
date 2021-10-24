using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
public class Inventory : MonoBehaviourPunCallbacks
{
    public EQslot[] Slots;
    public GameObject InventorySlots;
    public GameObject LocalPlayerReference; //The players MovementScript will assign the playerObj to this ref will
    [SerializeField] Weapon weaponScript;
   
    private void Update()
    {
        //if (!photonView.IsMine) return;
        if (Input.GetKeyUp(KeyCode.E)) InventorySlots.SetActive(!InventorySlots.activeInHierarchy);

    }
    public void EquipItem(Item item)
    {
        weaponScript = LocalPlayerReference.GetComponent<Weapon>();
        weaponScript.Loadout[0] = (Gun)item;
        Debug.Log("ON CLICK2");
        /*
        for(int i=0; i < weaponScript.Loadout.Length; i++)
        {
            if(weaponScript.Loadout[i] != null)
            {
                weaponScript.Loadout[i] = (Gun)item;
                Slots[i].SetItem(item);
            }
        }*/


    }



}
