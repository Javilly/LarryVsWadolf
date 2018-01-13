using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class ReiniciarJuego : MonoBehaviour 
{
	private GameObject jugador;

	public void Update()
	{
		jugador = GameObject.Find ("Jugador");
		if (jugador == null) 
		{
			// SI EL JUGADOR MUERE (O DEJA DE EXISTIR EN ESCENA), SE EJECUTA EL EVENTO "Reiniciar" A LOS 2 SEGUNDOS
			Invoke ("Reiniciar",2);
		}
	}

	public void Reiniciar()
	{
		// SE VUELVE A CARGAR LA ESCENA 
		SceneManager.LoadScene ("Scenes/Videojuego", LoadSceneMode.Single);
	}
}
