using UnityEngine;

[RequireComponent (typeof (Animator))]
public class IK_Handler : MonoBehaviour 
{
	public bool enableIK;

	public float leftHandWeight = 1;
	public Transform leftHandTarget;

	public float rightHandWeight = 1;
	public Transform rightHandTarget;

//	public Transform weapon;
	public Transform lookObj;

	Animator _anim;

	void Awake ()
	{
		_anim = GetComponent<Animator>();
	}

	void OnAnimatorIK ()
	{
		if (enableIK == false)
		{          
			_anim.SetIKPositionWeight(AvatarIKGoal.LeftHand,0);
			_anim.SetIKRotationWeight(AvatarIKGoal.LeftHand,0); 
			_anim.SetIKPositionWeight(AvatarIKGoal.RightHand,0);
			_anim.SetIKRotationWeight(AvatarIKGoal.RightHand,0); 
			_anim.SetLookAtWeight(0);
			return;
		}

		_anim.SetIKPositionWeight(AvatarIKGoal.LeftHand, leftHandWeight);
		_anim.SetIKPosition(AvatarIKGoal.LeftHand, leftHandTarget.position);
		_anim.SetIKRotationWeight(AvatarIKGoal.LeftHand, leftHandWeight);
		_anim.SetIKRotation(AvatarIKGoal.LeftHand, leftHandTarget.rotation);

		_anim.SetIKPositionWeight(AvatarIKGoal.RightHand, rightHandWeight);
		_anim.SetIKPosition(AvatarIKGoal.RightHand, rightHandTarget.position);
		_anim.SetIKRotationWeight(AvatarIKGoal.RightHand, rightHandWeight);
		_anim.SetIKRotation(AvatarIKGoal.RightHand, rightHandTarget.rotation);

		if(lookObj != null) {
			_anim.SetLookAtWeight(1);
			_anim.SetLookAtPosition(lookObj.position);
		}    
	}
}
