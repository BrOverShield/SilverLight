using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateSound : MonoBehaviour {

	
	public AudioSource chatGarouSound;
	public AudioSource clocheSound;
	public AudioSource crounchSound;
	public AudioSource PaysanAttackSound;
	public AudioSource paysanCuriousSound1;
	public AudioSource paysanCuriousSound2;
	public AudioSource paysanCuriousSound3;
	public AudioSource deathSound;
	// Use this for initialization
	void Start ()
	{
        
	}
	
	// Update is called once per frame
	void Update ()
	{
	}

	public void PlayChatGarouSound()
	{
		chatGarouSound.Play(0);
	}

    public void PlayClocheSound()
	{
		clocheSound.Play(0);
	}

    public void PlayCrounchSound()
	{
		crounchSound.Play(0);
	}

    public void PlayPaysanAttackSound()
	{
		PaysanAttackSound.Play(0);
	}

    public void PlayPaysanCuriousSound1()
	{
		paysanCuriousSound1.Play(0);
	}

    public void PlayPaysanCuriousSound2()
	{
		paysanCuriousSound2.Play(0);
	}

    public void PlayPaysanCuriousSound3()
	{
		paysanCuriousSound3.Play(0);
	}

    public void PlayDeathSound()
	{
		deathSound.Play(0);
	}
}
