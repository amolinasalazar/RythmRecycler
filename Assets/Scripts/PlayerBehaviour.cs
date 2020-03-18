using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : BeatAnimator {
	private Animator myAnimation;
	private Animator farolaAnimation;


	// Use this for initialization
	void Start () {
		myAnimation = GetComponent<Animator> ();
		var farola = GameObject.FindGameObjectsWithTag("farola")[0];
		farolaAnimation = farola.GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public override void Beat(GameManager.BeatTypes type){
		Debug.Log (type);
		farolaAnimation.SetTrigger ("idle");
		switch (type) {

		case GameManager.BeatTypes.ZagalLaunch:

			myAnimation.SetTrigger ("idle");
			break;

		case GameManager.BeatTypes.Catch:

			myAnimation.SetTrigger ("catch");

			break;

		case GameManager.BeatTypes.Fail:

			myAnimation.SetTrigger ("fail");
			break;

		case GameManager.BeatTypes.FailYellow:

			myAnimation.SetTrigger ("failamarillo");
			break;

		case GameManager.BeatTypes.FailBlue:
			Debug.Log ("azulllllll");
			myAnimation.SetTrigger ("failazul");
			break;

		case GameManager.BeatTypes.FailGreen:

			myAnimation.SetTrigger ("failverde");
			break;

		case GameManager.BeatTypes.FailGrey:

			myAnimation.SetTrigger ("failgris");
			break;

		case GameManager.BeatTypes.CatchYellow:

			myAnimation.SetTrigger ("catchamarillo");
			break;

		case GameManager.BeatTypes.CatchBlue:
			Debug.Log ("azulllllll");
			myAnimation.SetTrigger ("catchazul");
			break;

		case GameManager.BeatTypes.CatchGreen:

			myAnimation.SetTrigger ("catchverde");
			break;

		case GameManager.BeatTypes.CatchGrey:

			myAnimation.SetTrigger ("catchgris");
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
