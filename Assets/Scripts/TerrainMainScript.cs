using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.UIElements;

public class TerrainMainScript : MonoBehaviour {

	public Slider guiSliderObject;
	public GameObject Terrain;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		print(this.gameObject.transform.localScale);
		this.gameObject.transform.localScale = new Vector3(0, guiSliderObject.value,0);
	}

	public void RotateMyObject()
	{	
	}
}

