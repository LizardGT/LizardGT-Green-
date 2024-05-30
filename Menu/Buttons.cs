using StupidTemplate.Classes;
using StupidTemplate.Mods;
using static StupidTemplate.Settings;

namespace StupidTemplate.Menu
{
    internal class Buttons
    {
        public static ButtonInfo[][] buttons = new ButtonInfo[][]
        {
            new ButtonInfo[] { // Main Mods
                new ButtonInfo { buttonText = "Settings",  method =() => Settings.SettingsPage(), isTogglable = false, toolTip="Go to Settings"},
                new ButtonInfo { buttonText = "Simple mods",  method =() => Settings.SimplePage(), isTogglable = false, toolTip="Go to Simple mods"}
            },

            new ButtonInfo[] { // Settings
                new ButtonInfo { buttonText = "Return to Main", method =() => Global.ReturnHome(), isTogglable = false, toolTip = "Returns to the main page of the menu."},
                new ButtonInfo { buttonText = "Menu", method =() => SettingsMods.MenuSettings(), isTogglable = false, toolTip = "Opens the settings for the menu."},
                new ButtonInfo { buttonText = "Movement", method =() => SettingsMods.MovementSettings(), isTogglable = false, toolTip = "Opens the movement settings for the menu."},
                new ButtonInfo { buttonText = "Projectile", method =() => SettingsMods.ProjectileSettings(), isTogglable = false, toolTip = "Opens the projectile settings for the menu."},
                 new ButtonInfo { buttonText = "Return to Settings", method =() => SettingsMods.EnterSettings(), isTogglable = false, toolTip = "Returns to the main settings page for the menu."},
                new ButtonInfo { buttonText = "Right Hand", enableMethod =() => SettingsMods.RightHand(), disableMethod =() => SettingsMods.LeftHand(), toolTip = "Puts the menu on your right hand."},
                new ButtonInfo { buttonText = "Notifications", enableMethod =() => SettingsMods.EnableNotifications(), disableMethod =() => SettingsMods.DisableNotifications(), enabled = !disableNotifications, toolTip = "Toggles the notifications."},
                new ButtonInfo { buttonText = "FPS Counter", enableMethod =() => SettingsMods.EnableFPSCounter(), disableMethod =() => SettingsMods.DisableFPSCounter(), enabled = fpsCounter, toolTip = "Toggles the FPS counter."},
                new ButtonInfo { buttonText = "Disconnect Button", enableMethod =() => SettingsMods.EnableDisconnectButton(), disableMethod =() => SettingsMods.DisableDisconnectButton(), enabled = disconnectButton, toolTip = "Toggles the disconnect button."},
            },

            new ButtonInfo[]{//Simple Mods
                 new ButtonInfo { buttonText = "Return to Settings", method =() => SettingsMods.EnterSettings(), isTogglable = false, toolTip = "Returns to the main settings page for the menu."},
                new ButtonInfo { buttonText = "Set Master [Auto]", method =() => Simple.AutoSetMaster(), toolTip = "Description"},
                new ButtonInfo { buttonText = "Fast Master", method =() => Simple.FastMaster(), toolTip = "Description"},
                new ButtonInfo { buttonText = "Anti-Report [Disconnect]", method =() => Simple.AntiReportDisconnect(), toolTip = "Description"},
                new ButtonInfo { buttonText = "Anti Tag", method =() => Simple.AntiTag(), toolTip = "Description"},
                new ButtonInfo { buttonText = "Grip Tag Aura [BETA]", method =() => Simple.GripTagAuraBETA(), toolTip = "Description"},
                new ButtonInfo { buttonText = "Crash All", method =() => Simple.CrashAll(), toolTip = "Description"},
                new ButtonInfo { buttonText = "Untag [All]", method =() => Simple.UntagAll(), toolTip = "Description"},
                new ButtonInfo { buttonText = "Untag [Self]", method =() => Simple.UntagSelf(), toolTip = "Description"},
                new ButtonInfo { buttonText = "ESP", method =() => Simple.ESP(), toolTip = "Description"},
                new ButtonInfo { buttonText = "Box [ESP]", method =() => Simple.BoxESP(), toolTip = "Description"},
                new ButtonInfo { buttonText = "Grab Rig", method =() => Simple.GrabRig(), toolTip = "Description"},
                new ButtonInfo { buttonText = "Rig Gun", method =() => Simple.RigGun(), toolTip = "Description"},
                new ButtonInfo { buttonText = "SKIBIDY", method =() => Simple.SKIDIBI(), toolTip = "Description"},
                new ButtonInfo { buttonText = "Platforms", method =() => Simple.Platforms(), toolTip = "Description"},
                new ButtonInfo { buttonText = "NoClip", method =() => Simple.NoClip(), toolTip = "Description"},
                new ButtonInfo { buttonText = "Fly [RH]", method =() => Simple.Fly(), toolTip = "Description"},
                new ButtonInfo { buttonText = "Fly [left hand]", method =() => Simple.LeftFly(), toolTip = "Description"},
                new ButtonInfo { buttonText = "Fly [LT]", method =() => Simple.LeftTriggerFly(), toolTip = "Description"},
                new ButtonInfo { buttonText = "Tag on Join", method =() => Simple.TagOnJoin(), toolTip = "Description"},
                new ButtonInfo { buttonText = "TP Gun", method =() => Simple.TpGun(), toolTip = "Description"},
                new ButtonInfo { buttonText = "Fly [RT]", method =() => Simple.TriggerFly(), toolTip = "Description"},
            },

            new ButtonInfo[] { // Menu Settings
                new ButtonInfo { buttonText = "Return to Settings", method =() => SettingsMods.EnterSettings(), isTogglable = false, toolTip = "Returns to the main settings page for the menu."},
                new ButtonInfo { buttonText = "Right Hand", enableMethod =() => SettingsMods.RightHand(), disableMethod =() => SettingsMods.LeftHand(), toolTip = "Puts the menu on your right hand."},
                new ButtonInfo { buttonText = "Notifications", enableMethod =() => SettingsMods.EnableNotifications(), disableMethod =() => SettingsMods.DisableNotifications(), enabled = !disableNotifications, toolTip = "Toggles the notifications."},
                new ButtonInfo { buttonText = "FPS Counter", enableMethod =() => SettingsMods.EnableFPSCounter(), disableMethod =() => SettingsMods.DisableFPSCounter(), enabled = fpsCounter, toolTip = "Toggles the FPS counter."},
                new ButtonInfo { buttonText = "Disconnect Button", enableMethod =() => SettingsMods.EnableDisconnectButton(), disableMethod =() => SettingsMods.DisableDisconnectButton(), enabled = disconnectButton, toolTip = "Toggles the disconnect button."},
            },

            new ButtonInfo[] { // Movement Settings
                new ButtonInfo { buttonText = "Return to Settings", method =() => SettingsMods.EnterSettings(), isTogglable = false, toolTip = "Returns to the main settings page for the menu."},
            },

            new ButtonInfo[] { // Projectile Settings
                new ButtonInfo { buttonText = "Return to Settings", method =() => SettingsMods.MenuSettings(), isTogglable = false, toolTip = "Opens the settings for the menu."},
            },
        };
    }
}
