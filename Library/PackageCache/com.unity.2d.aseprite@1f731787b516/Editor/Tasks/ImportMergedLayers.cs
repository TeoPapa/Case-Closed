using System.Collections.Generic;
using Unity.Collections;
using Unity.Mathematics;
using UnityEngine;

namespace UnityEditor.U2D.Aseprite
{
    internal static class ImportMergedLayers
    {
        public static void Import(string assetName, ref List<Layer> layers, out List<NativeArray<Color32>> cellBuffers, out List<int2> cellSize)
        {
            var cellsPerFrame = CellTasks.GetAllCellsPerFrame(layers);
            var mergedCells = CellTasks.MergeCells(cellsPerFrame, assetName);

            CellTasks.CollectDataFromCells(mergedCells, out cellBuffers, out cellSize);
            UpdateLayerList(mergedCells, assetName, ref layers);
        }

        static void UpdateLayerList(IReadOnlyList<Cell> cells, string assetName, ref List<Layer> layers)
        {
            layers.Clear();
            var flattenLayer = new Layer()
            {
                cells = new List<Cell>(cells),
                index = 0,
                name = assetName
            };
            flattenLayer.guid = Layer.GenerateGuid(flattenLayer, layers);
            layers.Add(flattenLayer);
        }
    }
}
