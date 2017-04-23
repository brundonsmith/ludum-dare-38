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
    if(this.camera != null) {
      this.GetComponent<Transform>().up = this.camera.GetComponent<Transform>().up;
      this.GetComponent<Transform>().LookAt(this.camera.GetComponent<Transform>(), this.GetComponent<Transform>().up);
    }
 }
}
