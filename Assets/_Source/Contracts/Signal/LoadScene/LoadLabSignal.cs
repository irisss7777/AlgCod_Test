namespace Contracts.Signal.LoadScene
{
    public struct LoadLabSignal
    {
        public int LabId { get; private set; }

        public LoadLabSignal(int labId)
        {
            LabId = labId;
        }
    }
}