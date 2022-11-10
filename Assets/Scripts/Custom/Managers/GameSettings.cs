using UnityEngine;

[CreateAssetMenu(fileName = "Game Settings", menuName = "Add/More/Game Settings", order = 1)]
public class GameSettings : ScriptableObject
{
    [Header("Scenes names")]
    [SerializeField] private string _mainSceneName = "Main";
    public string mainSceneName => _mainSceneName;
}
