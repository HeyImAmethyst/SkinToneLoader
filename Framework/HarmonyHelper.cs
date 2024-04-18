
using HarmonyLib;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SkinToneLoader.Framework.Patches;
using StardewModdingAPI;
using StardewValley;
using StardewValley.Menus;
using System;
using System.Reflection;

namespace SkinToneLoader.Framework
{
    public class HarmonyHelper
    {
        // Instance of Harmony
        private Harmony harmony;

        // The mods entry
        private ModEntry modEntry;

        /// <summary>
        /// Constructor - Used for all Harmony related patching.
        /// </summary>
        /// <param name="entry">The Mod's Entry class.</param>
        public HarmonyHelper(ModEntry entry)
        {
            modEntry = entry;
        }

        /// <summary>
        /// Initializes the Harmony Instance and starts the patches.
        /// </summary>
        public void InitializeAndPatch()
        {
            harmony = new Harmony(modEntry.ModManifest.UniqueID);
            PatchWithHarmony();
        }

        /// <summary>
        /// Harmony patch for accessory length and skin color length.
        /// </summary>
        private void PatchWithHarmony()
        {
            // Patch the skin color length and skin colors in the save menu
            PatchSkinTone();
        }

        /// <summary>
        /// Patches changeSkinColor using a harmony transpiler.
        /// </summary>
        private void PatchSkinTone()
        {
            SkinTonePatch skinColorPatch = new SkinTonePatch(modEntry);
            SaveMenuSkinTonePatch saveMenuSkinTonePatch = new SaveMenuSkinTonePatch(modEntry);

            modEntry.Monitor.Log("Patching changeSkinColor()", LogLevel.Trace);
            modEntry.Monitor.Log("Patching SaveFileSlot", LogLevel.Trace);

            skinColorPatch.Apply(harmony);
            saveMenuSkinTonePatch.Apply(harmony);
        }
    }
}
