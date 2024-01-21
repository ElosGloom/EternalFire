using Unity.Collections;
using Unity.Jobs;
using UnityEngine;

namespace FoW
{
    public struct FogPlaneJob : IJob
    {
        private readonly NativeArray<Vector3> _fogPlaneVertices;
        private readonly float _fowPlaneHeight;
        private readonly NativeArray<Vector3> _revealersPositions;
        private readonly NativeArray<float> _revealersRadius;
        private readonly NativeArray<float> _revealForces;
        private readonly float _edgeSharpness;

        private NativeArray<float> _fogPlaneAlphaResult;

        public FogPlaneJob(
            NativeArray<float> fogPlaneAlphaResult,
            NativeArray<Vector3> fogPlaneVertices,
            float fowPlaneHeight,
            NativeArray<Vector3> revealersPositions,
            NativeArray<float> revealersRadius,
            NativeArray<float> revealForces,
            float edgeSharpness)
        {
            _fogPlaneAlphaResult = fogPlaneAlphaResult;
            _fogPlaneVertices = fogPlaneVertices;
            _fowPlaneHeight = fowPlaneHeight;
            _revealersPositions = revealersPositions;
            _revealersRadius = revealersRadius;
            _revealForces = revealForces;
            _edgeSharpness = edgeSharpness;

            for (int i = 0; i < _fogPlaneAlphaResult.Length; i++)
            {
                _fogPlaneAlphaResult[i] = 1;
            }
        }

        public void Execute()
        {
            for (int i = 0; i < _fogPlaneVertices.Length; i++)
                _fogPlaneAlphaResult[i] = 1;

            Vector3 positionBuffer = new Vector3 { y = _fowPlaneHeight };
            for (var r = 0; r < _revealersPositions.Length; r++)
            {
                for (int i = 0; i < _fogPlaneVertices.Length; i++)
                {
                    positionBuffer.x = _revealersPositions[r].x;
                    positionBuffer.z = _revealersPositions[r].z;
                    _fogPlaneAlphaResult[i] = Mathf.Min(
                        _fogPlaneAlphaResult[i],
                        (Vector3.SqrMagnitude(_fogPlaneVertices[i] - positionBuffer) / _revealForces[r] - _edgeSharpness) / (_revealersRadius[r] * _revealersRadius[r]));
                }
            }
        }
    }
}