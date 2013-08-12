using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	// Use this for initialization
	void Start ()
	{
	    GestureController.OnDrag += DragCamera;
	}

    void DragCamera(Vector2 moveDelta)
    {
      transform.Translate(new Vector3(-moveDelta.x,-moveDelta.y,0)); 
    }

}
