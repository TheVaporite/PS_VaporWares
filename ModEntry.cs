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
                modItemDirectory.Add("kotton_bolt", Items.CreateKottonBolt);
                modItemDirectory.Add("zilk_bolt", Items.CreateZilkBolt);
                modItemDirectory.Add("zilk_fabric", Items.CreateZilkFabric);
                modItemDirectory.Add("zilk_fiber", Items.CreateZilkFiber);
                modItemDirectory.Add("tshirt", Items.CreateTShirt);
                modItemDirectory.Add("sleeveless_shirt", Items.CreateSleevelessShirt);
                modItemDirectory.Add("autoloom", AutoLoom.CreateAutoLoom);
                modItemDirectory.Add("autoloom_note", AutoLoom.CreateNote);
                modItemDirectory.Add("autotailor", AutoTailor.CreateAutoTailor);
                modItemDirectory.Add("autotailor_note", AutoTailor.CreateNote);
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
                if (RNG.Roll(50)) PlayerStore.Instance.AddDirectSellingItemToTable(DirectoryMaster.Item("kotton_fabric"));
                if (RNG.Roll(50)) PlayerStore.Instance.AddDirectSellingItemToTable(DirectoryMaster.Item("kotton_fabric"));
                if (RNG.Roll(50)) PlayerStore.Instance.AddDirectSellingItemToTable(DirectoryMaster.Item("kotton_fabric"));
                if (RNG.Roll(50)) PlayerStore.Instance.AddDirectSellingItemToTable(DirectoryMaster.Item("kotton_fabric"));
                if (RNG.Roll(50)) PlayerStore.Instance.AddDirectSellingItemToTable(DirectoryMaster.Item("zilk_raw"));
                if (RNG.Roll(50)) PlayerStore.Instance.AddDirectSellingItemToTable(DirectoryMaster.Item("zilk_raw"));
                if (RNG.Roll(50)) PlayerStore.Instance.AddDirectSellingItemToTable(DirectoryMaster.Item("tshirt"));
                if (RNG.Roll(50)) PlayerStore.Instance.AddDirectSellingItemToTable(DirectoryMaster.Item("autoloom"));
                if (RNG.Roll(50)) PlayerStore.Instance.AddDirectSellingItemToTable(DirectoryMaster.Item("autotailor"));
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
                ModHelper.MoveItemToInventory(ModHelper.SpawnItem("kotton_fiber"));
                ModHelper.MoveItemToInventory(ModHelper.SpawnItem("kotton_fiber"));
                ModHelper.MoveItemToInventory(ModHelper.SpawnItem("kotton_fiber"));
                ModHelper.MoveItemToInventory(ModHelper.SpawnItem("kotton_fiber"));
                ModHelper.MoveItemToInventory(ModHelper.SpawnItem("kotton_fiber"));
                ModHelper.MoveItemToInventory(ModHelper.SpawnItem("kotton_fiber"));
                ModHelper.MoveItemToInventory(ModHelper.SpawnItem("kotton_fiber"));
                ModHelper.MoveItemToInventory(ModHelper.SpawnItem("kotton_fiber"));
                ModHelper.MoveItemToInventory(ItemFeatureHelper.SetShabby(ModHelper.SpawnItem("autotailor")));
                ModHelper.MoveItemToInventory(ModHelper.SpawnItem("autotailor_note"));
                ModHelper.MoveItemToInventory(ItemFeatureHelper.SetShabby(ModHelper.SpawnItem("autoloom")));
                ModHelper.MoveItemToInventory(ModHelper.SpawnItem("autoloom_note"));
                ModHelper.AddCredit(-100, false);
            },
            iconKey: "scenario_tailor",          // a sprite from your Textures/Items
            tag: () => "Advanced start",                // optional short line next to the name
            skipIntro: true,
            tagTone: StartingScenario.TagTone.Warning); // Positive (green), Warning (orange), Danger (red)

            #region LootTables
            ModHelper.AddLootEntry("vapor_ware", "dumpingGroundTG", "kotton_fabric", 10);
            ModHelper.AddLootEntry("vapor_ware", "dumpingGroundTG", "zilk_fabric", 1);
            ModHelper.AddLootEntry("vapor_ware", "dumpingGroundTG", "zilk_raw", 5);
            #endregion

            #region Debug Functionality
            ModHelper.AddDebugFunction("vapor_wares", ModHelper.DEBUG_CATEGORY_SPAWN, "Spawn My Items", () =>
            {
                ModHelper.MoveItemToInventory(ModHelper.SpawnItem("kotton_fabric"));
                ModHelper.MoveItemToInventory(ModHelper.SpawnItem("zilk_fabric"));
                ModHelper.MoveItemToInventory(ModHelper.SpawnItem("zilk_raw"));
                ModHelper.MoveItemToInventory(ModHelper.SpawnItem("tshirt"));
                ModHelper.MoveItemToInventory(ModHelper.SpawnItem("sleeveless_shirt"));
                ModHelper.MoveItemToInventory(ModHelper.SpawnItem("autoloom"));
                ModHelper.MoveItemToInventory(ModHelper.SpawnItem("autotailor"));
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
