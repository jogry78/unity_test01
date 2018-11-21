using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class UnitCtrl : MonoBehaviour {
	protected Animator com_animator;
	protected NavMeshAgent com_naviMeshAgent;
	protected SphereCollider com_colliderSphere;
	public Rigidbody com_rbody;

	protected GameObject target;
	public GameObject GameManager;
	public List<GameObject> m_targetlist;

	public int team_flag = 0;
	public float speed = 1f;
	public int hp = 2;
	public int status = 0;

	public bool m_isAttActive = false;
	//public int search_lv = 0;
	//public int max_deg = 10;
	//protected float fp_search_pre_deg = 0.1f;

	public GameObject weapon_01;
	private SphereCollider col_weapon_01;

	// Use this for initialization
	void Start () {
		InitComponent ();
	}

	public virtual void InitComponent()
	{
		com_animator = GetComponent<Animator>();
		com_naviMeshAgent = GetComponent<NavMeshAgent> ();
		com_colliderSphere = GetComponent<SphereCollider> ();
		com_rbody = GetComponent<Rigidbody> ();
		m_targetlist = new List<GameObject>();
		col_weapon_01 = weapon_01.GetComponent<SphereCollider> ();
		col_weapon_01.enabled = false;
	}

	protected void SetTargetObject(GameObject obj)
	{
		if (null == obj) {
			target = null;
			// remove from queue
		} else {
			target = obj;
			m_targetlist.Add (target);
		}
	}

	public int HitByEnemy(int nType, GameObject obj)
	{
		if (-1 == status)
			return 0;
		
		//////////////////////////////////////
		/// + Set new enemy target
		int r = (int)Random.Range(0f, 3f);
		if (null == target) {
			SetTargetObject (obj);
		}
		else if (obj != target) {
			if (0 != r) {
				SetTargetObject (obj);
			}
		}
		/// - 
		////////////////////////////////////// 

		//////////////////////////////////////
		/// + Hit
		if (0 == r) {
			return 0;
		} else {
			Vector3 v = obj.transform.forward;
			v.Normalize ();
			com_rbody.AddForce (v * 5f, ForceMode.Impulse);
			//Debug.LogFormat ("com_rbody.AddForce");
			if (--hp == 0) {
				com_animator.SetBool ("isWalking", false);
				com_animator.Play ("Death");
				status = -1;
				BoxCollider col = this.GetComponent<BoxCollider> ();
				col.enabled = false;
				com_naviMeshAgent.enabled = false;
				com_colliderSphere.enabled = false;
				com_rbody.constraints = RigidbodyConstraints.FreezePosition;
			} else {
				com_animator.SetTrigger ("hit");
			}
			return 1;
		}
		/// -
		////////////////////////////////////// 
	}

	public void AttActive(int nState)
	{
		if (1 == nState) {
			m_isAttActive = true;
			col_weapon_01.enabled = true;
		} else {
			m_isAttActive = false;
			col_weapon_01.enabled = false;
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