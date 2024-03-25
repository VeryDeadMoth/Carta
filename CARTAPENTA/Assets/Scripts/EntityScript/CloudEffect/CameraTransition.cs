
using System.Collections;
using UnityEngine;

public class CameraTransition
{
    private Camera _Camera;
    private float SizeCameraInit;
    private Vector3 PositionInit;

    public CameraTransition(Camera cam)
    {
        this._Camera = cam;
        this.SizeCameraInit = _Camera.orthographicSize;
        this.PositionInit = _Camera.transform.localPosition;
    }

    /// <summary>
    /// Coroutine that will move the camera to the given position (DirectionCam) at a fixed speed (vitesseCam).
    /// it will then wait attenteCam seconds before coming back to its original position.
    /// </summary>
    /// <param name="DirectionCam"></param>
    /// <param name="sizeToAttain"></param>
    /// <param name="attenteCam"></param>
    /// <param name="vitesseCam"></param>
    /// <returns></returns>
    public IEnumerator CamMovementToCenter(Vector3 directionCam, float sizeToAttain, float attenteCam, float vitesseCam)
    {
        Vector3 CameraPosition = _Camera.transform.localPosition;
        float prevDistance = Vector2.Distance(CameraPosition, directionCam);


        _Camera.transform.parent.GetComponent<OutOfBox>().enabled = false;

        while (prevDistance > 0.1)
        {
            _Camera.transform.localPosition = Vector3.MoveTowards(CameraPosition, directionCam, vitesseCam);
            _Camera.orthographicSize = Mathf.MoveTowards(_Camera.orthographicSize, sizeToAttain, vitesseCam * 0.5f);

            CameraPosition = _Camera.transform.localPosition;
            prevDistance = Vector2.Distance(CameraPosition, directionCam);

            yield return null;
        }


        //if camera isn't back at original position, then redo the coroutine with its original position as destination. Else, end of Coroutine, we can move again.
        if (Vector2.Distance(CameraPosition, PositionInit) > 0.1)
        {
            yield return new WaitForSeconds(attenteCam);
            yield return CamMovementToCenter(PositionInit, SizeCameraInit, attenteCam, vitesseCam);
        }
        else
        {
            _Camera.transform.parent.GetComponent<OutOfBox>().enabled = true;
        }
    }
}
