using System;

namespace Engine
{
    public interface IMakeContinued
    {
        void MakeContinued();
        Action OnContinued { get; set; }
    }

    public interface ILevelContinued
    {
        void LevelContinued();
    }
}
