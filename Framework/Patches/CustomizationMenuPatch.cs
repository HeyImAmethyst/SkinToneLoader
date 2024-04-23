using HarmonyLib;
using StardewModdingAPI;
using StardewValley;
using StardewValley.Menus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static StardewValley.Menus.CharacterCustomization;

namespace SkinToneLoader.Framework.Patches
{
    
    /// <summary>
    /// Class that patches the customization menu buttons
    /// </summary>
    public class CustomizationMenuPatch
    {
        private readonly Type _menu = typeof(CharacterCustomization);

        // Instance of ModEntry
        private static ModEntry modEntryInstance;

        /// <summary>
        /// SkinColorPatch Constructor
        /// </summary>
        /// <param name="entry">The instance of ModEntry</param>
        public CustomizationMenuPatch(ModEntry entry)
        {
            // Set the field
            modEntryInstance = entry;
        }

        internal void Apply(Harmony harmony)
        {
            modEntryInstance.Monitor.Log("Patching customization menu", LogLevel.Info);

            harmony.Patch(
                AccessTools.Method(_menu, "selectionClick", new[] { typeof(string), typeof(int) }),
                postfix: new HarmonyMethod(GetType(), nameof(SelectionClickPostfix))
            );
        }

        private static void SelectionClickPostfix(CharacterCustomization __instance, string name, int change, List<ClickableComponent> ___leftSelectionButtons)
        {
            switch (name)
            {
                case "Skin":
                    Game1.player.changeSkinColor((int)Game1.player.skin + change);
                    Game1.playSound("skeletonStep");
                    break;
            }
        }
    }
}
