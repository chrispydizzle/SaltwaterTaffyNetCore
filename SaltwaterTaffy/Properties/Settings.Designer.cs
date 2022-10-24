namespace SaltwaterTaffy.Properties
{
    internal sealed partial class Settings
    {

        private static Settings defaultInstance = new Settings();

        public static Settings Default
        {
            get { return defaultInstance; }
        }
        
        
        public string NmapPath { get; set; }
    }
}
