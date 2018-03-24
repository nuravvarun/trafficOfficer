using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraControl : MonoBehaviour {

	public float perspectiveZoomSpeed = 0.5f;        // The rate of change of the field of view in perspective mode.
    public float orthoZoomSpeed = 0.5f;        // The rate of change of the orthographic size in orthographic mode.

	public GameObject mainTerrain;
    private Vector2 worldStartPoint;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update()
    {
            // only work with one touch
    if (Input.touchCount == 1) {
        Touch currentTouch = Input.GetTouch(0);

        if (currentTouch.phase == TouchPhase.Began) {
            this.worldStartPoint = this.getWorldPoint(currentTouch.position);
        }

        if (currentTouch.phase == TouchPhase.Moved) {
            Vector2 worldDelta = this.getWorldPoint(currentTouch.position) - this.worldStartPoint;

            Camera.main.transform.Translate(
                -worldDelta.x,
                -worldDelta.y,
                0
            );
        }
    } else if (Input.touchCount == 2)
        {
            // Store both touches.
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            // Find the position in the previous frame of each touch.
            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            // Find the magnitude of the vector (the distance) between the touches in each frame.
            float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

            // Find the difference in the distances between each frame.
            float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

            // If the camera is orthographic...
            if (Camera.current.orthographic)
            {
                // ... change the orthographic size based on the change in distance between the touches.
                Camera.current.orthographicSize += deltaMagnitudeDiff * orthoZoomSpeed;

                // Make sure the orthographic size never drops below zero.
                Camera.current.orthographicSize = Mathf.Max(Camera.current.orthographicSize, 0.1f);
            }
            else
            {
                // Otherwise change the field of view based on the change in distance between the touches.
                Camera.current.fieldOfView += deltaMagnitudeDiff * perspectiveZoomSpeed;

                // Clamp the field of view to make sure it's between 0 and 180.
                Camera.current.fieldOfView = Mathf.Clamp(Camera.current.fieldOfView, 10.0f, 21.0f);
            }
        }
    }


    // convert screen point to world point
    private Vector2 getWorldPoint (Vector2 screenPoint) {
        RaycastHit hit;
        Physics.Raycast(Camera.main.ScreenPointToRay(screenPoint), out hit);
        return hit.point;
    }
	public void rotateTerrain(float sliderValue) {
		//this function rotates terrain on Y axis via GUI element

		// sliderValue is updated in Inspector Window of 'Slider'
		mainTerrain.transform.rotation = Quaternion.Euler(0, sliderValue * 360, 0);
	}
}
