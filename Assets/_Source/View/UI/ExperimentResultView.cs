using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace View.UI
{
    public class ExperimentResultView : MonoBehaviour
    {
        public Animator Animator => _animator;
        
        public GameObject ResultPanel => _resultPanel;
        public GameObject TrueResult => _trueResult;
        public GameObject FalseResult => _falseResult;
        
        [SerializeField] private Animator _animator;
        
        [Header("Results")]
        [SerializeField] private GameObject _resultPanel;
        [SerializeField] private GameObject _trueResult;
        [SerializeField] private GameObject _falseResult;
    }
}
