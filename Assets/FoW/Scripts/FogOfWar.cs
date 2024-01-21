using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace FoW
{
    public class FogOfWar : MonoBehaviour
    {
        private static readonly List<FogRevealer> Revealers = new();
        private static readonly HashSet<FogCell> VisibleCells = new();

        [SerializeField] private float edgeSharpness = 1;
        [SerializeField] private FogGridBuilder _fogGridBuilder;

        private float _fogHeight;

        public static void AddRevealer(FogRevealer revealer)
        {
            Revealers.Add(revealer);
        }

        public static void RemoveRevealer(FogRevealer revealer)
        {
            Revealers.Remove(revealer);
        }

        public static bool IsHiderVisible(IHider hider)
        {
            for (int i = 0; i < Revealers.Count; i++)
            {
                if ((hider.Position - Revealers[i].Position).sqrMagnitude <
                    Revealers[i].Radius * Revealers[i].Radius * Revealers[i].RevealForce * Revealers[i].RevealForce)
                {
                    return true;
                }
            }
            return false;
        }

        private void Awake()
        {
            _fogHeight = transform.position.y;
            var mainCamera = Camera.main;
            var fowCamera = mainCamera!.transform.GetChild(0).GetComponent<Camera>();
            fowCamera.depth = mainCamera.depth + 1;
            fowCamera.farClipPlane = mainCamera.farClipPlane;
            fowCamera.nearClipPlane = mainCamera.nearClipPlane;


            if (_fogGridBuilder.CreatedCells.Length == 0)
                BuildFogGrid();

                
            foreach (var cell in _fogGridBuilder.CreatedCells)
                cell.Init(_fogGridBuilder.Color);

            FogCell.VisionStatusChangedEvent += UpdateVisibleCells;
            _fogGridBuilder = null;
        }

        private static void UpdateVisibleCells(FogCell cell)
        {
            if (VisibleCells.Contains(cell))
                VisibleCells.Remove(cell);
            else
                VisibleCells.Add(cell);
        }

        [ContextMenu(nameof(BuildFogGrid))]
        private void BuildFogGrid()
        {
            _fogGridBuilder.Build(transform);
        }


        private void Update()
        {
            foreach (var cell in VisibleCells)
                cell.StartJob(Revealers, _fogHeight, edgeSharpness);
        }

        private void LateUpdate()
        {
            foreach (var cell in VisibleCells)
                cell.FinishJob();
        }

        [Serializable]
        private class FogGridBuilder
        {
            private const float CellSize = 6;

            [SerializeField] private Color color = Color.black;
            [SerializeField] private Vector2Int size = new(10, 10);
            [SerializeField] private FogCell fogCellPrefab;
            [SerializeField] private FogCell[] createdCells;

            public FogCell[] CreatedCells => createdCells;
            public Color Color => color;

            public void Build(Transform parent)
            {
                for (int i = 0; i < createdCells.Length; i++)
                {
#if UNITY_EDITOR
                    if (!Application.isPlaying)
                    {
                        DestroyImmediate(createdCells[i].gameObject);
                        continue;
                    }
#endif
                    Destroy(createdCells[i].gameObject);
                }

                if (fogCellPrefab == null)
                    return;

                createdCells = new FogCell[size.x * size.y];
                float zDefault = -((size.y - 1) * CellSize / 2);
                Vector3 nextLocalPos = new() { x = -((size.x - 1) * CellSize / 2), z = zDefault };

                for (int x = 0; x < size.x; x++)
                {
                    for (int y = 0; y < size.y; y++)
                    {
                        createdCells[x * size.y + y] = Instantiate(fogCellPrefab, parent.position + nextLocalPos, parent.rotation, parent);
                        nextLocalPos.z += CellSize;
                    }
                    nextLocalPos.x += CellSize;
                    nextLocalPos.z = zDefault;
                }
                
                int tintColor = Shader.PropertyToID("_TintColor");
                CreatedCells.First().GetComponent<Renderer>().sharedMaterial.SetColor(tintColor, color);
            }
        }
    }
}