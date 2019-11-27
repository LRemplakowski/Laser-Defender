using UnityEngine;
using System.Collections;

public class EnemyFormationScript : MonoBehaviour {

	public GameObject enemyPrefab;
	public GameObject engineTrailPrefab;
	public GameObject engineExhaustPrefab;
	public float width, height;
	public float enemyMovementSpeed;
	public float verticalSpeed;
	public float respawnDelay = 0.5f;	
	
	GameObject enemy;
	float formationEdgePad;
	float xMin, xMax;
    private bool formationEmpty;
	
	public static bool movingRight;

    // Use this for initialization
    void Start () {
		formationEdgePad = width * 0.5f;
		float distance = transform.position.z - Camera.main.transform.position.z;
		Vector3 leftEdge = Camera.main.ViewportToWorldPoint (new Vector3 (0, 0, distance));
		Vector3 rightEdge = Camera.main.ViewportToWorldPoint (new Vector3 (1, 0, distance));
		xMin = leftEdge.x + formationEdgePad;
		xMax = rightEdge.x - formationEdgePad;
	}
	
	public void OnDrawGizmos () {
		Gizmos.DrawWireCube (transform.position, new Vector3 (width, height));
	}
	
	// Update is called once per frame
	void Update () {
		MoveFormation (formationEmpty);
		
		if (AllMembersDead ()) {
			SpawnUntilFull ();
		}
	}
	
	Transform NextFreePosition () {
		foreach (Transform childPositionGameObject in transform) {
			if (childPositionGameObject.childCount == 0) {
				return childPositionGameObject;
			}
		}
		return null;
	}
	
	bool AllMembersDead() {
		foreach (Transform childPositionGameObject in transform) {
			if (childPositionGameObject.childCount > 0)
            {
				return false;
			}
		}
        formationEmpty = true;
		return true;
	}	
	
//	OBSOLETE
//	void SpawnInFormation () {
//		foreach (Transform child in transform) {
//			GameObject enemy = Instantiate (enemyPrefab, child.transform.position, Quaternion.identity) as GameObject;
//			enemy.transform.parent = child;
//		}
//	}
	
	void SpawnUntilFull () {
		Transform freePosition = NextFreePosition ();
		if (freePosition) {
			//enemy = Instantiate (enemyPrefab, freePosition.position, Quaternion.identity) as GameObject;
            enemy = ObjectPooler.SharedInstance.GetPooledObject(enemyPrefab);
            if (enemy != null)
            {
                enemy.transform.position = freePosition.position;
                enemy.transform.parent = freePosition;
                enemy.SetActive(true);
            }
			SpawnEffects ();
//			Debug.Log ("Enemy respawned!");
			Invoke ("SpawnUntilFull", respawnDelay);
		} else { formationEmpty = false; }
	}
	
	void SpawnEffects () {
		GameObject engineTrail = Instantiate (engineTrailPrefab, new Vector3 (enemy.transform.position.x, enemy.transform.position.y, 2), Quaternion.identity) as GameObject;
		engineTrail.transform.parent = enemy.transform;
		
		GameObject engineExhaust = Instantiate (engineExhaustPrefab, new Vector3 (enemy.transform.position.x, enemy.transform.position.y, 1), 
		Quaternion.AngleAxis (270, Vector3.right)) as GameObject;
		engineExhaust.transform.parent = enemy.transform;
	}
	
	void MoveFormation (bool isEmpty) {
        if (isEmpty) // If formation is empty, move it to the center and stop to avoid glitching out @ borders.
        {
            this.transform.position = new Vector3(0f, 3.39f, 0f);
        } else if (!movingRight) {
			this.transform.position += Vector3.left * enemyMovementSpeed * Time.deltaTime;
			//if (this.transform.position.x <= xMin) {movingRight = !movingRight;}
		} else if (movingRight) {
			this.transform.position += Vector3.right * enemyMovementSpeed * Time.deltaTime;
			//if (this.transform.position.x >= xMax) {movingRight = !movingRight;}
		}
	}
}
