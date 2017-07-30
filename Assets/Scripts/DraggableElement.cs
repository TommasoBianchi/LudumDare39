using System;
using System.Collections.Generic;
using UnityEngine;

public class DraggableElement : MonoBehaviour {

    private Vector3 startPos;
    private Vector3 offset;
    private bool isDragging = false;
    private DragManager dragManager;

	void Start () 
    {
        startPos = transform.position;
        dragManager = FindObjectOfType<DragManager>();
	}
	
	void Update () 
    {
        if (isDragging)
        {
            if (!Input.GetMouseButton(0))
            {
                isDragging = false;
                IDragDestination destination = dragManager.GetDragDestinationUnderMouse();
                if (destination != null && destination.CanRelease(this))
                {
                    destination.Release(this);
                    Destroy(gameObject);
                }
                else
                {
                    transform.position = startPos;
                }
                return;
            }

            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition) + offset;
            pos.z = 0;
            transform.position = pos;
        }
	}

    public void StartDrag(Vector3 clickPosition)
    {
        offset = transform.position - clickPosition;
        isDragging = true;
    }
}
