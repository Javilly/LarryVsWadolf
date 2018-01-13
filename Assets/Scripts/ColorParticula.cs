using UnityEngine;
using System.Collections;

public class ColorParticula : MonoBehaviour 
{
    private GameObject jugador;
    private SaltoJetpack j;
	private float combustibleInicial;

    public void Awake()
    {
		// SE TRAE EL COMPONENTE SALTOJETPACK DEL JUGADOR PARA CHEQUEAR EL COMBUSTIBLE ACTUAL EN EL UPDATE
        jugador = GameObject.Find("Jugador");
        j = jugador.GetComponent<SaltoJetpack>();
		combustibleInicial = j.getCombustible ();
    }

	public void Update () 
	{

		// SI EL COMBUSTIBLE ES MENOR O IGUAL A CERO
		if (j.getCombustible () <= 0) {
			// LA PARTICULA DEL JETPACK SE PONE NEGRO
			GetComponent<ParticleSystem> ().startColor = new Color (0, 0, 0, .5f);
		}
		// SI EL COMBUSTIBLE ES MAYOR A 0 Y MENOR AL 30%
		else if (j.getCombustible () > 0 && j.getCombustible () < combustibleInicial  * 0.3f) {
			// LA PARTICULA DEL JETPACK SE PONE ROJA
			GetComponent<ParticleSystem> ().startColor = new Color (1, 0, 0, .5f);
		}
		// SI EL COMBUSTIBLE ES MAYOR O IGUAL AL 30% Y MENOR AL 60%
		else if (j.getCombustible () >= combustibleInicial * 0.3f && j.getCombustible () < combustibleInicial * 0.6f) {
			// LA PARTICULA DEL JETPACK SE PONE AMARILLA
			GetComponent<ParticleSystem> ().startColor = new Color (1, 1, 0, .5f);
		}
		// SI EL COMBUSTIBLE ES MAYOR O IGUAL AL 60%
		else if (j.getCombustible() >= combustibleInicial * 0.6f){
			// LA PARTICULA DEL JETPACK SE PONE VERDE
			GetComponent<ParticleSystem>().startColor = new Color(0, 1, 0, .5f);}
	}
}
