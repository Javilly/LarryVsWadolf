using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Trucos : MonoBehaviour {

	// TRUCOS O HERRAMIENTAS PARA DESARROLLADORES :D!

	// VARIABLES QUE VOY A MODIFICAR CON LOS TRUCOS
	private Inventario i;
	private Vida v;
	private SaltoJetpack sj;
	private AudioSource[] sonidos;
	private AudioSource sonidoTrucosActivados;
	private bool granadasInfinitas;
	private TextMesh textoTrucoActivado;

	// STRING DONDE SE ESCRIBE EL TRUCO
	public string Truco;

	// CHAR DONDE VOY GUARDANDO CADA LETRA QUE SE APRETA
	public char letraPresionada;

	public void Awake()
	{
		Truco = "";
		granadasInfinitas = false;
		if (SceneManager.GetActiveScene ().name == "Scenes/Videojuego") 
		{
			textoTrucoActivado = GameObject.Find("textoTrucoActivado").GetComponent<TextMesh>();
			sonidoTrucosActivados = GameObject.Find ("textoTrucoActivado").GetComponent<AudioSource> ();
		}
	}

	public void Update()
	{
		// CADA VEZ QUE SE ESCRIBE ALGO, LO PONGO EN MAYUSCULA PARA FACILITAR LAS COMPARACIONES
		Truco = Truco.ToUpper ();

		// LIMITO LOS CARACTERES A 10 DE LA VARIABLE "Truco" PARA QUE NO SE LLENE LA MEMORIA
		if (Truco.Length > 10) 
		{
			// SE VA BORRANDO EL PRIMER CARACTER AL MISMO TIEMPO QUE SE COPIA EL ULTIMO QUE APRETO
			Truco = Truco.Remove (0, 1);
		}

		// TRUCOS EN EL MENU
		if (SceneManager.GetActiveScene().name == "Scenes/Menu") 
		{
			// SI ESCRIBIS "RICK"
			if (Truco.Contains ("RICK"))
				SceneManager.LoadScene ("Scenes/RickAstley", LoadSceneMode.Single);

			// SI ESCRIBIS "MEGAN"
			if (Truco.Contains ("MEGAN")) 
				SceneManager.LoadScene ("Scenes/MeganFox", LoadSceneMode.Single);
		}

		// TRUCOS EN EL JUEGO
		if (SceneManager.GetActiveScene().name == "Scenes/Videojuego" && GameObject.Find ("Jugador") != null && GameObject.Find ("Enemigo") != null) 
		{
			// SI ESCRIBIS "IDDQD" ACTIVAS EL MODO DIOS
			if (Truco.Contains ("IDDQD")) 
			{
				ModoDios ();
				sonidoTrucosActivados.Play ();
				textoTrucoActivado.text = "Modo Dios ON";
				Invoke ("BorrarTexto", 0.5f);
			}

			// SI ESCRIBIS "IDKFA" TE DA GRANADAS INFINITAS
			if (Truco.Contains ("IDKFA")) 
			{
				granadasInfinitas = true;
				sonidoTrucosActivados.Play ();
				textoTrucoActivado.text = "Granadas Infinitas ON";
				Invoke ("BorrarTexto", 0.5f);
				Truco = "";
			}
			if (granadasInfinitas == true)
				GranadasInfinitas ();

			// SI ESCRIBIS "BIGDADDY" TE DA COMBUSTIBLE INFINITO
			if (Truco.Contains ("BIGDADDY")) 
			{
				CombustibleInfinito ();
				sonidoTrucosActivados.Play ();
				textoTrucoActivado.text = "Combustible Infinito ON";
				Invoke ("BorrarTexto", 0.5f);
			}

			// SI ESCRIBIS "MOTHERLODE" ACTIVAS LA MUSICA SECRETA
			if (Truco.Contains ("MOTHERLODE")) 
			{
				MusicaSecreta ();
				sonidoTrucosActivados.Play ();
				textoTrucoActivado.text = "Musica Secreta ON";
				Invoke ("BorrarTexto", 0.5f);
			}
		}
	}

	public void ModoDios()
	{
		v = GameObject.Find ("Jugador").GetComponent<Vida> ();
		v.setVida (99999);
		Truco = "";
	}

	public void GranadasInfinitas()
	{
		i = GameObject.Find ("Jugador").GetComponent<Inventario> ();
		if (GameObject.Find ("Enemigo") != null) 
		{
			i.HabilitarGranada ();
		}
		else
		{
			i.DeshabilitarGranada ();
			granadasInfinitas = false;
		}
	}

	public void CombustibleInfinito()
	{
		sj = GameObject.Find ("Jugador").GetComponent<SaltoJetpack> ();
		sj.setCombustibleMaximo (99999);
		sj.setCombustibleDisponible (99999);
		Truco = "";
	}

	public void MusicaSecreta()
	{
		sonidos = GameObject.Find("CosasGenerales").GetComponents<AudioSource>();

		if (sonidos[0].isPlaying == true)
			sonidos[0].Stop();
		
		if (sonidos[1].isPlaying == true)
			sonidos[1].Stop();
		
		if (sonidos[2].isPlaying == true)
			sonidos[2].Stop();

		if (sonidos[4].isPlaying == true)
			sonidos[4].Stop();
		
		sonidos[4].Play();
		Truco = "";
	}

	public void BorrarTexto()
	{
		textoTrucoActivado.text = "";
	}
		
	// OnGUI() ES SIMILAR AL Update() SOLO QUE SE EJECUTA VARIAS VECES POR FRAME (SI NO ME EQUIVOCO) Y ME DEJA MANEJAR OTROS EVENTOS, EN ESTE CASO LO USE SOLO PARA EL INPUT
	public void OnGUI()
	{
		// GUARDO EL EVENTO ACTUAL
		Event tecla = Event.current;
		// VERIFICO QUE SE HAYA APRETADO UNA TECLA Y QUE NO SEA "\0" (SIGNIFICA CARACTER NULO)
		// LO TUVE QUE FILTRAR ASI PARA QUE SOLO ME TRAIGA EL CARACTER QUE APRETO, SINO ME EJECUTA 3 EVENTOS SEGUIDOS CADA VEZ QUE APRETO UNA TECLA Y SE ME BORRA LO QUE APRETO (EL PRIMER EVENTO CUANDO SE APRETA LA TECLA ES "\0", EL SEGUNDO ES LA LETRA QUE APRETO Y EL TERCERO ES CUANDO SE LEVANTA LA TECLA QUE VUELVE A SER "\0")
		if (tecla.isKey && tecla.character != '\0') 
		{
			letraPresionada = tecla.character;
			Truco += letraPresionada;
		}
	}
}