using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DragDropSprite : MonoBehaviour
{
    [HideInInspector]
    public SpriteRenderer referenceSpriteRenderer; // Reference to the reference sprite's SpriteRenderer component
    private bool isDragging = false;
    public bool isSound = false;
    private Vector3 offset;
    public static DragDropSprite ins;
    public GameObject finger;
    private void Awake()
    {
        ins = this;
    }
    void OnMouseDown()
    {
        isDragging = true;
        finger.GetComponent<Image>().enabled=false;
        // Calculate the offset between the mouse position and the object's position
        offset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if(isSound)
        {
            SoundManager.instance.PlayEffect_Instance(6);
            SoundManager.instance.PlayEffect_Loop(20);// Play a sound effect when the object is clicked
        }
    }

    void OnMouseUp()
    {
        isDragging = false;
        SoundManager.instance.StopEffect(20);
        finger.GetComponent<Image>().enabled = true;
    }

    void Update()
    {
        if (isDragging && referenceSpriteRenderer != null)
        {
            // Get the current mouse position
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // Get the sprite boundaries in world space
            float minX = referenceSpriteRenderer.bounds.min.x;
            float maxX = referenceSpriteRenderer.bounds.max.x;
            float minY = referenceSpriteRenderer.bounds.min.y;
            float maxY = referenceSpriteRenderer.bounds.max.y;

            // Clamp the object's position within the sprite boundaries
            float clampedX = Mathf.Clamp(mousePos.x + offset.x, minX, maxX);
            float clampedY = Mathf.Clamp(mousePos.y + offset.y, minY, maxY);

            // Update the position of the object while considering the offset and clamping within bounds
            transform.position = new Vector3(clampedX, clampedY, transform.position.z);
        }
    }
}