using UnityEngine;
using System.Collections;

public class Shooting_Handler : MonoBehaviour 
{
//	public bool shoot;
	public ParticleSystem bullet;
	AudioSource _audioSource;

//	public Animator anim;

//	float side;

	void Awake ()
	{
		_audioSource = GetComponent<AudioSource>();
	}

	void Update ()
	{
		if (Input.GetMouseButton(0))
		{
			bullet.Emit(1);
			if (_audioSource != null )
			{
				_audioSource.Play();
			}
		}
		else
		{
			if (_audioSource != null && _audioSource.isPlaying)
			{
				_audioSource.Stop();
			}
		}
//
//		bool sideways = Input.GetMouseButton(1);
//
//		side = Mathf.MoveTowards(side, sideways ? 1 : 0, Time.deltaTime);
//		anim.SetFloat("Sideways", side);
	}
}
