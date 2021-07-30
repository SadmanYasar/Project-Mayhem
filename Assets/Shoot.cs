using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    [SerializeField] private Rigidbody bullet;

    [SerializeField] private Transform barrel;

    private float bulletspeed = 1000;
    private float firerate = 1f;
    private float nextfire = 0f;


    // Update is called once per frame
    void Update()
    {
        if ( Input.GetMouseButton(0) && Time.time > nextfire )
        {
            nextfire = Time.time + firerate;
            var spawnBullet = Instantiate(bullet, barrel.position,barrel.rotation);
            spawnBullet.AddForce(Player.Direction * bulletspeed);
        }
    }
}
