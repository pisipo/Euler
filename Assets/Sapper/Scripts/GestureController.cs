using System;
using UnityEngine;
using System.Collections;

public class GestureController : MonoBehaviour
{
    public static event Action<CellClassic> OnCellShortTap;
    public static event Action<CellClassic> OnCellLongTap;
    public static event Action<CellClassic> OnCellDoubleTap;
    public static event Action<Vector2> OnDrag;

    private void Start()
    {
        GetComponent<TapGestureRecognizer>().OnTap += OnTap;
        GetComponent<LongPressGestureRecognizer>().OnLongPress += OnLongTap;
        GetComponent<DragGestureRecognizer>().OnDragMove += OnDraging;
    }

    private void OnTap(TapGestureRecognizer gesture)
    {
        var GO = PickObject(gesture.Position);
        Debug.Log("ShortTap"+GO.name);
        if (GO.tag == "classic_cell")
            if (OnCellShortTap != null)
                OnCellShortTap(GO.GetComponent<CellViewClassic>().Model);
    }

    private void OnLongTap(LongPressGestureRecognizer gesture)
    {
        var GO = PickObject(gesture.Position);
        Debug.Log("LongTap" + GO.name);
        if (GO.tag == "classic_cell")
            if (OnCellLongTap != null)
                OnCellLongTap(GO.GetComponent<CellViewClassic>().Model);
    }

    private void OnDraging(DragGestureRecognizer gesture)
    {
        OnDrag(gesture.MoveDelta);
    }

    #region Utils

    // Convert from screen-space coordinates to world-space coordinates on the Z = 0 plane
    public static Vector3 GetWorldPos(Vector2 screenPos)
    {
        Ray ray = Camera.main.ScreenPointToRay(screenPos);

        // we solve for intersection with z = 0 plane
        float t = -ray.origin.z/ray.direction.z;

        return ray.GetPoint(t);
    }

    // Return the GameObject at the given screen position, or null if no valid object was found
    public static GameObject PickObject(Vector2 screenPos)
    {
        Ray ray = Camera.main.ScreenPointToRay(screenPos);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
            return hit.collider.gameObject;

        return null;
    }

    #endregion
}