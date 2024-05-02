using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Abstract base class for controllers.
/// </summary>
/// <typeparam name="T">Type of data model.</typeparam>
public abstract class AbstractController<T> : MonoBehaviour where T : AbstractDataModel
{
    [SerializeField]
    protected T _model;

    [SerializeField]
    protected readonly List<AbstractView<T>> Views = new List<AbstractView<T>>();

    /// <summary>
    /// Gets a new instance of the model.
    /// </summary>
    public T Model { get { return Instantiate(_model); } }

    private bool hasStarted = false;

    private void Start()
    {
        foreach (var view in Views)
        {
            view.OnViewUpdated += OnViewUpdated;
            _model.OnModelUpdated += view.OnModelUpdated;
        }

        hasStarted = true;
    }

    private void OnDestroy()
    {
        foreach (var view in Views)
        {
            view.OnViewUpdated -= OnViewUpdated;
            _model.OnModelUpdated -= view.OnModelUpdated;
        }
    }

    public void Subscribe(AbstractView<T> view)
    {
        Views.Add(view);
        if (hasStarted)
        {
            // Start method already run, configure events.
            view.OnViewUpdated += OnViewUpdated;
            _model.OnModelUpdated += view.OnModelUpdated;
        }
    }

    public void Unsubscribe(AbstractView<T> view)
    {
        Views.Remove(view);
        if (hasStarted)
        {
            // Start method already run, configure events.
            view.OnViewUpdated -= OnViewUpdated;
            _model.OnModelUpdated -= view.OnModelUpdated;
        }
    }

    public void SetModel(T model)
    {
        _model = model;
    }

    /// <summary>
    /// Override this method to implement updating model and propagating changes.
    /// </summary>
    /// <param name="model">Updated model.</param>
    protected virtual void OnViewUpdated(T model)
    {
        _model.OnUpdated();
    }
}
