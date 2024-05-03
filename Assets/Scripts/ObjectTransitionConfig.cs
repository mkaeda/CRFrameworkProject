using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ObjectTransitionConfig : AbstractTransitionConfig
{
    public override GameObject CopyToAR(GameObject target, System.Action callback)
    {
        // Create a copy of the object in AR space
        var arObject = Instantiate(
            target,
            SystemConfig.Instance.ARCamera.transform.position + SystemConfig.Instance.ARCamera.transform.forward * 1.5f,
            SystemConfig.Instance.ARCamera.transform.rotation,
            SystemConfig.Instance.ARCamera.transform
        );
        // Set the object's parent to the AR camera
        //arObj.transform.SetParent(SystemConfig.Instance.ARCamera.transform);
        // Set the object's scale to match the AR camera's scale
        arObject.transform.localScale                  = Vector3.one * SystemConfig.Instance.ARCamera.transform.localScale.x;

        // Disable gravity in AR so cube hovers in midair
        arObject.GetComponent<Rigidbody>().useGravity  = false;
        // Ensure colliders and rigidbody are activated
        arObject.GetComponent<Collider>().enabled      = true;
        arObject.GetComponent<Renderer>().enabled      = true;

        var arCubeView  = arObject.GetComponent<CubeView>();
        var controller  = target.GetComponent<CubeView>().Controller;
        controller.Subscribe(arCubeView);
        arCubeView.SetController(controller);

        callback?.Invoke();

        return arObject;
    }

    public override GameObject CopyToDesktop(GameObject target, System.Action callback)
    {
        // Create a copy of the object in Desktop space
        var desktopObject = Instantiate(
            target,
            SystemConfig.Instance.DesktopCamera.transform.position + SystemConfig.Instance.DesktopCamera.transform.forward * 1.5f,
            SystemConfig.Instance.DesktopCamera.transform.rotation,
            SystemConfig.Instance.DesktopCamera.transform
        );
        // Set the object's parent and layer
        //desktopObject.transform.SetParent(SystemConfig.Instance.DesktopCamera.transform);
        // Set the object's scale to match the Desktop camera's scale
        desktopObject.transform.localScale = Vector3.one * SystemConfig.Instance.DesktopCamera.transform.localScale.x;

        // Enable gravity on Desktop to cube falls and remain on top of plane
        desktopObject.GetComponent<Rigidbody>().useGravity  = true;
        // Ensure colliders and rigidbody are activated
        desktopObject.GetComponent<Collider>().enabled      = true;
        desktopObject.GetComponent<Renderer>().enabled      = true;

        // Subscribe new view to the controller class.
        var desktopCubeView = desktopObject.GetComponent<CubeView>();
        var controller = target.GetComponent<CubeView>().Controller;
        controller.Subscribe(desktopCubeView);
        desktopCubeView.SetController(controller);

        callback?.Invoke();

        return desktopObject;
    }

    public override GameObject MoveToAR(GameObject target, System.Action callback)
    {
        var arObject = CopyToAR(target, callback);
        target.SetActive(false);
        return arObject;
    }

    public override GameObject MoveToDesktop(GameObject target, System.Action callback)
    {
        var desktopObject = CopyToDesktop(target, callback);
        target.SetActive(false);
        return desktopObject;
    }
}
