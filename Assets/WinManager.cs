using UnityEngine;
using System.Collections;

public class WinManager : MonoBehaviour 
{
	private bool oneShot = true;

	public Sprite heresACookieSprite;

	void Start()
	{
		GetComponent<SpriteRenderer>().enabled = false;
	}

	void ShowWinSign()
	{
		GetComponent<SpriteRenderer>().enabled = true;

		Invoke("PresentTastiness", 2);
	}

	void PresentTastiness()
	{
		GetComponent<SpriteRenderer>().sprite = heresACookieSprite;

		transform.Find("Cookie").gameObject.SetActive(true);
	}

	void OnTriggerEnter2D(Collider2D collider)
	{
		if(oneShot)
		{
			oneShot = false;
			Invoke("ShowWinSign", 1.5f);
		}
	}
}
