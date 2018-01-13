using UnityEngine;
using System.Collections;

public class Vida : MonoBehaviour 
{
	public GameObject objeto;
	public GameObject prefab;
	private int cantVida;

	public void Awake ()
	{
		// SE DEFINE LA VIDA DEL JUGADOR Y DEL ENEMIGO
		if (objeto.tag == "Jugador")
			cantVida = 1;
		
		if (objeto.tag == "Enemigo")
			cantVida = 3;
	}

	// HAY QUE MODIFICAR LA VIDA A TRAVES DE UN EVENTO PORQUE LA PROPIEDAD "cantVida" ES UNA VARIABLE PRIVADA
	public void ModificarVida(int valor)
	{
		cantVida += valor;
	}

	// RETORNA EL VALOR ACTUAL DE "cantVida" A TRAVES DE UN EVENTO YA QUE ES UNA PROPIEDAD PRIVADA
	public int getVida()
	{
		return cantVida;
	}
		
	public void setVida(int vida)
	{
		cantVida = vida;
	}

	public void Update()
	{
		// SI LA VIDA DEL OBJETO (JUGADOR O ENEMIGO) ES IGUAL A 0
		if (cantVida == 0) 
		{
			// SE DESTRUYE EL OBJETO
			Destroy (objeto);

			// APARECE UN EFECTO DE PARTICULAS EN EL LUGAR
			Instantiate (prefab, objeto.transform.position, objeto.transform.rotation);
		}
	}
}
