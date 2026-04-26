using System;
using System.Collections.Generic;
using Contracts.Database;
using UnityEngine;
using View.Controllers;
using Random = UnityEngine.Random;

namespace Infrastructure.Database
{
    [CreateAssetMenu(fileName = "MaterialDatabase", menuName = "Scriptable/Database/MaterialDatabase")]
    public class MaterialDatabase : ScriptableObject, IMaterialDatabase
    {
        public int MaxAngle => _config.MaxAngle;
        public List<MaterialData> Materials => _config.Materials;
        public MaterialSelectionView StaticMaterialPrefab => _staticMaterialPrefab;
        public MaterialSelectionView DynamicMaterialPrefab => _dynamicMaterialPrefab;

        [SerializeField] private MaterialSelectionView _staticMaterialPrefab;
        [SerializeField] private MaterialSelectionView _dynamicMaterialPrefab;

        private MaterialDatabaseConfig _config;
        private int _firstMaterial;
        private int _secondMaterial;
        private const string ConfigResourcesPath = "Configs/MaterialDatabase";

        public void LoadFromResources()
        {
            TextAsset jsonFile = Resources.Load<TextAsset>(ConfigResourcesPath);
            if (jsonFile == null)
                return;

            try
            {
                _config = JsonUtility.FromJson<MaterialDatabaseConfig>(jsonFile.text);
            }
            catch (Exception e)
            {
            }
        }
        
        public float GetFriction(int firstMaterial, int secondMaterial)
        {
            _firstMaterial = firstMaterial;
            _secondMaterial = secondMaterial;

            var frictionData = _config.Materials[_firstMaterial].MaterialFriction[_secondMaterial];

            var friction = Random.Range(frictionData.MinValue, frictionData.MaxValue);
            
            return friction;
        }

        public (int, int) GetMaterial() =>
            (_firstMaterial, _secondMaterial);
    }
    
    [Serializable]
    public class MaterialDatabaseConfig
    {
        public int MaxAngle;
        public List<MaterialData> Materials;
    }

    [Serializable]
    public class MaterialData
    {
        public List<FrictionData> MaterialFriction;
    }

    [Serializable]
    public class FrictionData
    {
        public float MinValue;
        public float MaxValue;
    }
    
}