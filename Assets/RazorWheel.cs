using UnityEngine;
using System.Collections;

public class RazorWheel : MonoBehaviour 
{
	public float rotationSpeed;

	public float movementSpeed;

	private bool rotatingLeft;

	private bool movingLeft;

	private Vector3 lerpStart;

	private float lerp;

	private Transform leftNode;
	private Transform rightNode;

	void Start () 
	{
		rotatingLeft = false;
		movingLeft = true;
		lerp = 0;

		rightNode = transform.parent.FindChild("RightNode");
		leftNode = transform.parent.FindChild("LeftNode");
	}

	void Update () 
	{
		if(movingLeft)
		{
			transform.position = Vector3.Lerp(rightNode.position, leftNode.position, lerp);
		}
		else
		{
			transform.position = Vector3.Lerp(leftNode.position, rightNode.position, lerp);
		}

		lerp += movementSpeed * Time.deltaTime;

		if(Mathf.Min(lerp, 1.0f) == 1.0f)
		{
			lerp = 0;

			rotatingLeft = !rotatingLeft;
			movingLeft = !movingLeft;
		}

		float rotation = 0;

		if(rotatingLeft)
		{
			rotation += rotationSpeed * Time.deltaTime;
		}
		else
		{
			rotation -= rotationSpeed * Time.deltaTime;
		}

		transform.RotateAround(Vector3.forward, rotationSpeed);
	}
}
