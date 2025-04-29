using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drag : MonoBehaviour
{
    [SerializeField] private Transform targetPoint;
    [SerializeField] private float snapDistance = 0.5f;
    [SerializeField] private AudioSource pieceSnapSound; // Parça yerleþtirme sesi

    private Vector3 originalPosition;
    private bool isDragging = false;
    private bool isPlaced = false;
    private Camera mainCamera;

    public bool IsPlaced => isPlaced;
    public static bool IsGamePaused { get; set; }

    void Start()
    {
        originalPosition = transform.position;
        mainCamera = Camera.main;
        IsGamePaused = false;
    }

    void Update()
    {
        if (isPlaced || IsGamePaused) return;

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector3 touchPosition = mainCamera.ScreenToWorldPoint(touch.position);
            touchPosition.z = 0;

            if (touch.phase == TouchPhase.Began)
            {
                RaycastHit2D hit = Physics2D.Raycast(touchPosition, Vector2.zero);
                if (hit.collider != null && hit.collider.gameObject == gameObject)
                {
                    isDragging = true;
                }
            }
            else if (touch.phase == TouchPhase.Moved && isDragging)
            {
                transform.position = touchPosition;
            }
            else if (touch.phase == TouchPhase.Ended && isDragging)
            {
                isDragging = false;
                float distanceToTarget = Vector3.Distance(transform.position, targetPoint.position);
                if (distanceToTarget < snapDistance)
                {
                    transform.position = targetPoint.position;
                    isPlaced = true;
                    if (pieceSnapSound != null) pieceSnapSound.Play(); // Ses çal
                }
                else
                {
                    transform.position = originalPosition;
                }
            }
        }
    }
}
