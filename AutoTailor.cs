using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace VaporWares
{
    public class AutoTailor
    {
        public const string AUTO_TAILOR_TAG = "AUTO_TAILOR_TAG";
        public const int BASE_POWER_USAGE = 10;
        public const int BASE_PROCESSING_SPEED = 20;
        public const int TARGET_PROGRESS = 50; // 2 cycles (days) at base speed of 100

        public static GameItem CreateAutoTailor()
        {
            (PixelWindow itemWindow, GameSlotInventory batterySlot, GameGridInventory moduleGrid, GameGridInventory inputGrid, GameSlotInventory chipSlot, GameGridInventory outputGrid, GameSlotInventory noteSlot) = CreateMachineInventoryWindow();

            inputGrid.mayInventoryAddItemFunc = (item, _) =>
            {
                if (GeneralHelper.IsItemOwned(item) == false) return false;
                return item.IsGameItemType("FABRIC") || item.IsGameItemType(TypeHelper.MATERIAL);
            };

            outputGrid.mayInventoryAddItemFunc = (item, _) => item.identifier.Contains("vaporwares_clothing_");

            //noteSlot.mayInventoryAddItemFunc = (item, _) => item.identifier.Equals("postit");
            noteSlot.SetBackgroundFadeSprite("Items/items_tool", "postit");
            noteSlot.TryAcceptAll(CreateNote());
            noteSlot.LockInv();

            chipSlot.mayInventoryAddItemFunc = (GameItem item, GameInventory _) => GeneralHelper.IsItemOwned(item) && item.identifier.Contains("vaporwares_chip_");
            chipSlot.SetBackgroundFadeSprite("vapor_wares", "tailor_chip_basic");

            GameItem machine = ItemDirectory.CreateEmptyItem()
                .SetContentWindow(itemWindow)
                .SetSpriteAndShape("Items/items_tool2", "printer_off");


            machine.name = "Auto Tailor";
            machine.shortDescription = "A machine that turns fabric into clothing.";
            machine.flavorText = "";

            machine.unitValue = 400;

            machine.SetGameItemType(TypeHelper.MACHINE);
            machine.EnableTag(ContainerHelper.CONTAINER_TAG);
            machine.EnableTag("manufacture");
            machine.EnableTag(MachineHelper.STANDARD_MACHINE_TAG);
            machine.EnableTag(AUTO_TAILOR_TAG);

            MachineHelper.SetupBatterySlot(batterySlot, machine);

            MachineryHelper.InitMachinery(machine, BASE_POWER_USAGE);
            MachineProgressHelper.InitProgressTypeMachine(machine, BASE_PROCESSING_SPEED, useDefaultTooltip: false);

            // Production speed bonus from modules is applied at ratio 1 (full effect).
            machine.ModifyTag(MachineHelper.MACHINE_PERFORMANCE_RATIO_INT, state => state.Enable().SetInt(1));

            string[] possibleModule = { ModuleHelper.MODULE_TYPE_UNIVERSAL };
            ContainerHelper.AllowOnlyTaggedItemsOr(moduleGrid, possibleModule, true);

            moduleGrid.onSlotAddItemEndFunc = (addedItem, parentInventory, slot) =>
            {
                ModuleEffectHelper.OnAddedModuleToGrid(addedItem, machine, moduleGrid.childItems);
                if (addedItem.IsTag(ModuleHelper.MODULE_TAG))
                {
                    ModuleHelper.ComputeModuleEffect(moduleGrid, addedItem, machine);
                    ModuleHelper.ApplyBasicModuleEffect(moduleGrid, addedItem, machine);
                }
                MachineryHelper.UpdatePowerUsageProcessTypeMachine(machine);
            };
            moduleGrid.onSlotRemoveItemEndFunc = (removedItem, parentInventory, slot) =>
            {
                ModuleEffectHelper.OnRemovedModuleFromGrid(removedItem, machine, moduleGrid.childItems);
                if (removedItem.IsTag(ModuleHelper.MODULE_TAG))
                {
                    ModuleHelper.ComputeModuleEffect(moduleGrid, removedItem, machine);
                    ModuleHelper.ApplyBasicModuleEffect(moduleGrid, removedItem, machine);
                }
                MachineryHelper.UpdatePowerUsageProcessTypeMachine(machine);
            };

            machine.onCycleEndSlotItemFunc = (item, parentInventory, slot) =>
            {
                int energyUsage = MachineryHelper.GetMachinePowerUsage(item);
                if (MachineHelper.CanPower(item, batterySlot) == false) return;

                var Slotted = chipSlot.childItem.identifier;
                string ResourceConsumed;
                int ResourceCount;
                string ProductionItem;
                int ProductionCount;
                int TargetProgress = 0;
                switch(Slotted)
                {
                    //If this can be moved to the chips, that'd be prefered.
                    case "vaporwares_chip_tshirt":
                        {
                            ResourceConsumed = "kotton_fabric";
                            ResourceCount = 3;
                            ProductionItem = "vaporwares_clothing_tshirt";
                            ProductionCount = 4;
                            TargetProgress = 45;
                            break;
                        }
                    //If this can be moved to the chips, that'd be prefered.
                    case "vaporwares_chip_mass_tshirt":
                        {
                            ResourceConsumed = "vaporwares_kotton_bolt";
                            ResourceCount = 1;
                            ProductionItem = "vaporwares_clothing_tshirt";
                            ProductionCount = 6;
                            TargetProgress = 55;
                            break;
                        }
                    case "vaporwares_chip_sleeveless":
                        {
                            ResourceConsumed = "kotton_fabric";
                            ResourceCount = 3;
                            ProductionItem = "vaporwares_clothing_sleeveless_shirt";
                            ProductionCount = 5;
                            TargetProgress = 35;
                            break;
                        }
                    case "vaporwares_chip_beanie":
                        {
                            ResourceConsumed = "kotton_fabric";
                            ResourceCount = 1;
                            ProductionItem = "vaporwares_clothing_beanie";
                            ProductionCount = 2;
                            TargetProgress = 18;
                            break;
                        }
                    case "vaporwares_chip_zilk_tshirt":
                        {
                            ResourceConsumed = "vaporwares_zilk_fabric";
                            ResourceCount = 1;
                            ProductionItem = "vaporwares_clothing_zilk_tshirt";
                            ProductionCount = 1;
                            TargetProgress = 40;
                            break;
                        }
                    case "vaporwares_chip_zilk_scarf":
                        {
                            ResourceConsumed = "vaporwares_zilk_fabric";
                            ResourceCount = 1;
                            ProductionItem = "vaporwares_clothing_zilk_scarf";
                            ProductionCount = 1;
                            TargetProgress = 24;
                            break;
                        }
                    default:
                        {
                            //No chip, no production.
                            return;
                        }
                }

                GameItem outputTester = DirectoryMaster.Item(ProductionItem);
                bool outputHasRoom = GraphUtils.CanAccept(outputGrid, outputTester);
                outputTester.Destroy();
                if (outputHasRoom == false) return;

                //GameItem fabric = GraphUtils.FindChildType<GameItem>(inputGrid, c => c.identifier.Equals(ResourceConsumed));
                List<GameItem> Materials = new List<GameItem>();
                foreach(var child in inputGrid.childItems)
                {
                    if(child.identifier.Equals(ResourceConsumed))
                    {
                        Materials.Add(child);
                        if (Materials.Count >= ResourceCount)
                        {
                            break;
                        }
                    }
                }


                string state = MachineProgressHelper.GetMachineState(item);

                if (state.Equals(MachineProgressHelper.STATE_READY))
                {
                    if (Materials == null || Materials.Count != ResourceCount) return;

                    PowerHelper.DrawPowerSource(batterySlot.childItem, energyUsage);

                    item.ModifyTag(MachineProgressHelper.MACHINE_STATE_TAG, s => s.Enable().SetString(MachineProgressHelper.STATE_WORKING));
                    item.ModifyTag(MachineProgressHelper.MACHINE_PROGRESS_CURRENT_TAG, s => s.Enable().SetInt(0));
                    item.ModifyTag(MachineProgressHelper.MACHINE_PROGRESS_TARGET_TAG, s => s.Enable().SetInt(TargetProgress));
                    item.SetSpriteAndShape("Items/items_tool2", "printer_on");

                    MachineProgressHelper.ContinueProgressTypeMachine(item);
                    MachineHelper.OnMachineActioned(machine, moduleGrid);
                    inputGrid.LockInv();
                    chipSlot.LockInv();

                    if (MachineProgressHelper.IsProgressTypeMachineFinished(item))
                    {
                        FinishSynthesis(item, Materials, outputGrid, ProductionItem, ProductionCount);
                        inputGrid.UnlockInv();
                        chipSlot.UnlockInv();
                        item.SetSpriteAndShape("Items/items_tool2", "printer_off");
                    }
                }
                else if (state.Equals(MachineProgressHelper.STATE_WORKING))
                {
                    PowerHelper.DrawPowerSource(batterySlot.childItem, energyUsage);

                    MachineProgressHelper.ContinueProgressTypeMachine(item);
                    MachineHelper.OnMachineActioned(machine, moduleGrid);

                    if (MachineProgressHelper.IsProgressTypeMachineFinished(item))
                    {
                        item.SetSpriteAndShape("Items/items_tool2", "printer_off");
                        inputGrid.UnlockInv();
                        chipSlot.UnlockInv();
                        if (Materials == null || Materials.Count != ResourceCount)
                        {
                            MachineProgressHelper.FinishProgressTypeMachine(item);
                            return;
                        }
                        FinishSynthesis(item, Materials, outputGrid, ProductionItem, ProductionCount);
                    }
                }
            };

            return machine;
        }

        public static void FinishSynthesis(GameItem machine, List<GameItem> materials, GameGridInventory outputGrid, string ProductionItem, int ProductionCount)
        {
            MachineProgressHelper.FinishProgressTypeMachine(machine);
            foreach(var mat in materials)
            {
                mat.Destroy();
            }
            for (int i = 0; i < ProductionCount; i++)
            {
                GraphUtils.TryAcceptAll(outputGrid, DirectoryMaster.Item(ProductionItem));
            }
        }

        public static (PixelWindow, GameSlotInventory, GameGridInventory, GameGridInventory, GameSlotInventory, GameGridInventory, GameSlotInventory) CreateMachineInventoryWindow(bool isDraggable = true, bool isCentered = true, int minWidth = 5)
        {
            PixelWindow window = new PixelWindow(isDraggable, "Auto Tailor", WindowStyle.Machinery);
            GridPixelElement mainGrid = new GridPixelElement(6, 2, isCentered);

            GameSlotInventory batterySlot = new GameSlotInventory();
            GameGridInventory moduleGrid = new GameGridInventory(MachineHelper.DEFAULT_MODULE_BAY_CONFIG, MachineHelper.DEFAULT_MODULE_BAY_WIDTH);
            GameGridInventory inputGrid = new GameGridInventory(3, 4);
            GameSlotInventory chipSlot = new GameSlotInventory();
            GameGridInventory outputGrid = new GameGridInventory(4, 4);
            GameSlotInventory noteSlot = new GameSlotInventory();

            TagElement batteryLabel = new TagElement().SetText("Battery");
            TagElement moduleLabel = new TagElement().SetText("Modules");
            TagElement inputLabel = new TagElement().SetText("Fabric");
            TagElement chipLabel = new TagElement().SetText("Chip");
            TagElement outputLabel = new TagElement().SetText("Output");
            TagElement noteLabel = new TagElement().SetText("Note");

            mainGrid.Attach(batteryLabel, 0, 0, minimumHeight: 20, minimumWidth: 20);
            mainGrid.Attach(moduleLabel, 1, 0, minimumHeight: 20, minimumWidth: 20);
            mainGrid.Attach(inputLabel, 2, 0, minimumHeight: 20, minimumWidth: minWidth);
            mainGrid.Attach(chipLabel, 3, 0, minimumHeight: 20, minimumWidth: minWidth);
            mainGrid.Attach(outputLabel, 4, 0, minimumHeight: 20, minimumWidth: 5);
            mainGrid.Attach(noteLabel, 5, 0, minimumHeight: 5, minimumWidth: 5);

            mainGrid.Attach(batterySlot, 0, 1, minimumHeight: 50, minimumWidth: 20);
            mainGrid.Attach(moduleGrid, 1, 1, minimumHeight: 50, minimumWidth: 20);
            mainGrid.Attach(inputGrid, 2, 1, minimumHeight: 50, minimumWidth: 100);
            mainGrid.Attach(chipSlot, 3, 1, minimumHeight: 50, minimumWidth: 20);
            mainGrid.Attach(outputGrid, 4, 1, minimumHeight: 100, minimumWidth: 100);
            mainGrid.Attach(noteSlot, 5, 1, minimumHeight: 5, minimumWidth: 5);

            window.Attach(mainGrid);

            return (window, batterySlot, moduleGrid, inputGrid, chipSlot, outputGrid, noteSlot);
        }

        public static GameItem CreateNote()
        {
            GameItem item = DirectoryMaster.Item("postit", isOwned: true);

            item.name = "AutoTailor - Instructions";
            item.shortDescription = "Place a battery in the power slot and load Fabric into the input row. Each synthesis cycle consumes 2 Fabric and runs for 2 days, producing 1 T-Shirt in the output grid.\n\nUniversal modules can be installed in the module bay to improve power efficiency, production speed, and quality bonuses.";

            item.SetGameItemType(TypeHelper.DOCUMENT);
            item.EnableTag(GeneralHelper.IMPORTANT_TAG);
            item.unitValue = 0;

            return item;
        }
    }
}
