using UnityEngine;
using System.Collections;

public class MatarJugador : MonoBehaviour
{	
	private Transform posicion;
	private float x,y;
	private int daño = -1;

	public void Awake()
	{
		// TRAIGO LA POSICION DEL JUGADOR
		posicion = GetComponent<Transform> ();

		// ASIGNO LAS POSICIONES QUE MARCAN EL LIMITE EN LOS EJES X E Y
		x = posicion.position.x;
        y = -1.5f;
	}

	public void Update()
	{
		// EL JUGADOR MUERE EN AMBOS CASOS:
		// SI LA POSICION DEL JUGADOR EN EL EJE X ES MENOR A LA QUE ASIGNE EN "x"
		if (transform.position.x < x)
		{
			Vida v = GetComponent<Vida>();
			if (v != null)
			{
				v.ModificarVida(daño);
			}
		}
		// SI LA POSICION DEL JUGADOR EN EL EJE Y ES MENOR A LA QUE ASIGNE EN "y"
        if (transform.position.y < y)
        {
            Vida v = GetComponent<Vida>();
            if (v != null)
            {
                v.ModificarVida(daño);
            }
        }
	}
}
