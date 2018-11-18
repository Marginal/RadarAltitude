using UnityEngine;
using KSP.UI.Screens.Flight;

namespace RadarAltitude
{
    static class RDA1
    {
        public const double maxRange = 3000;	// Maximum range of RDA-1 in-cockpit instrument
        public const double mediumRez = 1000;	// Medium resolution between 1000m and 500m
        public const double highRez = 500;	// Highest resolution below 500m
    }

    [KSPAddon(KSPAddon.Startup.Flight, false)]
    public class RadarAltitude : MonoBehaviour
    {
        public AltitudeTumbler altTumbler;
        public Color32 radarColour = new Color32(165, 87, 12, 255);	// NavBall ground colour

        public void Start()
        {
            altTumbler = FindObjectOfType<AltitudeTumbler>();
        }

        public void Update()
        {
            double radarAlt = FlightGlobals.ActiveVessel.radarAltitude;

            if (radarAlt <= 0)			// Splashed or crashed
            {
                altTumbler.tumbler.SetColor(Color.black);
            }
            else if (radarAlt < RDA1.maxRange)		// Maximum range of RDA-1 in-cockpit instrument
            {
                if (radarAlt > RDA1.mediumRez)		// Low resolution between 1000-3000m
                    radarAlt = ((int) radarAlt / 10) * 10;
                else if (radarAlt > RDA1.highRez)	// Reduced resolution between 500-1000m
                    radarAlt = ((int) radarAlt / 5) * 5;

                altTumbler.tumbler.SetValue(radarAlt);
                altTumbler.tumbler.SetColor(radarColour);
            }
            else
            {
                altTumbler.tumbler.SetColor(Color.black);
            }
        }
    }
}
