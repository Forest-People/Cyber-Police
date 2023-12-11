using Godot;

namespace CyberPolice.scripts;

public partial class PlayerCamera : Camera2D
{
    [Export] private float _zoomSpeed;
    [Export] private float _movingSpeed;
    [Export] private float _minimalZoom;
    [Export] private float _maximumZoom;

    public void ZoomIn()
    {
        Zoom *= _zoomSpeed;
    }
    
    public void ZoomOut()
    {
        Zoom /= _zoomSpeed;
    }
}