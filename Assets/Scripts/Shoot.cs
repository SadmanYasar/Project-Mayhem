using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    [SerializeField] private Rigidbody bullet;

    [SerializeField] private Transform barrel;
    
    [SerializeField] private float firerate = .2f;
    [SerializeField] private ParticleSystem muzzleFlash;

    private float nextfire = 0f;
    private bool Equipped;

    public int ammoCapacity = 15;

    //For object pooling
    public GameObject bulletPrefab;

    private void Start() {
        PoolManager.instance.CreatePool(bulletPrefab, 100);
    }

    // Update is called once per frame
    void Update()
    {
        if ( Input.GetMouseButton(0) && Time.time > nextfire && GetComponent<PIckUpDropController>().equipped && ammoCapacity>0 )
        {
            //Method 4
            GameManager.shotByPlayer = 1;
            PoolManager.instance.ReuseObject(bulletPrefab, barrel.position, barrel.rotation);

            ammoCapacity--;
            muzzleFlash.Play();
            //audioManager.Play("ShootSound");
            AudioManager.instance.Play("ShootSound");
            Camera.main.transform.position = Camera.main.transform.position - (Player.Direction*0.2f);
            nextfire = Time.time + firerate;

            
           
            GameManager.ammoText.text = ammoCapacity.ToString();
        }

        Reload();

        
    }

    void Reload() {
        if ( ammoCapacity <= 0 && GetComponent<PIckUpDropController>().equipped )
        {
            GameManager.ammoText.text = "No ammo!";
        }
    }

}
