using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class drawingDetector : MonoBehaviour {

	public float startWidth = 0.1f;
	public float endWidth = 0.1f;
	public float threshold = 0.001f;
	public Camera thisCamera;

	private int lineCount = 0;
	private drawingPath _drawPath;
	private bool recording = false;
	private bool initialized = false;
	private Vector3 lastPos = Vector3.one * float.MaxValue;
	private LineRenderer lineRenderer;
	private string prevColliderSensor;
	private Vector3 currMousePos;

	void Start () {
		
		thisCamera = Camera.main;
		_drawPath = new drawingPath ();
		lineRenderer = this.GetComponent<LineRenderer> ();

	}

	void Update () {


		if (Input.GetMouseButtonDown (0) && !initialized ) {
			recording = true;
		}

		if (Input.GetMouseButtonUp (0)) {
			recording = false;
			initialized = true;
			_drawPath.reconstructPath ();
			clearGameView ();
			passPathToLineBooster ();
		}

		if (recording) {
			
			Vector3 mousePoisition = Input.mousePosition;
			//touch
			Vector3 pos = thisCamera.ScreenToWorldPoint(mousePoisition);
			RaycastHit2D hit = Physics2D.Raycast ( pos, Vector2.zero );

			if ( prevColliderSensor == null || ( prevColliderSensor != null && prevColliderSensor != hit.collider.name ) ) {
				_drawPath.addPath2 (hit.collider.name);
			}

			prevColliderSensor = hit.collider.name; //in order to avoid add same path
			hit.collider.gameObject.SendMessage ("touchFeedBack" );

			//drawline
			mousePoisition.z = thisCamera.nearClipPlane + 0.5f;
			Vector3 mouseWorld = thisCamera.ScreenToWorldPoint (mousePoisition);

			float dist = Vector3.Distance (lastPos, mouseWorld);

			if (dist < threshold)
				return;

			if (currMousePos == null || (currMousePos != null && currMousePos != mouseWorld)) {
				_drawPath.addPath (mouseWorld);
			}

			currMousePos = mouseWorld;

			UpdateLine ();
		}
			

			
	}

	void UpdateLine(){

		//Debug.Log ("update line, lineCount = " + lineCount );
		lineRenderer.SetVertexCount(_drawPath.path.Count);

		for(int i = lineCount; i < _drawPath.path.Count; i++)
		{
			lineRenderer.SetPosition(i, _drawPath.path[i]);
		}
		lineCount = _drawPath.path.Count;
	}

	void clearGameView(){


		//clear line , future work: animation
		lineRenderer.SetVertexCount (0);

		//StartCoroutine (clearLine());


		//clear feedback block
		GameObject sensors = GameObject.Find("Sensors");

		SpriteRenderer[] childrenSprite = sensors.GetComponentsInChildren<SpriteRenderer> ();
		foreach( SpriteRenderer _sprite in childrenSprite ){
			_sprite.color = new Color( 1f, 1f, 1f, 0f );
		}
			

	}

//	IEnumerator clearLine(){
//
//		for ( int i = _drawPath.step-1; i > 0; i-- ) {
//			float prevTime = Time.time;
//			Debug.Log ("prevTime = " + prevTime );
//			_drawPath.path.RemoveAt (i-1);
//
//			Debug.Log ("point num = " + _drawPath.path.Count );
//			//lineRenderer.SetVertexCount (i);
//			UpdateLine ();
//
//			yield return new WaitForSeconds(1.0f);
//
//			float elaspedTime = Time.time-prevTime;
//			Debug.Log ("currTime = " + Time.time );
//			Debug.Log ("elasped = " + elaspedTime );
//		}
//
//
//	}


	void passPathToLineBooster(){
		GameObject lineBooster = GameObject.Find ("lineBooster");
		lineBooster.GetComponent<lineBooster> ()._drawPath = _drawPath;
		lineBooster.GetComponent<lineBooster> ().boost = true;
	}
		
}
