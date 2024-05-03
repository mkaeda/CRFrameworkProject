using System.Xml.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class SpawnCubeARBehaviour : MonoBehaviour
{
    [SerializeField]
    private GameObject  _controllerPrefab;

    [SerializeField]
    private GameObject  _cubePrefab;

    private int         _numCubes;

    private void Start()
    {
        // Find the bounds of the plane. If no plane, just use infinity.
        _numCubes = 0;
    }

    public void OnClick()
    {
        // Create new instance of Cube model 
        var model           = ScriptableObject.CreateInstance<Cube>();
        model.Name          = "CubeAR" + (_numCubes + 1).ToString();
        model.Position      = GetSpawnPosition();

        // Instantiate controller GameObject
        var cubeController  = Instantiate(_controllerPrefab, Vector3.zero, Quaternion.identity);

        // Instantiate Cube GameObject
        var cubeInstance    = Instantiate(_cubePrefab, model.Position, Quaternion.identity);
        cubeInstance.name   = model.Name;

        // Get CubeView script component of Cube GameObject
        var cubeView        = cubeInstance.GetComponent<CubeView>();

        // Initialize script variables for controller and view 
        // Set model and subscribe cube's view to the controller object
        var controller      = cubeController.GetComponent<CubeController>();
        controller.SetModel(model);
        controller.Subscribe(cubeView);
        cubeView.SetController(controller);

        // Turn off gravity in AR so cube is suspended in midair.
        cubeView.GetComponent<Rigidbody>().useGravity = false;

        // Set the parent of the cubeView to be the AR camera.
        // isKinematic flag turned on and off to prevent weird behaviours due to 
        // RigidBody component on the view
        cubeView.GetComponent<Rigidbody>().isKinematic = true;
        cubeView.transform.SetParent(SystemConfig.Instance.ARCamera.transform);
        cubeView.GetComponent<Rigidbody>().isKinematic = false;

        _numCubes++;
    }

    private Vector3 GetSpawnPosition()
    {
        return SystemConfig.Instance.ARCamera.transform.position + SystemConfig.Instance.ARCamera.transform.forward * 1.5f;
    }
}