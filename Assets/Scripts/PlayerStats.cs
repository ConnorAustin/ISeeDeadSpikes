using UnityEngine;
using System.Collections;

public class PlayerStats : MonoBehaviour 
{
	public string nameOfLayerPlayerCanSee;

	public GameObject theSpawnerThatCreatedMe;

	void OnSerializeNetworkView (BitStream stream, NetworkMessageInfo info)
	{
		Vector3 pos = transform.position;
		stream.Serialize(ref pos);
		transform.position = pos;

		Vector3 scale = transform.localScale;
		stream.Serialize(ref scale);
		transform.localScale = scale;
	}
}
