
namespace Engine.Attribute
{
    [System.AttributeUsage(System.AttributeTargets.Class)]
    public class TemplateSettingsAttribute : System.Attribute
    {
        public string path { get; private set; }
        public string name { get; private set; }

        public TemplateSettingsAttribute(string path, string name)
        {
            this.path = path;
            this.name = name;
        }
    }
}