using BTD_Mod_Helper;
using BTD_Mod_Helper.Api.ModOptions;
using MelonLoader;

using Assets.Scripts.Utils;

[assembly: MelonInfo(typeof(MoreKillMoreSpeed.Class1), "More Kill More Speed", "1.0.0", "Steven")]
[assembly: MelonGame("Ninja Kiwi", "BloonsTD6")]
namespace MoreKillMoreSpeed
{
    public class Class1 : BloonsTD6Mod
    {
        public static double speed = 3;
        public static int slowAmount = 1;
        public static int maxSimulationStepsPerUpdate = 3;
        public static bool slow = false;
        public static readonly ModSettingDouble speedPerBloonKill = new ModSettingDouble(0.001)
        {
            displayName = "Speed per bloon kill",
            minValue = 0.0,
            maxValue = 100.0,
            isSlider = true
        };

        public override void OnApplicationStart()
        {
            base.OnApplicationStart();

            LoggerInstance.Msg("Started MoreKillMoreSpeed!");
        }

        public override void OnBloonDestroy(Assets.Scripts.Simulation.Bloons.Bloon bloon)
        {
            LoggerInstance.Msg("Speed: " + speed);

            speed += (double) speedPerBloonKill.GetValue();
        }

        public override void OnMainMenu()
        {
            base.OnMainMenu();

            speed = 3;
        }

        public override void OnMatchStart()
        {
            base.OnMatchStart();

            speed = 3;
        }

        public override void OnRestart()
        {
            base.OnRestart();

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

            TimeManager.maxSimulationStepsPerUpdate = (float) (slow ? slowAmount : max);
        }
    }
}
