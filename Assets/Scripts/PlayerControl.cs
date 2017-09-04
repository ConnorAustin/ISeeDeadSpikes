using UnityEngine;
using System.Collections;

public class PlayerControl : MonoBehaviour
{
	[HideInInspector]
	public bool facingRight = true;

	[HideInInspector]
	public bool jump = false;

	public float moveForce = 365f;
	public float maxSpeed = 5f;
	public float jumpForce = 1000f;

	public string groundLayer;

	public float respawnTime;

	private Transform groundCheck;
	private bool grounded = false;

	private SpriteRenderer spriteRenderer;

	void Start()
	{
		spriteRenderer = GetComponentInChildren<SpriteRenderer>();

		//Server is red sighted guy
		if(Network.isServer)
		{
			//Player is not the controlled player, disable most of the components
			if(GetComponent<PlayerStats>().nameOfLayerPlayerCanSee == "BlueSight")
			{
				//Physics are done on the other side
				Destroy(GetComponent<Rigidbody2D>());

				//Disable this script
				enabled = false;
			}
			else gameObject.name = "LocalPlayer";
		}
		//Client is blue sighted guy
		else
		{
			//Player is not the controlled player, disable most of the components
			if(GetComponent<PlayerStats>().nameOfLayerPlayerCanSee == "RedSight")
			{
				//Physics are done on the other side
				Destroy(GetComponent<Rigidbody2D>());

				//Disable this script
				enabled = false;
			}
			else gameObject.name = "LocalPlayer";
		}
	}

	[RPC]
	void NetDied()
	{
		Explode();
		spriteRenderer.enabled = false;
	}

	[RPC]
	void NetRespawn()
	{
		spriteRenderer.enabled = true;
	}

	void Awake()
	{
		groundCheck = transform.FindChild("groundCheck");

		Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Player"), true);	
		Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Player"), LayerMask.NameToLayer("Physic Particle"), true);
	}

	void Update()
	{
		grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer(groundLayer));  

		if(Input.GetButtonDown("Jump") && grounded)
			jump = true;
	}

	void FixedUpdate ()
	{
		float h = Input.GetAxis("Horizontal");

		if(h * GetComponent<Rigidbody2D>().velocity.x < maxSpeed)
			GetComponent<Rigidbody2D>().AddForce(Vector2.right * h * moveForce);

		if(Mathf.Abs(GetComponent<Rigidbody2D>().velocity.x) > maxSpeed)
			GetComponent<Rigidbody2D>().velocity = new Vector2(Mathf.Sign(GetComponent<Rigidbody2D>().velocity.x) * maxSpeed, GetComponent<Rigidbody2D>().velocity.y);

		if(h > 0 && !facingRight)
		{
			Flip();
		}
		else if(h < 0 && facingRight)
		{
			Flip();
		}

		// If the player should jump...
		if(jump)
		{
			// Add a vertical force to the player
			GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, jumpForce));

			// Make sure the player can't jump again until the jump conditions from Update are satisfied.
			jump = false;
		}
	}

	void Flip ()
	{
		// Switch the way the player is labelled as facing.
		facingRight = !facingRight;

		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	[RPC]
	void NetFlip()
	{
		Flip ();
	}

	void OnRespawn()
	{
		this.enabled = true;

		NetworkView view = GetComponent<NetworkView>();
		view.RPC("NetRespawn", RPCMode.Others, null);

		transform.position = GetComponent<PlayerStats>().theSpawnerThatCreatedMe.transform.position;
	}

	void Explode()
	{
		GameObject particleExploder = GameObject.Find("ParticleExploder");
		particleExploder.GetComponent<ParticleExplo>().Explode(transform.position, spriteRenderer.color);
	}

	void OnDied ()
	{
		Explode();

		NetworkView view = GetComponent<NetworkView>();
		view.RPC("NetDied", RPCMode.Others, null);

		Invoke ("OnRespawn", respawnTime);

		this.enabled = false;
	}
}
