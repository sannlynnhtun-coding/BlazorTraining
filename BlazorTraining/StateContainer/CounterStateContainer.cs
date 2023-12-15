namespace BlazorTraining.StateContainer
{
    public class CounterStateContainer
    {
        private int count;

        public int Count
        {
            get => count;
            set
            {
                count = value;
                NotifyStateChanged();
            }
        }

        public event Action? OnChange;

        private void NotifyStateChanged() => OnChange?.Invoke();
    }
}
