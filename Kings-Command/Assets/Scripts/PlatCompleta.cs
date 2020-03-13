using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
/*
 * Fija la posición de las plataformas inferiores
 * Cuenta las vidad restantes
 */
public class PlatCompleta : MonoBehaviour
{
	public int idPlat;	//Asignado por la fábrica
	public Text texto;	//Asignado por la fábrica
	public int tasaDeRefresco;
	private int i;
	
	void Start() {}
	
	void Update()
	{
		if (i++ >= tasaDeRefresco) {
			i = 0;
			GameObject sup = transform.GetChild(0).gameObject;
			texto.text = "" + (3 - sup.GetComponent<PlatSuperior>().platAlcanzadas);
			
			int aux = (int)(sup.transform.position.y * 1000);
			texto.text += ", " + aux * 0.001;
		}
	}
	
	/*
	 * Adopta a sus nietos para fijar su posición
	 */
	public void Iniciar()
	{
		Transform aux = transform.GetChild(0);
		int childs = aux.transform.childCount;
		for (int i = childs - 1; i >= 0; i--)
			aux.GetChild(i).parent = transform;
		
	}
}
