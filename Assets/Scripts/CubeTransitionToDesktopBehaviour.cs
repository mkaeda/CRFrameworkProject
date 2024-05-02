using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeTransitionToDesktopBehaviour : MonoBehaviour
{
    [SerializeField]
    private AbstractTransitionConfig _transitionManager;

    public void OnMoveToDesktopClick()
    {
        Debug.Log("Moving object to Desktop...");
        _transitionManager.MoveToDesktop(ObjectSelectionHandler.Instance.GetSelectedObject(), null);
    }

    public void OnCopyToDesktopClick()
    {
        Debug.Log("Copying object to Desktop...");
        _transitionManager.CopyToDesktop(ObjectSelectionHandler.Instance.GetSelectedObject(), null);
    }
}
