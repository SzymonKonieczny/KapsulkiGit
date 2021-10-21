using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EQslot : MonoBehaviour
{
    public Item item;
    public Image Icon;
    public void SetItem(Item newItem)
    {
        item = newItem;
        Icon.sprite = item.Icon;

    }
    

}
