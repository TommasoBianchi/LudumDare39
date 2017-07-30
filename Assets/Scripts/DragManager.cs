using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DragManager : MonoBehaviour {

    public LayerMask dragDestinationLayers;
    public LayerMask draggableLayers;

	void Start () 
    {
		
	}
	
	void Update () 
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hitInfo;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo, draggableLayers))
            {
                hitInfo.transform.GetComponent<DraggableElement>().StartDrag(hitInfo.point);
            }
        }
	}

    public IDragDestination GetDragDestinationUnderMouse()
    {
        // Cast to physical objects
        RaycastHit hitInfo;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo, dragDestinationLayers))
        {
            IDragDestination destination = hitInfo.transform.GetComponent<IDragDestination>();
            if (destination != null)
                return destination;
        }

        // Cast to UI objects
        UnityEngine.EventSystems.PointerEventData p = new UnityEngine.EventSystems.PointerEventData(UnityEngine.EventSystems.EventSystem.current);
        p.position = Input.mousePosition;
        List<UnityEngine.EventSystems.RaycastResult> res = new List<UnityEngine.EventSystems.RaycastResult>();
        UnityEngine.EventSystems.EventSystem.current.RaycastAll(p, res);

        if (res.Count > 0)
        {
            IDragDestination destination = res[0].gameObject.GetComponent<IDragDestination>();
            if (destination != null)
                return destination;
        }

        return null;
    }
}
