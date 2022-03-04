using BTD_Mod_Helper;
using BTD_Mod_Helper.Api.ModOptions;
using MelonLoader;
using UnityEngine;

using System.Reflection;
using System.Linq;

using HarmonyLib;

using Assets.Scripts.Unity.UI_New.Popups;
using Assets.Scripts.Simulation.Towers;
using Assets.Scripts.Models.Towers;

[assembly: MelonInfo(typeof(MoreUpgradeMoreSpeed.Main), "More Upgrade More Speed", "1.0.0", "Steven")]
[assembly: MelonGame("Ninja Kiwi", "BloonsTD6")]
namespace MoreUpgradeMoreSpeed
{
    [HarmonyPatch]
    public class Main : BloonsTD6Mod
    {
        private static MethodInfo SetSpeed = null;

        public static readonly ModSettingDouble speedPerTowerUpgrade = new ModSettingDouble(0.1)
        {
            displayName = "Speed per upgrading the tower",
            minValue = 0.0,
            maxValue = 100.0,
            isSlider = true
        };

        public static string myDiscordFull = "susgus#2407";
        public static string speedAPIDownloadURL = "https://github.com/StevenRafft/BloonsTD6Mods/raw/main/SpeedAPI/bin/Debug/net4.7.2/SpeedAPI.dll";

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

        public static void WhyTheHellIsItNotWorking() {
            bool setSpeedExists = true;

            if(SetSpeed == null)
            {
                setSpeedExists = false;
            }

            PopupScreen.instance.ShowOkPopup("Send your MelonLoader/Latest.log file and this (" + speedPerTowerUpgrade.GetValue() + " | " + setSpeedExists + ") to me in discord " + myDiscordFull);
        }
        
        public static void DownloadSpeedAPI() {
            Application.OpenURL(speedAPIDownloadURL);
            Application.Quit();
        }

        [HarmonyPatch(typeof(PopupScreen), nameof(PopupScreen.Awake))]
        [HarmonyPostfix]
        public static void OnGameScreen() {
            if(SetSpeed == null)
            {
                PopupScreen.instance.ShowPopup(
                    placement: PopupScreen.Placement.menuCenter,
                    title: "SpeedAPI failed to load",
                    body: "SpeedAPI has failed to load, Are you sure that you have installed it?",
                    okCallback: new System.Action(() => WhyTheHellIsItNotWorking()),
                    okString: "Yes",
                    cancelString: "Install",
                    cancelCallback: new System.Action(() => DownloadSpeedAPI()),
                    transition: Popup.TransitionAnim.Scale
                );
            }
        }

        private static void SetGameSpeed(double newSpeed) =>
            SetSpeed?.Invoke(null, new object[] { newSpeed });
    }
}
