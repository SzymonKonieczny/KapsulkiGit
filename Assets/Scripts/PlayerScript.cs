using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public int HP = 100;
    // Update is called once per frame
    void Update()
    {
        
    }
    public void takeDamage(int dmgTaken)
    {
        HP = -dmgTaken;
    }
}
