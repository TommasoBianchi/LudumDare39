using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class UIDragDestination : MonoBehaviour, IDragDestination {

    private Image image;

	void Start () 
    {
        image = GetComponent<Image>();
	}

    public bool CanRelease(DraggableElement element)
    {
        SpriteRenderer spriteRenderer = element.GetComponent<SpriteRenderer>();
        return spriteRenderer != null;
    }

    public void Release(DraggableElement element)
    {
        SpriteRenderer spriteRenderer = element.GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            image.sprite = spriteRenderer.sprite;
        }
    }
}
