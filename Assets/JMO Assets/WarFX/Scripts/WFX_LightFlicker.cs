using UnityEngine;
using System.Collections;


[RequireComponent(typeof(Light))]
public class WFX_LightFlicker : MonoBehaviour
{
    public Light gunlight;
    private bool Equipped;

    private void Start() {
        Equipped = GetComponent<PIckUpDropController>().equipped;
    }

    void Update() {
        if ( Equipped == true )
        {
            gunlight.gameObject.SetActive(false);
        } else {
            gunlight.gameObject.SetActive(true);
        }
    }
}
