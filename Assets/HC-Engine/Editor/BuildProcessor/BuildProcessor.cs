using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using editor;
using Engine;

class BuildProcessor : IPreprocessBuildWithReport
{
    public int callbackOrder { get { return 0; } }
    public void OnPreprocessBuild(BuildReport report)
    {
        AssetsSettings settings = AssetUtility.FindScribtableObjectOfType<AssetsSettings>() ?? throw new System.NullReferenceException("AssetsSettings has a null value!...");

        IBuild[] builds = EditorManager.FindAllAssetsOfType<IBuild>();

        foreach (IBuild build in builds)
        {
            build?.OnBuild();
        }

        if (settings.resetData)
        {
            HeadTemplateEditor.ResetAllData();
        }

        if (settings.validate)
        {
            HeadTemplateEditor.ValidateAll();
        }

        if (settings.saveAssets)
        {
            HeadTemplateEditor.SaveAssets();
        }
    }
}
