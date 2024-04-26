using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RoomTemplates : MonoBehaviour {

	public GameObject[] bottomRooms;
	public GameObject[] topRooms;
	public GameObject[] leftRooms;
	public GameObject[] rightRooms;

	public GameObject closedRoom;

	public List<GameObject> rooms;

	public float waitTime;
	public bool spawnedBoss;
	public bool RoomspawnedBoss;

	public GameObject BossIconMap;
	public GameObject bossRoomCentral;
	public GameObject bossRoomRight;
	public GameObject bossRoomLeft;
	public GameObject bossRoomBottom;
	public GameObject bossRoomTop;

	public NavMeshSurface[] surface;

	// Use this for initialization

	private GameObject rotatingSpriteObj;
	private RotatingSprite rotatingSprite;
	
	private GameObject Player;
	private Weapon WeaponScript;

	public GameObject[] objectsToActivate;

	private void Start()
	{
		ActivateObjects();
		rotatingSpriteObj = GameObject.Find("loadingIcon");
		rotatingSprite = rotatingSpriteObj.GetComponent<RotatingSprite>();

		Player = GameObject.Find("player_controller");
		WeaponScript = Player.GetComponent<Weapon>();
	}

	void ActivateObjects()
	{
		foreach (GameObject obj in objectsToActivate)
		{
			obj.SetActive(true);
		}
	}

	void Update(){

		if(waitTime <= 0 && spawnedBoss == false){
			for (int i = 0; i < rooms.Count; i++) {
				if(i == rooms.Count-1){
                    //Destroy(rooms[i], 2.0f);
                    if (!RoomspawnedBoss)
                    {
						Instantiate(BossIconMap, rooms[i].transform.position, Quaternion.identity);
					}
					///Spawn della stanza del boss;
					string name = rooms[i].GetComponent<Object_namer>().Objectname.ToString();
					//controllare TopBottom, LeftBottom, LeftTop
					switch (name) 
					{
						case "Bottom":
							Debug.Log("Bottom_room");
							Instantiate(bossRoomBottom, rooms[i].transform.position, Quaternion.identity);
							break;
						case "Right":
							Debug.Log("Right_room");
							Instantiate(bossRoomRight, rooms[i].transform.position, Quaternion.identity);
							break;
						case "Left":
							Debug.Log("Left_room");
							Instantiate(bossRoomLeft, rooms[i].transform.position, Quaternion.identity);
							break;
						case "Top":
							Debug.Log("Top_room");
							Instantiate(bossRoomTop, rooms[i].transform.position, Quaternion.identity);
							break;
						default:
							Debug.Log("Central");
							Instantiate(bossRoomCentral, rooms[i].transform.position, Quaternion.identity);
							break;
					}

					Destroy(rooms[i]);

					RoomspawnedBoss = true;

					///Build del NavMesh
					StartCoroutine(NavmeshBuilding());

					///Spawn del Boss
					StartCoroutine(bossSpawner(i));
				}
			}
		} else {
			if(waitTime >= -1)
            {
				waitTime -= Time.deltaTime;
			}
		}
	}

	IEnumerator bossSpawner(int elem)
    {
		yield return new WaitForSeconds(1);
		spawnedBoss = true;
		rooms.Remove(rooms[elem]);
		rotatingSprite.SetDisappearCondition(true);
		WeaponScript.canMove = true;
	}

	IEnumerator NavmeshBuilding()
    {
		yield return new WaitForSeconds(1);
		surface[0].BuildNavMesh();
		surface[1].BuildNavMesh();
	}
}

