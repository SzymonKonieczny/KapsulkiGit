using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayingItem : MonoBehaviour
{
    public Item item_identity;
    public void Destroy_me()
    {
        Destroy(this.gameObject);
    }

}
