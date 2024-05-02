using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ObjectTransitionConfig : AbstractTransitionConfig
{
    public override GameObject CopyToAR(GameObject target, System.Action callback)
    {
        // Create a copy of the object in AR space
        var arObj = Instantiate(
            target,
            SystemConfig.Instance.ARCamera.transform.position + SystemConfig.Instance.ARCamera.transform.forward * 1.5f,
            SystemConfig.Instance.ARCamera.transform.rotation,
            SystemConfig.Instance.ARCamera.transform
        );
        // Set the object's parent to the AR camera
        arObj.transform.SetParent(SystemConfig.Instance.ARCamera.transform);
        //arObj.layer = LayerMask.NameToLayer("AR");
        // Set the object's scale to match the AR camera's scale
        arObj.transform.localScale = Vector3.one * SystemConfig.Instance.ARCamera.transform.localScale.x;

        // Disable gravity
        arObj.GetComponent<Rigidbody>().useGravity = false;
        arObj.GetComponent<Collider>().enabled = true;
        arObj.GetComponent<Renderer>().enabled = true;

        callback?.Invoke();

        return arObj;
    }

    public override GameObject CopyToDesktop(GameObject target, System.Action callback)
    {
        // Create a copy of the object in Desktop space
        var desktopObject = Instantiate(target);
        // Set the object's parent and layer
        desktopObject.transform.SetParent(SystemConfig.Instance.DesktopCamera.transform);
        //desktopObject.layer = LayerMask.NameToLayer("Desktop");
        //Set the object's scale to match the Desktop camera's scale
        desktopObject.transform.localScale = Vector3.one * SystemConfig.Instance.DesktopCamera.transform.localScale.x;
        desktopObject.transform.SetPositionAndRotation(GetDesktopPosition(target), SystemConfig.Instance.DesktopCamera.transform.rotation);
        // Enable gravity
        desktopObject.GetComponent<Rigidbody>().useGravity = true;
        desktopObject.GetComponent<Collider>().enabled = true;
        desktopObject.GetComponent<Renderer>().enabled = true;
        // Subscribe new view to the controller class.
        var desktopCubeView = desktopObject.GetComponent<CubeView>();
        target.GetComponent<CubeView>().Controller.Subscribe(desktopCubeView);

        // Disable gravity
        target.GetComponent<Rigidbody>().useGravity = false;
        target.GetComponent<Collider>().enabled = true;
        target.GetComponent<Renderer>().enabled = true;

        target.SetActive(true);

        //callback?.Invoke();

        return desktopObject;
    }

    public override GameObject MoveToAR(GameObject target, System.Action callback)
    {
        var arObject = CopyToAR(target, callback);
        target.SetActive(false);
        callback?.Invoke();
        return arObject;
    }

    public override GameObject MoveToDesktop(GameObject target, System.Action callback)
    {
        var desktopObject = CopyToDesktop(target, callback);
        target.SetActive(false);
        callback?.Invoke();
        return desktopObject;
    }

    private Vector3 GetDesktopPosition(GameObject originalARGameObject)
    {
        var arCameraPosition = SystemConfig.Instance.ARCamera.transform.position;
        var desktopCameraPosition = SystemConfig.Instance.DesktopCamera.transform.position;
        var xOffset = originalARGameObject.transform.localPosition.x - arCameraPosition.x;
        var zOffset = originalARGameObject.transform.localPosition.z - arCameraPosition.z;

        var position = new Vector3(
            desktopCameraPosition.x + xOffset,
            0.5f,
            desktopCameraPosition.z + zOffset
        );
        return position;
    }
}
