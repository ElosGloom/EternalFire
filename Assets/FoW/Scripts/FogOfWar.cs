using System;
using System.Collections.Generic;
using UnityEngine;

namespace FoW
{
    public class FogOfWar : MonoBehaviour
    {
        private static readonly List<FogRevealer> Revealers = new();

        [SerializeField] private Color color = Color.black;
        [SerializeField] private float edgeSharpness = 1;
        [SerializeField] private FogCell[] fogCells;
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

        public static bool IsVisible(IHider hider)
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

            if (fogCells.Length == 0)
            {
                fogCells = GetComponentsInChildren<FogCell>();
                if (fogCells.Length == 0)
                    BuildFogGrid();
            }

            for (int i = 0; i < fogCells.Length; i++)
            {
                fogCells[i].Init(color);
            }
        }

        [ContextMenu(nameof(BuildFogGrid))]
        private void BuildFogGrid()
        {
            for (int i = 0; i < fogCells.Length; i++)
                Destroy(fogCells[i].gameObject);

            fogCells = _fogGridBuilder.Build(transform);
        }


        private void Update()
        {
            for (int i = 0; i < fogCells.Length; i++)
            {
                if (!fogCells[i].IsVisible)
                    continue;

                fogCells[i].StartJob(Revealers, _fogHeight, edgeSharpness);
            }
        }

        private void LateUpdate()
        {
            for (int i = 0; i < fogCells.Length; i++)
            {
                if (!fogCells[i].IsVisible)
                    continue;

                fogCells[i].FinishJob();
            }
        }

        private void OnValidate()
        {
            if (fogCells.Length == 0)
                fogCells = GetComponentsInChildren<FogCell>();
        }

        [Serializable]
        private class FogGridBuilder
        {
            private const float CellSize = 6;

            [SerializeField] private Vector2Int size = new(10, 10);
            [SerializeField] private FogCell fogCellPrefab;

            public FogCell[] Build(Transform parent)
            {
                if (fogCellPrefab == null)
                    return null;

                FogCell[] fogCells = new FogCell[size.x * size.y];
                float zDefault = -((size.y - 1) * CellSize / 2);
                Vector3 nextLocalPos = new() { x = -((size.x - 1) * CellSize / 2), z = zDefault };

                for (int x = 0; x < size.x; x++)
                {
                    for (int y = 0; y < size.y; y++)
                    {
                        fogCells[x * size.y + y] = Instantiate(fogCellPrefab, parent.position + nextLocalPos, parent.rotation, parent);
                        nextLocalPos.z += CellSize;
                    }
                    nextLocalPos.x += CellSize;
                    nextLocalPos.z = zDefault;
                }
                return fogCells;
            }
        }
    }
}