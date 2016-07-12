using UnityEngine;
using System.Collections;

public class MissileParticleLogic : PointLogic {

	// Use this for initialization
	void Start () {
        StartCoroutine("Die");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
