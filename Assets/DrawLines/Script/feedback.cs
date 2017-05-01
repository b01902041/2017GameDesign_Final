using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class feedback : MonoBehaviour {

	private SpriteRenderer renderer;


	void Start () {
		renderer = this.GetComponent<SpriteRenderer> ();
	}
	

	void Update () {
		
	}

	void touchFeedBack(){
		//Debug.Log (this.name + " change color" );
		renderer.color = new Color( 1f, 0f, 0f, 0.3f );

	}
}
