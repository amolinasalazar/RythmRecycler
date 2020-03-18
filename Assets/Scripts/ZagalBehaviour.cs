using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZagalBehaviour : BeatAnimator {

	private Animator myAnimation;

	// Use this for initialization
	void Start () {
		myAnimation = GetComponent<Animator> ();
		//myAnimation.SetTrigger ("idle");

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public override void Beat(GameManager.BeatTypes type){
//		Debug.Log (type);
		switch (type) {

		case GameManager.BeatTypes.ZagalLaunch:
			//Debug.Log ("lanzaaaaa");
			myAnimation.SetTrigger ("zagallaunch");
			break;

		case GameManager.BeatTypes.Catch:

			myAnimation.SetTrigger ("zagalfeliz");
			break;

		case GameManager.BeatTypes.Fail:

			myAnimation.SetTrigger ("zagaltriste");
			break;

		default:
			//myAnimation.Play ();
			//Debug.Log ("animando");
			//myAnimation.GetCurrentAnimatorStateInfo
			myAnimation.SetTrigger ("idle");

			break;
		}
	}

}
