using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeBody : MonoBehaviour
{
	public bool ChangeKinematic;

	bool isRewinding = false;
	public bool stato;
	bool canAbility2 = true;
	public bool Stop;

	public float recordTime = 5f;

	List<PointInTime> pointsInTime;

	Rigidbody rb;

	public GameObject Timemanager;
	public TimeStop timeStop;
	public TimeManager timeManager;

	public PlayerManager playerManager;
	public GameObject player;

	void Start()
	{
		player = GameObject.Find("player_controller");
		playerManager = player.GetComponent<PlayerManager>();

		Timemanager = GameObject.Find("TimeManager");
		timeStop = Timemanager.GetComponent<TimeStop>();
		timeManager = Timemanager.GetComponent<TimeManager>();

		StartCoroutine(cooldownstart());
		pointsInTime = new List<PointInTime>();
		rb = GetComponent<Rigidbody>();
	}

	void Update()
	{

		float y = Input.GetAxis("Fire3");

		if (!playerManager.isGameOver)
		{
			if (!timeStop.stato && !timeManager.slowtimeactive)
			{
				if (!Stop)
				{
					if ((Input.GetKeyDown(KeyCode.Alpha2) || y == -1) && canAbility2 == true)
					{
						//Debug.Log("Attivo");
						StartCoroutine(timer());
						StartCoroutine(cooldown());
					}
				}
				else
				{
					if ((Input.GetKeyDown(KeyCode.Alpha2) || y == -1) && canAbility2 == true)
					{
						StartCoroutine(statetimer());
					}
				}
			}
		}
	}

	public bool spe()
	{
		return false;
	}

	public bool att()
	{
		return true;
	}

	private IEnumerator timer()
	{
		stato = att();
		StartRewind();
		yield return new WaitForSeconds(5);
		//Debug.Log("Spento");
		stato = spe();
		StopRewind();
	}
	private IEnumerator statetimer()
	{
		stato = att();
		yield return new WaitForSeconds(5);
		//Debug.Log("Spento");
		stato = spe();
	}

	private IEnumerator cooldown()
	{
		canAbility2 = false;
		//Debug.Log(canAbility2);
		yield return new WaitForSeconds(20);
		canAbility2 = true;
		//Debug.Log(canAbility2);
	}
	private IEnumerator cooldownstart()
	{
		canAbility2 = false;
		//Debug.Log(canAbility2);
		yield return new WaitForSeconds(15);
		canAbility2 = true;
		//Debug.Log(canAbility2);
	}

	void FixedUpdate()
	{
		if (isRewinding)
			Rewind();
		else
			Record();
	}

	void Rewind()
	{
		if (pointsInTime.Count > 0)
		{
			PointInTime pointInTime = pointsInTime[0];
			transform.position = pointInTime.position;
			transform.rotation = pointInTime.rotation;
			pointsInTime.RemoveAt(0);
		}
		else
		{
			StopRewind();
		}

	}

	void Record()
	{
		if (pointsInTime.Count > Mathf.Round(recordTime / Time.fixedDeltaTime))
		{
			pointsInTime.RemoveAt(pointsInTime.Count - 1);
		}

		pointsInTime.Insert(0, new PointInTime(transform.position, transform.rotation));
	}

	public void StartRewind()
	{
		isRewinding = true;
        if (!ChangeKinematic)
        {
			rb.isKinematic = true;
		}
	}

	public void StopRewind()
	{
		isRewinding = false;
		if (!ChangeKinematic)
        {
			rb.isKinematic = false;
		}
	}
}