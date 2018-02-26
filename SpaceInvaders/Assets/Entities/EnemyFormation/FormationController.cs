using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FormationController : MonoBehaviour {
	public GameObject enemyPrefab;		//	GameObject to instantiate the enemy's ships
	public float width = 10f;			//	Width to draw the enemy's formation gizmo
	public float height = 5f;			//	Height to draw the enemy's formation gizmo
	public float speed = 5f;			//	Speed of the enemy's ships horizontal movement.
	public float spawnDelay = 0.1f;		//	How fast are the enemy's ships spawned
    public GameObject smoke;

    private bool movingRight = true;	//	Flag to know if the ship is moving rightwards or lftwards
	private float xMax;					//	Right boundary of our gamespace
	private float xMin;					//	Left boundary of our gamespace

	// Use this for initialization
	void Start () {
		//	We find the WorldPoint coordinates of our scene, using the ViewportToWorldPoint method
		//	in our main camera.
		float distanceToCamera = transform.position.z - Camera.main.transform.position.z;
		Vector3 leftBoundary = Camera.main.ViewportToWorldPoint (new Vector3 (0, 0, distanceToCamera));
		Vector3 rightBoundary = Camera.main.ViewportToWorldPoint (new Vector3 (1, 0, distanceToCamera));

		//	We user the WorldPoint coordinates of our scene to set the boundaries for the enemy's formation
		//	horizontal movement.  We use a padding so the ship never touches the screen edge.
		xMax = rightBoundary.x;
		xMin = leftBoundary.x;

		//	We spawn our enemy's ships
		SpawnUntilFull();	//	Previously SpawnEnemies ();
	}

	//	This method spawn all the enemy's ships at once.
	void SpawnEnemies() {
		//	We instantiate an enemy ship for each spot in the enemy's formation
		foreach (Transform child in transform) {
			GameObject enemy = Instantiate (enemyPrefab, child.transform.position, Quaternion.identity) as GameObject;
			enemy.transform.parent = child;
		}
	}

	//	This method spawn an enemy's ship at a time.  If the method detects that there is another free
	//	position, then it calls itself once more after a given delay.
	void SpawnUntilFull() {
		//	We get the next free position in the enemy's formation
		Transform freePosition = NextFreePosition ();

		//	If there is indeed a free position, we instantiate a new enemy ship and place it in that
		//	free position.
		if (freePosition) {
			GameObject enemy = Instantiate (enemyPrefab, freePosition.position, Quaternion.identity) as GameObject;
			enemy.transform.parent = freePosition;
		}

		//	If there is still another free position, we call this very same method again, using the
		//	Invoke() method, after waiting for spawnDelay time.
		if (NextFreePosition()) {
			Invoke ("SpawnUntilFull", spawnDelay);
		}
	}

	//	Draws a wire cube as a gizmo for the enemy's formation in our gamespace
	public void OnDrawGizmos() {
		Gizmos.DrawWireCube (transform.position, new Vector3 (width, height));
	}

	// Update is called once per frame
	void Update () {
		// If the formation is moving right, we keep moving right at the defined speed rate times the
		// deltaTime property; otherwise, we keep moving left.
		if (movingRight) {
			transform.position += Vector3.right * speed * Time.deltaTime; 
		} else {
			transform.position += Vector3.left * speed * Time.deltaTime; 
		}

		//	We compute the right and left edges of our formation using the formation's center x value,
		//	and the formation's width by half.
		float rightEdgeOfFormation = transform.position.x + (0.5f * width);
		float leftEdgeOfFormation = transform.position.x - (0.5f * width);

		//	If the formation's left edge becomes less than the xMin boundary, then we switch to moving
		//	rightwards.  However, if the formation's right edge becomes greater than the xMax boundary,
		//	then we switch to moving leftwards.
		if (leftEdgeOfFormation < xMin) {
			movingRight = true;
		} else if (rightEdgeOfFormation > xMax) {
			movingRight = false;
		}

		//	If all of the enemy ships are dead, then we respawn them
		if (AllMembersDead ()) {
			SpawnUntilFull ();	//	Previously SpawnEnemies ();
		}
	}

	//	This method returns the next available (free) position within the enemy's formation
	Transform NextFreePosition() {
		//	The method iterates througouth all the transform's children.  If there is at least one
		//	empty position, it returns such position.
		foreach (Transform childPositionGameObject in transform) {
			if (childPositionGameObject.childCount == 0) {
				return childPositionGameObject;
			}
		}

		//	If none of the formation's positions is free, it returns null.
		return null;
	}

	//	Checks if all of the enemy ships are destroyed.
	bool AllMembersDead() {
        //	The method iterates througouth all the transform's children.  If there is at least one
        //	enemy ship still alive, it returns false.
        PuffSmoke();
		foreach (Transform childPositionGameObject in transform) {
			if (childPositionGameObject.childCount > 0) {
				return false;
			}
		}

		//	If none of the enemy's ships is alive, it returns true.
		return true;
	}

    void PuffSmoke()
    {
        //Copy our empty object with the Particle system attached
        GameObject smokePuff = Instantiate(smoke, gameObject.transform.position, Quaternion.identity);

        //GameObject smokePuff = Instantiate (smoke, transform.position, Quaternion.identity);
        //var smokePuff = Instantiate(smoke, transform.position, Quaternion.identity).GetComponent<ParticleSystem>().main;
        /*
		 * How would we do this in the newest version of Unity?
		smokePuff.GetComponent<ParticleSystem> ().main.startColor = gameObject.GetComponent<SpriteRenderer> ().color;
		*/
        //smokePuff.startColor = gameObject.GetComponent<SpriteRenderer>().color;

        //Extract the main class from the particleSystem of the smokePuff we just created
        ParticleSystem.MainModule main = smokePuff.GetComponent<ParticleSystem>().main;


        //Change the new smokePuff's particle settings using its ParticleSystem's main class
        //main.startColor = gameObject.GetComponent<SpriteRenderer>().color;
        //main.startColor = this.GetComponent<SpriteRenderer>().color;


    }
}
