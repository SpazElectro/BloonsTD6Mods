using BTD_Mod_Helper;
using BTD_Mod_Helper.Api.ModOptions;
using MelonLoader;

using System.Reflection;
using System.Linq;

using Assets.Scripts.Simulation.Towers;
using Assets.Scripts.Models.Towers;

[assembly: MelonInfo(typeof(MoreUpgradeMoreSpeed.Main), "More Upgrade More Speed", "1.0.0", "Steven")]
[assembly: MelonGame("Ninja Kiwi", "BloonsTD6")]
namespace MoreUpgradeMoreSpeed
{
    public class Main : BloonsTD6Mod
    {
        private static MethodInfo SetSpeed = null;

        public static readonly ModSettingDouble speedPerTowerUpgrade = new ModSettingDouble(0.001)
        {
            displayName = "Speed per upgrading the tower",
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

            LoggerInstance.Msg("Started MoreUpgradeMoreSpeed!");
        }

        public override void OnTowerUpgraded(Tower tower, string upgradeName, TowerModel newBaseTowerModel)
        {
            SetGameSpeed((double) speedPerTowerUpgrade.GetValue());
        }

        private static void SetGameSpeed(double newSpeed) =>
            SetSpeed?.Invoke(null, new object[] { newSpeed });
    }
}
