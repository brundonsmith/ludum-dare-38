using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteTimer : MonoBehaviour {

  public float lifetime;

  private float creationTime;

	// Use this for initialization
	void Start () {
    this.creationTime = Time.time;
	}

	// Update is called once per frame
	void Update () {
    if(Time.time > this.creationTime + this.lifetime) {
      GameObject.Destroy(this.gameObject);
    }
	}
}
