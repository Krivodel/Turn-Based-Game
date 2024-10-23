using System;

namespace TurnBasedGame.Client
{
    public class BattlePresenter
    {
        private readonly BattleView _view;
        private readonly IBattleNetwork _battleNetwork;

        public BattlePresenter(BattleView view, IBattleNetwork battleNetwork)
        {
            _view = view;
            _battleNetwork = battleNetwork;
        }

        public void Enable()
        {
            _view.RestartClicked += OnRestartClicked;
        }

        public void Disable()
        {
            _view.RestartClicked -= OnRestartClicked;
        }

        private void OnRestartClicked()
        {
            throw new NotImplementedException();
        }
    }
}
