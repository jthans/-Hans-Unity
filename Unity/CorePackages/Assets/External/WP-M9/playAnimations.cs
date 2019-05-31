using UnityEngine;
using System.Collections;

public class playAnimations : MonoBehaviour {
	public GameObject arms;
	public GameObject weapon;
	//public bool[] animPlaying;
	//private int animAmount = 6;
	//private int playingAnimIndex = -1;
	public Camera mainCameraGO;
	public GameObject mainCameraSnapObject;
	public float cameraNormalFov = 60f;
	public float cameraZoomFov = 24f;
	public float cameraNearClipping = 0.1f;
	// Use this for initialization
	void Start () {
//		animPlaying = new bool[animAmount];
//		for (int x=0; x> animAmount; x++)
//		{
//
//			animPlaying[x] = false;
//
//		}
		mainCameraGO = Camera.main;
		mainCameraSnapObject = GameObject.FindGameObjectWithTag("mainCameraSnapObject");

		mainCameraGO.transform.position = mainCameraSnapObject.transform.position;
		mainCameraGO.transform.rotation = mainCameraSnapObject.transform.rotation;
		mainCameraGO.nearClipPlane = cameraNearClipping;

	}
	
	// Update is called once per frame
	void Update () {



	}




	public void playReload()
	{
		arms.GetComponent<Animation>().Play("M9-Reload");
		weapon.GetComponent<Animation>().Play("M9-Reload", PlayMode.StopAll);
		mainCameraGO.fieldOfView = cameraNormalFov;
	}
	public void playFire()
	{
		arms.GetComponent<Animation>().Play("M9-Fire");
		weapon.GetComponent<Animation>().Play("M9-Fire", PlayMode.StopAll);
		mainCameraGO.fieldOfView = cameraNormalFov;
	}

	public void playIdle()
	{
		arms.GetComponent<Animation>().Play("M9-Idle");
		weapon.GetComponent<Animation>().Play("M9-Idle", PlayMode.StopAll);
		mainCameraGO.fieldOfView = cameraNormalFov;
	}

	public void playSprint()
	{
		arms.GetComponent<Animation>().Play("M9-Sprint");
		weapon.GetComponent<Animation>().Play("M9-Idle", PlayMode.StopAll);
		mainCameraGO.fieldOfView = cameraNormalFov;
	}

	public void playADSFire()
	{
		arms.GetComponent<Animation>().Play("M9-ADS-Fire");
		weapon.GetComponent<Animation>().Play("M9-Fire", PlayMode.StopAll);
		mainCameraGO.fieldOfView = cameraZoomFov;
	}
	public void playADSIdle()
	{
		arms.GetComponent<Animation>().Play("M9-ADS-Idle");
		weapon.GetComponent<Animation>().Play("M9-Idle", PlayMode.StopAll);
		mainCameraGO.fieldOfView = cameraZoomFov;
	}

	public void playNothing()
	{
		arms.GetComponent<Animation>().Stop();
		weapon.GetComponent<Animation>().Stop();
		//mainCameraGO.fieldOfView = cameraNormalFov;

	}

	}

