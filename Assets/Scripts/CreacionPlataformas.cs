using UnityEngine;
using System.Collections;

public class CreacionPlataformas: MonoBehaviour 
	{
		private GameObject enemigo;
		public GameObject prefab;
		public GameObject prefab2;

		public void Awake()
		{
			// SE CREA UN OBJETO CON LOS PARAMETROS DEL ENEMIGO
			enemigo = GameObject.Find ("Enemigo");

		 	// "Piso" SE USA EN LA PRIMER PARTE DEL JUEGO, CON LAS GRANADAS INCENDIARAS
			if (prefab.tag == "Piso") 
			{
			// SE CREA UN PISO NUEVO CADA 6 SEGUNDOS (EL PISO EN PANTALLA MIDE 120 UNIDADES Y EL PERSONAJE SE MUEVE A 10 UNIDADES POR SEGUNDO) LA MITAD DEL PISO ACTUAL SE UNE CON LA MITAD DEL SIGUIENTE PISO.
				Invoke("Crear", 6);
			}
			
			// "Plataforma" SE USA EN LA SEGUNDA PARTE DEL JUEGO, CON LAS GRANADAS EXPLOSIVAS
			if (prefab.tag == "Plataforma") 
			{
				Invoke("Crear", Random.Range (2f, 4f));
			}
		}
		
		public void Crear()
		{
			// SE VERIFICA QUE EL ENEMIGO EXISTA EN ESCENA (PARA EVITAR ERRORES)
			if (enemigo != null) 
			{
				// SE CREA UN COMPONENTE CON LA VIDA ACTUAL DEL ENEMIGO
				Vida v = enemigo.GetComponent<Vida> ();
				// SI ES UN PISO Y LA VIDA DEL ENEMIGO ES 3 
				if (prefab.tag == "Piso" && v.getVida () == 3) 
				{
					// SE GUARDA LA POSICION DEL SIGUIENTE PISO
					Vector3 posicion;
					posicion.x = transform.position.x + 60;
					posicion.y = transform.position.y;
					posicion.z = transform.position.z;

					// SE CREA EL PISO EXACTAMENTE AL LADO (LA POSICION DE LA MITAD DEL PISO NUEVO COINCIDE CON LA MITAD DEL PISO ANTERIOR)
					Instantiate (prefab, posicion, transform.rotation);
				} 
				
				// SI ES UN PISO Y LA VIDA DEL ENEMIGO ES 2
				else if (prefab.tag == "Piso" && v.getVida () <= 2) 
				{
					// SE CREA LA PRIMER PLATAFORMA
					Instantiate (prefab2, new Vector3 (70, 5, 10), Quaternion.Euler (0, 0, 0));

					// EL PISO DEJA DE GENERARSE AUTOMATICAMENTE
					CancelInvoke ("Crear");
				}
				
				// DESPUES DE QUE SE CREA LA PRIMER PLATAFORMA, SE EMPIEZAN A GENERAR AUTOMATICAMENTE ENTRANDO EN EL SIGUIENTE IF
				if (prefab.tag == "Plataforma") 
				{
					Instantiate (prefab, transform.position, transform.rotation);
					transform.position = new Vector3 (60, Random.Range (0, 25), 10);
				}
			}
		}
	}

