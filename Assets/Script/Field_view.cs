#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

[CustomEditor (typeof (Field_of_view))] public class Field_view : Editor
{
    private void OnSceneGUI()
    {
        Field_of_view fov = (Field_of_view)target;

        ///Attacco
        Handles.color = Color.white;
        Handles.DrawWireArc(fov.transform.position, Vector3.up, Vector3.forward, 360, fov.radiusAttack);

        Vector3 viewAngle21 = DirectionFromAngle(fov.transform.eulerAngles.y, -fov.angleAttack / 2);
        Vector3 viewAngle22 = DirectionFromAngle(fov.transform.eulerAngles.y, fov.angleAttack / 2);

        Handles.color = Color.blue;
        Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngle21 * fov.radiusAttack);
        Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngle22 * fov.radiusAttack);

        ///Controllo
        Handles.color = Color.white;
        Handles.DrawWireArc(fov.transform.position, Vector3.up, Vector3.forward, 360, fov.radius);

        Vector3 viewAngle01 = DirectionFromAngle(fov.transform.eulerAngles.y, -fov.angle / 2);
        Vector3 viewAngle02 = DirectionFromAngle(fov.transform.eulerAngles.y, fov.angle / 2);

        Handles.color = Color.yellow;
        Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngle01 * fov.radius);
        Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngle02 * fov.radius);

        ///Attacco melee
        Handles.color = Color.white;
        Handles.DrawWireArc(fov.transform.position, Vector3.up, Vector3.forward, 360, fov.radiusmeleeAttack);

        Vector3 viewAngle31 = DirectionFromAngle(fov.transform.eulerAngles.y, -fov.anglemeleeAttack / 2);
        Vector3 viewAngle32 = DirectionFromAngle(fov.transform.eulerAngles.y, fov.anglemeleeAttack / 2);

        Handles.color = Color.green;
        Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngle31 * fov.radiusmeleeAttack);
        Handles.DrawLine(fov.transform.position, fov.transform.position + viewAngle32 * fov.radiusmeleeAttack);

        foreach (Transform visibleTarget in fov.visibleTargets)
        {
            if (fov.canSeePlayer)
            {
                Handles.color = Color.green;
                Handles.DrawLine(fov.transform.position, visibleTarget.transform.position);
            }
        }
    }

    private Vector3 DirectionFromAngle(float eulerY, float angleInDegrees)
    {
        angleInDegrees += eulerY;

        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }

    /*
    private void OnSceneGUI()
    {
        Field_of_view fow = (Field_of_view)target;
        Handles.color = Color.white;
        Handles.DrawWireArc(fow.transform.position, Vector3.up, Vector3.forward, 360, fow.viewRadius);
        Vector3 viewAngleA = fow.DirFromAngle(-fow.viewAngle / 2, false);
        Vector3 viewAngleB = fow.DirFromAngle(fow.viewAngle / 2, false);
        
        Handles.color = Color.yellow;
        Handles.DrawLine(fow.transform.position, fow.transform.position + viewAngleA * fow.viewRadius);
        Handles.DrawLine(fow.transform.position, fow.transform.position + viewAngleB * fow.viewRadius);

        foreach(Transform visibleTarget in fow.visibleTargets)
        {
            Handles.color = Color.green;
            Handles.DrawLine(fow.transform.position, visibleTarget.transform.position);
        }
    }

    private Vector3 DirectionFromAngle(float eulerY, float angleInDegrees)
    {
        angleInDegrees += eulerY;

        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }*/
}
#endif
