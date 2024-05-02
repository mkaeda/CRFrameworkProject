using System.Xml.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class SpawnCubeBehaviour : MonoBehaviour
{
    [SerializeField]
    private GameObject _plane;

    [SerializeField]
    private Vector2 _spawnRangeMultiplier;

    [SerializeField]
    private GameObject _controllerPrefab;

    [SerializeField]
    private GameObject _cubePrefab;

    private Bounds _planeBounds;
    private int _numCubes;

    private void Start()
    {
        // Find the bounds of the plane. If no plane, just use infinity.
        var collider = _plane.GetComponent<Collider>();
        _planeBounds = 
            collider != null 
            ? collider.bounds 
            : new Bounds(Vector3.negativeInfinity, Vector3.positiveInfinity);
        _numCubes = 0;
        _spawnRangeMultiplier = new Vector2(0.5f, 0.5f);

    }

    public void OnClick()
    {
        var model = ScriptableObject.CreateInstance<Cube>();
        model.Name = "Cube" + (_numCubes + 1).ToString();
        model.Position = GetRandomSpawnPosition();

        // Instantiate the cube prefab using the new Cube instance as the model
        var cubeInstance = Instantiate(_cubePrefab, model.Position, Quaternion.identity);
        var cubeController = Instantiate(_controllerPrefab);

        cubeInstance.transform.SetParent(cubeController.transform);

        var controller = cubeController.GetComponent<CubeController>();
        controller.SetModel(model);

        var cubeView = cubeInstance.GetComponent<CubeView>();
        cubeView.SetController(controller);

        controller.Subscribe(cubeView);

        cubeInstance.name = model.Name;

        cubeController.transform.SetParent(SystemConfig.Instance.DesktopCamera.transform);

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