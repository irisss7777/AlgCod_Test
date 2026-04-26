using System;
using UnityEngine;
using UnityEngine.UI;

namespace View.UI.Menu.Labs
{
    public class LaboratoryCardView : MonoBehaviour
    {
        public event Action<int> StartLaboratory;
        
        [SerializeField] private int _laboratoryId;
        
        [SerializeField] private GameObject _theory;
        
        [Header("Buttons")]
        [SerializeField] private Button _startButton;
        [SerializeField] private Button _theoryOpenButton;
        [SerializeField] private Button _theoryCloseButton;

        private void Awake()
        {
            _startButton.onClick.AddListener(OnClickStartLaboratory);
            _theoryOpenButton.onClick.AddListener(OpenTheory);
            _theoryCloseButton.onClick.AddListener(CloseTheory);
        }

        private void OnClickStartLaboratory()
        {
            StartLaboratory?.Invoke(_laboratoryId);
        }

        private void OpenTheory()
        {
            _theory.SetActive(true);
        }
        
        private void CloseTheory()
        {
            _theory.SetActive(false);
        }

        private void OnDestroy()
        {
            _startButton.onClick.RemoveListener(OnClickStartLaboratory);
            _theoryOpenButton.onClick.RemoveListener(OpenTheory);
            _theoryCloseButton.onClick.RemoveListener(CloseTheory);
        }
    }
}