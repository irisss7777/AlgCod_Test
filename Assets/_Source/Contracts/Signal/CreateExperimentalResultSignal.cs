namespace Contracts.Signal
{
    public struct CreateExperimentalResultSignal
    {
        public int FirstMaterial { get; private set; }
        public int SecondMaterial { get; private set; }

        public CreateExperimentalResultSignal(int firstMaterial, int secondMaterial)
        {
            FirstMaterial = firstMaterial;
            SecondMaterial = secondMaterial;
        }
    }
}