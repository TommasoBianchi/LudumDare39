using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDragDestination {

    bool CanRelease(DraggableElement element);
    void Release(DraggableElement element);
}
