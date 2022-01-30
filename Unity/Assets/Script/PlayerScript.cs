using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerScript : MonoBehaviour
{
	enum Direction
	{
		up,
		down,
		left,
		right,
	}

	Direction direction;

	Vector3 lastPos;
	Quaternion lastRotation;
	public float frameRate = 0.2f;
	public float step = 0.1f;
	//tama�o del cuadrado

	public List<Transform> Tail = new List<Transform>();

	Collider2D myCollider;

	public UnityEvent OnAddPoint = new UnityEvent();
	public UnityEvent OnDamage = new UnityEvent();

	GameObject GameManager;
	GamaManager gameManagerScript;

	ColorChange playerColor;

	public Transform initialPosition;

	AkSoundEngine akSound;

	private void Start()
	{
		transform.position = initialPosition.position;
		direction = Direction.up;
		playerColor = GetComponent<ColorChange>();

		InvokeRepeating("Move", frameRate, frameRate);
		myCollider = GetComponent<Collider2D>();

		GameObject GamaManager = GameObject.FindGameObjectWithTag("GameManager");

		if (GameManager != null)
		{
			gameManagerScript = GamaManager.GetComponent<GamaManager>();

		}

		if (gameManagerScript != null)
		{
			gameManagerScript.OnLose.AddListener(OnRestart);
			
		}

	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.UpArrow)){ 
			direction = Direction.up; }
		else if (Input.GetKeyDown(KeyCode.DownArrow))
		{ 
			direction = Direction.down; 
		}
		else if (Input.GetKeyDown(KeyCode.LeftArrow))
		{ direction = Direction.left; }
		else if (Input.GetKeyDown(KeyCode.RightArrow))
		{ direction = Direction.right; }
	}

	void Move()
	{
		lastPos = transform.position;
		lastRotation = transform.rotation;

		Vector3 nextPos = Vector3.zero;
		if (direction == Direction.up){
			nextPos = Vector3.up;
			transform.rotation = Quaternion.Euler(new Vector3(0, 0, 90));
		}
		else if (direction == Direction.down){
			nextPos = Vector3.down;
			transform.rotation = Quaternion.Euler(new Vector3(0, 0, 270));
		}
		else if (direction == Direction.left){
			nextPos = Vector3.left;
			transform.rotation = Quaternion.Euler(new Vector3(0, 0, 180));
		}
		else if (direction == Direction.right){
			nextPos = Vector3.right;
			transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
		}
		nextPos *= step;

		transform.position += nextPos;

		MoveTail();
	}

	void MoveTail()
	{
		for (int i = 0; i < Tail.Count; i++)
		{
			Vector3 temp = Tail[i].position;
			Tail[i].position = lastPos;
			Tail[i].rotation = lastRotation;
			lastPos = temp;
		}

	}

	void AddPoint()
	{
		Debug.Log("conseguiste punto");
		OnAddPoint.Invoke();
	}

	private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Bloque")) 
		{
			ColorChange enemyColor = collision.GetComponent<ColorChange>();	
			if(enemyColor != null) 
			{
				if(enemyColor.colorWhite != playerColor.colorWhite) 
				{
					Damage();
				}
			}
			
		} 
		else if (collision.CompareTag("Point")) 
		{
			AddPoint();
		};
    }

	void Damage() 
	{
		Debug.Log("perdiste");
		
		OnDamage.Invoke();
		transform.position = initialPosition.position;
		AkSoundEngine.PostEvent("LosePoint", gameObject);
	}

	void OnRestart() 
	{
		
	}
	
}


	
	
