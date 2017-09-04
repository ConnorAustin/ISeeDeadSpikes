using UnityEngine;
using System.Collections;

using System.Net;
using System.Net.Sockets;
using System.Net.NetworkInformation;

enum ViewState
{
	Menu,
	IPEnterView,
	ConnectingView,
	ServerCreated
}

public class MenuCamera : MonoBehaviour 
{
	private ViewState state = ViewState.Menu;
	private string ip = "";
	private string localIP = "";
	private NetworkManager netManager;

	void Start()
	{
		Application.runInBackground = true;

		netManager = GameObject.Find("NetworkManager").GetComponent<NetworkManager>();
	}

	void OnGUI ()
	{
		if(state == ViewState.ConnectingView)
		{
			Debug.Log(ip);

			GUI.Label(new Rect(20, 20, 200, 20), "Connecting...");

			if(GUI.Button(new Rect(40, 120, 180, 20), "Cancel"))
			{
				state = ViewState.IPEnterView;
			}
		}
		else if (state == ViewState.IPEnterView) 
		{
			GUI.Label(new Rect(90, 20, 200, 40), "IP Address");

			ip = GUI.TextArea(new Rect(40, 40, 200, 60), ip);	

			if(GUI.Button(new Rect(40, 120, 180, 20), "Cancel"))
			{
				state = ViewState.Menu;
			}
			if(GUI.Button(new Rect(40, 160, 180, 20), "Connect"))
			{
				netManager.Connect(ip);
				state = ViewState.ConnectingView;
			}
		}
		else if(state == ViewState.Menu)
		{
			if(GUI.Button (new Rect (40, 120, 200, 60), "Create Server"))
			{
				state = ViewState.ServerCreated;
				netManager.CreateServer();
			}
			if (GUI.Button (new Rect (40, 40, 200, 60), "Connect to Server")) 
			{
				state = ViewState.IPEnterView;
			}
		}
		else if(state == ViewState.ServerCreated)
		{
			GUI.Label(new Rect(200, 200, 200, 200), "IP Address: " + Network.player.ipAddress);
			GUI.Label(new Rect(40, 40, 200, 200), "Waiting for player to connect");
		}
	}
}
