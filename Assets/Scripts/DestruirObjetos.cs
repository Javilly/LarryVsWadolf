using UnityEngine;
using System.Collections;

public class DestruirObjetos : MonoBehaviour 
{
	private GameObject enemigo;
	private Vida v;
	private float chanceExplosion;
	public GameObject objeto;
	private AudioSource[] sonidos;

	// ESTOS 3 SOLO SE USAN EN LA GRANADA, HAY QUE BUSCAR LA FORMA DE HACERLOS PRIVATE Y CARGAR LOS ASSETS
	public GameObject prefab1; // EFECTO1-FUEGO
	public GameObject prefab2; // EFECTO2-EXPLOSION
	public GameObject prefab3; // PARTICULAS QUE APARECEN CUANDO FALLA LA GRANADA

	private float tiempo;


	public void Awake()
	{
		// CHANCE INICIAL DE LA EXPLOSION, SOLO SE MODIFICA EN LA TERCER ETAPA DEL JUEGO
		chanceExplosion = 60;
		sonidos = GameObject.Find ("CosasGenerales").GetComponents<AudioSource>();
		enemigo = GameObject.Find ("Enemigo");
		if (enemigo != null) 
		{
			v = enemigo.GetComponent<Vida> ();
			// SI ES PISO
			if (objeto.tag == "Piso" || objeto.tag == "Plataforma") {
				tiempo = 12;
			}

			// SI ES GRANADA
			if (objeto.tag == "Granada" || objeto.name == "ParticulaGranadaQueFalla(Clone)") {
				tiempo = 1;
			}

			// SI ES LA GRANADA DEL JUGADOR
			if (objeto.tag == "GranadaJugador") {
				tiempo = 3;
			}

			// SI ES EL EFECTO DE LA GRANADA (EFECTO1-FUEGO: ES UNA PLATAFORMA QUE VA SOBRE EL PISO | EFECTO2-EXPLOSION: ES UNA EXPLOSION QUE SE VA AGRANDANDO EN EL LUGAR)
			if (objeto.tag == "EfectoGranada") {
				tiempo = 3.8f;
			}

			// SI ES EL LASER DEL ROBOT
			if (objeto.tag == "Laser") 
			{
				tiempo = 3;
			}

			// SI ES EL TEXTO DE LOS CONTROLES
			if (objeto.name == "textoControles") 
			{
				tiempo = 4;
			}

			// SE EJECUTA EL EVENTO "Destruir" DESPUES DEL TIEMPO ASIGNADO
			Invoke ("Destruir", tiempo);
		}
	}

	public void Destruir()
	{

		// SI ES PISO O PLATAFORMA
		if (objeto.tag == "Piso" || objeto.tag == "Plataforma") 
		{
			Destroy (gameObject);
		}

		// SI EL OBJETO ES GRANADA
		if (objeto.tag == "Granada") 
		{
			if (v != null) 
			{
				// SE GENERA UN NUMERO RANDOM DE 1 A 100 PARA REPRESENTAR LA PROBABILIDAD DE EXPLOSION DE LA GRANADA
				float chance = Random.Range(1.0f,100.0f);

				// CUANDO EL ENEMIGO TIENE 1 DE VIDA, LA CHANCE DE FALLAR LA EXPLOSION ES 60%
				if (v.getVida () == 1)
					chanceExplosion = 40;
				
				// CHANCE REPRESENTA AL PORCENTAJE (EN ESTE CASO HAY UN 30% DE QUE FALLE LA GRANADA)
				if (chance <= chanceExplosion) 
				{
						if (v.getVida () == 3) 
							// SE DESTRUYE LA GRANADA Y SE CREA EL EFECTO CON EL SIGUIENTE PREFAB("Quaternion.Euler(0,0,0)" SE USA PARA QUE EL EFECTO DEL NAPALM SE CREE SIEMPRE DE FORMA HORIZONTAL)
							Instantiate (prefab1, transform.position, Quaternion.Euler (0, 0, 0));
						else if (v.getVida () == 2)
							Instantiate (prefab2, transform.position, Quaternion.Euler (0, 0, 0));
						else if (v.getVida () == 1) 
							Instantiate (prefab2, transform.position, Quaternion.Euler (0, 0, 0));
						Destroy (gameObject);
				} 			
				else 
				{
					// SE REPRODUCE EL SONIDO CUANDO LA GRANADA FALLA Y SE INSTANCIA EL EFECTO DE PARTICULAS (ES COMO UN HUMO)
					sonidos[3].Play ();
					Instantiate (prefab3, transform.position,  Quaternion.Euler (270, 0, 0));
					// SI EL JUGADOR NO LA AGARRA, HAY QUE HACERLA DESAPARECER
					Invoke ("GranadaQueFalla", 5);
				}
			}
		}

		// SI ES EL EFECTO DE LA GRANADA, EL LASER, LA GRANADA DEL JUGADOR O EL TEXTO DE LOS CONTROLES
		if (objeto.tag == "EfectoGranada" || objeto.name == "ParticulaGranadaQueFalla(Clone)" || objeto.tag == "Laser" || objeto.tag == "GranadaJugador" || objeto.name == "textoControles")
		{
			Destroy (gameObject);
		}

	}

	// SE DESTRUYE LA GRANADA FALLADA
	public void GranadaQueFalla()
	{
		Destroy (gameObject);
	}

	public void Update()
	{
		// AUMENTAR TAMAÑO DE EXPLOSION DE LA GRANADA (EFECTO 2) EN EL TRANSCURSO DEL TIEMPO
		if (objeto.name == "Efecto2-Explosion(Clone)" && v.getVida() < 3) 
		{
			transform.localScale += new Vector3(2 * Time.deltaTime, 2 * Time.deltaTime, 2 * Time.deltaTime);
		}

		// CUANDO MUERE EL ENEMIGO, SE DESTRUYEN TODOS LOS OBJETOS EN ESCENA (PARA EVITAR PROBLEMAS EN LA ESCENA FINAL)
		if (enemigo == null) 
		{
			if (objeto.tag == "Plataforma" || objeto.tag == "Piso" || objeto.tag == "Laser" || objeto.tag == "Granada" || objeto.tag == "GranadaJugador" || objeto.name == "Efecto2-Explosion(Clone)") 
			{
				Destroy (gameObject);
			}
		}

	}

	public void OnCollisionEnter(Collision c)
	{
		// SI EL JUGADOR CHOCA CON UNA GRANADA
		if (c.gameObject.tag == "Jugador" && gameObject.tag == "Granada") 
		{
			Inventario i = c.gameObject.GetComponent<Inventario> ();
			if (i != null) 
			{
				// SE CARGA 1 GRANADA PARA QUE EL JUGADOR PUEDA DISPARAR
				i.HabilitarGranada();

				// SE DESTRUYE LA GRANADA DEL ENEMIGO PARA QUE DESAPAREZCA DE ESCENA
				Destroy (gameObject);
			}
		}
	}
}

