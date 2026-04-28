using System;
using System.Collections.Generic;
using System.IO;
using _Source.Contracts.View;
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
        public IMaterialSelectionView StaticMaterialPrefab => _staticMaterialPrefab;
        public IMaterialSelectionView DynamicMaterialPrefab => _dynamicMaterialPrefab;

        [SerializeField] private MaterialSelectionView _staticMaterialPrefab;
        [SerializeField] private MaterialSelectionView _dynamicMaterialPrefab;
        [SerializeField] private MaterialDatabaseConfig _config;
        
        private int _firstMaterial;
        private int _secondMaterial;
        private const string ConfigFileName = "MaterialDatabase.json";

        public void LoadFromResources()
        {
            var path = Path.Combine(Application.streamingAssetsPath, ConfigFileName);

            if (!File.Exists(path))
                return;

            try
            {
                string json = File.ReadAllText(path);
                _config = JsonUtility.FromJson<MaterialDatabaseConfig>(json);

                if (_config == null) return;

                if (_config.MaxAngle > 60)
                    _config.MaxAngle = 60;
                if (_config.MaxAngle <= 0)
                    _config.MaxAngle = 1;

                if (_config.Materials.Count > 4)
                {
                    _config.Materials.RemoveRange(4, _config.Materials.Count - 4);
                }

                foreach (var material in _config.Materials)
                {
                    if (material?.MaterialFriction != null && material.MaterialFriction.Count > 10)
                    {
                        material.MaterialFriction.RemoveRange(10, material.MaterialFriction.Count - 10);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Ошибка загрузки конфига: {ex.Message}");
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
}