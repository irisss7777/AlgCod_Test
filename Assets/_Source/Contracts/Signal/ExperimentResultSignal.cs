namespace Contracts.Signal
{
    public struct ExperimentResultSignal
    {
        public int FirstMaterial { get; private set; }
        public int SecondMaterial { get; private set; }

        public ExperimentResultSignal(int firstMaterial, int secondMaterial)
        {
            FirstMaterial = firstMaterial;
            SecondMaterial = secondMaterial;
        }
    }
}