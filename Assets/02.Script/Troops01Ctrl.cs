using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Troops01Ctrl : UnitCtrl {

	int CheckoutTarget(RaycastHit hit)
	{
		UnitCtrl u = (UnitCtrl)hit.transform.gameObject.GetComponent ("UnitCtrl");
		Debug.Log ("[Troops] Team flag " + u.team_flag + " " + team_flag);
		if (u.team_flag != team_flag) {
			target = hit.transform.gameObject;
			m_targetlist.Add (target);
			return 1;
		}
		return 0;
	}

	// Update is called once per frame
	void Update () {
		if (-1 == status)
			return;

		if (null == target) // look for target.
		{			
			com_animator.SetBool ("isWalking", false);
			if (search_lv > 0) {				
				RaycastHit hit;
				Vector3 pos = transform.position;
				Vector3 v = transform.forward * search_lv * 10.0f;
				v.x += fp_search_pre_deg;
				fp_search_pre_deg *= -1;
				if (fp_search_pre_deg > 0) 
					fp_search_pre_deg += 0.2f;
				if (fp_search_pre_deg > max_deg)
					fp_search_pre_deg = 0.1f;

				if (Physics.Raycast (pos, v, out hit, search_lv * 10.0f)) {
					CheckoutTarget (hit);					
				} else {
					Debug.DrawRay (pos, v);
				}
			}
		}
		else 
		{
			UnitCtrl obj = (UnitCtrl)target.GetComponent ("UnitCtrl");
			if (obj.isDead ()) {
				target = null;
				return;
			}
			Vector3 direction = target.transform.position - this.transform.position;
			float angle = Vector3.Angle (direction, this.transform.forward);
			float distance = Vector3.Distance (target.transform.position, this.transform.position);
			//if (distance < 10 && angle < 50) {
				com_animator.SetBool ("isWalking", true);
				this.transform.rotation = Quaternion.Slerp (this.transform.rotation, Quaternion.LookRotation (direction), 0.1f);
				if (direction.magnitude > this.transform.localScale.y) {
					//direction.y = 0;
					//this.transform.Translate (0f, 0f, speed * Time.deltaTime);
					com_naviMeshAgent.SetDestination (target.transform.position);
				} else {
					com_naviMeshAgent.enabled = false;
					com_animator.SetTrigger ("isAttack");
					com_animator.SetBool ("isWalking", false);
				}
			//} else {
			//	anim.SetBool ("isWalking", false);
			//	Debug.LogFormat ("target dis {0} angle {1}", distance, angle);
			//}
		}

	}

	void OnTriggerEnter(Collider col)
	{
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
		if (null == target) {
			UnitCtrl u = (UnitCtrl)col.gameObject.gameObject.GetComponent ("UnitCtrl");
			if (u)
			{
				Debug.Log ("[Troops] Team flag " + u.team_flag + " " + team_flag);
				if (u.team_flag != team_flag) {
					target = col.gameObject;
					m_targetlist.Add (target);
				}
			}			
		}

		//	if (col.CompareTag ("Player") && (anim.GetBool ("isAttack") == true)) {
		//SkeletonCtrl enemyCtrl = GameObject.Find ("enemy").GetComponent ("SkeletonCtrl");
		//		Debug.Log ("[Skeleton] Hit [{0}]" + col.gameObject.tag);
		//	}
	}
}
