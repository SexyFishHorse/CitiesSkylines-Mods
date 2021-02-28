namespace SexyFishHorse.CitiesSkylines.Ragnarok.Configuration
{
    public class DisasterSettingKeys
    {
        public DisasterSettingKeys(string disasterName)
        {
            AutoEvacuate = string.Format("AutoEvacuate{0}", disasterName);
            Disable = string.Format("Disable{0}", disasterName);
            MaxIntensity = string.Format("MaxIntensity{0}", disasterName);
            Probability = string.Format("Probability{0}", disasterName);
        }

        public string AutoEvacuate { get; private set; }

        public string Disable { get; private set; }

        public string MaxIntensity { get; private set; }

        public string Probability { get; private set; }
    }
}
