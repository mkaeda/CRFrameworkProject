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
        var target = ObjectSelectionHandler.Instance.GetSelectedObject();
        var createdARObject = _transitionManager.CopyToAR(target, null);
        var cubeView = createdARObject.GetComponent<CubeView>();
        var controller = target.GetComponent<CubeView>().Controller;
        controller.Subscribe(cubeView);
    }
}
