using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour
{
	void Start() {}
	
	void Update() {}
	
	private void OnTriggerEnter(Collider other)
	{
		Fabrica.fabrica.Destruir(other.gameObject);
	}
}
