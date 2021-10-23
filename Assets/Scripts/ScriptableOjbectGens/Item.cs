using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public enum TYPE {GUN, FOOD };
[CreateAssetMenu(fileName = "New Hun", menuName = "Item/Item")]
public class Item : ScriptableObject
{
 
    public TYPE ITEM_TYPE;
    public float EquipTime;
    public GameObject Prefab;
    public GameObject Prefab_Laying;
    public Sprite Icon;



}
