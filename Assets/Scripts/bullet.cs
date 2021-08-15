using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    [SerializeField] ParticleSystem spark;
    private void OnCollisionEnter(Collision other) {
        if ( other.gameObject.CompareTag("Enemy") )
        {
            other.gameObject.GetComponent<Enemy>().Die();
        }
        spark.Play();
        Destroy(gameObject);
    }
}
