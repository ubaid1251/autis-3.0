using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicColliderUpdater : MonoBehaviour
{
    private SpriteRenderer referenceSpriteRenderer;

    private BoxCollider referenceCollider;
    private Sprite lastSprite;

    void Start()
    {
        referenceSpriteRenderer = transform.GetComponent<SpriteRenderer>();
        referenceCollider = GetComponent<BoxCollider>();
        lastSprite = referenceSpriteRenderer.sprite;
        UpdateCollider();
    }

    void UpdateCollider()
    {
        if (referenceSpriteRenderer != null && referenceCollider != null)
        {
            // Set the size of the Box Collider based on the sprite's bounds
            referenceCollider.size = new Vector3(referenceSpriteRenderer.sprite.bounds.size.x,
                                                referenceSpriteRenderer.sprite.bounds.size.y,
                                                referenceCollider.size.z);
        }
    }

    void Update()
    {
        referenceSpriteRenderer = transform.GetComponent<SpriteRenderer>();
        // Check for sprite changes and update collider accordingly
        if (referenceSpriteRenderer.sprite != lastSprite)
        {
            lastSprite = referenceSpriteRenderer.sprite;
            UpdateCollider();
        }
    }
}
