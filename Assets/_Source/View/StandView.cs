using UnityEngine;

namespace View
{
    public class StandView : MonoBehaviour
    {
        public GameObject RotationObject => _rotationObject;
        public GameObject Cube => _cube;
        public GameObject FallPoint => _fallPoint;
        public Animator AddAngleButtonAnimator => _addAngleButtonAnimator;
        
        [SerializeField] private GameObject _rotationObject;
        [SerializeField] private GameObject _cube;
        [SerializeField] private GameObject _fallPoint;
        [SerializeField] private Animator _addAngleButtonAnimator;
    }
}