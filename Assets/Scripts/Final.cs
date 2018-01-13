using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Final : MonoBehaviour 
{
	private GameObject enemigo;
	private SaltoJetpack sj;
	public GameObject pisofinal;
	public GameObject particulafinal;
	private int contador;
	private float tiempo;

	public void Awake()
	{
		// TRAIGO EL COMPONENTE "SaltoJetpack" DEL JUGADOR 
		sj = GetComponent<SaltoJetpack> ();

		// TRAIGO AL OBJETO "Enemigo" Y LO GUARDO EN UNA VARIABLE
		enemigo = GameObject.Find ("Enemigo");

		// SETEO EN 0 UN CONTADOR Y UN TEMPORIZADOR
		contador = 0;
		tiempo = 0;
	}

	public void FixedUpdate () 
	{
		// SI EL ENEMIGO DEJA DE EXISTIR
		if (enemigo == null) 
		{
			// SE INICIA EL TEMPORIZADOR
			tiempo += Time.deltaTime;

			if (contador == 0) 
			{
				// SE CREA EL PISOFINAL
				Instantiate (pisofinal, new Vector3 (60, -5.5f, 10), Quaternion.Euler (0, 0, 0));

				// LE SACO LA FUERZA AL SALTO (PARA EVITAR PROBLEMAS EN LA ESCENA)
				sj.setFuerzaSalto(0);

				// SUMO 1 AL CONTADOR
				contador++;
			} 
			else if (contador == 1) 
			{
				// SE EJECUTAN LOS MOVIMIENTOS FINALES
				MovimientoFinal ();
			}	
		}
	}

	public void MovimientoFinal ()
	{
		// ENTRE LOS 6 SEGUNDOS Y LOS 9
		if (tiempo >= 6 && tiempo < 9)
			// EL PERSONAJE SE MUEVE DE A 15 UNIDADES POR SEGUNDO HACIA LA DERECHA
			transform.Translate (15 * Time.deltaTime, 0, 0);

		// ENTRE LOS 9 SEGUNDOS Y LOS 11
		if (tiempo >= 9 && tiempo < 11)
			// EL PERSONAJE MIRA A LA IZQUIERDA
			transform.localScale = new Vector3 (-1, 3, 1);

		// ENTRE LOS 11 SEGUNDOS Y LOS 12
		if (tiempo >= 11 && tiempo < 12)
			// EL PERSONAJE MIRA A LA DERECHA
			transform.localScale = new Vector3 (1, 3, 1);

		// ENTRE LOS 12 SEGUNDOS Y LOS 17
		if (tiempo >= 12 && tiempo < 17) 
		{
			// LE SACO LA GRAVEDAD AL PERSONAJE (PARA EVITAR PROBLEMAS CUANDO SE MUEVE HACIA ARRIBA)
			sj.setGravedad(0);

			// MUEVO AL PERSONAJE DE A 8 UNIDADES POR SEGUNDO HACIA ARRIBA Y LO VOY HACIENDO MAS CHICO AL MISMO TIEMPO, ASI DA LA SENSACION DE QUE SE ALEJA
			transform.Translate (0, 8 * Time.deltaTime, 0);
			transform.localScale -= new Vector3 (0.2f*Time.deltaTime, 0.6f*Time.deltaTime, 0.2f*Time.deltaTime);
		}
		if (tiempo >= 17 && contador == 1) 
		{
			// CREO UNA PARTICULA QUE HACE UNA LUCESITA POR DONDE SE FUE EL PERSONAJE
			Instantiate (particulafinal, new Vector3 (50.2f, 41.5f, 10), particulafinal.transform.rotation);

			// LE SUMO 1 AL CONTADOR PARA QUE NO SE CREE LA PARTICULA DE NUEVO
			contador++;

			// VUELVO AL MENU EN 3 SEGUNDOS
			Invoke("VolverAlMenu",3);
		}
			
	}

	public void VolverAlMenu()
	{
		SceneManager.LoadScene("Scenes/Menu", LoadSceneMode.Single);
	}
}
