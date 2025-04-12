using System;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Serialization;

namespace UnityEditor.U2D.Aseprite
{
    [Serializable]
    internal class Layer
    {
        [SerializeField] int m_LayerIndex;
        [SerializeField] int m_Guid;
        [SerializeField] string m_Name;
        [SerializeField] LayerFlags m_LayerFlags;
        [SerializeField] LayerTypes m_LayerType;
        [SerializeField] BlendModes m_BlendMode;
        [SerializeField] List<Cell> m_Cells = new List<Cell>();
        [SerializeField] List<LinkedCell> m_LinkedCells = new List<LinkedCell>();
        [SerializeField] int m_ParentIndex = -1;

        [NonSerialized] public float opacity;

        public int index
        {
            get => m_LayerIndex;
            set => m_LayerIndex = value;
        }

        public int guid
        {
            get => m_Guid;
            set => m_Guid = value;
        }
        public string name
        {
            get => m_Name;
            set => m_Name = value;
        }
        public LayerFlags layerFlags
        {
            get => m_LayerFlags;
            set => m_LayerFlags = value;
        }
        public LayerTypes layerType
        {
            get => m_LayerType;
            set => m_LayerType = value;
        }
        public BlendModes blendMode
        {
            get => m_BlendMode;
            set => m_BlendMode = value;
        }
        public List<Cell> cells
        {
            get => m_Cells;
            set => m_Cells = value;
        }
        public List<LinkedCell> linkedCells
        {
            get => m_LinkedCells;
            set => m_LinkedCells = value;
        }
        public int parentIndex
        {
            get => m_ParentIndex;
            set => m_ParentIndex = value;
        }

        public static int GenerateGuid(Layer layer, IReadOnlyList<Layer> layers)
        {
            var fullName = layer.name;
            var parent = layer;
            do
            {
                var parentIndex = parent.parentIndex;
                parent = layers.Find(x => x.index == parentIndex);
                if (parent != null)
                    fullName = fullName.Insert(0, parent.name + "/");
                
            } while (parent != null);
            
            var hash = fullName.GetHashCode();
            return hash;
        }
    }

    [Serializable]
    internal struct Cell : IEquatable<Cell>
    {
        [SerializeField] string m_Name;
        [SerializeField] int m_FrameIndex;
        [SerializeField] int m_AdditiveSortOrder;
        [SerializeField] RectInt m_CellRect;
        [SerializeField] string m_SpriteId;

        [NonSerialized] public bool updatedCellRect;
        [NonSerialized] public float opacity;
        [NonSerialized] public BlendModes blendMode;
        [NonSerialized] public NativeArray<Color32> image;

        public string name
        {
            get => m_Name;
            set => m_Name = value;
        }
        public int frameIndex
        {
            get => m_FrameIndex;
            set => m_FrameIndex = value;
        }
        public int additiveSortOrder
        {
            get => m_AdditiveSortOrder;
            set => m_AdditiveSortOrder = value;
        }
        public RectInt cellRect
        {
            get => m_CellRect;
            set => m_CellRect = value;
        }
        public GUID spriteId
        {
            get => new GUID(m_SpriteId);
            set => m_SpriteId = value.ToString();
        }

        public bool Equals(Cell other)
        {
            return m_Name == other.m_Name && 
                   m_FrameIndex == other.m_FrameIndex && 
                   m_AdditiveSortOrder == other.m_AdditiveSortOrder && 
                   m_CellRect.Equals(other.m_CellRect) && 
                   m_SpriteId == other.m_SpriteId && 
                   updatedCellRect == other.updatedCellRect && 
                   opacity.Equals(other.opacity) && 
                   blendMode == other.blendMode && 
                   image.Equals(other.image);
        }

        public override bool Equals(object obj)
        {
            return obj is Cell other && Equals(other);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (m_Name != null ? m_Name.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ m_FrameIndex;
                hashCode = (hashCode * 397) ^ m_AdditiveSortOrder;
                hashCode = (hashCode * 397) ^ m_CellRect.GetHashCode();
                hashCode = (hashCode * 397) ^ (m_SpriteId != null ? m_SpriteId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ updatedCellRect.GetHashCode();
                hashCode = (hashCode * 397) ^ opacity.GetHashCode();
                hashCode = (hashCode * 397) ^ (int) blendMode;
                hashCode = (hashCode * 397) ^ image.GetHashCode();
                return hashCode;
            }
        }
    }

    [Serializable]
    internal class LinkedCell
    {
        [SerializeField] int m_FrameIndex;
        [SerializeField] int m_LinkedToFrame;

        public int frameIndex
        {
            get => m_FrameIndex;
            set => m_FrameIndex = value;
        }
        public int linkedToFrame
        {
            get => m_LinkedToFrame;
            set => m_LinkedToFrame = value;
        }
    }

    internal class Frame
    {
        int m_Duration;
        string[] m_EventStrings;

        public int duration
        {
            get => m_Duration;
            set => m_Duration = value;
        }
        public string[] eventStrings
        {
            get => m_EventStrings;
            set => m_EventStrings = value;
        }
    }

    internal class Tag
    {
        public string name { get; set; }
        public int fromFrame { get; set; }
        public int toFrame { get; set; }
        public int noOfRepeats { get; set; }

        public int noOfFrames => toFrame - fromFrame;
        public bool isRepeating => noOfRepeats == 0;
    }

    /// <summary>
    /// Import modes for the file.
    /// </summary>
    public enum FileImportModes
    {
        /// <summary>The file is imported as a Sprite Sheet, and can be sliced up in the Sprite Editor.</summary>
        SpriteSheet = 0,
        /// <summary>The file is imported with animation in mind. Animation assets are generated and attached to a model prefab on import.</summary>
        AnimatedSprite = 1,
    }

    /// <summary>
    /// Import modes for all layers.
    /// </summary>
    public enum LayerImportModes
    {
        /// <summary>
        /// Every layer per frame generates a Sprite.
        /// </summary>
        IndividualLayers,
        /// <summary>
        /// All layers per frame are merged into one Sprite.
        /// </summary>
        MergeFrame
    }

    /// <summary>
    /// The space the Sprite pivots are being calculated.
    /// </summary>
    public enum PivotSpaces
    {
        /// <summary>
        /// Canvas space. Calculate the pivot based on where the Sprite is positioned on the source asset's canvas.
        /// This is useful if the Sprite is being swapped out in an animation.
        /// </summary>
        Canvas,
        /// <summary>
        /// Local space. This is the normal pivot space.
        /// </summary>
        Local
    }
}
