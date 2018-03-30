using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour {

    private AudioSource audioSource;
    public GameObject shot;
    public Transform shotSpawn;
    public float fireRate;
    public float delay;

	void Start ()
    {
        audioSource = GetComponent<AudioSource>();
        // calls Fire () after delay time at a rate of fireRate (use Random.Range for unpredictable rate)
        InvokeRepeating("Fire", delay, fireRate);
	}

    void Fire ()
    {
        Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
        audioSource.Play();
    }

}
