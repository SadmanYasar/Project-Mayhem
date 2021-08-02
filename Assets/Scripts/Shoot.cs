using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    [SerializeField] private Rigidbody bullet;

    [SerializeField] private Transform barrel;

    [SerializeField] private float bulletspeed = 1;
    [SerializeField] private float firerate = .2f;
    [SerializeField] private ParticleSystem muzzleFlash;

    private float nextfire = 0f;
    private bool Equipped;

    private int ammoCapacity = 10;


    // Update is called once per frame
    void Update()
    {
        if ( Input.GetMouseButton(0) && Time.time > nextfire && GetComponent<PIckUpDropController>().equipped && ammoCapacity>0 )
        {
            ammoCapacity--;
            muzzleFlash.Play();
            Camera.main.transform.position = Camera.main.transform.position - (Player.Direction*0.1f);
            nextfire = Time.time + firerate;
            var spawnBullet = Instantiate(bullet, barrel.position,barrel.rotation);
            spawnBullet.AddForce(Player.Direction * bulletspeed);
        }

        Reload();

        
    }

    void Reload() {
        if ( ammoCapacity < 10 && Input.GetKeyDown(KeyCode.R) )
        {
            ammoCapacity = 10;
        }
    }
}
