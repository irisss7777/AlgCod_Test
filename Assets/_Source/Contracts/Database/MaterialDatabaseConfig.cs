using System;
using System.Collections.Generic;

namespace Contracts.Database
{
    [Serializable]
    public class MaterialDatabaseConfig
    {
        public int MaxAngle;
        public List<MaterialData> Materials;
    }

    [Serializable]
    public class MaterialData
    {
        public List<FrictionData> MaterialFriction;
    }

    [Serializable]
    public class FrictionData
    {
        public float MinValue;
        public float MaxValue;
    }
}