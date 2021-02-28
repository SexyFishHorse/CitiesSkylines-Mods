namespace SexyFishHorse.CitiesSkylines.Infrastructure.Configuration
{
    using System.Collections.Generic;
    using System.Xml.Serialization;

    public class ModConfiguration
    {
        public ModConfiguration()
        {
            Settings = new List<KeyValuePair<string, object>>();
        }

        [XmlArrayItem("Setting")]
        public List<KeyValuePair<string, object>> Settings { get; set; }
    }
}
