using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDropWithDepth : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool isDragging = false;
    private Vector3 offset;

    private SpriteRenderer spriteRenderer;
    public int minSortingOrder = 1;
    public int maxSortingOrder = 5;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Щелчок мыши или первое касание
        {
            Vector3 inputPosition = GetInputWorldPos();
            if (IsTouchingObject(inputPosition))
            {
                isDragging = true;
                rb.gravityScale = 0;
                offset = transform.position - inputPosition;
            }
        }

        if (Input.GetMouseButton(0)) // Удерживая мышь или касаясь
        {
            if (isDragging)
            {
                transform.position = GetInputWorldPos() + offset;
            }
        }

        if (Input.GetMouseButtonUp(0)) // Отпустите мышь или завершите касание
        {
            if (isDragging)
            {
                isDragging = false;
                rb.gravityScale = 1;
                AdjustDepth();
            }
        }
    }

    private Vector3 GetInputWorldPos()
    {
        Vector3 inputPosition;

        if (Input.touchCount > 0) // Если доступен сенсорный ввод
        {
            inputPosition = Input.GetTouch(0).position;
        }
        else // Otherwise, use mouse position
        {
            inputPosition = Input.mousePosition;
        }

        inputPosition.z = 0;
        return Camera.main.ScreenToWorldPoint(inputPosition);
    }

    private bool IsTouchingObject(Vector3 inputPosition)
    {
        return GetComponent<Collider2D>().OverlapPoint(inputPosition);
    }

    private void AdjustDepth()
    {
        float depth = transform.position.y; // Используйте положение Y для определения глубины
        int sortingOrder = Mathf.RoundToInt(Mathf.Lerp(minSortingOrder, maxSortingOrder, depth));
        spriteRenderer.sortingOrder = sortingOrder;
    }
}
