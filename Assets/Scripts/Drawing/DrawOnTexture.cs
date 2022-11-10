using UnityEngine;
using UnityEngine.Rendering;
using System;
using Random = UnityEngine.Random;

public class DrawOnTexture : MonoBehaviour
{
    [SerializeField] private LayerMask _targetLayer;
    [SerializeField] private float _sizeMult = .1f;
    [SerializeField] private int _renderTextureSize = 512;
    [Space]
    [SerializeField] private Renderer _renderer;
    [SerializeField] private Texture[] _brushTextures;
    [SerializeField] private Texture _blankTexture;
    [SerializeField] private Material _brushMaterial;

    private RenderTexture _rt;
    private MaterialPropertyBlock _propertyBlock;
    private Camera _camera;

    public Action OnDraw;

    private const string DRAW_TEXTURE_PROPERTY = "_DrawTex";
    private const string EMISSION_TEXTURE_PROPERTY = "_EmissionMap";
    private const string BRUSH_MAIN_TEXTURE_PROPERTY = "_MainTex";
    private const string BRUSH_ROTATION_ANGLE_PROPERTY = "_BrushTexRotation";

    private void Start()
    {
        _camera = Camera.main;
        _propertyBlock = new MaterialPropertyBlock();
        _rt = RenderTexture.GetTemporary(_renderTextureSize, _renderTextureSize, 0, RenderTextureFormat.ARGB32);
        //_rt.wrapMode = TextureWrapMode.Repeat;
        _brushMaterial = new Material(_brushMaterial);

        _renderer.GetPropertyBlock(_propertyBlock);
        _propertyBlock.SetTexture(EMISSION_TEXTURE_PROPERTY, _rt);
        _renderer.SetPropertyBlock(_propertyBlock);
        DrawBlank();
    }

    // Initialization RenderTexture
    private void DrawBlank()
    {
        //  Activate _rt
        RenderTexture.active = _rt;
        //  Save current state 
        GL.PushMatrix();
        //  Set up the matrix 
        GL.LoadPixelMatrix(0, _rt.width, _rt.height, 0);

        //  Draw maps 
        Rect rect = new Rect(0, 0, _rt.width, _rt.height);
        Graphics.DrawTexture(rect, _blankTexture);

        //  Pop up changes 
        GL.PopMatrix();

        RenderTexture.active = null;
    }

    // Stay RenderTexture Of (x,y) Draw brush patterns at coordinates 
    private void Draw(int x, int y, float splashSize, Color color, float brushRotation)
    {
        RenderTexture temp = RenderTexture.GetTemporary(_rt.width, _rt.height, 0, RenderTextureFormat.ARGB32);

        if (SystemInfo.copyTextureSupport != CopyTextureSupport.None)
            Graphics.CopyTexture(_rt, temp);
        else
            Graphics.Blit(_rt, temp);

        RenderTexture.active = _rt;
        GL.PushMatrix();
        GL.LoadPixelMatrix(0, _rt.width, _rt.height, 0);

        Texture brushTexture = _brushTextures[Random.Range(0, _brushTextures.Length)];

        // delete this after fix draw
        float xDivider = transform.localScale.x;

        if (transform.localScale.x == 1f && transform.localScale.z > 1f)
            xDivider = transform.localScale.z;

        int sizeX = Mathf.FloorToInt(_renderTextureSize / xDivider * splashSize * _sizeMult);
        int sizeY = Mathf.FloorToInt(_renderTextureSize / transform.localScale.z * splashSize * _sizeMult);
        x -= (int)(sizeX * 0.5f);
        y -= (int)(sizeY * 0.5f);
        Rect rect = new Rect(x, y, sizeX, sizeY);

        _brushMaterial.SetTexture(BRUSH_MAIN_TEXTURE_PROPERTY, brushTexture);
        _brushMaterial.SetTexture(DRAW_TEXTURE_PROPERTY, temp);
        _brushMaterial.SetFloat(BRUSH_ROTATION_ANGLE_PROPERTY, brushRotation);
        Graphics.DrawTexture(rect, brushTexture, _brushMaterial);

        GL.PopMatrix();

        RenderTexture.active = null;
        RenderTexture.ReleaseTemporary(temp);

        OnDraw?.Invoke();
    }

    //private void Update()
    //{
    //    if (Input.GetMouseButton(0))
    //    {
    //        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
    //        RaycastDraw(ray);
    //    }
    //}

    public void Draw(Vector2 textureCoord, float size, float brushRotation)
    {
        var x = (int)(textureCoord.x * _rt.width);
        var y = (int)(_rt.height - textureCoord.y * _rt.height);
        Draw(x, y, size, Color.white, brushRotation);
    }

    public void RaycastDraw(Ray ray, float size, float brushRotation)
    {
        RaycastDraw(ray, size, Color.white, brushRotation);
    }

    public void RaycastDraw(Ray ray, float size, Color color, float brushRotation)
    {
        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, _targetLayer))
        {
            var x = (int)(hit.textureCoord.x * _rt.width);
            var y = (int)(_rt.height - hit.textureCoord.y * _rt.height);
            Draw(x, y, size, color, brushRotation);
        }
    }

    public void DrawByPosition(Vector3 position, float size, float brushRotation)
    {
        Ray ray = new Ray(_camera.transform.position, (position - _camera.transform.position).normalized);
        RaycastDraw(ray, size, brushRotation);
    }

    private void OnDestroy()
    {
        _rt.Release();
    }
}