using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerBody : MonoBehaviour
{
    public PlanetGravity attract;
    private Transform mytrans;
    private Rigidbody rb;
    private void Start()
    {
        mytrans = transform;
        rb = gameObject.GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotation;
        rb.useGravity = false;
    }
    

    // Update is called once per frame
    void Update()
    {
        PlanetGravity.instance.pull(mytrans);
    }
}
