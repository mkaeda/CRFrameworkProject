using UnityEngine;

/// <summary>
/// Abstract base class for data models.
/// </summary>
public abstract class AbstractDataModel : ScriptableObject
{
    public event System.Action OnModelUpdated;

    /// <summary>
    /// Invokes model updated event. Override to add custom implementation.
    /// </summary>
    public virtual void OnUpdated()
    {
        OnModelUpdated?.Invoke();
    }
}
