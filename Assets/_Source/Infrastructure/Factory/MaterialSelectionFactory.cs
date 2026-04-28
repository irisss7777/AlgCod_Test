using System.Collections.Generic;
using Contracts.Database;
using Infrastructure.Database;
using UnityEngine;
using View.Controllers;
using Zenject;

namespace Infrastructure.Factory
{
    public class MaterialSelectionFactory
    {
        [Inject] private readonly IMaterialDatabase _materialDatabase;
        [Inject] private readonly ChoosePlatformsController _choosePlatformsController;

        private const float ColorShift = 0.3f;

        public void CreateMaterialSelection()
        {
            int staticCount = _materialDatabase.Materials.Count;
            int dynamicCount = _materialDatabase.Materials[0].MaterialFriction.Count;

            float offset = 0.1f;
            var spawnedViews = new List<MaterialSelectionView>();

            Transform firstParent = _choosePlatformsController.FirstPosition;
            Transform secondParent = _choosePlatformsController.SecondPosition; 

            for (int i = 0; i < staticCount; i++)
            {
                Vector3 pos = firstParent.position + -Vector3.forward * i * offset;
                MaterialSelectionView view = Object.Instantiate(_materialDatabase.StaticMaterialPrefab as MaterialSelectionView, pos, Quaternion.identity, firstParent);
                view.Initialize(i, true);
                spawnedViews.Add(view);
                ApplyRandomColorShift(view);
            }

            for (int i = 0; i < dynamicCount; i++)
            {
                Vector3 pos = secondParent.position + -Vector3.forward * i * offset;
                MaterialSelectionView view = Object.Instantiate(_materialDatabase.DynamicMaterialPrefab as MaterialSelectionView, pos, Quaternion.identity, secondParent);
                view.Initialize(i, false);
                spawnedViews.Add(view);
                ApplyRandomColorShift(view);
            }

            _choosePlatformsController.Initialize(spawnedViews);
        }
        
        private void ApplyRandomColorShift(MaterialSelectionView view)
        {
            Material mat = view.MaterialObject.material; 
            Color color = mat.color;

            color.r += Random.Range(-ColorShift, ColorShift);
            color.g += Random.Range(-ColorShift, ColorShift);
            color.b += Random.Range(-ColorShift, ColorShift);
            
            color = new Color(
                Mathf.Clamp01(color.r),
                Mathf.Clamp01(color.g),
                Mathf.Clamp01(color.b),
                color.a
            );
            mat.color = color;
        }
    }
}