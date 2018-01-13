using UnityEngine;
using System.Collections;

public class Particulas : MonoBehaviour
{
	public ParticleSystem explosion;
	public GameObject humo;
	public GameObject chispa;
    private Vida V;
	private int cont=0;

    public void Update()
	{	V = GetComponent<Vida>();
		if (V != null) 
		{
			// CUANDO LA VIDA DEL ENEMIGO ES IGUAL A 2 Y EL CONTADOR ESTA EN 0
			if (V.getVida () == 2 && cont==0) {
				// SE REPRODUCE LA PARTICULA DE EXPLOSION DEL ENEMIGO Y EMPIEZA A SALIR HUMO
				explosion.Play ();
				humo.SetActive (true);

				// SE SUMA 1 AL CONTADOR
				cont++;

				// SE DETIENE LA EXPLOSION
				Invoke ("DetenerParticula", 0.5f);
			}
			// CUANDO LA VIDA DEL ENEMIGO ES IGUAL A 1 Y EL CONTADOR ESTA EN 1
			if (V.getVida () == 1 && cont==1) {
				// ES IGUAL AL IF DE ARRIBA PERO AHORA EMPIEZAN A SALIR CHISPAS
				explosion.Play ();
				chispa.SetActive (true);
				cont++;
				Invoke ("DetenerParticula", 0.5f);
			}
		}
    }

	public void DetenerParticula()
	{
		// DETIENE LA PARTICULA EXPLOSION
		explosion.Stop ();
	}
}

