using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alcance : MonoBehaviour
{
	private List<GameObject> objAlAlcance;
	
	void Start()
	{
		objAlAlcance = new List<GameObject>();
	}
	
	void Update() {}
	
	private void OnTriggerEnter(Collider other)
	{
		GameObject aux = other.gameObject;
		if (aux.tag != "ObjetoArrojable")
			return;
		if (!objAlAlcance.Contains(aux))
			objAlAlcance.Add(aux);
	}
	
	private void OnTriggerExit(Collider other)
	{
		objAlAlcance.Remove(other.gameObject);
	}
	
	public GameObject GetObject()
	{
		if (objAlAlcance.Count == 0)
			return null;
		GameObject aux = objAlAlcance[0];
		objAlAlcance.RemoveAt(0);
		aux.SetActive(false);
		return aux;
	}
}
