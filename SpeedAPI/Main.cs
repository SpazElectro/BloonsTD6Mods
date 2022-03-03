using BTD_Mod_Helper;
using BTD_Mod_Helper.Api.ModOptions;
using MelonLoader;

using Assets.Scripts.Utils;


[assembly: MelonInfo(typeof(SpeedAPI.Main), "SpeedAPI", "1.0.0", "Steven")]
[assembly: MelonGame("Ninja Kiwi", "BloonsTD6")]
namespace SpeedAPI
{
    public class Main : BloonsTD6Mod
    {
        public static MelonLogger.Instance Logger;
        public static bool descend = false;
        public static double speed = 3;
        public static int slowAmount = 1;
        public static int maxSimulationStepsPerUpdate = 3;
        public static bool slow = false;
        public static readonly ModSettingBool canDescend = false;
        public static readonly ModSettingDouble descendAt = new ModSettingDouble(50)
        {
            displayName = "Max speed (descend at)",
            minValue = 0.0,
            maxValue = 100.0,
            isSlider = true
        };

        public override void OnApplicationStart()
        {
            base.OnApplicationStart();

            Logger.Msg("Started SpeedAPI!");
        }

        public static void SetSpeed(double newSpeed)
        {
            if(!descend) { speed += newSpeed; } else if(descend && canDescend) { speed -= newSpeed; }

            Logger.Msg("Speed: " + speed);
        }

        public override void OnMainMenu()
        {
            base.OnMainMenu();

            descend = false;
            speed = 3;
        }

        public override void OnMatchStart()
        {
            base.OnMatchStart();

            descend = false;
            speed = 3;
        }

        public override void OnRestart()
        {
            base.OnRestart();

            descend = false;
            speed = 3;
        }

        public override void OnUpdate()
        {
            // Code from https://github.com/Timotheeee/btd6_mods/blob/master/speedhackmelon/Main.cs
            base.OnUpdate();

            TimeManager.timeScaleWithoutNetwork = (float) speed;
            TimeManager.networkScale = (float) speed;

            double max;

            if (speed == 3)
            {
                max = 3;
            }
            else
            {
                max = speed * 2;
            }

            if(speed >= (double) descendAt.GetValue()) {
                descend = true;
            }

            if(speed <= (double) descendAt.GetValue() / 2 && descend == true) {
                descend = false;
            }

            TimeManager.maxSimulationStepsPerUpdate = (float) (slow ? slowAmount : max);
        }
    }
}
