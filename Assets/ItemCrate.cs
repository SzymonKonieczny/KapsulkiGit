using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCrate : MonoBehaviour
{
    // Start is called before the first frame update
    public Item item;
    //public Item item_backup;

    public void Open()
    {
        for(int i = 0; i < 3; i++)
        {
            GameObject obj = Instantiate(item.Prefab_Laying);
            obj.transform.position = transform.position;
            obj.GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(1, 5), Random.Range(1, 5), Random.Range(1, 50)));
        }

    }

    public void SetItem()
    {

    }
    public void TakeItem(Inventory inv)
    {
        
       

    }
    IEnumerator ResetItem(Item item_p,float waitTime)
    {

        yield return new WaitForSeconds(waitTime);
    }
    void Update()
    {
        
    }
}
