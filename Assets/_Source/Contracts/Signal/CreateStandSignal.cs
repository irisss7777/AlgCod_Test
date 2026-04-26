namespace Contracts.Signal
{
    public struct CreateStandSignal
    {
        public int MaxAngle { get; private set; }
        public float TargetFriction { get; private set; }

        public CreateStandSignal(int maxAngle, float targetFriction)
        {
            MaxAngle = maxAngle;
            TargetFriction = targetFriction;
        }
    }
}