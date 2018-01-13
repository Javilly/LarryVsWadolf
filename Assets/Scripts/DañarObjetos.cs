using UnityEngine;
using System.Collections;

public class DañarObjetos : MonoBehaviour 
{
	// SE CREA UNA PROPIEDAD CON LA CANTIDAD DE DAÑO
	private int daño = -1;
	private AudioSource[] sonidos;
	private AudioSource sonido1;
	private AudioSource sonido2;
	private AudioSource sonido3;
	private AudioSource sonido4;
	private AudioSource sonido5;

	public void OnCollisionEnter(Collision c)
	{
		// SI LOS OBJETOS QUE CHOCAN SON EL JUGADOR Y EL EFECTO DE UNA GRANADA
		if (c.gameObject.tag == "Jugador" && gameObject.tag == "EfectoGranada")
		{
			// SE CREA UN OBJETO DE LA CLASE "Vida" Y SE LE ASIGNA EL COMPONENTE "Vida" DEL JUGADOR
			Vida v = c.gameObject.GetComponent<Vida> ();

			// SE FIJA QUE EL OBJETO "Vida" NO ESTÉ VACIO (EN NUESTRO CASO SIEMPRE VA A TENER UN VALOR PORQUE LA UNICA FORMA DE LLEGAR A ESTE IF ES SI CHOCAMOS CONTRA EL JUGADOR, Y EL JUGADOR SIEMPRE TIENE VIDA)
			if (v != null) 
			{
				//SE MODIFICA LA VIDA DEL JUGADOR MEDIANTE EL EVENTO "ModificarVida", ENVIANDO EL DAÑO COMO PARAMETRO
				v.ModificarVida (daño);
			}
		}
	}

	public void OnTriggerEnter(Collider c)
	{
		// ESTE IF ES IGUAL AL DE ARRIBA (OnCollisionEnter), SOLO QUE EL LASER FUNCIONA COMO TRIGGER Y LAS EXPLOSIONES NO
		if (c.gameObject.tag == "Jugador" && gameObject.tag == "Laser") 
		{
			Vida v = c.gameObject.GetComponent<Vida> ();
			if (v != null) 
			{
				v.ModificarVida (daño);
			}
		}

		// SI LOS OBJETOS QUE CHOCAN SON EL ENEMIGO Y LA GRANADA DEL JUGADOR
		if (c.gameObject.tag == "Enemigo" && gameObject.tag == "GranadaJugador") 
		{
			// SE TRAE EL COMPONENTE "Vida" DEL ENEMIGO
			Vida v = c.GetComponent<Vida> ();

			if (v != null) 
			{
				// SE TRAE EL AUDIO "pierdevida" DEL ENEMIGO Y DESPUES SE REPRODUCE
				sonido4 = c.gameObject.GetComponent<AudioSource>();
				sonido4.Play();

				// SE MODIFICA LA VIDA DEL ENEMIGO
				v.ModificarVida (daño);

				// SE DESTRUYE LA GRANADA DEL JUGADOR
				Destroy (gameObject);

				if (v.getVida() == 0) 
				{
					// GENERO UN NUMERO RANDOM ENTRE 0 Y 2 PARA LA CANCION CUANDO SE DESTRUYE AL ROBOT (HAY 2 CANCIONES Y TIENEN UN 50% DE REPRODUCIRSE CADA UNA)
					float numeroRandom = Random.Range (0, 2);

					// GUARDO TODOS LOS SONIDOS DEL OBJETO EN ESCENA "CosasGenerales" EN UN VECTOR (ES UN CONJUNTO DE VARIABLES DE UN MISMO TIPO) DE AUDIOSOURCES Y LOS VOY ASIGNANDO EN VARIABLES INDEPENDIENTES DEL TIPO AUDIOSOURCES
					// EL ORDEN EN EL QUE ESTAN GUARDADOS EN EL VECTOR ES EL MISMO QUE COMO ESTAN EN EL OBJETO "CosasGenerales"
					sonidos = GameObject.Find ("CosasGenerales").GetComponents<AudioSource> ();

					// ES LA MUSICA INICIAL
					sonido1 = sonidos[0];

					// SON LAS CANCIONES DE VICTORIA
					sonido2 = sonidos[1];
					sonido3 = sonidos[2];

					// ES LA MUSICA SECRETA
					sonido5 = sonidos[4];

					// CUANDO SE GANA EL JUEGO, HAY QUE DEJAR DE REPRODUCIR LA MUSICA INICIAL O LA SECRETA
					sonido1.Stop ();
					sonido5.Stop ();

					// SE REPRODUCE UNA DE LAS 2 CANCIONES DE VICTORIA SEGUN EL VALOR DEL NUMERO RANDOM
					if (numeroRandom >= 0 && numeroRandom < 1)
						sonido2.Play ();
					else if (numeroRandom >= 1 && numeroRandom <= 2)
						sonido3.Play ();
				}
			}

		}
	}
}
