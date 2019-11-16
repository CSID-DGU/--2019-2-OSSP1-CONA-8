using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MirrorFlipCamera : MonoBehaviour
{
    private Camera cameraCurrent;
    public bool flipHorizontal;
    private Vector3 currentScale;

    void Start()
    {
        cameraCurrent = GetComponent<Camera>();
    }

    void OnPreCull()
    {
        cameraCurrent.ResetWorldToCameraMatrix();
        cameraCurrent.ResetProjectionMatrix();

        currentScale = new Vector3(flipHorizontal ? -1 : 1, 1, 1);

        cameraCurrent.projectionMatrix = cameraCurrent.projectionMatrix * Matrix4x4.Scale(currentScale);
    }

    void OnPreRender()
    {
        GL.invertCulling = flipHorizontal;
    }

    void OnPostRender()
    {
        GL.invertCulling = false;
    }
}