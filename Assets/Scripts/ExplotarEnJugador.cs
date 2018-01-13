using UnityEngine;
using System.Collections;

public class ExplotarEnJugador : MonoBehaviour 
{
	private MeshRenderer mr;
	private TextMesh timerbomba;
	private GameObject texturatimer;
	private Inventario inv;
	private Vida v;
	private int contador = 0;
	private float tiempo;

	private void Awake()
	{		
		if (tag == "Jugador") 
		{
			// SE TRAEN ALGUNOS COMPONENTES Y OBJETOS DEL JUGADOR
			inv = GetComponent<Inventario> ();
			v = GetComponent<Vida> ();

			// "TimerBomba" Y "TexturaTimer" SON OBJETOS HIJO DE JUGADOR (EL PRIMERO ES UN TEXTO QUE ESTA SIEMPRE ACTIVO PERO QUE NO TIENE NADA ESCRITO, POR ESO NO SE VE Y EL SEGUNDO ES UNA TEXTURA QUE SOLO SE ACTIVA CUANDO EL JUGADOR AGARRA UNA GRANADA)
			timerbomba = transform.Find ("TimerBomba").GetComponent<TextMesh> ();
			texturatimer = transform.Find ("TexturaTimer").gameObject;
		}
	}

	private void Update()
	{
		// SI EL JUGADOR AGARRA UNA GRANADA
		if (tag == "Jugador" && inv.GranadaDisponible () == true) 
		{
			// INICIA UN TEMPORIZADOR
			tiempo += Time.deltaTime;

			// SE ACTIVA LA TEXTURA EN ESCENA (ES UNA CALAVERA ARRIBA DEL JUGADOR)
			texturatimer.SetActive (true);

			// HAY UN CONTADOR QUE CAMBIA DE VALOR CADA SEGUNDO QUE PASA
			if (contador == 0) {
				// SE ESCRIBE UN TEXTO EN EL OBJETO "TimerBomba" Y SE LE CAMBIA EL COLOR
				timerbomba.text = "3";
				timerbomba.color = Color.green;

				// SE SUMA 1 AL CONTADOR
				contador++;
			}

			// SI PASÓ 1 SEGUNDO Y EL CONTADOR ES 1
			if (tiempo > 1 && contador == 1) {
				// SE CAMBIA EL COLOR DE LA MIRA Y EL TEXTO
				CambiarColor ();
				timerbomba.text = "2";
			}

			// SI PASARON 2 SEGUNDOS Y EL CONTADOR ES 2
			if (tiempo > 2 && contador == 2) {
				// SE CAMBIA EL COLOR DE LA MIRA Y EL TEXTO
				CambiarColor ();
				timerbomba.text = "1";
			}

			// SI PASARON 3 SEGUNDOS
			if (tiempo > 3) {
				// MUERE EL JUGADOR
				v.ModificarVida (-1);
			}

		}
		// SI EL JUGADOR DISPARÓ LA GRANADA
		else if (tag == "Jugador" && inv.GranadaDisponible () == false) 
		{
			// SE DESACTIVA "TexturaTimer", SE BORRA EL TEXTO DE "TimerBomba" Y SE RESETEAN EL TEMPORIZADOR Y EL CONTADOR.
			texturatimer.SetActive (false);
			timerbomba.text = "";
			contador = 0;
			tiempo = 0;
		}
	}

	private void CambiarColor()
	{
		if (contador == 1) 
		{
			// LA MIRA ESTA FORMADA POR 9 ESFERAS, PARA CAMBIARLE EL COLOR TENGO QUE ENTRAR EN UNA POR UNA CON EL NOMBRE DE CADA UNA, EN ESTE CASO SOLO CAMBIA EL NUMERO QUE ESTA ENTRE PARENTESIS
			// USÉ UN CICLO "for" PARA HACER MAS RAPIDO, "i" EMPIEZA EN 1 Y SE EJECUTA 9 VECES (SUMANDO 1 EN CADA VUELTA), CUANDO "i" SEA IGUAL A 10 SALE DEL CICLO "for". "i.ToString()" ES PARA CONVERTIR EL VALOR EN TEXTO
			for (int i = 1; i < 10; i++) 
			{
				// TRAIGO EL COMPONENTE DEL OBJETO HIJO DE LA MIRA, QUE A LA VEZ ES HIJO DEL JUGADOR
				mr = transform.Find ("/Jugador/DondeApuntar/DondeApuntar (" + i.ToString () + ")").GetComponent<MeshRenderer> ();

				// LE CAMBIO EL COLOR
				mr.material.color = Color.yellow;
			}

			// CAMBIO EL COLOR DEL TEXTO "TimerBomba"
			timerbomba.color = Color.yellow;

			// SUMO 1 AL CONTADOR
			contador++;
		}
		// ES IGUAL AL IF DE ARRIBA PERO CON OTRO COLOR
		else if (contador == 2) 
		{
			for (int i = 1; i < 10; i++) 
			{
				mr = transform.Find ("/Jugador/DondeApuntar/DondeApuntar (" + i.ToString () + ")").GetComponent<MeshRenderer> ();
				mr.material.color = Color.red;
			}
			timerbomba.color = Color.red;
			contador++;
		} 
	}
}
