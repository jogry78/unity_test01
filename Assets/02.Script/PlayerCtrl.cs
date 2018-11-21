using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : UnitCtrl {
	
	/* for debug */
	public float m_dbg_Foward;
	public float m_dbg_Side;
	public float m_dbg_Rotate;

	private Vector2 m_mouseLook;
	private Vector2 m_smoothV;
	public float sensitivity = 5f;
	public float smoothing = 2f;

	// Use this for initialization
	void Start () {
		Cursor.lockState = CursorLockMode.Locked;
		InitComponent ();
	}

	public override void InitComponent() {
		base.InitComponent ();
	}

	// Update is called once per frame
	void Update () {
		if (-1 == status)
			return;

		float straffe = Input.GetAxis ("Horizontal") * speed;
		float translation = Input.GetAxis ("Vertical") * speed;
		straffe *= Time.deltaTime;
		translation *= Time.deltaTime;
		transform.Translate (straffe, 0f, translation);

		/* for Debug information */
		m_dbg_Foward = translation;
		m_dbg_Side = straffe;


		float mouseRotX = Input.GetAxisRaw("Mouse X");
		float mouseRotY = Input.GetAxisRaw("Mouse Y");
		var md = new Vector2 (mouseRotX, mouseRotY);
		md = Vector2.Scale(md, new Vector2(sensitivity * smoothing, sensitivity * smoothing));
		m_smoothV.x = Mathf.Lerp (m_smoothV.x, md.x, 1f / smoothing);
		m_smoothV.y = Mathf.Lerp (m_smoothV.y, md.y, 1f / smoothing);
		m_mouseLook += m_smoothV;
			
		/* for Debug information */
		m_dbg_Rotate = mouseRotX;

		if (translation == 0 && straffe == 0) {
			com_animator.SetBool ("isWalking", false);
		} else {
			com_animator.SetBool ("isWalking", true);
		}

		if (Input.GetKeyDown (KeyCode.Mouse0)) {
			if (!com_animator.GetBool ("isPending"))
				com_animator.SetTrigger ("isAttack");
		}

		//transform.localRotation = Quaternion.AngleAxis (-m_mouseLook.y, Vector3.right);
		transform.localRotation = Quaternion.AngleAxis (m_mouseLook.x, transform.up);
			//Quaternion.Slerp (transform.rotation, Quaternion.LookRotation (new Vector3(mouseRotX, mouseRotY, 0f)), 0.05f);

		if (Input.GetKeyDown(KeyCode.Escape)){
			Cursor.lockState = CursorLockMode.None;
		}
	}

}
