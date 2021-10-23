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

    private void Update()
    {
        //if (!photonView.IsMine) return;
        if (Input.GetKeyUp(KeyCode.E)) InventorySlots.SetActive(!InventorySlots.activeInHierarchy);

    }
    public void EquipItem(Item item)
    {
        LocalPlayerReference.GetComponent<Weapon>().Loadout[0] = (Gun)item;
    }



}
