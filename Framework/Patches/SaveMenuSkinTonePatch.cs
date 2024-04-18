﻿using HarmonyLib;
using StardewModdingAPI;
using StardewValley.Menus;
using StardewValley;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static StardewValley.Menus.LoadGameMenu;
using Microsoft.Xna.Framework.Graphics;
using System.IO;

namespace SkinToneLoader.Framework.Patches
{
    public class SaveMenuSkinTonePatch
    {
        private readonly Type _menu = typeof(SaveFileSlot);

        // Instance of ModEntry
        private static ModEntry modEntryInstance;

        public SaveMenuSkinTonePatch(ModEntry entry)
        {
            modEntryInstance = entry;
        }

        internal void Apply(Harmony harmony)
        {
            harmony.Patch(
                AccessTools.Constructor(_menu, new[] { typeof(LoadGameMenu), typeof(Farmer) }), 
                postfix: new HarmonyMethod(GetType(), nameof(SaveFileSlotPostfix))
            );
        }

        /// <summary>
        /// A Postfix patch that patches the skin color in the save menu.
        /// </summary>
        /// <param name="__instance">The instance of the SaveFileSlot class</param>
        /// <param name="menu">The LoadGameMenuObject</param>
        /// <param name="farmer">The Farmer Object</param>
        private static void SaveFileSlotPostfix(SaveFileSlot __instance, LoadGameMenu menu, Farmer farmer)
        {
            SkinToneConfigModel model = SkinToneConfigModelManager.ReadCharacterLayout(modEntryInstance, farmer);
            LoadFarmersSkinToneForLoadMenu(farmer, model);
        }

        /// <summary>
        /// Loads the farmer's layout for the save menu.
        /// </summary>
        /// <param name="farmer">The Farmer Object</param>
        /// <param name="configModel">The ConfigModel Object</param>
        public static void LoadFarmersSkinToneForLoadMenu(Farmer farmer, SkinToneConfigModel configModel)
        {
            // Set a particular farmer for the loading screen
            SetLoadScreenFarmerStyle(farmer, configModel);
        }

        /// <summary>
        /// Recolors the farmer's skin color based on the save slot's skin color config.
        /// </summary>
        /// <param name="farmer">The Farmer Object</param>
        /// <param name="configModel">The ConfigModel Object</param>
        private static void SetLoadScreenFarmerStyle(Farmer farmer, SkinToneConfigModel configModel)
        {
            farmer.FarmerRenderer.recolorSkin(configModel.SkinIndex, true);
        }
    }
}
