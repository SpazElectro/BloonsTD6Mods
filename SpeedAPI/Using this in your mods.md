## If you want to use this in your mods
# Disclaimer: you want to tell the user to download the API DLL, put it in their mods folder, so you can use it

# Add a variable declaring the function (private static MethodInfo SetSpeed = null;)
# Create a new function OnApplicationLateStart, add this into it
```
public override void OnApplicationLateStart()
{
    Assembly[] assemblies = System.AppDomain.CurrentDomain.GetAssemblies();
    Assembly speedAPI = assemblies.FirstOrDefault(assembly => assembly.GetName().Name.Equals("SpeedAPI"));

    System.Type mod = speedAPI?.GetType("SpeedAPI.Main");

    SetSpeed = mod?.GetMethod("SetSpeed", new System.Type[] {
        typeof(double)
    });
}
```
# Go to the end of the class, add this
```
private static void SetGameSpeed(double newSpeed) =>
    SetSpeed?.Invoke(null, new object[] { newSpeed });
```
# Now you can use SetGameSpeed to set the game's speed!

# The original game speed changing code was made by Timotheeee, but i modified it. (https://github.com/Timotheeee/btd6_mods/blob/master/speedhackmelon/Main.cs)
