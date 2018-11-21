using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Troops01Ctrl : UnitCtrl {

	int CheckoutTarget(RaycastHit hit)
	{
		UnitCtrl u = (UnitCtrl)hit.transform.gameObject.GetComponent ("UnitCtrl");
		if (null == u)
			return 0;
		Debug.Log ("[Troops] Team flag " + u.team_flag + " " + team_flag);
		if (u.team_flag != team_flag) {
			target = hit.transform.gameObject;
			m_targetlist.Add (target);
			return 1;
		}
		return 0;
	}
	void Start () {
		InitComponent ();
	}

	public override void InitComponent() {
		base.InitComponent ();
		GameManagerStep01 scr = (GameManagerStep01)GameManager.GetComponent<GameManagerStep01> ();
		scr.m_Unitlist.Add (gameObject);
	}
	// Update is called once per frame
	void Update () {
		if (-1 == status)
			return;
		if (com_animator.GetBool ("isPending"))
			return;
		if (null == target) // look for target.
		{
			return;
		}
		else 
		{
			//////////////////////////////////////
			/// + Reset target if it is not available anymore.
			UnitCtrl obj = (UnitCtrl)target.GetComponent ("UnitCtrl");
			if (obj.isDead ()) {
				SetTargetObject (null);
				return;
			}
			/// - 
			////////////////////////////////////// 

			//////////////////////////////////////
			/// + Check angle and distance to determin attack or move 
			Vector3 direction = target.transform.position - this.transform.position;
			float angle = Vector3.Angle (direction, this.transform.forward);
			float distance = Vector3.Distance (target.transform.position, this.transform.position);
			//if (distance < 10 && angle < 50) {
				com_animator.SetBool ("isWalking", true);
				this.transform.rotation = Quaternion.Slerp (this.transform.rotation, Quaternion.LookRotation (direction), 0.1f);
				if (direction.magnitude > this.transform.localScale.y) {
					//direction.y = 0;
					//this.transform.Translate (0f, 0f, speed * Time.deltaTime);
					com_naviMeshAgent.enabled = true;
					com_naviMeshAgent.SetDestination (target.transform.position);

				} else {
					com_naviMeshAgent.enabled = false;
					com_animator.SetBool ("isWalking", false);
					if (!com_animator.GetBool ("isPending")) {
						com_animator.SetTrigger ("isAttack");
					}
				}
			//} else {
			//	anim.SetBool ("isWalking", false);
			//	Debug.LogFormat ("target dis {0} angle {1}", distance, angle);
			//}

			/// - 
			////////////////////////////////////// 
		}

	}

	void OnTriggerEnter(Collider col)
	{
		Debug.Log ("[Troops] OnTriggerEnter [{0}]" + col.gameObject.tag);
		if (null == target) {
		}
		//Debug.Log ("[Skeleton] OnTriggerEnter [{0}]" + col.gameObject.tag);
		//	if (col.CompareTag ("Player") && (anim.GetBool ("isAttack") == true)) {
		//SkeletonCtrl enemyCtrl = GameObject.Find ("enemy").GetComponent ("SkeletonCtrl");
		//		Debug.Log ("[Skeleton] Hit [{0}]" + col.gameObject.tag);
		//	}
	}
	void OnTriggerStay(Collider col)
	{
		Debug.Log ("[Troops] OnTriggerStay [{0}]" + col.gameObject.tag);
		if (null == target) {
			UnitCtrl u = (UnitCtrl)col.gameObject.gameObject.GetComponent ("UnitCtrl");
			if (u)
			{
				Debug.Log ("[Troops] Team flag " + u.team_flag + " " + team_flag);
				if (u.team_flag != team_flag) {
					SetTargetObject (col.gameObject);
				}
			}			
		}

		//	if (col.CompareTag ("Player") && (anim.GetBool ("isAttack") == true)) {
		//SkeletonCtrl enemyCtrl = GameObject.Find ("enemy").GetComponent ("SkeletonCtrl");
		//		Debug.Log ("[Skeleton] Hit [{0}]" + col.gameObject.tag);
		//	}
	}
}
