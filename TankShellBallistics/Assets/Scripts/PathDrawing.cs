using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class PathDrawing : MonoBehaviour
{
    private LineRenderer _line;
    public float timeStep;

    void Start()
    {
        _line = gameObject.GetComponent<LineRenderer>();
        _line.enabled = false;
    }

    public void DisablePath()
    {
        if (_line.enabled)
        {
            _line.enabled = false;
        }
    }

    public void DrawPath(Vector3 velocity, Vector3 startPos, float time, float gravity)
    {
        if (!_line.enabled)
            _line.enabled = true;
        CreatePoints(velocity, startPos, gravity, time);
    }
    
    

    void CreatePoints(Vector3 newVel, Vector3 startPosition, float gravity, float pathTime)
    {
        Vector3 position = startPosition;
        float elapsedTime = 0.0f;
        int index = 0;
        while (elapsedTime <= pathTime)
        {
            _line.positionCount = index + 1;
            position += new Vector3(newVel.x * timeStep,
                +(newVel.y - (gravity * elapsedTime)) * timeStep, +newVel.z * timeStep);
            _line.SetPosition(index, position);
            index++;
            elapsedTime += timeStep;
        }
    }
}