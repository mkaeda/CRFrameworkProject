using System.Xml.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class SpawnCubeARBehaviour : MonoBehaviour
{
    [SerializeField]
    private GameObject _controllerPrefab;

    [SerializeField]
    private GameObject _cubePrefab;

    private int _numCubes;

    private void Start()
    {
        // Find the bounds of the plane. If no plane, just use infinity.
        _numCubes = 0;
    }

    public void OnClick()
    {
        var model = ScriptableObject.CreateInstance<Cube>();
        model.Name = "CubeAR" + (_numCubes + 1).ToString();
        model.Position = GetSpawnPosition();

        // Instantiate the cube prefab using the new Cube instance as the model
        var cubeInstance = Instantiate(_cubePrefab, model.Position, Quaternion.identity);
        var cubeController = Instantiate(_controllerPrefab);

        cubeInstance.transform.SetParent(cubeController.transform);

        var controller = cubeController.GetComponent<CubeController>();
        controller.SetModel(model);

        var cubeView = cubeInstance.GetComponent<CubeView>();
        cubeView.SetController(controller);
        // Set the object's position and rotation to match the AR camera's position and rotation
        cubeView.transform.SetPositionAndRotation(
            SystemConfig.Instance.ARCamera.transform.position + SystemConfig.Instance.ARCamera.transform.forward * 1.5f,
            SystemConfig.Instance.ARCamera.transform.rotation
        );
        // Do not use gravity in AR
        cubeView.GetComponent<Rigidbody>().useGravity = false;

        controller.Subscribe(cubeView);
        cubeInstance.name = model.Name;
        cubeController.transform.SetParent(SystemConfig.Instance.ARCamera.transform);
        _numCubes++;
    }

    private Vector3 GetSpawnPosition()
    {
        return SystemConfig.Instance.ARCamera.transform.position + SystemConfig.Instance.ARCamera.transform.forward * 1.5f;
    }
}