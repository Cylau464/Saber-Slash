using System;

namespace Engine
{
    public interface IMakeFailed
    {
        void MakeFailed(int progress, string reason);
        Action OnFailed { get; set; }
    }

    public interface ILevelFailed
    {
        void LevelFailed();
    }
}
