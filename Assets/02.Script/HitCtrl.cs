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
		target.HitByEnemy (0);
	}
}
