using UnityEngine;
using System.Collections;

public class DisparosDelEnemigo : MonoBehaviour 
{
	private float tiempo = 0;
    private string sentido = "Arriba";
	private Vida v;
	public GameObject prefab;
	public GameObject prefab2;
	public GameObject apuntar;
	public Transform posicion;


	public void Awake()
	{
		
		if (gameObject.tag == "Enemigo") 
		{
			// A LOS 2 SEGUNDOS DE INICIAR, EL ENEMIGO TIRA GRANADAS CADA 4 SEGUNDOS
			v = GetComponent<Vida>();
			InvokeRepeating ("DispararAlJugador", 4, 4);
		}
	}
		
	public void DispararAlJugador()
	{
		// CREO UNA VARIABLE CON UN NUMERO RANDOM ENTRE -30 y 30 (ES EL ANGULO DEL DISPARADOR) PARA EL PRIMER TIPO DE GRANADA, ENTRE 5 Y -30 PARA EL SEGUNDO TIPO, ENTRE 20 Y -20 PARA EL TERCER TIPO
		float numeroRandom = 0;
		if (v != null) 
		{
			if (v.getVida () == 3) 
				numeroRandom = Random.Range (-10, 30);
			 else if (v.getVida () == 2) 
				numeroRandom = Random.Range (-25, 5);
			 else if (v.getVida () == 1) 
				numeroRandom = Random.Range (-20, 20);
		}

		// SE ROTA EL DISPARADOR
		posicion.Rotate (0, 0, numeroRandom);

		// DISPARA LA GRANADA
		Instantiate (prefab, posicion.position, posicion.rotation);

		// EL DISPARADOR VUELVE A LA POSICION ORIGINAL (SI NO PONGO ESTO, EL PROXIMO DISPARO SE PUEDE SALIR DEL RANGO PORQUE USA LA POSICION ACTUAL CUANDO VUELVE A ROTAR)
		posicion.Rotate (0, 0, -numeroRandom);
	}

	public void Update()
	{
		// SI ES EL JUGADOR
		if (gameObject.tag == "Jugador")
		{

			// VELOCIDAD DE LA MIRA
			float Velocidad = 15;

			// PARA MOVER LA MIRA DE ARRIBA A ABAJO Y VICEVERSA
			// SI EL SENTIDO ES HACIA ARRIBA Y LA ROTACION EN EL EJE Z NO SE PASA DE LAS 10 UNIDADES (0,1 = 10 unidades);
			if (apuntar.transform.rotation.z < 0.1f && sentido == "Arriba") 
			{
				// "apuntar" ES LA MIRA y "posicion" ES EL DISPARADOR INVISIBLE QUE ESTA AL LADO DEL JUGADOR
				apuntar.transform.Rotate (0, 0, Velocidad*Time.deltaTime);
				posicion.transform.Rotate (0, 0, Velocidad*Time.deltaTime);
			} 
			else 
			{
				// SE CAMBIA EL SENTIDO	
				sentido = "Abajo";
			}

			// SI EL SENTIDO ES HACIA ABAJO Y LA ROTACION EN EL EJE Z NO SE PASA DE LAS -10 UNIDADES 
			if (apuntar.transform.rotation.z > -0.1f && sentido == "Abajo")
			{
				apuntar.transform.Rotate (0, 0, -Velocidad*Time.deltaTime);
				posicion.transform.Rotate (0, 0, -Velocidad*Time.deltaTime);
			} 
			else 
			{
				// SE CAMBIA EL SENTIDO
				sentido = "Arriba";
			}

			// SI SE APRETA EL BOTON "Fire1" (CLICK IZQUIERDO O CTRL IZQ)
            if (Input.GetButtonDown("Fire1")) 
			{
				// SE TRAE EL COMPONENTE INVENTARIO DEL JUGADOR
				Inventario i = gameObject.GetComponent<Inventario> ();

				// VERIFICA QUE EL JUGADOR TENGA UNA GRANADA DISPONIBLE PARA PODER DISPARAR
				if (i.GranadaDisponible()) 
				{
					// DISPARA Y SE LE QUITAN LAS GRANADAS DISPONIBLES AL JUGADOR
					Instantiate (prefab, posicion.position, posicion.rotation);
					i.DeshabilitarGranada ();
				}
			}
		}

		if (gameObject.tag == "Enemigo") 
		{
			if (v != null)
			{
				// "tiempo" ES UN TEMPORIZADOR QUE VA DE 0 A 1
				tiempo += Time.deltaTime;

				// SI LA VIDA DEL ENEMIGO ES 1 Y EL TIEMPO ES MAYOR A 1 SEGUNDO 
				if (v.getVida () == 1 && tiempo > 1) 
				{
					// EL ENEMIGO DISPARA EL LASER Y SE RESETEA EL TEMPORIZADOR (DISPARA 1 VEZ POR SEGUNDO)
					DispararLaser ();
					tiempo = 0;
				}
			}
		}
	}


	public void DispararLaser()
	{
		// SE INSTANCIA EL LASER DESDE LOS OJOS DEL ROBOT
		Instantiate (prefab2, new Vector3 (posicion.position.x - 3, posicion.position.y + 2.5f, posicion.position.z), Quaternion.Euler (0, 0, 0));
	}
}
