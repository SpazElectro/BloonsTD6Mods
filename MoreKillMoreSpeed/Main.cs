using BTD_Mod_Helper;
using BTD_Mod_Helper.Api.ModOptions;
using MelonLoader;

using System.Reflection;
using System.Linq;

[assembly: MelonInfo(typeof(MoreKillMoreSpeed.Main), "More Kill More Speed", "1.0.0", "Steven")]
[assembly: MelonGame("Ninja Kiwi", "BloonsTD6")]
namespace MoreKillMoreSpeed
{
    public class Main : BloonsTD6Mod
    {
        private static MethodInfo SetSpeed = null;

        public static readonly ModSettingDouble speedPerBloonKill = new ModSettingDouble(0.001)
        {
            displayName = "Speed per bloon kill",
            minValue = 0.0,
            maxValue = 100.0,
            isSlider = true
        };

        public override void OnApplicationLateStart()
        {

            Assembly[] assemblies = System.AppDomain.CurrentDomain.GetAssemblies();
            Assembly speedAPI = assemblies.FirstOrDefault(assembly => assembly.GetName().Name.Equals("SpeedAPI"));

            System.Type mod = speedAPI?.GetType("SpeedAPI.Main");

            SetSpeed = mod?.GetMethod("SetSpeed", new System.Type[] {
                typeof(double)
            });
        }

        public override void OnApplicationStart()
        {
            base.OnApplicationStart();

            LoggerInstance.Msg("Started MoreKillMoreSpeed!");
        }

        public override void OnBloonDestroy(Assets.Scripts.Simulation.Bloons.Bloon bloon)
        {
            SetGameSpeed((double) speedPerBloonKill.GetValue());
        }

        private static void SetGameSpeed(double newSpeed) =>
            SetSpeed?.Invoke(null, new object[] { newSpeed });
    }
}
