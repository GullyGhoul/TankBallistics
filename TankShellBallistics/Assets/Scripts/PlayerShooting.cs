using UnityEngine;
using UnityEngine.UI;

public class PlayerShooting : MonoBehaviour
{
    public Transform shootingPoint;
    public BulletPrefabBehaviour bulletPrefabBehaviour;
    public float startVelocity;
    public TurretRotation turretRotation;
    public PathDrawing pathDrawing;
    public ScopeColor scopeColor;
    public bool useHighTrajectory;
    public float gravity = 9.8f;
    private Camera playerCam => Camera.main;

    private Vector3 ShootingPointPosition()
    {
        return shootingPoint.position;
    }


    void Update()
    {
        Ray ray = playerCam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
        if (Physics.Raycast(ray, out var hit) && IsFireRange(ShootingPointPosition(), hit.point, startVelocity))
        {
            if (Input.GetMouseButtonDown(1))
                Shoot(hit.point);

            float alpha = CalculateAlpha(ShootingPointPosition(), hit.point, startVelocity);
            pathDrawing.DrawPath(CalculateVelocity(ShootingPointPosition(), hit.point, startVelocity),
                ShootingPointPosition(), CalculateTime(hit.point, alpha), gravity);
            scopeColor.SetGreen();
            turretRotation.SetRotation(hit.point, alpha);
        }
        else
        {
            pathDrawing.DisablePath();
            scopeColor.SetRed();
            turretRotation.ResetRotation();
        }
    }


    public bool IsFireRange(Vector3 startPosition, Vector3 endPosition, float startVel)
    {
        Vector3 direction = endPosition - startPosition;
        float targetDistance = new Vector3(direction.x, 0, direction.z).magnitude;
        float verticalDifference = Mathf.Abs(endPosition.y - startPosition.y);
        float A = gravity * targetDistance * targetDistance / (2 * startVel * startVel);
        float B = -targetDistance;
        float C = A - verticalDifference;
        float D = B * B - 4 * A * C;
        if (D < 0)
            return false;
        else
            return true;
    }

    private float CalculateAlpha(Vector3 startPosition, Vector3 endPosition, float startVel)
    {
        Vector3 direction = endPosition - startPosition;
        float targetDistance = new Vector3(direction.x, 0, direction.z).magnitude;
        float A = gravity * targetDistance * targetDistance / (2 * startVel * startVel);
        float B = -targetDistance;
        float C = A - Mathf.Abs(direction.y);
        float D = B * B - 4 * A * C;
        float x1 = (-B + Mathf.Sqrt(D)) / (2 * A);
        float x2 = (-B - Mathf.Sqrt(D)) / (2 * A);
        if (useHighTrajectory)
            return Mathf.Rad2Deg * Mathf.Atan(Mathf.Max(x1, x2));
        else
            return Mathf.Rad2Deg * Mathf.Atan(Mathf.Min(x1, x2));
    }

    private void Shoot(Vector3 endPosition)
    {
        float alpha = CalculateAlpha(ShootingPointPosition(), endPosition, startVelocity);
        Instantiate(bulletPrefabBehaviour).Init(ShootingPointPosition(),
            CalculateVelocity(ShootingPointPosition(), endPosition, startVelocity), CalculateTime(endPosition, alpha),
            gravity);
    }

    private float CalculateTime(Vector3 target, float fireAngle)
    {
        return Vector3.Distance(ShootingPointPosition(), target) /
               (startVelocity * Mathf.Cos(fireAngle * Mathf.Deg2Rad));
    }

    private Vector3 CalculateVelocity(Vector3 startPosition, Vector3 endPosition, float startVel)
    {
        Vector3 direction = endPosition - startPosition;
        //float targetDistance = new Vector3(direction.x, 0, direction.z).magnitude;
        float fireAngle = CalculateAlpha(startPosition, endPosition, startVel);
        float horizontalVel = startVel * Mathf.Cos(fireAngle * Mathf.Deg2Rad);
        Vector3 newVel = Vector3.zero;
        newVel.x = horizontalVel * direction.normalized.x;
        newVel.y = startVel * Mathf.Sin(fireAngle * Mathf.Deg2Rad);
        newVel.z = horizontalVel * direction.normalized.z;
        return newVel;
    }
}