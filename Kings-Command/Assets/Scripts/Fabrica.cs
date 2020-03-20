using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;
using UnityEngine.UI;
/*
 * Administra jugadores, plataformas, objetos y escenarios.
 */
public class Fabrica : MonoBehaviour
{
	//prefabs
	public static Fabrica fabrica;
	public GameObject escenarioPrefab;	//Asignado por fuera
	public GameObject plataformaPrefab;	//Asignado por fuera
	public GameObject jugadorPrefab;	//Asignado por fuera
	public float		probLittle;	//Asignado por fuera
	public float		probMedium;	//Asignado por fuera
	public float		probBig;	//Asignado por fuera
	public bool		playing;
	public UnityEvent	inicio;
	private GameObject	escenario;
	private GameObject[]	jugador;
	
	//TODO Asignar limites apropiados a los arreglos
	public GameObject[]	littlePrefab;	//Asignado por fuera
	public GameObject[]	mediumPrefab;	//Asignado por fuera
	public GameObject[]	bigPrefab;	//Asignado por fuera
	
	private const byte	objLittleAmount = 16;
	private const byte	objMediumAmount = 8;
	private const byte	objBigAmount    = 4;
	
	private GameObject[]	objLittle;
	private GameObject[]	objMedium;
	private GameObject[]	objBig;
	
	private byte		littleCurrent;
	private byte		mediumCurrent;
	private byte		bigCurrent;
	
	void Start()
	{
		fabrica = this;
		jugador = new GameObject[2];
		inicio = new UnityEvent();
		objLittle = new GameObject[objLittleAmount];
		objMedium = new GameObject[objMediumAmount];
		objBig = new GameObject[objBigAmount];
	}
	
	void Update() {}
	
	public void ComenzarJuego()
	{
		playing = true;
		inicio.Invoke();
	}
	
	public void CargarEscenario()
	{
		if (escenarioPrefab == null)
			return;
		if (escenario == null)
			escenario = Instantiate(escenarioPrefab);
		else if (!escenario.activeSelf)
			escenario.SetActive(true);
	}
	
	public void CargarPlataformas(Text t1, Text t2)
	{
		GameObject plat;
		PlatCompleta pc;
		//Plataforma1
		plat = Instantiate(
			plataformaPrefab,
			new Vector3(-8, 0, 0),
			Quaternion.identity
		);
		pc = plat.GetComponent<PlatCompleta>();
		pc.idPlat = 1;
		pc.texto = t1;
		inicio.AddListener(pc.Iniciar);
		//Plataforma2
		plat = Instantiate(
			plataformaPrefab,
			new Vector3(8, 0, 0),
			Quaternion.identity
		);
		pc = plat.GetComponent<PlatCompleta>();
		pc.idPlat = 2;
		pc.texto = t2;
		inicio.AddListener(pc.Iniciar);
	}
	
	public void CargarJugadores()
	{
		if (jugador[0] == null) {
			Jugador j;
			//jugador 1
			jugador[0] = Instantiate(
				jugadorPrefab,
				new Vector3(-8, 15, 0),
				Quaternion.Euler(0, 180, 0)
			);
			j = jugador[0].GetComponent<Jugador>();
			inicio.AddListener(j.Iniciar);
			j.idJugador = 1;
			j.armaDir.x *= -1;
			//jugador 2
			jugador[1] = Instantiate(
				jugadorPrefab,
				new Vector3(8, 15, 0),
				Quaternion.Euler(0, 0, 0)
			);
			j = jugador[1].GetComponent<Jugador>();
			inicio.AddListener(j.Iniciar);
			j.idJugador = 2;
		}
		else {
			//jugador 1
			jugador[0].SetActive(false);
			jugador[0].transform.position = new Vector3(-8, 15, 0);
			jugador[0].SetActive(true);
			//jugador 2
			jugador[1].SetActive(false);
			jugador[1].transform.position = new Vector3(8, 15, 0);
			jugador[1].SetActive(true);
		}
	}
	
	public GameObject CargarObjeto()
	{
		float kind = Random.Range(0f, probLittle + probMedium + probBig);
		int index;
		//little
		int currentAux, amountAux;
		GameObject[] objAux, prefabAux;;
		if (kind < probLittle) {
			prefabAux = littlePrefab;
			currentAux = mediumCurrent;
			amountAux = objLittleAmount;
			objAux = objLittle;
			if (currentAux < amountAux)
				littleCurrent++;
		}//medium
		else if (kind < probLittle + probMedium) {
			prefabAux = mediumPrefab;
			currentAux = mediumCurrent;
			amountAux = objMediumAmount;
			objAux = objMedium;
			if (currentAux < amountAux)
				mediumCurrent++;
		}//big
		else {
			prefabAux = bigPrefab;
			currentAux = bigCurrent;
			amountAux = objBigAmount;
			objAux = objBig;
			if (currentAux < amountAux)
				bigCurrent++;
		}
		if (currentAux < amountAux) {
			index = currentAux % prefabAux.Length;
			objAux[currentAux] = Instantiate(prefabAux[index]);
			objAux[currentAux].SetActive(false);
			return objAux[currentAux];
		}
		index = Random.Range(0, amountAux);
		for (int i = 0; i < amountAux; i++) {
			int realIndex = (i + index) % amountAux;
			if (!objAux[realIndex].activeSelf)
				return objAux[realIndex];
		}
		return null;
	}
	
	public void Destruir(GameObject obj)
	{
		if (obj.tag == "Player") {
			Jugador auxJ = obj.GetComponent<Jugador>();
			Manager.manager.FinDelJuego(auxJ.idJugador);
		}
		obj.SetActive(false);
	}
	
	public void FinalizarJuego()
	{
		playing = false;
		for (int i = 0; i < littleCurrent; i++)
			objLittle[i].SetActive(false);
		for (int i = 0; i < mediumCurrent; i++)
			objMedium[i].SetActive(false);
		for (int i = 0; i < bigCurrent; i++)
			objBig[i].SetActive(false);
	}
}
