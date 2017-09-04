using UnityEngine;
using System.Collections;

public class Lifetime : MonoBehaviour 
{
	public float lifetime;

	void Start () 
	{
		Invoke("Die", lifetime);
	}

	void Die()
	{
		Destroy(gameObject);
	}
}
