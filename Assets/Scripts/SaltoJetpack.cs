using UnityEngine;
using System.Collections;

public class SaltoJetpack : MonoBehaviour {

	private float aceleracion;
	private float gravedad = 50;
	private float fuerzaSalto = 15;
	private float combustibleMaximo = 2;
	private float combustibleDisponible = 2;
	private AudioSource sonido;


	public void Awake()
	{
		sonido = GetComponent<AudioSource> ();
	}

	public void FixedUpdate()
	{
		aceleracion -= gravedad * Time.deltaTime;

		// SI SE APRETA O MANTIENE EL BOTON "JUMP" Y EL COMBUSTIBLE ES MAYOR A 0
		if (Input.GetButton ("Jump") && combustibleDisponible > 0) 
		{
			aceleracion = fuerzaSalto;

			// SE RESTA EL COMBUSTIBLE DISPONIBLE EN SEGUNDOS (O FRACCIONES DE SEGUNDO) BASANDOSE EN EL TIEMPO QUE SE HAYA APRETADO O MANTENIDO LA TECLA "JUMP"
			combustibleDisponible -= Time.deltaTime;

			// SI EL COMBUSTIBLE ES MENOR O IGUAL A 0
			if (combustibleDisponible <= 0) 
			{
				// LO IGUALO A 0 POR SI QUEDA CON VALORES NEGATIVOS, AUNQUE SEAN SOLO DECIMAS (PARA QUE LA RECARGA SEA MAS PRECISA EN EL TIEMPO)
				combustibleDisponible = 0;

				aceleracion = 0;
			}

			// ACTIVAR SONIDO DEL JETPACK
			if (sonido.isPlaying == false)
				sonido.Play ();
		} 

		if (!Input.GetButton("Jump")) 
		{	
			// DESACTIVAR SONIDO DEL JETPACK
			sonido.Stop ();
		}

		// SE REALIZA EL MOVIMIENTO
		transform.Translate (0, aceleracion * Time.deltaTime, 0);
	}

	// METODO PARA ACCEDER AL COMBUSTIBLE ACTUAL
	public float getCombustible()
	{
		return combustibleDisponible;
	}

	// METODO PARA CAMBIAR EL VALOR AL COMBUSTIBLE MAXIMO (SE USA EN LOS TRUCOS)
	public void setCombustibleMaximo(float cmbMax)
	{
		 combustibleMaximo = cmbMax;
	}

	// METODO PARA CAMBIAR EL VALOR AL COMBUSTIBLE DISPONIBLE (SE USA EN LOS TRUCOS)
	public void setCombustibleDisponible(float cmbDisp)
	{
		combustibleDisponible = cmbDisp;
	}

	// METODO PARA CAMBIAR EL VALOR DE LA GRAVEDAD
	public void setGravedad(float grav)
	{
		gravedad = grav;
	}

	// METODO PARA CAMBIAR EL VALOR DE LA FUERZA DEL SALTO
	public void setFuerzaSalto(float fuersalto)
	{
		fuerzaSalto = fuersalto;
	}

	public void OnCollisionStay(Collision c)
	{
		// SI EL JUGADOR COLISIONA CON OBJETOS DEL TIPO "PLATAFORMA"
		if (c.gameObject.tag == "Plataforma" || c.gameObject.tag == "Piso")
		{
			// SE PONE LA ACELERACION EN 0 PARA QUE NO TRASPASE EL PISO O LA PLATAFORMA (MIENTRAS ESTÁ CORRIENDO O DURANTE LA CAIDA DEL SALTO) YA QUE LA GRAVEDAD LO EMPUJA HACIA ABAJO 
			aceleracion = 0;

			// SI EL COMBUSTIBLE DISPONIBLE ES MENOR AL COMBUSTIBLE MAXIMO
			if (combustibleDisponible < combustibleMaximo)
				// SE RECARGA 1 UNIDAD POR SEGUNDO
				combustibleDisponible += Time.deltaTime;
			else
				// SI AL FINALIZAR LA CARGA SE PASA VALOR MAXIMO, SE ASIGNA DIRECTAMENTE EL VALOR MAXIMO AL COMBUSTIBLE DISPONIBLE PARA QUE QUEDEN IGUALES
				combustibleDisponible = combustibleMaximo;
		}
	}
}
