using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    [SerializeField] ParticleSystem spark;
    private void OnCollisionEnter(Collision other) {
        spark.Play();
        Destroy(gameObject);
    }
}
