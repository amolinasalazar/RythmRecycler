using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuncionesSonidos : MonoBehaviour {

	void BLUE() {
		FMODUnity.RuntimeManager.PlayOneShot("event:/BLUE");
	}

	void CLICK() {
		FMODUnity.RuntimeManager.PlayOneShot("event:/CLICK");
	}

	void MUSIC() {
		FMODUnity.RuntimeManager.PlayOneShot("event:/MUSIC");
	}

	void OUCH() {
		FMODUnity.RuntimeManager.PlayOneShot("event:/OUCH");
	}

	void PRACTICE() {
		FMODUnity.RuntimeManager.PlayOneShot("event:/PRACTICE");
	}

	void THROW() {
		FMODUnity.RuntimeManager.PlayOneShot("event:/THROW");
	}

	void WHITE() {
		FMODUnity.RuntimeManager.PlayOneShot("event:/WHITE");
	}

	void YELLOW() {
		FMODUnity.RuntimeManager.PlayOneShot("event:/YELLOW");
	}
}
