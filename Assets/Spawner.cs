using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour 
{
	public GameObject thingToSpawn;
	public bool spawningRedPlayer;

	void Start ()
	{
		if (thingToSpawn != null) 
		{
			GameObject spawnedThing = null;

			if(spawningRedPlayer && Network.isServer)
			{
				spawnedThing = (GameObject)Network.Instantiate (thingToSpawn, transform.position, transform.rotation, 1);
				spawnedThing.GetComponent<PlayerStats>().theSpawnerThatCreatedMe = gameObject;
			}
			else if(!spawningRedPlayer && Network.isClient)
			{
				spawnedThing = (GameObject)Network.Instantiate (thingToSpawn, transform.position, transform.rotation, 2);
				spawnedThing.GetComponent<PlayerStats>().theSpawnerThatCreatedMe = gameObject;
			}
		}
	}
}
