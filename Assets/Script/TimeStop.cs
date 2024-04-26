using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeStop : MonoBehaviour
{
	bool isStopped = false;
	public bool stato;
	bool canAbility3 = true;
	public bool Stop;
	private Vector3 actualrotation;

	public List<PointInTime> pointsInTime;

	Rigidbody rb;

	Vector3 recordedVelocity;
	float recordedMagnitude;

	public GameObject Timemanager;
	public TimeBody timeBody;
	public TimeManager timeManager;
	public PlayerManager playerManager;
	public GameObject player;

	void Start()
	{
		player = GameObject.Find("player_controller");
		playerManager = player.GetComponent<PlayerManager>();

		Timemanager = GameObject.Find("TimeManager");
		timeBody = Timemanager.GetComponent<TimeBody>();
		timeManager = Timemanager.GetComponent<TimeManager>();

		StartCoroutine(cooldownstart());
		pointsInTime = new List<PointInTime>();
		rb = GetComponent<Rigidbody>();
	}

	void Update()
	{
		float y = Input.GetAxis("Fire4");
        //Debug.Log(y);


        if (!playerManager.isGameOver)
        {
			if (!timeBody.stato && !timeManager.slowtimeactive)
			{
				if (!Stop)
				{
					if ((Input.GetKeyDown(KeyCode.Alpha3) || y == -1) && canAbility3 == true)
					{
						//Debug.Log("Attivo");
						StartCoroutine(timer());
						StartCoroutine(cooldown());
					}
				}
				else
				{
					if ((Input.GetKeyDown(KeyCode.Alpha3) || y == -1) && canAbility3 == true)
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
		StartAbility();
		yield return new WaitForSeconds(5);
		//Debug.Log("Spento");
		stato = spe();
		StopAbility();
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
		canAbility3 = false;
		//Debug.Log(canAbility3);
		yield return new WaitForSeconds(20);
		canAbility3 = true;
		//Debug.Log(canAbility3);
	}
	private IEnumerator cooldownstart()
	{
		canAbility3 = false;
		//Debug.Log(canAbility3);
		yield return new WaitForSeconds(15);
		canAbility3 = true;
		//Debug.Log(canAbility3);
	}

	void FixedUpdate()
	{
		if (isStopped)
			StopTime();
		else
			Record();
	}

	void StopTime()
	{
		if (pointsInTime.Count > 0)
		{
			pointsInTime.RemoveAt(0);
		}
		else
		{
			StopAbility();
		}

	}

	void Record()
	{
		pointsInTime.Insert(0, new PointInTime(transform.position, transform.rotation));
	}

	public void StartAbility()
	{
		recordedVelocity = rb.velocity.normalized;
		recordedMagnitude = rb.velocity.magnitude;

		actualrotation = rb.angularVelocity;
		//Debug.Log(actualrotation);
		rb.velocity = Vector3.zero;

		isStopped = true;
		rb.isKinematic = true;
	}

	public void StopAbility()
	{
		isStopped = false;
		rb.isKinematic = false;

		rb.velocity = recordedVelocity * recordedMagnitude;
		rb.AddRelativeTorque(actualrotation, ForceMode.Impulse);
	}
}
