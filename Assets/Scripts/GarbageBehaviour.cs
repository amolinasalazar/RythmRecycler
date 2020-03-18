using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarbageBehaviour : MonoBehaviour {

	private Animator anim;
	void Start () {
		
		anim = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		if(anim.GetCurrentAnimatorStateInfo(0).IsName("FlyingGarbage"))
			Destroy(gameObject);
	}

	public void DestroyElement(){
		Destroy(gameObject);
	}
}
