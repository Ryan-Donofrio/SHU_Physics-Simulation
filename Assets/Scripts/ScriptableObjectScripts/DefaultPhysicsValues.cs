using UnityEngine;

[CreateAssetMenu(fileName = "PhysicsData", menuName = "ScriptableObjects/SavedPhysicsValues", order = 1)]
public class DefaultPhysicsValues : ScriptableObject
{
    [SerializeField] private float boulderMass = 1f;
    [SerializeField] private float gravity = -9.8f;
    [SerializeField] private float angleOfLaunch = 45f;
    [SerializeField] private float launchForce = 10f;
    [SerializeField] private float launchHeight = 10f;

    /// <summary>
    /// Returns default values: 0 = boulder mass, 1 = gravity, 2 = angleOfLaunch, 3 = launchForce, 4 = launchHeight
    /// </summary>
    /// <returns></returns>
    public float[] GetAllValues()
    {
        float[] values = new float[5];
        values[0] = boulderMass;
        values[1] = gravity;
        values[2] = angleOfLaunch;
        values[3] = launchForce;
        values[4] = launchHeight;

        return values;
    }

    public float GetBoulderMass
    {
        get { return boulderMass; }
    }

    public float GetGravity
    {
        get { return gravity; }
    }

    public float GetAngleOfLaunch
    {
        get { return angleOfLaunch; }
    }

    public float GetLaunchForce
    {
        get { return launchForce; }
    }

    public float GetLaunchHeight
    {
        get { return launchHeight; }
    }
}
