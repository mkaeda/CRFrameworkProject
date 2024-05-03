using System.Xml.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class SpawnCubeBehaviour : MonoBehaviour
{
    [SerializeField]
    private GameObject  _plane;

    [SerializeField]
    private Vector2     _spawnRangeMultiplier;

    [SerializeField]
    private GameObject  _controllerPrefab;

    [SerializeField]
    private GameObject  _cubePrefab;

    private Bounds      _planeBounds;
    private int         _numCubes;

    private void Start()
    {
        // Find the bounds of the plane. If no plane, just use infinity.
        var collider            = _plane.GetComponent<Collider>();
        _planeBounds            = collider != null 
                                    ? collider.bounds 
                                    : new Bounds(Vector3.negativeInfinity, Vector3.positiveInfinity);
        _numCubes               = 0;
        _spawnRangeMultiplier   = new Vector2(0.5f, 0.5f);
    }

    public void OnClick()
    {
        // Create new instance of Cube model
        var model           = ScriptableObject.CreateInstance<Cube>();
        model.Name          = "Cube" + (_numCubes + 1).ToString();
        model.Position      = GetRandomSpawnPosition();

        // Instantiate controller GameObject
        var cubeController  = Instantiate(_controllerPrefab, Vector3.zero, Quaternion.identity);
        
        // Instantiate Cube GameObject
        var cubeInstance    = Instantiate(_cubePrefab, model.Position, Quaternion.identity);
        cubeInstance.name   = model.Name;

        // Get CubeView script component of Cube GameObject
        var cubeView = cubeInstance.GetComponent<CubeView>();

        // Initialize script variables for controller and view 
        // Set model and subscribe cube's view to the controller object
        var controller = cubeController.GetComponent<CubeController>();
        controller.SetModel(model);
        controller.Subscribe(cubeView);
        cubeView.SetController(controller);

        // Turn on gravity on desktop so cube falls to rest on plane.
        cubeView.GetComponent<Rigidbody>().useGravity = true;

        // Set the parent of the cubeView to be the AR camera.
        // isKinematic flag turned on and off to prevent weird behaviours due to 
        // RigidBody component on the view
        cubeView.GetComponent<Rigidbody>().isKinematic = true;
        cubeView.transform.SetParent(SystemConfig.Instance.DesktopCamera.transform);
        cubeView.GetComponent<Rigidbody>().isKinematic = false;

        _numCubes++;
    }

    private Vector3 GetRandomSpawnPosition()
    {
        // Generate a random position within the bounds of the plane
        var xPos = Random.Range(_planeBounds.min.x, _planeBounds.max.x) * _spawnRangeMultiplier.x;
        var yPos = 0.5f;
        var zPos = Random.Range(_planeBounds.min.z, _planeBounds.max.z) * _spawnRangeMultiplier.y;

        return new Vector3(xPos, yPos, zPos);
    }
}