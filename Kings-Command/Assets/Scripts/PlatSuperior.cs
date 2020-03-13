using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * Imprime el cambio con respecto a su posición original cada UPDATE.
 * Rompe los spring al tocar la tercera plataforma.
 */
public class PlatSuperior : MonoBehaviour
{
	public int platAlcanzadas;
	
	void Start()
	{
		platAlcanzadas = 0;
	}
	
	void Update() {}
	
	/*
	 * Las plataformas inferiores notifican su contacto
	 */
	public void TouchedBy(int id)
	{
		if (id > platAlcanzadas)
			platAlcanzadas = id;
		print("Plat" + id + " alcanzada");
		if (id == 3) {
			SpringJoint sj;
			for (sj = GetComponent<SpringJoint>();
				sj != null;
				sj = GetComponent<SpringJoint>()
			)
				Destroy(sj);
		}
	}
}
