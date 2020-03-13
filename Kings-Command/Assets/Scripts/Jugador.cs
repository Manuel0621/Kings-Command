using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * Personaje provisional. Será removido.
 * Se mueve en dos direcciones.
 * Levanta y lanza objetos.
 */
public class Jugador : MonoBehaviour
{
	public float velocidad;		//Asignado por fuera
	public Alcance alcance;		//Asignado por fuera
	public Vector3 armaDir;		//Asignado por fuera
	public int idJugador;		//Asignado por la fábrica
	private GameObject arma;	//Objeto para lanzar
	private bool jugando;
	
	void Start()
	{
		arma = null;
		jugando = false;
	}
	
	void Update()
	{
		if (jugando) {
			Mover();
			if (arma == null)
				Agarrar();
			else
				Lanzar();
		}
	}
	
	private void Mover()
	{
		Vector3 dir = new Vector3(
			Input.GetAxisRaw("HorizontalJ" + idJugador),
			0,
			Input.GetAxisRaw("VerticalJ" + idJugador)
		);
		Vector3.Normalize(dir);
		transform.Translate(dir * Time.deltaTime * velocidad);
	}
	
	private void Agarrar()
	{
		if (Input.GetKeyUp(KeyCode.K)) {
			arma = alcance.GetObject();
			if (arma == null)
				return;
			Transform aux = arma.transform;
			arma.GetComponent<Rigidbody>().useGravity = false;
			aux.parent = transform;
			aux.position = new Vector3(0, 2, 0);
			arma.SetActive(true);
		}
	}
	
	private void Lanzar()
	{
		if (Input.GetKeyUp(KeyCode.K)) {
			arma.transform.parent = null;
			Rigidbody aux = arma.GetComponent<Rigidbody>();
			aux.useGravity = true;
			aux.AddForce(armaDir, ForceMode.VelocityChange);
			arma = null;
		}
	}
	
	public void Iniciar()
	{
		jugando = true;
	}
}
