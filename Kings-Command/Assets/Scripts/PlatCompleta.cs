using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * Sube la plataforma al comenzar el juego (Como si la preparara el rey).
 */
public class PlatCompleta : MonoBehaviour
{
	public int idPlat;	//Asignado por el Manager
// 	public float velocidad;	//Asignado por fuera
// 	private bool enPosicion;
	
	void Start()
	{
// 		enPosicion = false;
	}
	
	void Update()
	{
// 		if (enPosicion)
// 			return;
// 		Vector3 aux = new Vector3(
// 			transform.position.x,
// 			0,
// 			transform.position.y
// 		);
// 		transform.position = Vector3.MoveTowards(
// 			transform.position,
// 			aux,
// 			velocidad * Time.smoothDeltaTime
// 		);
// 		if (transform.position.y == 0) {
// 			enPosicion = true;
// 			GetComponentInParent<Manager>().TraerJugador(idPlat);
// 		}
	}
	
	/*
	 * Adopta a sus nietos para fijar su posición
	 */
	public void Iniciar()
	{
		Transform aux = transform.GetChild(0);
		int childs = transform.childCount;
		for (int i = childs - 1; i >= 0; i--)
			aux.GetChild(i).parent = transform;
		
	}
}
