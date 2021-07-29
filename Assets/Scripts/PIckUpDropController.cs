using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PIckUpDropController : MonoBehaviour
{
    public Rigidbody rb;
    public BoxCollider coll;
    public Transform player, gunContainer, camera;

    public float pickupRange;
    public float dropForwardForce, dropUpForce;

    public bool equipped;
    public static bool slotfull;

    void Update() {
        //check if inrange and right clicked
        Vector3 distanceToPlayer = player.position - transform.position;
        if (!equipped && distanceToPlayer.magnitude <= pickupRange && Input.GetMouseButton(1) && !slotfull )
        {
            PickUp();
        }
        if ( equipped && Input.GetMouseButton(1) )
        {
            Drop();
        }
    }

    private void PickUp() {
        equipped = true;
        slotfull = true;

        rb.isKinematic = true;
        coll.isTrigger = true;

    }

    private void Drop() {
        equipped = false;
        slotfull = false;

        rb.isKinematic = false;
        coll.isTrigger = false;
    }

}
