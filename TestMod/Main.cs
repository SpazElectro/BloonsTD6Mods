using BTD_Mod_Helper;
using MelonLoader;

using UnityEngine;

[assembly: MelonInfo(typeof(TestMod.Main), "Test Mod", "1.0.0", "Steven")]
[assembly: MelonGame("Ninja Kiwi", "BloonsTD6")]
namespace TestMod
{
    public class Main : BloonsTD6Mod
    {
        public double speed = 1.12345678999999999;

        public override void OnApplicationStart()
        {
            base.OnApplicationStart();

            LoggerInstance.Msg("Started TestMod!");
        }

        public override void OnGUI()
        {
            GUI.Label(new Rect(500, 100, 500, 500), "Speed: " + speed.ToString());

            speed += 0.001;
            
            base.OnGUI();
        }
    }

}
