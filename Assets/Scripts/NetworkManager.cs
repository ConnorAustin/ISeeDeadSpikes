using UnityEngine;
using System.Collections;

public class NetworkManager : MonoBehaviour
{
	public void Connect(string ip) 
	{
		Network.Connect(ip, 2728);
	}

	public void CreateServer()
	{
		Network.InitializeServer(1, 2728);
	}

	void OnConnectedToServer()
	{
		Application.LoadLevel("game");
	}

	void OnPlayerConnected()
	{
		Application.LoadLevel("game");
	}

	void OnServerInitialized()
	{
		Debug.Log("Server created");
	}

	void Update () 
	{
	
	}
}
