using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Wallcpfur : MonoBehaviour
{
    public int wallHealth;
    public BoxCollider boxCollider;
    public Rigidbody rigid;



    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Melee")
        {
            wallHealth = wallHealth - 20;
        }

    }

    void Awake()
    {
        boxCollider = GetComponent<BoxCollider>();
        rigid = GetComponent<Rigidbody>();
    }




    public void HitByWallGrenade(Vector3 explosionPos)
    {
        wallHealth -= 100;
    }

    void FixedUpdate()
    {
        Destroy();
    }

    void Destroy()
    {
        if (wallHealth <= 0)
        {
            rigid.useGravity = true;
            rigid.isKinematic = false;
        }
    }

}