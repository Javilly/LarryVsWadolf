using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Audio;
using System.Collections;

public class Menu : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{

	private Text texto;
	private AudioSource sonido;
	private GameObject objeto;
	private SceneManager escena;

	public void Awake()
	{
		objeto = GameObject.Find ("ClickBotonMenu");
		// SE INICIA LA APLICACION CON LA INTRO
		if (tag == "Intro")
			// SE EJECUTA EL EVENTO "Intro" EN 1.5 SEGUNDOS
			Invoke ("Intro", 1.5f);
	}

	public void Intro()
	{
		// SE CARGA EL MENU
		SceneManager.LoadScene("Scenes/Menu", LoadSceneMode.Single);
	}

	public void Update()
	{
		// SI LA ESCENA ACTUAL NO ES EL MENU, CUANDO SE APRETA ESCAPE SE PUEDE VOLVER AL MENU
		if (SceneManager.GetActiveScene().name != "Scenes/Menu")
			if(Input.GetButtonDown("Cancel"))
				SceneManager.LoadScene("Scenes/Menu", LoadSceneMode.Single);
	}

	// EVENTO CUANDO EL MOUSE ENTRA A UN BOTON
	public void OnPointerEnter(PointerEventData eventData)
	{
		// SE CAMBIA EL TAMAÑO DEL TEXTO, EL COLOR DEL TEXTO Y SE REPRODUCE UN SONIDO 
		if (name == "TextoJugar") 
		{
			texto = GetComponent<Text> ();
			texto.fontSize = 28;
			texto.color = new Color (0, 1, 0, 0.5f);
			sonido = GetComponent<AudioSource> ();
			sonido.Play ();
		}

		if (name == "TextoSalir") 
		{
			texto = GetComponent<Text> ();
			texto.fontSize = 28;
			texto.color = new Color (1, 0, 0, 0.8f);
			sonido = GetComponent<AudioSource> ();
			sonido.Play ();
		}
	}

	// EVENTO CUANDO EL MOUSE SALE DE UN BOTON
	public void OnPointerExit(PointerEventData eventData)
	{
		if (name == "TextoJugar") 
		{
			texto = GetComponent<Text> ();
			texto.fontSize = 20;
			texto.color = new Color (0, 0, 1, 0.8f);
		}
			
		if (name == "TextoSalir") 
		{
			texto = GetComponent<Text> ();
			texto.fontSize = 20;
			texto.color = new Color (0, 0, 1, 0.8f);
		}
			
	}

	// EVENTO CUANDO SE CLICKEA UN BOTON
	public void OnPointerClick(PointerEventData eventData)
	{
		if (name == "TextoJugar") 
		{
			sonido = objeto.GetComponent<AudioSource> ();
			sonido.Play ();

			// ENTRA EN LA FUNCION "IniciarJuego"
			IniciarJuego();
		}

		if (name == "TextoSalir") 
		{
			sonido = objeto.GetComponent<AudioSource> ();
			sonido.Play ();

			// SE CIERRA EL JUEGO
			Application.Quit ();
		}
	}
		
	public void IniciarJuego()
	{
		// SE CARGA LA ESCENA DEL JUEGO
		SceneManager.LoadScene ("Scenes/Videojuego", LoadSceneMode.Single);	
	}
}


