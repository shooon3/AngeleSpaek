using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionSound : MonoBehaviour {

    public AudioClip[] SE;

    private AudioSource AS;

    private void Awake(){
        AS = GetComponent<AudioSource>();

        int index = Random.Range(0, SE.Length);
        AS.clip = SE[index];
        AS.Play();
        Destroy(this.gameObject, SE[index].length);
    }


}
