using HarmonyLib;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace VaporWares
{
    /// <summary>
    /// Entry point of the mod. The loader finds the one class implementing IMod, creates it,
    /// applies the [HarmonyPatch] classes of this assembly, then calls Init and OnEnable.
    /// All of that happens in the main menu: no save is loaded yet, so do not touch game
    /// state here. Subscribe to ModHook events instead (see ModdingGuide.md, "Hook Points").
    /// </summary>
    public class ModEntry : IMod
    {
        public static ModEntry Instance { get; private set; }
        public ModManifest Manifest { get; private set; }
        public ModLog Log { get; private set; }
        ConfigEntry<bool> config_allow_undergarments;

        public void Init(ModManifest manifest)
        {
            Instance = this;
            Manifest = manifest;
            Log = new ModLog(manifest);
            Log.Log($"{manifest.Name} v{manifest.ModVersion} loaded.");

            ModConfig config = ModHelper.GetConfig(manifest.ID);

            // Init Items into the modded item directory
            ModHook.OnModItemDirectoryInit += (ModItemDirectory modItemDirectory) =>
            {
                modItemDirectory.Add("vaporwares_kotton_bolt", Items.CreateKottonBolt);
                modItemDirectory.Add("vaporwares_zilk_fiber", Items.CreateZilkFiber);
                modItemDirectory.Add("vaporwares_zilk_fabric", Items.CreateZilkFabric);
                modItemDirectory.Add("vaporwares_zilk_bolt", Items.CreateZilkBolt);
                modItemDirectory.Add("vaporwares_clothing_tshirt", Items.CreateTShirt);
                modItemDirectory.Add("vaporwares_clothing_sleeveless_shirt", Items.CreateSleevelessShirt);
                modItemDirectory.Add("vaporwares_clothing_beanie", Items.CreateBeanie);
                modItemDirectory.Add("vaporwares_clothing_zilk_scarf", Items.CreateZilkScarf);
                modItemDirectory.Add("vaporwares_clothing_zilk_tshirt", Items.CreateZilkTShirt);
                modItemDirectory.Add("vaporwares_autoloom", AutoLoom.CreateAutoLoom);
                modItemDirectory.Add("vaporwares_autoloom_note", AutoLoom.CreateNote);
                modItemDirectory.Add("vaporwares_autotailor", AutoTailor.CreateAutoTailor);
                modItemDirectory.Add("vaporwares_autotailor_note", AutoTailor.CreateNote);
                modItemDirectory.Add("vaporwares_chip_tshirt", Items.CreateTailorChipTShirt);
                modItemDirectory.Add("vaporwares_chip_mass_tshirt", Items.CreateTailorChipMassTShirt);
                modItemDirectory.Add("vaporwares_chip_sleeveless", Items.CreateTailorChipSleevelessShirt);
                modItemDirectory.Add("vaporwares_chip_beanie", Items.CreateTailorChipBeanie);
                modItemDirectory.Add("vaporwares_chip_zilk_tshirt", Items.CreateTailorChipZilkTShirt);
                modItemDirectory.Add("vaporwares_chip_zilk_scarf", Items.CreateTailorChipZilkScarf);
            };

            //Debugging hook
            //ModHook.OnWakingUpLate += ()
            //{
            //    PlayerStore.Instance.AddedItemToMainInvArea(DirectoryMaster.Item("purple_blood_synthesizer"));
            //};

            // Add what dr. jackson might have in stock
            ModHook.OnPlaceInventorInventoryItemLate += (List<GameItem> items) =>
            {
                // 50% chance dr. jackson will have the purple blood synthesizer in stock
                if (RNG.Roll(50)) PlayerStore.Instance.AddDirectSellingItemToTable(DirectoryMaster.Item("vaporwares_chip_tshirt"));
                if (RNG.Roll(50)) PlayerStore.Instance.AddDirectSellingItemToTable(DirectoryMaster.Item("vaporwares_chip_sleeveless"));
                if (RNG.Roll(50)) PlayerStore.Instance.AddDirectSellingItemToTable(DirectoryMaster.Item("vaporwares_chip_beanie"));
                if (RNG.Roll(25)) PlayerStore.Instance.AddDirectSellingItemToTable(DirectoryMaster.Item("vaporwares_chip_zilk_tshirt"));
                if (RNG.Roll(25)) PlayerStore.Instance.AddDirectSellingItemToTable(DirectoryMaster.Item("vaporwares_chip_zilk_scarf"));
                if (RNG.Roll(50)) PlayerStore.Instance.AddDirectSellingItemToTable(DirectoryMaster.Item("vaporwares_autoloom"));
                if (RNG.Roll(50)) PlayerStore.Instance.AddDirectSellingItemToTable(DirectoryMaster.Item("vaporwares_autotailor"));
            };
        }

        public void OnEnable()
        {
            // Runs right after Init, still in the main menu. Register hooks, items, loot edits,
            // options and configuration here. Examples:
            //   ModHook.OnGameLoadedInit += OnGameLoaded;
            //   ModHelper.AddLootEntry(Manifest.ID, "junkTable", "my_item", 10);
            Log.Log("Enabled.");

            //ModHelper.AddNetworkUpgrade("vaporwares", "vaporwares.COURIER",
            //    cost: 100,
            //    subtitle: "Supplier",                          // the card it sits in
            //    title: () => ModHelper.GetLocalized("vaporwares", "network_courier_title"),
            //    description: () => ModHelper.GetLocalized("vaporwares", "network_courier_desc"),
            //    onUnlock: () => courierUnlocked = true,
            //    iconKey: "vaporwares:fabric_courier_icon",              // a sprite from your Textures/Items);

            //// A repeatable upgrade: usable again after the cooldown, like Evidence Removal.
            //ModHelper.AddNetworkUpgrade("vaporwares", "vaporwares.TEMP",
            //    cost: 250, subtitle: "Temp",
            //    title: () => "Loom",
            //    description: () => "Skips the next inspection.",
            //    onUnlock: () => skipNextInspection = true,
            //    cooldownDays: 7,
            //    alreadyBought: () => "The inspector is already paid off.");

            //// Upgrades on your own tab.
            ModHelper.SetNetworkTabLabel("vapor_wares", () => "Vapor Wares");
            //ModHelper.AddNetworkUpgrade("vaporwares", "vaporwares.DRONE", 800, "Logistics",
            //    () => "Delivery drone", () => "Deliveries arrive a day earlier.", () => droneUnlocked = true,
            //    tab: "vaporwares");

            ModHelper.AddStartingScenario("vapor_wares", "vaporwares.tailor",
            title: () => ModHelper.GetLocalized("vapor_wares", "scenario_tailor_title"),
            description: () => ModHelper.GetLocalized("vapor_wares", "scenario_tailor_body"),
            startWith: new System.Func<string>[]
            {
                //() => ModHelper.GetLocalized("vaporwares", "scenario_tailor_gear1"),
                () => "Autotailor and four Fabric.",
            },
            onStart: () =>
            {
                ModHelper.MoveItemToInventory(ModHelper.SpawnItem("energy_credit"));
                ModHelper.MoveItemToInventory(ModHelper.SpawnItem("energy_credit"));
                ModHelper.MoveItemToInventory(ModHelper.SpawnItem("kotton_fiber"));
                ModHelper.MoveItemToInventory(ModHelper.SpawnItem("kotton_fiber"));
                ModHelper.MoveItemToInventory(ModHelper.SpawnItem("kotton_fiber"));
                ModHelper.MoveItemToInventory(ModHelper.SpawnItem("kotton_fiber"));
                ModHelper.MoveItemToInventory(ModHelper.SpawnItem("kotton_fiber"));
                ModHelper.MoveItemToInventory(ModHelper.SpawnItem("kotton_fiber"));
                ModHelper.MoveItemToInventory(ModHelper.SpawnItem("kotton_fiber"));
                ModHelper.MoveItemToInventory(ModHelper.SpawnItem("kotton_fiber"));
                ModHelper.MoveItemToInventory(ModHelper.SpawnItem("vaporwares_kotton_bolt"));
                ModHelper.MoveItemToInventory(ModHelper.SpawnItem("vaporwares_zilk_fiber"));
                ModHelper.MoveItemToInventory(ModHelper.SpawnItem("vaporwares_zilk_fabric"));
                ModHelper.MoveItemToInventory(ModHelper.SpawnItem("vaporwares_zilk_bolt"));
                ModHelper.MoveItemToInventory(ItemFeatureHelper.SetShabby(ModHelper.SpawnItem("vaporwares_autotailor")));
                ModHelper.MoveItemToInventory(ModHelper.SpawnItem("vaporwares_autotailor_note"));
                ModHelper.MoveItemToInventory(ItemFeatureHelper.SetShabby(ModHelper.SpawnItem("vaporwares_autoloom")));
                ModHelper.MoveItemToInventory(ModHelper.SpawnItem("vaporwares_autoloom_note"));
                ModHelper.MoveItemToInventory(ModHelper.SpawnItem("vaporwares_chip_tshirt"));
                ModHelper.MoveItemToInventory(ModHelper.SpawnItem("vaporwares_chip_sleeveless"));
                ModHelper.AddCredit(-100, false);
            },
            iconKey: "vapor_wares:scenario_tailor",          // a sprite from your Textures/Items
            tag: () => "Advanced start",                // optional short line next to the name
            skipIntro: true,
            tagTone: StartingScenario.TagTone.Warning); // Positive (green), Warning (orange), Danger (red)

            #region Debug Functionality
            ModHelper.AddDebugFunction("vapor_wares", ModHelper.DEBUG_CATEGORY_SPAWN, "Spawn My Items", () =>
            {
                ModHelper.MoveItemToInventory(ModHelper.SpawnItem("kotton_fabric"));
                ModHelper.MoveItemToInventory(ModHelper.SpawnItem("vaporwares_kotton_bolt"));
                ModHelper.MoveItemToInventory(ModHelper.SpawnItem("vaporwares_zilk_fiber"));
                ModHelper.MoveItemToInventory(ModHelper.SpawnItem("vaporwares_zilk_fabric"));
                ModHelper.MoveItemToInventory(ModHelper.SpawnItem("vaporwares_zilk_bolt"));
                ModHelper.MoveItemToInventory(ModHelper.SpawnItem("vaporwares_clothing_tshirt"));
                ModHelper.MoveItemToInventory(ModHelper.SpawnItem("vaporwares_clothing_sleeveless_shirt"));
                ModHelper.MoveItemToInventory(ModHelper.SpawnItem("vaporwares_autoloom"));
                ModHelper.MoveItemToInventory(ModHelper.SpawnItem("vaporwares_autotailor"));
            });

            // tall: true gives the button twice the normal height, for the one you press most.
            //ModHelper.AddDebugFunction("my_mod_id", ModHelper.DEBUG_CATEGORY_COMMAND, "Reset My Mod State", ResetState, tall: true);
            #endregion
        }

        public void OnDisable()
        {
            // Runs when the game shuts down. Unsubscribe what you subscribed in OnEnable.
            //   ModHook.OnGameLoadedInit -= OnGameLoaded;
            Log.Log("Disabled.");

            ModHelper.RemoveNetworkUpgrades("vapor_wares");
            ModHelper.RemoveStartingScenarios("vapor_wares");
            ModHelper.RemoveDebugFunctions("vapor_wares");
            ModHelper.RemoveGameOptions("vapor_wares");
        }

        // private void OnGameLoaded()
        // {
        //     // A save is loaded: game systems exist from here on.
        // }
    }
}
