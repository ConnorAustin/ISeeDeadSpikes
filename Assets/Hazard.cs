using UnityEngine;
using System.Collections;

public class Hazard : MonoBehaviour 
{
	void OnCollisionEnter2D(Collision2D collision)
	{
		if(collision.gameObject.tag == "Player")
		{
			collision.gameObject.SendMessage("OnDied");
		}
	}
}
