using View;
using DG.Tweening;
using UnityEngine;

namespace Presenter
{
    public class StandPresenter
    {
        private readonly StandView _standView;
        
        private Tween _moveTween;

        public StandPresenter(StandView standView)
        {
            _standView = standView;
        }

        public void SetAngle(int angle, bool canMove)
        {
            _standView.RotationObject.transform.DORotate(
                new Vector3(-25 + angle,
                    _standView.RotationObject.transform.eulerAngles.y,
                    _standView.RotationObject.transform.eulerAngles.z),
                0.5f);
            
            _standView.AddAngleButtonAnimator.SetTrigger("Click");
            
            if (canMove && _moveTween == null)
                StartMove();
        }

        private void StartMove()
        {
            _moveTween = _standView.Cube.transform.DOLocalMove(Vector3.zero, 10f);
        }
    }
}