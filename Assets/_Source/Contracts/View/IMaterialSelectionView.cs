using System;
using UnityEngine;

namespace _Source.Contracts.View
{
    public interface IMaterialSelectionView
    {
        public GameObject GameObject { get; }
        public event Action<bool, int> IsSelected;
        public MeshRenderer MaterialObject { get; }

        public void Initialize(int materialIndex, bool isFirstMaterial);

        public void Select(int material, bool isFirst);
    }
}