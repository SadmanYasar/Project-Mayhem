using UnityEngine;
using System.Collections;

public class door : MonoBehaviour {
	public GameObject thedoor;

void OnTriggerEnter ( Collider obj  ){
	if (obj.gameObject.CompareTag("Player") )
	{
		//thedoor= GameObject.FindWithTag("SF_Door");
		thedoor.GetComponent<Animation>().Play("open");
	}
	
}

void OnTriggerExit ( Collider obj  ){
	if (obj.gameObject.CompareTag("Player") )
	{
		//thedoor= GameObject.FindWithTag("SF_Door");
		thedoor.GetComponent<Animation>().Play("close");
	}
	
}
}