using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
/*
 * Instancia los jugadores y las plataformas.
 */
public class Manager : MonoBehaviour
{
	public GameObject plataformaPrefab;	//Asignado por fuera
	public GameObject jugadorPrefab;	//Asignado por fuera
	public GameObject comenzarButton;	//Asignado por fuera
	public GameObject salirButton;		//Asignado por fuera
	public GameObject relojInicio;		//Asignado por fuera
	private UnityEvent inicio;		//Comenzar a jugar
	private Scene escena;
	private bool escenaCargada;
	
	void Start()
	{
		inicio = new UnityEvent();
		escenaCargada = false;
		CargarEscenario();
	}
	
	public void CargarEscenario()
	{
		if (escenaCargada) {
			SceneManager.UnloadSceneAsync(escena);
			escenaCargada = false;
			//TODO borrar a todos los hijos del manager
		}
		SceneManager.LoadScene(0, LoadSceneMode.Additive);
		escena = SceneManager.GetSceneByBuildIndex(0);
		comenzarButton.SetActive(true);
		salirButton.SetActive(true);
		relojInicio.SetActive(false);
	}
	
	public void ComenzarAJugar()
	{
		SceneManager.SetActiveScene(escena);
		TraerPlataformas();
		comenzarButton.SetActive(false);
		salirButton.SetActive(false);
		relojInicio.SetActive(true);
		Vector3 aux = relojInicio.transform.position;
		StartCoroutine(Comenzar());
	}
	
	private IEnumerator Comenzar()
	{
		Text tiempoRestante = relojInicio.GetComponent<Text>();
		tiempoRestante.text = "3";
		yield return new WaitForSeconds(1f);
		TraerJugador(1);
		TraerJugador(2);
		tiempoRestante.text = "2";
		yield return new WaitForSeconds(1f);
		tiempoRestante.text = "1";
		yield return new WaitForSeconds(1f);
		tiempoRestante.text = "Start";
		yield return new WaitForSeconds(0.5f);
		relojInicio.SetActive(false);
		inicio.Invoke();
		//TODO Agregar los demás efectos
		
	}
	
	void Update() {}
	
	public void TraerPlataformas()
	{
		GameObject plat;
		PlatCompleta pc;
		
		plat = Instantiate(plataformaPrefab,
			new Vector3(-8, 0, 0),
			Quaternion.identity,
			transform
		);
		pc = plat.GetComponent<PlatCompleta>();
		pc.idPlat = 1;
		inicio.AddListener(pc.Iniciar);
		
		plat = Instantiate(plataformaPrefab,
			new Vector3(8, 0, 0),
			Quaternion.identity,
			transform
		);
		pc = plat.GetComponent<PlatCompleta>();
		pc.idPlat = 2;
		inicio.AddListener(pc.Iniciar);
		
	}
	
	public void TraerJugador(int jugador)
	{
		GameObject aux;
		if (jugador == 1)
			aux = Instantiate(jugadorPrefab,
				new Vector3(-8, 15, 0),
				Quaternion.Euler(0, 180, 0),
				transform
			);
		else
			aux = Instantiate(jugadorPrefab,
				new Vector3(8, 15, 0),
				Quaternion.identity,
				transform
			);
		Jugador j = aux.GetComponent<Jugador>();
		inicio.AddListener(j.Iniciar);
		j.idJugador = jugador;
	}
}
