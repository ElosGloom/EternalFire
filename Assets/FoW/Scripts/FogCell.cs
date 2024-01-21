using System.Collections.Generic;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;

namespace FoW
{
    public class FogCell : MonoBehaviour
    {
        [SerializeField] private Transform fowPlane;
        private Mesh _fogPlaneMesh;
        private NativeArray<Vector3> _fogPlaneVertices;
        private Color[] _fogPlaneColors;
        private JobHandle _jobHandle;

        private NativeArray<float> _fogPlaneAlphaResult;
        private NativeArray<Vector3> _revealersPositions;
        private NativeArray<float> _revealersRadius;
        private NativeArray<float> _revealForces;

        public bool IsVisible { get; private set; }

        private void OnBecameVisible()
        {
            IsVisible = true;
        }

        private void OnBecameInvisible()
        {
            IsVisible = false;
        }

        public void Init(Color color)
        {
            if (fowPlane.gameObject.layer == 0)
            {
                Debug.LogError("Error: Fog plane is missing the FOW layer!");
            }

            _fogPlaneMesh = fowPlane.GetComponent<MeshFilter>().mesh;

            _fogPlaneVertices = new(_fogPlaneMesh.vertices.Length, Allocator.Persistent);
            _fogPlaneMesh.colors = new Color[_fogPlaneMesh.vertices.Length];
            for (int i = 0; i < _fogPlaneVertices.Length; i++)
            {
                _fogPlaneVertices[i] = _fogPlaneMesh.vertices[i] + transform.position;
                _fogPlaneMesh.colors[i] = color;
            }

            int tintColor = Shader.PropertyToID("_TintColor");
            fowPlane.GetComponent<Renderer>().material.SetColor(tintColor, color);
            _fogPlaneColors = new Color[_fogPlaneVertices.Length];
            for (int i = 0; i < _fogPlaneColors.Length; i++)
            {
                _fogPlaneColors[i] = color;
            }

            _fogPlaneAlphaResult = new NativeArray<float>(_fogPlaneVertices.Length, Allocator.Persistent);
        }

        public void StartJob(
            List<FogRevealer> revealers,
            float fogPlaneHeight,
            float edgeSharpness)
        {
            _revealersPositions = new(revealers.Count, Allocator.TempJob);
            _revealersRadius = new(revealers.Count, Allocator.TempJob);
            _revealForces = new(revealers.Count, Allocator.TempJob);

            for (int i = 0; i < revealers.Count; i++)
            {
                _revealersPositions[i] = revealers[i].Position;
                _revealersRadius[i] = revealers[i].Radius;
                _revealForces[i] = revealers[i].RevealForce;
            }

            var job = new FogPlaneJob(
                _fogPlaneAlphaResult,
                _fogPlaneVertices,
                fogPlaneHeight,
                _revealersPositions,
                _revealersRadius,
                _revealForces,
                edgeSharpness);
            _jobHandle = job.Schedule();
        }

        public void FinishJob()
        {
            _jobHandle.Complete();
            for (int i = 0; i < _fogPlaneColors.Length; i++)
                _fogPlaneColors[i].a = _fogPlaneAlphaResult[i];

            _fogPlaneMesh.colors = _fogPlaneColors;
            _revealersPositions.Dispose();
            _revealersRadius.Dispose();
            _revealForces.Dispose();
        }

        private void OnDestroy()
        {
            _fogPlaneAlphaResult.Dispose();
            _fogPlaneVertices.Dispose();
        }
    }
}