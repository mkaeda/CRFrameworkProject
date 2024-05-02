using UnityEngine;

/// <summary>
/// Abstract base class for views.
/// </summary>
/// <typeparam name="T">Type of data model.</typeparam>
public abstract class AbstractView<T> : MonoBehaviour where T : AbstractDataModel
{
    public event System.Action<T> OnViewUpdated;

    public AbstractController<T> Controller;

    protected T _currentState;

    private void Start()
    {
        _currentState = Instantiate(Controller.Model);
    }

    protected void InvokeViewUpdated(T state)
    {
        OnViewUpdated?.Invoke(state);
    }

    public void SetController(AbstractController<T> controller)
    {
        Controller = controller;
    }

    public abstract void OnModelUpdated();
}
