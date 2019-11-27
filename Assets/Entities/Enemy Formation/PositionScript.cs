using UnityEngine;
using System.Collections;

public class PositionScript : MonoBehaviour {

	EnemyFormationScript enemyFormation;
	
	float height;
	float verticalSpeed;
	
	private bool moveUp;

	void OnDrawGizmos () {
		Gizmos.DrawWireSphere (transform.position, 0.5f);
	}
	
	void Start () {
		enemyFormation = transform.parent.GetComponent<EnemyFormationScript>();
		height = enemyFormation.height;
		verticalSpeed = enemyFormation.verticalSpeed;
		if (transform.position.y > transform.parent.position.y) {
			moveUp = true;
		} else moveUp = false;
	}
	
	void Update () {
		MoveVertical ();
	}
	
	void MoveVertical () {
		if (moveUp == true) {
			transform.position += Vector3.up * verticalSpeed * Time.deltaTime;
			if (transform.position.y >= transform.parent.position.y + (height/2)) {moveUp = false;}
		}
		if (moveUp == false) {
			transform.position += Vector3.down * verticalSpeed * Time.deltaTime;
			if (transform.position.y < transform.parent.position.y - (height/2)) {moveUp = true;}
		}
	}
}
