using System.Collections.Generic;
using UnityEngine;
using static SystemConfig;

/// <summary>
/// Manages system configuration and environment settings.
/// </summary>
public class SystemConfig : MonoBehaviour
{
    public enum RealityEnvironment
    {
        Desktop, AR
    }

    private static SystemConfig _instance;

    public static SystemConfig Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<SystemConfig>();
                if (_instance == null)
                {
                    var obj = new GameObject("SystemConfig");
                    _instance = obj.AddComponent<SystemConfig>();
                }
            }
            return _instance;
        }
    }

    [SerializeField]
    private Camera _desktopCamera;

    [SerializeField]
    private Camera _arCamera;

    [SerializeField]
    private GameObject _desktopSceneContainer;

    [SerializeField]
    private GameObject _arSceneContainer;

    [SerializeField]
    private HashSet<RealityEnvironment> _supportedRealities;

    public Camera DesktopCamera { get { return _desktopCamera; } }
    public Camera ARCamera { get { return _arCamera; } }
    public GameObject DesktopSceneContainer { get { return _desktopSceneContainer; } }
    public GameObject ARSceneContainer { get { return _arSceneContainer; } }
    public HashSet<RealityEnvironment> SupportedRealities { get { return _supportedRealities; } }
}

/// <summary>
/// Extension methods for RealityEnvironment enum.
/// </summary>
static class RealityEnvironmentExtensions
{
    public static string ToString(this RealityEnvironment value)
    {
        return value switch
        {
            RealityEnvironment.Desktop => "Desktop",
            RealityEnvironment.AR => "AR",
            _ => "Unknown",
        };
    }

    public static Camera GetCamera(this RealityEnvironment value)
    {
        return value switch
        {
            RealityEnvironment.Desktop => SystemConfig.Instance.DesktopCamera,
            RealityEnvironment.AR => SystemConfig.Instance.ARCamera,
            _ => null,
        };
    }

    public static GameObject GetSceneContainer(this RealityEnvironment value)
    {
        return value switch
        {
            RealityEnvironment.Desktop => SystemConfig.Instance.DesktopSceneContainer,
            RealityEnvironment.AR => SystemConfig.Instance.ARSceneContainer,
            _ => null,
        };
    }
}
