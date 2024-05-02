using UnityEngine;
using UnityEngine.Events;

public class CubeView : AbstractView<Cube>
{
    private void Update()
    {
        if (_currentState.Position != transform.localPosition)
        {
            _currentState.Position = transform.localPosition;
            InvokeViewUpdated(_currentState);
        }

        var selectedObj = ObjectSelectionHandler.Instance.GetSelectedObject();
        var outline = GetComponent<Outline>();
        if (selectedObj && selectedObj.Equals(gameObject))
        {
            if (outline)
            {
                outline.enabled = true;             
            }
            else
            {
                outline = gameObject.AddComponent<Outline>();
                outline.OutlineColor = Color.cyan;
                outline.OutlineWidth = 5.0f;
            }
        }
        else
        {
            if (outline)
            {
                outline.enabled = false;
            }
        }
    }

    private Vector3 _mOffset;
    private float _mZCoord;

    public void OnMouseDown()
    {

        ObjectSelectionHandler.Instance.SetSelectedObject(gameObject);

        // Left click. Initialise dragging action.
        _mZCoord = SystemConfig.Instance.DesktopCamera.WorldToScreenPoint(transform.position).z;
        _mOffset = transform.position - GetMouseWorldPos();
    }

    private void OnMouseDrag()
    {
        transform.position = GetMouseWorldPos() + _mOffset;
    }

    private Vector3 GetMouseWorldPos()
    {
        var mousePoint = Input.mousePosition;
        mousePoint.z = _mZCoord;

        return SystemConfig.Instance.DesktopCamera.ScreenToWorldPoint(mousePoint);
    }

    public override void OnModelUpdated()
    {
        gameObject.transform.localPosition = Controller.Model.Position;
    }
}
