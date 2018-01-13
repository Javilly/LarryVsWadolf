using UnityEngine;
using System.Collections;

public class ReproducirVideo : MonoBehaviour 
{

	public MovieTexture mt;


	public void Start ()
	{
		// REPRODUCE EL VIDEO
		mt.Play ();
	}
}
