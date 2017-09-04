using UnityEngine;
using System.Collections;

public class ParticleExplo : MonoBehaviour 
{
	public float particles;
	public float explosionForce;

	public GameObject particle;

	public void Explode(Vector3 pos, Color particleColor)
	{
		transform.position = pos;

		for(int i = 0; i < particles; i++)
		{
			GameObject part = (GameObject)Instantiate(particle);

			Vector3 partPos = transform.position;
			partPos.x += Random.Range(-0.5f, 0.5f);
			partPos.y += Random.RandomRange(-0.5f, 0.5f);

			part.transform.position = partPos;

			part.GetComponent<SpriteRenderer>().color = particleColor;

			Vector3 force = Vector3.Normalize(partPos - transform.position);
			force *= explosionForce;

			part.GetComponent<Rigidbody2D>().AddForce(force);
		}
	}
}
