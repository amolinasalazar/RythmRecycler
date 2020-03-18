using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BeatAnimator : MonoBehaviour {

	public abstract void Beat (GameManager.BeatTypes type);
}
