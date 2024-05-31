using UnityEngine;

public class LookAtCamera : MonoBehaviour
{

    private enum Mode
    {
        Lookat,
        LookAtInverted,
        CameraForwar,
        CameraForwarInverted
    }

    [SerializeField] Mode mode;
    private void LateUpdate()
    {
        switch (mode)
        {
            case Mode.Lookat:
                transform.LookAt(Camera.main.transform);
                break;
            case Mode.LookAtInverted:
                Vector3 DirFromCamera = transform.position - Camera.main.transform.forward;
                transform.LookAt(DirFromCamera);
                break;
            case Mode.CameraForwar:
                transform.forward = Camera.main.transform.forward;
                break;
            case Mode.CameraForwarInverted:
                transform.forward = -Camera.main.transform.forward;
                break;
        }


    }
}
