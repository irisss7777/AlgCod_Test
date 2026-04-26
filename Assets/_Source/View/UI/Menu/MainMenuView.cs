using System;
using System.Collections.Generic;
using Contracts.Signal.LoadScene;
using Plugins.MessagePipe.MessageBus.Runtime;
using UnityEngine;
using UnityEngine.UI;
using View.UI.Menu.Labs;
using Zenject;

namespace View.UI.Menu
{
    public class MainMenuView : MonoBehaviour
    {
        [Inject] private readonly MessageBus _messageBus;
        
        [Header("Labs")] [SerializeField] private List<LaboratoryCardView> _laboratoryCardViews;
        
        [Header("Panels")]
        [SerializeField] private GameObject _laboratoryPanel;
        [SerializeField] private GameObject _settingsPanel;
        [SerializeField] private GameObject _infoPanel;
        
        
        [Header("Buttons")]
        [SerializeField] private Button _laboratoryButton;
        [SerializeField] private Button _settingsButton;
        [SerializeField] private Button _infoButton;
        [SerializeField] private Button _exitButton;
        
        [Header("Hover")]
        [SerializeField] private GameObject _laboratoryHover;
        [SerializeField] private GameObject _settingsHover;
        [SerializeField] private GameObject _infoHover;
        [SerializeField] private GameObject _exitHover;

        private void Awake()
        {
            _laboratoryButton.onClick.AddListener(LaboratoryClick);
            _settingsButton.onClick.AddListener(SettingsClick);
            _infoButton.onClick.AddListener(InfoClick);
            _exitButton.onClick.AddListener(ExitClick);

            foreach (var card in _laboratoryCardViews)
                card.StartLaboratory += StartLaboratory;
        }

        private void StartLaboratory(int labId)
        {
            _messageBus.Publish(new LoadLabSignal(labId));
        }

        private void LaboratoryClick()
        {
            _laboratoryHover.SetActive(true);
            _settingsHover.SetActive(false);
            _infoHover.SetActive(false);
            _exitHover.SetActive(false);
            
            _laboratoryPanel.SetActive(true);
            _settingsPanel.SetActive(false);
            _infoPanel.SetActive(false);
        }
        
        private void SettingsClick()
        {
            _laboratoryHover.SetActive(false);
            _settingsHover.SetActive(true);
            _infoHover.SetActive(false);
            _exitHover.SetActive(false);
            
            _laboratoryPanel.SetActive(false);
            _settingsPanel.SetActive(true);
            _infoPanel.SetActive(false);
        }
        
        private void InfoClick()
        {
            _laboratoryHover.SetActive(false);
            _settingsHover.SetActive(false);
            _infoHover.SetActive(true);
            _exitHover.SetActive(false);
            
            _laboratoryPanel.SetActive(false);
            _settingsPanel.SetActive(false);
            _infoPanel.SetActive(true);
        }
        
        private void ExitClick()
        {
            #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
            #else
                Application.Quit();
            #endif
        }

        private void OnDestroy()
        {
            _laboratoryButton.onClick.RemoveListener(LaboratoryClick);
            _settingsButton.onClick.RemoveListener(SettingsClick);
            _infoButton.onClick.RemoveListener(InfoClick);
            _exitButton.onClick.RemoveListener(ExitClick);
            
            foreach (var card in _laboratoryCardViews)
                card.StartLaboratory -= StartLaboratory;
        }
    }
}