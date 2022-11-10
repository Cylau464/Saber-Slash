
namespace Main.Level
{
    public interface ILevelsGroup
    {
        int totalLevels { get; }
        Level GetLevelPrefab(int idLevel);
    }
}