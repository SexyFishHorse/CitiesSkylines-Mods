﻿namespace SexyFishHorse.CitiesSkylines.Birdcage.Helpers
{
    using System.Reflection;
    using ColossalFramework.UI;

    public static class ChirperUtils
    {
        public static void CollapseChirperInstantly()
        {
            ChirpPanel.instance.Collapse();
            if (ChirpPanel.instance == null)
            {
                return;
            }

            if (ChirpPanel.instance.isShowing == false)
            {
                return;
            }

            var chirpField = ChirpPanel.instance
                .GetType()
                .GetField("m_Chirps", BindingFlags.NonPublic | BindingFlags.Instance);

            if (chirpField != null)
            {
                var panel = (UIPanel)chirpField.GetValue(ChirpPanel.instance);
                panel.Hide();
            }
            else if (ChirpPanel.instance != null)
            {
                ChirpPanel.instance.Hide();
            }
        }
    }
}
