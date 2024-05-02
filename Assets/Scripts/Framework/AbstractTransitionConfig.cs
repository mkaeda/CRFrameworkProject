using UnityEngine;

/// <summary>
/// Abstract base class for transition configurations. Defines method outlines for the four basic transition operations.
/// </summary>
public abstract class AbstractTransitionConfig : MonoBehaviour
{
    public abstract void MoveToAR(GameObject target, System.Action callback);
    public abstract void MoveToDesktop(GameObject target, System.Action callback);
    public abstract GameObject CopyToAR(GameObject target, System.Action callback);
    public abstract GameObject CopyToDesktop(GameObject target, System.Action callback);
}
