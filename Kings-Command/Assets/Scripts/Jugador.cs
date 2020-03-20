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
		if (Input.GetButtonUp("FireJ" + idJugador)) {
			arma = alcance.GetObject();
			if (arma == null)
				return;
			arma.SetActive(false);
			Rigidbody auxR = arma.GetComponent<Rigidbody>();
			auxR.useGravity = false;
			auxR.constraints = RigidbodyConstraints.FreezePosition;
			Transform auxT = arma.transform;
			auxT.parent = transform;
			Vector3 auxV = auxT.position;
			auxV.y += 1;
			auxT.position = auxV;
			arma.SetActive(true);
		}
	}
	
	private void Lanzar()
	{
		if (Input.GetButtonUp("FireJ" + idJugador)) {
			arma.transform.parent = null;
			Rigidbody auxR = arma.GetComponent<Rigidbody>();
			auxR.useGravity = true;
			auxR.constraints = RigidbodyConstraints.None;
			auxR.AddForce(armaDir, ForceMode.VelocityChange);
			arma = null;
		}
	}
	
	public void Iniciar()
	{
		jugando = true;
	}
	
	public void Terminar()
	{
		jugando = false;
	}
}
