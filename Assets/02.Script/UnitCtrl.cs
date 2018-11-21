using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UnitCtrl : MonoBehaviour {
	protected Animator com_animator;
	protected NavMeshAgent com_naviMeshAgent;
	protected SphereCollider com_colliderSphere;

	protected GameObject target;
	public List<GameObject> m_targetlist;

	public int team_flag = 0;
	public float speed = 1f;
	public int hp = 2;
	public int status = 0;

	public bool m_isAttActive = false;
	public int search_lv = 0;
	public int max_deg = 10;
	protected float fp_search_pre_deg = 0.1f;

	public GameObject weapon_01;

	// Use this for initialization
	void Start () {
		InitComponent ();
	}

	public virtual void InitComponent()
	{
		com_animator = GetComponent<Animator>();
		com_naviMeshAgent = GetComponent<NavMeshAgent> ();
		com_colliderSphere = GetComponent<SphereCollider> ();
		m_targetlist = new List<GameObject>();
	}

	public void HitByEnemy(int nType)
	{
		if (-1 == status)
			return;

		if (--hp == 0) {
			com_animator.SetBool ("isWalking", false);
			com_animator.Play ("Death");
			status = -1;
			BoxCollider col = this.GetComponent<BoxCollider> ();
			col.enabled = false;
			com_naviMeshAgent.enabled = false;
			com_colliderSphere.enabled = false;
		} else {
			com_animator.SetTrigger ("hit");
		}
	}

	public void AttActive(int nState)
	{
		if (1 == nState) {
			//BoxCollider col = weapon_01.GetComponent<BoxCollider> ();
			m_isAttActive = true;
			//col.enabled = true;
		} else {
			//BoxCollider col = weapon_01.GetComponent<BoxCollider> ();
			m_isAttActive = false;
			//col.enabled = false;
		}
	}

	public void setPending(int pend)
	{
		if (0 == pend)
			com_animator.SetBool ("isPending", false);
		else
			com_animator.SetBool ("isPending", true);
	}
	public bool isDead()
	{
		if (-1 == status)
			return true;
		return false;
	}
}