using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace View.Controllers
{
    public class ChoosePlatformsController : MonoBehaviour
    {
        public event Action<int, int> OnSelected;
        public Transform FirstPosition => _firstPosition;
        public Transform SecondPosition => _secondPosition;

        [SerializeField] private GameObject _cameraObject;
        [SerializeField] private GameObject _targetCameraPosition;
        [SerializeField] private Button _tryChoose;
        
        [Header("Start positions")]
        [SerializeField] private Transform _firstPosition;
        [SerializeField] private Transform _secondPosition;

        private int _firstMaterial = -1;
        private int _secondMaterial = -1;
        private List<MaterialSelectionView> _materialSelectionViews;

        public void Initialize(List<MaterialSelectionView> materialSelectionViews)
        {
            _materialSelectionViews = materialSelectionViews;

            foreach (var materialSelection in _materialSelectionViews)
                materialSelection.IsSelected += SelectMaterial;
            
            _tryChoose.onClick.AddListener(FinalizeChoose);
            _tryChoose.interactable = false;
        }

        private void SelectMaterial(bool isFirst, int index)
        {
            if (isFirst)
                _firstMaterial = index;
            else
                _secondMaterial = index;

            if (_firstMaterial != -1 && _secondMaterial != -1)
                _tryChoose.interactable = true;

            foreach (var material in _materialSelectionViews)
                material.Select(index, isFirst);
        }

        private void FinalizeChoose()
        {
            OnSelected?.Invoke(_firstMaterial, _secondMaterial);
            
            foreach (var materialSelection in _materialSelectionViews)
                materialSelection.IsSelected -= SelectMaterial;

            _cameraObject.transform.DOMove(_targetCameraPosition.transform.position, 1f);
        }

        private void OnDestroy()
        {
            _tryChoose.onClick.AddListener(FinalizeChoose);
            
            foreach (var materialSelection in _materialSelectionViews)
                materialSelection.IsSelected -= SelectMaterial;
        }
    }
}