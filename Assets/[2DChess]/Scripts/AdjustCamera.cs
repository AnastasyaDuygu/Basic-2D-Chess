using UnityEngine;
//credits goes to Jar_Coding for this class
//https://forum.unity.com/threads/how-to-change-size-of-the-camera-depending-on-the-aspect-ratio.972234/

[RequireComponent(typeof(Camera))][ExecuteAlways]
public class AdjustCamera : MonoBehaviour
{
    [SerializeField] private Camera cam;
   
    private readonly Vector2 targetAspectRatio = new(1,1);
    private readonly Vector2 rectCenter = new(0.5f, 0.5f);
 
    private Vector2 lastResolution;
 
    private void OnValidate()
    {
        cam ??= GetComponent<Camera>();
    }
 
    public void LateUpdate()
    {
        var currentScreenResolution = new Vector2(Screen.width, Screen.height);
 
        // Don't run all the calculations if the screen resolution has not changed
        if (lastResolution != currentScreenResolution)
        {
            CalculateCameraRect(currentScreenResolution);
        }
 
        lastResolution = currentScreenResolution;
    }
 
    private void CalculateCameraRect(Vector2 currentScreenResolution)
    {
        var normalizedAspectRatio = targetAspectRatio / currentScreenResolution;
        var size = normalizedAspectRatio / Mathf.Max(normalizedAspectRatio.x, normalizedAspectRatio.y);
        cam.rect = new Rect(default, size) { center = rectCenter };
    }
}