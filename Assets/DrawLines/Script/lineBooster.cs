using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lineBooster : MonoBehaviour {

	public drawingPath _drawPath;
	public bool boost = false;

	private List<Vector2> centerPoint; 

	private float drawTime = 1.0f;
	private int height = 8;
	private LineRenderer lineRenderer;
	private float timeCount = 0;

	int pos2name( Vector2 pos ){
		
		int x = (int)pos.x;
		int y = (int)pos.y;

		return height * (x-1) + y ;
	}

	void Start () {


		lineRenderer = this.GetComponent<LineRenderer> (); 
		centerPoint = new List<Vector2> ();

		//construct centerPoint list
		int total_sensor = 96;
		GameObject Sensors = GameObject.Find ("Sensors");
		for (int i = 1; i <= total_sensor; i++) {

			string objectName = i.ToString ();
			GameObject _sensor = Sensors.transform.Find (objectName).gameObject;
			centerPoint.Add ( _sensor.GetComponent<SpriteRenderer>().bounds.center );
			//Debug.Log ("i = " + i + ", center Point = " + centerPoint[i-1] );
		}
			
	}
	

	void Update () {

		if (boost) {
			
			Debug.Log ("start boost");
			Debug.Log (_drawPath.startPoint);

			if (timeCount > 0) {
				
				timeCount -= Time.deltaTime;

			} else {

				Debug.Log ("here");
				lineRenderer.SetVertexCount (0);

				lineRenderer.SetVertexCount (_drawPath.step );

				Vector2 currPos = _drawPath.startPoint;

				int startName = pos2name( currPos );
				lineRenderer.SetPosition ( 0, new Vector3 (centerPoint[startName-1].x, centerPoint[startName-1].y, - 0.1f ) );

				//StartCoroutine (lineBoost(currPos));

				for ( int i = 0; i < _drawPath.shift.Count; i++ ) {
					
					Vector2 shift = _drawPath.shift [i];
					currPos.x += shift.x;
					currPos.y += shift.y;

					int name = pos2name (currPos);

					Vector3 drawPos = new Vector3 (centerPoint[name-1].x, centerPoint[name-1].y, - 0.1f ); // name-1, because index start from 0, not 1 
					lineRenderer.SetPosition(i+1, drawPos ); // i start from 1, because i=0 is start point.
				}


				_drawPath.startPoint = currPos;

				timeCount = 1.0f;

				
			}
				
		}

		
		
	}

//	IEnumerator lineBoost( Vector2 currPos ){
//
//		for ( int i = 0; i < _drawPath.shift.Count; i++ ) {
//
//			Vector2 shift = _drawPath.shift [i];
//			currPos.x += shift.x;
//			currPos.y += shift.y;
//
//			int name = pos2name (currPos);
//
//			Vector3 drawPos = new Vector3 (centerPoint[name-1].x, centerPoint[name-1].y, - 0.1f ); // name-1, because index start from 0, not 1 
//			yield return new WaitForSeconds(1.0f);
//			lineRenderer.SetPosition(i+1, drawPos ); // i start from 1, because i=0 is start point.
//
//		}
//
//		_drawPath.startPoint = currPos;
//		
//	}
}
