using UnityEngine;

public class ObjectSelectionHandler : MonoBehaviour
{
    private static ObjectSelectionHandler _instance;
    public static ObjectSelectionHandler Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<ObjectSelectionHandler>();
                if (_instance == null)
                {
                    var obj = new GameObject("ObjectSelectionHandler");
                    _instance = obj.AddComponent<ObjectSelectionHandler>();
                }
            }
            return _instance;
        }
    }

    private GameObject _selectedObject;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetSelectedObject(GameObject obj)
    {
        _selectedObject = obj;
    }

    public GameObject GetSelectedObject()
    {
        return _selectedObject;
    }
}