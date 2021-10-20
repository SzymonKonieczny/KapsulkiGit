using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName ="New Hun", menuName ="Item/Gun")]
public class Gun : Item
{


    public bool secondary;
    public ParticleSystem ParticlePrefab;
    public string name;
    public float RoF;
    
    public int damage;

    public float range;
    public float pushForce;
    public bool PushOnImpact;
    public bool Shooting;
    public bool automatic;
    public bool Scope;
    public int ammo;
    public int Maxammo;
    public Vector3 Recoil;
  


}
