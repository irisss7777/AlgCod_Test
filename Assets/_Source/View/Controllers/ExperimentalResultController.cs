using System;
using Contracts.Database;
using Contracts.Signal;
using Plugins.MessagePipe.MessageBus.Runtime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using View.UI;
using Zenject;

namespace View.Controllers
{
    [RequireComponent(typeof(ExperimentResultView))]
    public class ExperimentalResultController : MonoBehaviour
    {
        [Inject] private readonly MessageBus _messageBus;
        
        [Header("Buttons")]
        [SerializeField] private Button _openPaperButton;
        [SerializeField] private Button _closePaperButton;
        [SerializeField] private Button _getResultButton;

        [Header("Fields")] 
        [SerializeField] private TMP_InputField _firstMaterialInputField;
        [SerializeField] private TMP_InputField _secondMaterialInputField;
        
        private ExperimentResultView _view;

        private void Awake()
        {
            _view = GetComponent<ExperimentResultView>();
            
            _openPaperButton.onClick.AddListener(OpenPaper);
            _closePaperButton.onClick.AddListener(ClosePaper);
            _getResultButton.onClick.AddListener(GetResult);
        }

        private void OpenPaper()
        {
            _openPaperButton.gameObject.SetActive(false);
            _closePaperButton.gameObject.SetActive(true);
            
            _view.Animator.SetBool("IsOpen", true);
        }
        
        private void ClosePaper()
        {
            _openPaperButton.gameObject.SetActive(true);
            _closePaperButton.gameObject.SetActive(false);
            
            _view.Animator.SetBool("IsOpen", false);
        }

        private void GetResult()
        {
            if(_firstMaterialInputField.text == "" || _secondMaterialInputField.text == "")
                return;
            
            try
            {
                int firstMaterial = int.Parse(_firstMaterialInputField.text);
                int secondMaterial = int.Parse(_secondMaterialInputField.text);
            
                _messageBus.Publish(new ExperimentResultSignal(firstMaterial, secondMaterial));
            }
            catch(Exception ex)
            {
            }
        }

        private void OnDestroy()
        {
            _openPaperButton.onClick.RemoveListener(OpenPaper);
            _closePaperButton.onClick.RemoveListener(ClosePaper);
            _getResultButton.onClick.RemoveListener(GetResult);
        }
    }
}