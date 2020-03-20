using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
/*
 * Administra textos, menús y la fábrica. 
 */
public class Manager : MonoBehaviour
{
	public static Manager manager;
	public Fabrica fabrica;			//Asignado por fuera
	public GameObject comenzarButton;	//Asignado por fuera
	public GameObject salirButton;		//Asignado por fuera
	public GameObject relojInicio;		//Asignado por fuera
	public GameObject cont1;		//Asignado por fuera
	public GameObject cont2;		//Asignado por fuera
	public float	velInstancia;		//Asignado por fuera
	public float	velCambio;		//Asignado por fuera
	public int	instanciasParaCambiar;	//Asignado por fuera
	
	void Start()
	{
		fabrica.CargarEscenario();
		MenuInicio(true);
		manager = this;
	}
	
	public void MenuInicio(bool b)
	{
		comenzarButton.SetActive(b);
		salirButton.SetActive(b);
		relojInicio.SetActive(!b);
		cont1.SetActive(!b);
		cont2.SetActive(!b);
	}
	
	public void ComenzarAJugar()
	{
		fabrica.CargarPlataformas(cont1.GetComponent<Text>(), cont2.GetComponent<Text>());
		MenuInicio(false);
		StartCoroutine(Comenzar());
	}
	
	private IEnumerator Comenzar()
	{
		Text tiempoRestante = relojInicio.GetComponent<Text>();
		tiempoRestante.text = "3";
		yield return new WaitForSeconds(1f);
		fabrica.CargarJugadores();
		tiempoRestante.text = "2";
		yield return new WaitForSeconds(1f);
		tiempoRestante.text = "1";
		yield return new WaitForSeconds(1f);
		tiempoRestante.text = "Start";
		yield return new WaitForSeconds(0.5f);
		relojInicio.SetActive(false);
		fabrica.ComenzarJuego();
		StartCoroutine(CrearObjetos());
	}
	
	private IEnumerator CrearObjetos()
	{
		float velActual = velInstancia;
		for (int instancias = 0; fabrica.playing; instancias++) {
			//objeto1
			GameObject aux = fabrica.CargarObjeto();
			if (aux != null) {
				aux.transform.position = new Vector3(
					(Random.Range(2.0f, 14.0f)),
					8,
					Random.Range(-6.0f, 6.0f)
				);
				aux.SetActive(true);
			}
			//objeto2
			aux = fabrica.CargarObjeto();
			if (aux != null) {
				aux.transform.position = new Vector3(
					Random.Range(-14.0f, -2.0f),
					8,
					Random.Range(-6.0f, 6.0f)
				);
				aux.SetActive(true);
			}
			if (instancias >= instanciasParaCambiar) {
				if (velActual - velCambio > 0)
					velActual -= velCambio;
				instancias = 0;
			}
			yield return new WaitForSeconds(velActual);
		}
	}
	
	public void FinDelJuego(int perdedor)
	{
		print("jugador " + perdedor + " ha perdido");
		//TODO
	}
	
	void Update() {}
}
