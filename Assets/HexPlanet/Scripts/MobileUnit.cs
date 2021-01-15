using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MobileUnit : MonoBehaviour {

	//The currently selected unit.
	public static MobileUnit selectedUnit;
	[Tooltip("The instance of the HexSphere which this unit resides on")]
	public Hexsphere parentPlanet;
	[Tooltip("How quickly this unit moves between tiles")]
	public float moveSpeed;
	[Tooltip("The reference to the tile on which this unit currently resides")]
	public Tile currentTile;

	public bool moving;

	

	public void moveOnPath(Stack<Tile> path)
    {
		StartCoroutine ("move", path);
	}

	public IEnumerator move(Stack<Tile> path)
    {
		moving = true;
		//Pop the first tile from the stack as it is the one we are currently on
		currentTile = path.Pop ();
        Vector3 lastPos = transform.position;
		//Pop off the tiles in the path and move to each one
		while (path.Count > 0)
        {
			Tile next = path.Pop();
			//Vector3 currentPos = transform.position - parentPlanet.transform.position;
			Vector3 currentPos = transform.position;
			float t = 0f;
			//Spherically Interpolate current position to the next position

			while(t < 1f)
            {
				t += Time.deltaTime * moveSpeed;
				Vector3 vSlerp = Vector3.Slerp(currentPos, next.FaceCenter, t);
				transform.position = vSlerp;
                Vector3 lookDir = transform.position - lastPos;
                //Correct rotation to keep transform forward aligned with movement direction and transform up aligned with tile normal
                transform.rotation = Quaternion.LookRotation(lookDir, transform.position - parentPlanet.transform.position);
                lastPos = transform.position;
				yield return new WaitForSeconds(Time.deltaTime);
			}
			//Assign the unit's current tile when it has finished interpolating to it.
			currentTile = next;
		}
		moving = false;
	}
}
