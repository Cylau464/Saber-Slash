using System;

namespace Engine
{
    public interface IMakeStarted
    {
        void MakeStarted();
        Action OnStarted { get; set; }
    }

    public interface ILevelStarted
    {
        void LevelStarted();
    }
}
