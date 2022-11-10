using System;

namespace Engine
{
    public interface ILevelCompleted
    {
        void LevelCompleted();
    }

    public interface IMakeCompleted
    {
        void MakeCompleted();
        Action OnCompleted { get; set; }
    }
}
