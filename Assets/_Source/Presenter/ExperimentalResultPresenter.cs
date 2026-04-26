using View.UI;

namespace Presenter
{
    public class ExperimentalResultPresenter
    {
        private readonly ExperimentResultView _view;

        public ExperimentalResultPresenter(ExperimentResultView view)
        {
            _view = view;
        }

        public void SetResult(bool success)
        {
            var targetObject = success ? _view.TrueResult : _view.FalseResult;
            
            _view.ResultPanel.SetActive(true);
            targetObject.SetActive(true);
        }
    }
}