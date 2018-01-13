using UnityEngine;
using System.Collections;

public class CambiarModeloEnemigo : MonoBehaviour 
{
	private Vida v;
	public GameObject model1;
	public GameObject model2;
	public GameObject model3;


	public void Awake () 
	{
		// TRAER EL COMPONENTE VIDA DEL ENEMIGO
		v = GetComponent<Vida>();
	}
	

	public void Update () 
	{
		if (v != null) 
		{
			// SI LA VIDA ES IGUAL A 2, SE DESACTIVA EL PRIMER MODELO Y SE ACTIVA EL SEGUNDO.
			// SI LA VIDA ES IGUAL A 1, SE DESACTIVA EL SEGUNDO MODELO Y SE ACTIVA EL TERCERO.
			if (v.getVida () == 2) 
			{
				model1.SetActive (false);
				model2.SetActive (true);
			} 
			else if (v.getVida () == 1) 
			{
				model2.SetActive (false);
				model3.SetActive (true);
			}
		}
	}
}
