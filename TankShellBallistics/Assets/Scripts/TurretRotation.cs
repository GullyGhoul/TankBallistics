
using UnityEngine;

public class TurretRotation : MonoBehaviour
{
    public Transform turretHorizontalRotation;
    public float horizontalRotationSpeed;

    public Transform turretVerticalRotation;
    public float verticalRotationSpeed;

    public Transform mainPlatform;
    public float mainRotationSpeed;

    public float verticalClamp = 80;
    public float horizontalClamp = 20;

    public void SetRotation(Vector3 target, float verticalAngle)
    {
        Vector3 destination = target - mainPlatform.position;
        float angle = Mathf.Atan2(destination.x, destination.z) * Mathf.Rad2Deg;
        Quaternion horizontalRot = Quaternion.AngleAxis(angle, Vector3.up);
        turretHorizontalRotation.rotation = Quaternion.Slerp(turretHorizontalRotation.rotation, horizontalRot,
            Time.deltaTime * horizontalRotationSpeed);
        float angle2 = Vector3.SignedAngle(mainPlatform.forward, turretHorizontalRotation.forward, Vector3.up);
        if ( Mathf.Abs(angle2) >= horizontalClamp)
        {
            Quaternion mainRot = Quaternion.AngleAxis(angle2, Vector3.up);
            mainPlatform.rotation = Quaternion.Slerp(mainPlatform.rotation, mainRot,
                Time.deltaTime * mainRotationSpeed);
        }

        Quaternion verticalRot = Quaternion.AngleAxis(-verticalAngle, Vector3.right);
        turretVerticalRotation.localRotation = Quaternion.Slerp(turretVerticalRotation.localRotation, verticalRot,
            Time.deltaTime * verticalRotationSpeed);
    }

    public void ResetRotation()
    {
        Quaternion rot = Quaternion.Euler(0, 0, 0);
        turretVerticalRotation.localRotation = Quaternion.Slerp(turretVerticalRotation.localRotation, rot,
            Time.deltaTime * verticalRotationSpeed);
        turretHorizontalRotation.localRotation = Quaternion.Slerp(turretHorizontalRotation.localRotation, rot,
            Time.deltaTime * horizontalRotationSpeed);
    }
}