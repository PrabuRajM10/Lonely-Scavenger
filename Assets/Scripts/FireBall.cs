using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    bool canPull = true;
    Rigidbody rb;
    MeshRenderer mr;
    SphereCollider sc;

    [SerializeField]
    GameObject hitEffect,explosionEffect , crystal;

    float canAffectTimer = 1.5f;


    float hitObjDisolveWait = 0.5f;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        mr = GetComponent<MeshRenderer>();
        sc = GetComponent<SphereCollider>();

    }
    void Update()
    {
        if(canPull)PlanetGravity.instance.pull(transform);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.gameObject.tag == "World")
        {
            canPull = false;
            //rb.freezeRotation = true;
            rb.constraints = RigidbodyConstraints.FreezeAll;
            mr.enabled = false;
            transform.GetChild(0).gameObject.SetActive(false);

            var hitObj = Instantiate(hitEffect, collision.GetContact(0).point, Quaternion.identity);
            hitObj.transform.forward = collision.GetContact(0).normal;

            var explosionOBJ = Instantiate(explosionEffect, collision.GetContact(0).point, Quaternion.identity);
            explosionOBJ.transform.SetParent(transform);
            hitObj.transform.SetParent(transform);

            HandleCrystalSpawn(collision.GetContact(0).point , transform.rotation);

            StartCoroutine(DestroyObject(hitObj));
        }
    }

    void HandleCrystalSpawn(Vector3 contactPoint , Quaternion rot)
    {
        var maxRandVal = 10;
        var rVal = Random.Range(1, maxRandVal);
        var prob = Mathf.Abs( ( (maxRandVal / 2 ) * Spawner.spawner.timeToSpawn) - maxRandVal);
        if(rVal > prob)
        {
            var crystalObj = Instantiate(crystal, contactPoint, rot);
            crystalObj.transform.SetParent(transform);
        }
    }

    IEnumerator DestroyObject(GameObject hitObj)
    {
        var sr = hitObj.GetComponent<SpriteRenderer>().material;
        while(sr.color.a >= 0)
        {
            hitObj.GetComponent<SpriteRenderer>().material.color = new Color(sr.color.r, sr.color.g, sr.color.b, (sr.color.a - 0.1f));

            //if (sr.color.a <= 0.85f && sc.enabled)sc.enabled = false; 
            sc.enabled = false;

            yield return new WaitForSeconds(hitObjDisolveWait);
        }
        Destroy(gameObject);
    }
}
    