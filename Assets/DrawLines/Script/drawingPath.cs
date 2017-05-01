using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class drawingPath{

	public List<Vector3> path;
	public List<Vector2> path_discrete;
	public List<Vector2> shift;
	public int step;
	public Vector2 startPoint;

	public drawingPath(){
		path = new List<Vector3> ();
		path_discrete = new List<Vector2> ();
		shift = new List<Vector2> ();
	}

	public void addPath( Vector3 newPoint ){
		path.Add (newPoint);
	}

	public void addPath2( string name ){
		
		int idx = int.Parse (name);
		int idx_x = (idx / 8)+1;
		int idx_y = idx % 8;
		if (idx_y == 0)
			idx_y = 8;

		if (path_discrete.Count == 0)
			startPoint = new Vector2 ( idx_x, idx_y );

		//Debug.Log ("idx_x = " + idx_x + ", idx_y = " + idx_y );
		path_discrete.Add ( new Vector2( idx_x, idx_y ) );
	}

	public void reconstructPath(){
		
		//reconstruct path from path_discrete to shift
		step = path_discrete.Count;
		for (int i = 1; i < path_discrete.Count; i++) {
			
			float shift_x = path_discrete [i].x - path_discrete [i-1].x;
			float shift_y = path_discrete [i].y - path_discrete [i-1].y;

			shift.Add (new Vector2( shift_x, shift_y ) );
		}
			
		/* Debug */
//		for (int i = 0; i < shift.Count; i++) {
//			Debug.Log ("shift " + shift[i].x + ", " + shift[i].y ) ;
//		}
	}
}
