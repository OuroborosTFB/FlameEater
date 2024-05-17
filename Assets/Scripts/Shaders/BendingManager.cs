﻿using System;
using UnityEngine;
using UnityEngine.Rendering;

namespace Shaders
{
  [ExecuteAlways]
  public class BendingManager : MonoBehaviour
  {
    private const string BendingFeature = "ENABLE_BENDING";
    private const string PlanetFeature = "ENABLE_BENDING_PLANET";

    [SerializeField] private bool _enablePlanet;

    private void OnEnable()
    {
      if (!Application.isPlaying )
        return;
    
      RenderPipelineManager.beginCameraRendering += OnBeginCameraRendering;
      
      RenderPipelineManager.endCameraRendering += OnEndCameraRendering;
    }
    private void OnDisable()
    {
      RenderPipelineManager.beginCameraRendering -= OnBeginCameraRendering;
      RenderPipelineManager.endCameraRendering -= OnEndCameraRendering;
    }
    
    private void Awake()
    {
      if (Application.isPlaying )
        Shader.EnableKeyword(BendingFeature);
      else
        Shader.DisableKeyword(BendingFeature);
      
      if ( _enablePlanet )
        Shader.EnableKeyword(PlanetFeature);
      else
        Shader.DisableKeyword(PlanetFeature);
    }
    
    private static void OnBeginCameraRendering(ScriptableRenderContext ctx, Camera cam)
    {
      cam.cullingMatrix = Matrix4x4.Ortho(-150, 150, -150, 150, 0.01f, 100) * cam.worldToCameraMatrix;
    }

    private static void OnEndCameraRendering (ScriptableRenderContext ctx, Camera cam) =>
      cam.ResetCullingMatrix();
  }
}