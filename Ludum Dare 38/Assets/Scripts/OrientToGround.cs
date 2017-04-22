using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class OrientToGround : MonoBehaviour {

  void Update() {
    this.Orient();
  }

  public void Orient() {
    this.GetComponent<Transform>().rotation
      = Quaternion.FromToRotation(
          Vector3.up,
          this.GetComponent<Transform>().position - Vector3.zero
        );
  }
}
