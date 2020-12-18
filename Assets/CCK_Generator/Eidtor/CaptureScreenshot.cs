#if UNITY_EDITOR
using UnityEngine;


public class CaptureScreenshot
{
	[UnityEditor.MenuItem("CCK_Generator/CaptureScreenshot")]
	static void Capture() {
        ScreenCapture.CaptureScreenshot("image.png", 2);
	}
}
#endif
