namespace Contracts.Signal
{
    public struct OnAngleChangeSignal
    {
        public int Angle { get; private set; }
        public int MaxAngle { get; private set; }

        public OnAngleChangeSignal(int angle, int maxAngle)
        {
            Angle = angle;
            MaxAngle = maxAngle;
        }
    }
}