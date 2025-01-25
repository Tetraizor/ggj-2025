using UnityEngine;
using UnityEngine.UI;

public class RTManager : MonoBehaviour
{
    private Camera _camera;
    private RenderTexture _renderTexture;

    private int _currentScreenWidth;
    private int _currentScreenHeight;

    [SerializeField]
    private RawImage _rawImage;

    public delegate void RenderTextureUpdatedEventHandler(RenderTexture renderTexture);
    public event RenderTextureUpdatedEventHandler RenderTextureUpdated;

    void Start()
    {
        _camera = GetComponent<Camera>();
        _renderTexture = _camera.targetTexture;
        UpdateRenderTexture();
    }

    void Update()
    {
        // Check if screen dimensions have changed
        if (Screen.width != _currentScreenWidth || Screen.height != _currentScreenHeight)
        {
            UpdateRenderTexture();
        }
    }

    private void UpdateRenderTexture()
    {
        _currentScreenWidth = Screen.width;
        _currentScreenHeight = Screen.height;

        // Release the existing render texture if it exists
        if (_renderTexture != null)
        {
            _renderTexture.Release();
        }

        // Create a new render texture with updated dimensions
        _renderTexture = new RenderTexture(_currentScreenWidth, _currentScreenHeight, 24);
        _camera.targetTexture = _renderTexture;

        _rawImage.texture = _renderTexture;

        // Notify subscribers that the render texture has been updated
        RenderTextureUpdated?.Invoke(_renderTexture);
    }

    private void OnDestroy()
    {
        // Clean up the render texture when the object is destroyed
        if (_renderTexture != null)
        {
            _renderTexture.Release();
        }
    }
}
