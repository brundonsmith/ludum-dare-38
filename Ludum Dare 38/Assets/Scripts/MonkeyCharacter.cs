using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonkeyCharacter : Character {

  public const int specialEnergyCost = 400;
  // Update is called once per frame
  void Update () {
    base.Update();

  }

  public override void Special()
  {
    Debug.Log("special go??");
    if (energy >= specialEnergyCost && this.specialStatus == SpecialStatus.NotUsingSpecial)
    {
      Debug.Log("inside if");
      energy -= specialEnergyCost;
      StartCoroutine("Charging");
    }
  }

  IEnumerator Charging()
  {
    Debug.Log("special go!");
    this.specialStatus = SpecialStatus.UsingSpecial;
    float originalSpeed = this.speed;
    float originalDecelSpeed = this.decelSpeed;
    this.speed = originalSpeed * 2F;
    this.decelSpeed = originalDecelSpeed * .2F;
    //reduce speed specificall bc of launch
    for (int i = 0; i < 60; i++)
    {
      yield return null;
    }
    this.speed = originalSpeed;
    this.decelSpeed = originalDecelSpeed;
    this.specialStatus = SpecialStatus.NotUsingSpecial;
  }
}
