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
            Quaternion.identity,
            SystemConfig.Instance.ARCamera.transform
        );

        // Set the object's parent to the AR camera
        arObj.transform.SetParent(SystemConfig.Instance.ARCamera.transform);

        // Set the object's layer to the AR layer
        arObj.layer = LayerMask.NameToLayer("AR");

        // Set the object's scale to match the AR camera's scale
        arObj.transform.localScale = Vector3.one * SystemConfig.Instance.ARCamera.transform.localScale.x;

        // Set the object's position and rotation to match the AR camera's position and rotation
        arObj.transform.SetPositionAndRotation(SystemConfig.Instance.ARCamera.transform.position + SystemConfig.Instance.ARCamera.transform.forward * 1.5f, SystemConfig.Instance.ARCamera.transform.rotation);

        arObj.GetComponent<Rigidbody>().useGravity = false;
        
        // Enable the object's collider
        arObj.GetComponent<Collider>().enabled = true;

        // Enable the object's renderer
        arObj.GetComponent<Renderer>().enabled = true;

        callback?.Invoke();

        return arObj;
    }

    public override GameObject CopyToDesktop(GameObject target, System.Action callback)
    {
        callback?.Invoke();
        return null;
    }

    public override void MoveToAR(GameObject target, System.Action callback)
    {
        CopyToAR(target, callback);
        target.SetActive(false);
        callback?.Invoke();
    }

    public override void MoveToDesktop(GameObject target, System.Action callback)
    {
        callback?.Invoke();
    }
}
