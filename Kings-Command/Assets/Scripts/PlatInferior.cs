using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/* 
 * Avisa a la plataforma superior cuando es alcanzado.
 */
public class PlatInferior : MonoBehaviour
{
	public GameObject superior;	//Asignado por fuera
	public int	idPlat;		//Asignado por fuera
	public float	tiempoLimite;	//Asignado por fuera
	private bool	tocado;
	private int	tiempoTocando;
	
	void Start() {}
	
	void Update() {}
	
	private void OnTriggerStay(Collider other)
	{
		if (other.gameObject != superior || tocado)
			return;
		tiempoTocando++;
		if (tiempoTocando >= tiempoLimite) {
			superior.GetComponent<PlatSuperior>().TouchedBy(idPlat);
			tocado = true;
		}
	}
	
	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject == superior && !tocado)
			tiempoTocando = 0;
	}
	private void OnTriggerExit(Collider other)
	{
		if (other.gameObject == superior && !tocado)
			tiempoTocando = 0;
	}
}
