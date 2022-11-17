using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetGravity : MonoBehaviour
{
    public float gravity = -10;
    private Rigidbody rb;

    public  static PlanetGravity instance;

    private void Awake()
    {
        if(instance == null)instance = this;
    }


    public void pull(Transform player)
    {
        Vector3 playerGravDir = player.up;
        Vector3 planetGravDir = (player.position - transform.position).normalized;
        rb = player.GetComponent<Rigidbody>();

        rb.AddForce(planetGravDir * gravity);

        Quaternion targetRotation = Quaternion.FromToRotation(playerGravDir, planetGravDir) * player.rotation;
        player.rotation = Quaternion.Slerp(player.rotation, targetRotation, 50 * Time.deltaTime);

    }
}
