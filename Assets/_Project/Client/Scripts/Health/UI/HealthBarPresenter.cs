namespace TurnBasedGame.Client
{
    public class HealthBarPresenter
    {
        private readonly HealthBarView _view;
        private readonly Health _health;

        public HealthBarPresenter(HealthBarView view, Health health)
        {
            _view = view;
            _health = health;
        }

        public void Enable()
        {
            _health.CurrentHealthChanged += OnCurrentHealthChanged;
        }

        public void Disable()
        {
            _health.CurrentHealthChanged -= OnCurrentHealthChanged;
        }

        private void OnCurrentHealthChanged()
        {
            _view.SetHealth(_health.CurrentHealth, _health.MaxHealth);
        }
    }
}
