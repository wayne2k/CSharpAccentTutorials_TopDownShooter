using UnityEngine;

[RequireComponent (typeof (CapsuleCollider))]
[RequireComponent (typeof (Rigidbody))]
[RequireComponent (typeof (Animator))]
public class CharacterMovement : MonoBehaviour
{
	[SerializeField] float _speed = 4f;

	float _h = 0f;
	float _v = 0f;
	Vector3 _movement = Vector3.zero;
	Vector3 _lookPos = Vector3.zero;

	Transform _cam;
	Vector3 _camForward;
	Vector3 _move;
	Vector3 _moveInput;

	float _forwardAmount;
	float _turnAmount;

	Rigidbody _rb;
	Animator _anim;

	void Awake ()
	{
		_rb = GetComponent<Rigidbody>();
		_rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
		_rb.drag = 4f;
		_rb.angularDrag = 999f;

		SetupAnimator();

		_cam = Camera.main.transform;
	}

	void Update ()
	{
		_h = Input.GetAxis("Horizontal");
		_v = Input.GetAxis("Vertical");

		if (_cam != null)
		{
			_camForward = Vector3.Scale(_cam.parent.forward, new Vector3(1f, 0f, 1f)).normalized; 
			_move = _v * _camForward + _h * _cam.right;
		}
		else
		{
			_move = _v * Vector3.forward + _h * Vector3.right;
		}

		if (_move.magnitude > 1)
		{
			_move.Normalize();
		}

		Move(_move);


		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;

		if (Physics.Raycast(ray, out hit, 100f))
		{
			_lookPos = hit.point;
		}

		Vector3 lookDirection = _lookPos - transform.position;
		lookDirection.y = 0f;

		transform.LookAt(transform.position + lookDirection, Vector3.up);
	}

	void FixedUpdate ()
	{
		_movement.x = _h;
		_movement.y = 0f;
		_movement.z = _v;

		if (Mathf.Abs(_movement.x) > 0f || Mathf.Abs(_movement.z) > 0f)
		{
			_rb.AddForce(_movement * _speed / Time.deltaTime);
		}

	}

	void Move (Vector3 move)
	{
		if (_move.magnitude > 1)
		{
			_move.Normalize();
		}

		_moveInput = move;

		ConvertMoveInput();
		UpdateAnimator();
	}

	void ConvertMoveInput ()
	{
		Vector3 localMove = transform.InverseTransformDirection(_moveInput);

		_turnAmount = localMove.x;
		_forwardAmount = localMove.z;
	}

	void UpdateAnimator ()
	{
		_anim.SetFloat("Forward", _forwardAmount, 0.01f, Time.deltaTime);
		_anim.SetFloat("Turn", _turnAmount, 0.01f, Time.deltaTime);
	}

	void SetupAnimator ()
	{
		_anim = GetComponent<Animator>();

		foreach (var childAnimator in GetComponentsInChildren<Animator>()) {
			if (childAnimator != _anim)
			{
				_anim.avatar = childAnimator.avatar;
				Destroy(childAnimator);
				break;
			}
		}
	}
}
