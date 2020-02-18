using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * Personaje provisional. Será removido.
 * Se mueve en dos direcciones.
 */
public class Jugador : MonoBehaviour
{
	public float velocidad;		//Asignado por fuera
	public Alcance alcance;		//Asignado por fuera
	public Vector3 armaDir;		//Asignado por fuera
	public int idJugador;		//Asignado por el manager
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
			Lanzar();
			Agarrar();
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
		if (arma == null && Input.GetKeyUp(KeyCode.K)) {
			arma = alcance.GetObject();
			if (arma == null)
				return;
			Transform aux = arma.GetComponent<Transform>();
			arma.GetComponent<Rigidbody>().useGravity = false;
			aux.parent = transform;
			aux.position.Set(0, 2, 0);
		}
	}
	
	private void Lanzar()
	{
		if (arma == null)
			return;
		if (Input.GetKeyUp(KeyCode.K)) {
			arma.GetComponent<Transform>().parent = null;
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
