using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitCtrl : MonoBehaviour {

	public GameObject m_this;

	void OnTriggerEnter(Collider col)
	{
		if (m_this == col.gameObject || null == col.gameObject.tag)
			return;
		
		UnitCtrl unit = (UnitCtrl)m_this.GetComponent ("UnitCtrl");
		if (false == unit.m_isAttActive) {
			return;
		}
		UnitCtrl target = (UnitCtrl)col.gameObject.GetComponent ("UnitCtrl");
		if (target) {
			if (1 == target.HitByEnemy (0, m_this)) {
				//Vector3 v = m_this.transform.forward;
				//v.Normalize ();
				//target.com_rbody.AddForce (v * 5f, ForceMode.Impulse);
			}
		}
	}
}
