using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour {

  public Camera camera;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
  void Update () {
    this.GetComponent<Transform>().forward = this.camera.GetComponent<Transform>().forward;
 }
}
