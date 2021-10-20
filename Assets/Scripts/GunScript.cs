using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScript : MonoBehaviour
{
    public int damage = 50;
    public float range = 10f;
    public Camera cam;
    
    public ParticleSystem PArticlePrefab;
    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
    }
    void Shoot()
    {
        ParticleSystem BulletHitParticles;
        RaycastHit hit;
        LayerMask mask = LayerMask.GetMask("Ground");
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, range,mask))
        {

            BulletHitParticles = Instantiate(PArticlePrefab, hit.point, Quaternion.LookRotation(-cam.transform.forward, Vector3.up));
            
            BulletHitParticles.transform.rotation = Quaternion.LookRotation(-cam.transform.forward, Vector3.up);
            Entity HitEntity = hit.transform.GetComponent<Entity>();
            if (HitEntity) HitEntity.takeDamage(damage);
            BulletHitParticles.Play();
            Destroy(BulletHitParticles.gameObject,2);

        }

    }

}
