using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeTransitionToARBehaviour : MonoBehaviour
{
    [SerializeField]
    private AbstractTransitionConfig _transitionManager;

    public void OnMoveToARClick()
    {
        _transitionManager.MoveToAR(ObjectSelectionHandler.Instance.GetSelectedObject(), null);
        Debug.Log("Cube moved to AR.");
    }

    public void OnCopyToARClick()
    { 
        _transitionManager.CopyToAR(ObjectSelectionHandler.Instance.GetSelectedObject(), null);
        Debug.Log("Cube copied to AR.");
    }
}
