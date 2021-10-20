using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public int HP = 100;
    public int damage = 20;
    [SerializeField] PlayerScript Player;
   [SerializeField] bool InRange = false;
    [SerializeField] bool HitCoolDown = false;
    public List<GameObject> Model;
    // Update is called once per frame
    void Update()
    {
        if (InRange && !HitCoolDown)
        {
           StartCoroutine(Hit(1f));
        }
    }
    private void Awake()
    {
        foreach (GameObject m in Model)
        {
          // m.GetComponent<Rigidbody>().detectCollisions = false;


        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            InRange = false;
            Player = null;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            InRange = true;
            Player = other.gameObject.GetComponent<PlayerScript>();
        }
    }

    public void  takeDamage(int damage)
    {
        HP -= damage;
        if(HP<=0)
        {
            Rigidbody Rb;
            foreach (GameObject m in Model)
            {
                Rb = m.GetComponent<Rigidbody>() ;
                Rb.detectCollisions = true;
                Rb.AddForce(new Vector3(2, 4, 1));
            }
            Destroy(this, 3f);
        }
        
    }

    IEnumerator Hit(float time)
    {
        Debug.Log("Hitting");
        HitCoolDown = true;
        yield return new WaitForSeconds(time);
       if(Player) Player.takeDamage(damage);
        HitCoolDown = false;
        Debug.Log("done hitting");
    }
}
