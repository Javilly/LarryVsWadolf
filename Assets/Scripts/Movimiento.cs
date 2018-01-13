using UnityEngine;
using System.Collections;

public class Movimiento : MonoBehaviour 
{
	public GameObject objeto;
	private GameObject enemigo;
	private float Velocidad;
	private float Tiempo;
	private string Sentido;
	MeshRenderer mr;

	public void Start()
	{
		enemigo = GameObject.Find ("Enemigo");
		if (objeto.tag == "Fondo")
		{
			mr = gameObject.GetComponent<MeshRenderer> ();
		}
	}

	public void FixedUpdate () 
	{
		// SI EL ENEMIGO EXISTE EN ESCENA
		if (enemigo != null) {
			// SI ES UNA PLATAFORMA O UN PISO
			if (objeto.tag == "Plataforma" || objeto.tag == "Piso") {
				Velocidad = 10;
			}

			// SI ES UNA GRANADA
			if (objeto.tag == "Granada") {
				Velocidad = 25;
			}

			// SI ES EL EFECTO DE LA GRANADA 
			if (objeto.tag == "EfectoGranada") {
				Velocidad = 10;
			}

			// SI ES EL LASER DEL ROBOT
			if (objeto.tag == "Laser") {
				Velocidad = 40;
			}

			// SI ES LA GRANADA QUE DISPARA EL JUGADOR
			if (objeto.tag == "GranadaJugador") {
				Velocidad = -40;
			}

			// SI ES LA TEXTURA DEL FONDO
			if (objeto.tag == "Fondo") {
				Velocidad = 0.05f;
			}

			if (objeto.tag == "Enemigo") 
			{
				if (objeto.transform.position.y < 25 && Sentido == "subir") 
					transform.Translate (0, 5 * Time.deltaTime, 0);
				else
					Sentido = "bajar";

				if (objeto.transform.position.y > 11 && Sentido == "bajar") 
					transform.Translate (0, -5 * Time.deltaTime, 0);
				else 
					Sentido = "subir";
			} 
			else if (objeto.tag == "Plataforma" || objeto.tag == "Piso" || objeto.tag == "Granada" || objeto.tag == "EfectoGranada" || objeto.tag == "GranadaJugador" || objeto.tag == "Laser") 
			{
				// TODOS LOS OBJETOS SE MUEVEN CON UN VALOR NEGATIVO (EXCEPTO EL DISPARO DEL JUGADOR) EN EL EJE X PARA DAR LA SENSACION DE QUE EL JUGADOR Y EL ENEMIGO ESTAN AVANZANDO
				transform.Translate (-Velocidad * Time.deltaTime, 0, 0);	
			} 
			else if (objeto.tag == "Fondo")
			{
				// VELOCIDAD PARA LA TEXTURA DEL FONDO
				float offset = Velocidad * Time.time;

				// LE DA EL MOVIMIENTO A LA TEXTURA DEL FONDO
				mr.material.SetTextureOffset("_MainTex",new Vector2(offset,0));
			}
		} 
		else 
		{
			// TEMPORIZADOR PARA LIMITAR EL MOVIMIENTO DEL PISOFINAL
			Tiempo += Time.deltaTime;

			if (objeto.tag == "Piso") 
			{
				Velocidad = 10;

				// EL PISO SE MUEVE MIENTRAS NO HAYAN PASADO 6 SEGUNDOS
				if (Tiempo < 6)
					transform.Translate (-Velocidad * Time.deltaTime, 1 * Time.deltaTime, 0);
			}
			if (objeto.tag == "Fondo")
			{
				if (Tiempo < 6) {
					Velocidad = 0.05f;
					float offset = Velocidad * Time.time;
					mr.material.SetTextureOffset ("_MainTex", new Vector2 (offset, 0));
				}
			}
		}


	}
}
