using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeTransitionToARBehaviour : MonoBehaviour
{
    [SerializeField]
    private AbstractTransitionConfig _transitionManager;

    public void OnMoveToARClick()
    {
        Debug.Log("Moving object to AR...");
        _transitionManager.MoveToAR(ObjectSelectionHandler.Instance.GetSelectedObject(), null);
    }

    public void OnCopyToARClick()
    {
        Debug.Log("Copying object to AR...");
        var createdARObject = _transitionManager.CopyToAR(ObjectSelectionHandler.Instance.GetSelectedObject(), null);
        var cubeView = createdARObject.GetComponent<CubeView>();
        var controller = ObjectSelectionHandler.Instance.GetSelectedObject().GetComponent<CubeView>().Controller;
        controller.Subscribe(cubeView);
    }
}
