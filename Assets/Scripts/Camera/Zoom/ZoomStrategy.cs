using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ZoomStrategy
{
    void ZoomIn(Camera camera, float delta, float nearZoomLimit);
    void ZoomOut(Camera camera, float delta, float farZoomLimit);
}
