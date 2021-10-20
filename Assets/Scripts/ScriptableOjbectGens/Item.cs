using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public enum TYPE {GUN, FOOD };
public class Item : ScriptableObject
{
 
    public TYPE ITEM_TYPE;
    public float EquipTime;
    public GameObject Prefab;
    public Sprite Icon;



}
