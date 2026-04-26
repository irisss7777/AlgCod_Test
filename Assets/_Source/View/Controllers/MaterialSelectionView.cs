using System;
using UnityEngine;

namespace View.Controllers
{
    public class MaterialSelectionView : MonoBehaviour
    {
        public event Action<bool, int> IsSelected;
        public MeshRenderer MaterialObject => _materialObject;

        [SerializeField] private MeshRenderer _materialObject;
        [SerializeField] private GameObject _selectedObject;
        
        private bool _isFirstMaterial;
        private int _materialIndex;

        public void Initialize(int materialIndex, bool isFirstMaterial)
        {
            _materialIndex = materialIndex;
            _isFirstMaterial = isFirstMaterial;
        }

        public void Select(int material, bool isFirst)
        {
            if(isFirst != _isFirstMaterial)
                return;
            
            if(material != _materialIndex)
                _selectedObject.SetActive(false);
            else
                _selectedObject.SetActive(true);
        }
        
        private void OnMouseDown()
        {
            IsSelected?.Invoke(_isFirstMaterial, _materialIndex);
        }
    }
}