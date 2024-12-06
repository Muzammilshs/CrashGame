using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using DG.Tweening;

public class SmoothCanvasPath : MonoBehaviour
{
    public RectTransform player; // The RectTransform of the player
    public RectTransform canvas; // The Canvas RectTransform
    public GameObject lineSegmentPrefab; // Prefab for the line segment (a thin UI Image)
    public float segmentSpacing = 5f; // Minimum distance to add a new segment
    public float pathSmoothness = 0.5f; // Smoothness of DOTween animations

    private List<RectTransform> pathSegments = new List<RectTransform>();
    private Vector2 lastPosition;

    void Start()
    {
        if (player == null || canvas == null || lineSegmentPrefab == null)
        {
            Debug.LogError("Please assign all references in the Inspector!");
            return;
        }

        // Initialize lastPosition using the player's current anchored position
        lastPosition = ConvertToCanvasSpace(player.anchoredPosition);
    }

    void Update()
    {
        // Convert the player's anchored position to Canvas space
        Vector2 currentPosition = ConvertToCanvasSpace(player.anchoredPosition);

        // Add a new segment if the player has moved significantly
        if (Vector2.Distance(lastPosition, currentPosition) > segmentSpacing)
        {
            CreateSegment(lastPosition, currentPosition);
            lastPosition = currentPosition;
        }
    }

    void CreateSegment(Vector2 start, Vector2 end)
    {
        // Instantiate a new segment
        GameObject segmentObject = Instantiate(lineSegmentPrefab, canvas);
        RectTransform segmentRect = segmentObject.GetComponent<RectTransform>();

        // Calculate the position, size, and rotation
        Vector2 direction = (end - start).normalized;
        float distance = Vector2.Distance(start, end);

        segmentRect.sizeDelta = new Vector2(distance, 2f); // Adjust height for thickness
        segmentRect.anchoredPosition = start + direction * distance * 0.5f;
        segmentRect.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);

        // Smoothly animate the segment's width using DOTween
        segmentRect.localScale = new Vector3(0, 1, 1); // Start with zero width
        segmentRect.DOScaleX(1, pathSmoothness);

        // Store the segment
        pathSegments.Add(segmentRect);
    }

    Vector2 ConvertToCanvasSpace(Vector2 playerAnchoredPosition)
    {
        // Convert player's local anchored position to Canvas space
        Vector2 worldPosition = player.TransformPoint(playerAnchoredPosition);
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas, worldPosition, null, out localPoint);
        return localPoint;
    }
}
