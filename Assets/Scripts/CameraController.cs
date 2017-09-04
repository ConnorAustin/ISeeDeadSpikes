using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
	public Vector3 offset;			// The offset at which the Health Bar follows the player.

	private Transform localPlayer;

	public float smoothTime;

	private Vector3 smoothVelocity = Vector3.zero;

	void Update ()
	{
		Vector3 pos = transform.position;

		//Player hasn't spawned yet
		if (localPlayer == null)
		{
			//Try to find player
			localPlayer = GameObject.Find ("LocalPlayer").transform;

			Camera camera = GetComponent<Camera>();

			camera.cullingMask = 0;
			camera.cullingMask |= 1 << LayerMask.NameToLayer("Default");
			camera.cullingMask |= 1 << LayerMask.NameToLayer("Ground");
			camera.cullingMask |= 1 << LayerMask.NameToLayer("Physic Particle");
			camera.cullingMask |= 1 << LayerMask.NameToLayer("Player");
			camera.cullingMask |= 1 << LayerMask.NameToLayer(localPlayer.GetComponent<PlayerStats>().nameOfLayerPlayerCanSee);
			
			return;
		}

		pos = Vector3.SmoothDamp(pos, localPlayer.position, ref smoothVelocity, 0.5f);
		
		pos.z = transform.position.z;
		pos.y = transform.position.y;

		transform.position = pos;
	}
}
