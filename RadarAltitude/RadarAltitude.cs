using UnityEngine;
using KSP.UI.Screens.Flight;

namespace RadarAltitude
{
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
            else if (radarAlt < 3000)		// Maximum range of RDA-1 in-cockpit instrument
            {
                if (radarAlt > 1000)		// Low resolution between 1000-3000m
                    radarAlt = ((int) radarAlt / 10) * 10;
                else if (radarAlt > 500)	// Reduced resolution between 500-1000m
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
