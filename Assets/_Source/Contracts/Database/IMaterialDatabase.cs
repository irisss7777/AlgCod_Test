using System.Collections.Generic;
using _Source.Contracts.View;

namespace Contracts.Database
{
    public interface IMaterialDatabase
    {
        public int MaxAngle { get; }
        public List<MaterialData> Materials { get; }
        public IMaterialSelectionView StaticMaterialPrefab { get; }
        public IMaterialSelectionView DynamicMaterialPrefab { get; }
        
        public void LoadFromResources();
        public float GetFriction(int firstMaterial, int secondMaterial);
        public (int, int) GetMaterial();
    }
}