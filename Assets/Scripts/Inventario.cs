using UnityEngine;
using System.Collections;

public class Inventario : MonoBehaviour 
{
	// UNA VARIABLE PARA SABER SI EL JUGADOR TIENE UNA GRANADA O NO
	private bool granadaDisponible = false;

	// LA MIRA QUE SE ACTIVA/DESACTIVA SEGUN SI EL JUGADOR TIENE GRANADAS O NO
	public GameObject mira;

	private MeshRenderer mr;

	// EVENTO PARA HABILITAR QUE EL JUGADOR PUEDA DISPARAR
	public void HabilitarGranada()
	{
		// LE CARGA UNA GRANADA AL JUGADOR
		granadaDisponible = true;

		// ACTIVA LA MIRA
		mira.SetActive (granadaDisponible);

		// PINTAR LA MIRA DE COLOR VERDE
		for (int i=1; i < 10; i++)
		{
			mr = transform.Find ("/Jugador/DondeApuntar/DondeApuntar (" + i.ToString() + ")").GetComponent<MeshRenderer> ();
			mr.material.color = Color.green;
		}
	}

	// EVENTO PARA DESHABILITAR QUE EL JUGADOR PUEDA DISPARAR
	public void DeshabilitarGranada()
	{
		// SE DESCARGA LA GRANADA DEL JUGADOR
		granadaDisponible = false;

		for (int i=1; i < 10; i++)
		{
			mr = transform.Find ("/Jugador/DondeApuntar/DondeApuntar (" + i.ToString() + ")").GetComponent<MeshRenderer> ();
			mr.material.color = Color.green;
		}

		// SE DESACTIVA LA MIRA
		mira.SetActive (granadaDisponible);


	}

	// RETORNA EL VALOR ACTUAL DE "granadaDisponible" A TRAVES DE UN EVENTO YA QUE ES UNA PROPIEDAD PRIVADA
	public bool GranadaDisponible()
	{
		return granadaDisponible;
	}

	// METODO PARA OBLIGAR A ACTIVAR O DESACTIVAR LA MIRA (LO USABA PARA PROBAR NOMAS)
	public void ForzarMira(bool estado)
	{
		mira.SetActive (estado);
	}
}
