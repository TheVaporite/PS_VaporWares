using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace VaporWares
{
    public class AutoLoom
    {
        public const string AUTOLOOM_TAG = "AUTOLOOM_TAG";
        public const int BASE_POWER_USAGE = 6;
        public const int BASE_PROCESSING_SPEED = 20;
        public static GameItem CreateAutoLoom()
        {
            (PixelWindow itemWindow, GameSlotInventory batterySlot, GameGridInventory moduleGrid, GameGridInventory inputGrid, GameGridInventory outputGrid, GameSlotInventory noteSlot) = CreateMachineInventoryWindow();

            inputGrid.mayInventoryAddItemFunc = (item, _) =>
            {
                if (GeneralHelper.IsItemOwned(item) == false) return false;
                return item.IsGameItemType(TypeHelper.MATERIAL);
            };

            outputGrid.mayInventoryAddItemFunc = (item, _) => item.identifier.Equals("kotton_fabric") || item.identifier.Equals("vaporwares_zilk_fabric")
                                                            || item.identifier.Equals("vaporwares_kotton_bolt") || item.identifier.Equals("vaporwares_zilk_bolt");

            //noteSlot.mayInventoryAddItemFunc = (item, _) => item.identifier.Equals("postit");
            noteSlot.SetBackgroundFadeSprite("Items/items_tool", "postit");


            GameItem machine = ItemDirectory.CreateEmptyItem()
                .SetContentWindow(itemWindow)
                .SetSpriteAndShape("Items/items_tool2", "printer_off");


            machine.name = "Auto Loom";
            machine.shortDescription = "A machine that turns Kotton and Zilk into Fabric and Bolts.";
            machine.flavorText = "";

            machine.unitValue = 300;

            machine.SetGameItemType(TypeHelper.MACHINE);
            machine.EnableTag(ContainerHelper.CONTAINER_TAG);
            machine.EnableTag("manufacture");
            machine.EnableTag(MachineHelper.STANDARD_MACHINE_TAG);
            machine.EnableTag(AUTOLOOM_TAG);

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

                GameItem outputTester = DirectoryMaster.Item("kotton_fabric");
                bool outputHasRoom = GraphUtils.CanAccept(outputGrid, outputTester);
                outputTester.Destroy();
                if (outputHasRoom == false) return;

                string ResourceConsumed = "kotton_fiber";
                int ResourceCount = 3;
                string ProductionItem = "kotton_fabric";
                int ProductionCount = 4;
                int TargetProgress = 40;
                //Determine which process to work, go most valuable first.
                if(inputGrid.childItems.FindAll(x => x.identifier.Contains("vaporwares_zilk_fabric")).Count >= 3)
                {
                    ResourceConsumed = "vaporwares_zilk_fabric";
                    ResourceCount = 3;
                    ProductionItem = "vaporwares_zilk_bolt";
                    ProductionCount = 1;
                    TargetProgress = 45;
                }
                else if (inputGrid.childItems.FindAll(x => x.identifier.Contains("vaporwares_zilk_fiber")).Count >= 3)
                {
                    ResourceConsumed = "vaporwares_zilk_fiber";
                    ResourceCount = 3;
                    ProductionItem = "vaporwares_zilk_fabric";
                    ProductionCount = 2;
                    TargetProgress = 60;
                }
                else if (inputGrid.childItems.FindAll(x => x.identifier.Contains("kotton_fabric")).Count >= 3)
                {
                    ResourceConsumed = "kotton_fabric";
                    ResourceCount = 3;
                    ProductionItem = "vaporwares_kotton_bolt";
                    ProductionCount = 1;
                    TargetProgress = 30;
                }


                List<GameItem> Materials = new List<GameItem>();
                foreach (var child in inputGrid.childItems)
                {
                    if (child.identifier.Equals(ResourceConsumed))
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
                    if (Materials == null || Materials.Count < ResourceCount) return;

                    PowerHelper.DrawPowerSource(batterySlot.childItem, energyUsage);

                    item.ModifyTag(MachineProgressHelper.MACHINE_STATE_TAG, s => s.Enable().SetString(MachineProgressHelper.STATE_WORKING));
                    item.ModifyTag(MachineProgressHelper.MACHINE_PROGRESS_CURRENT_TAG, s => s.Enable().SetInt(0));
                    item.ModifyTag(MachineProgressHelper.MACHINE_PROGRESS_TARGET_TAG, s => s.Enable().SetInt(TargetProgress));
                    item.SetSpriteAndShape("Items/items_tool2", "printer_on");

                    MachineProgressHelper.ContinueProgressTypeMachine(item);
                    MachineHelper.OnMachineActioned(machine, moduleGrid);

                    if (MachineProgressHelper.IsProgressTypeMachineFinished(item))
                    {
                        FinishSynthesis(item, Materials, outputGrid, ProductionItem, ProductionCount);
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
                        if (Materials == null || Materials.Count < ResourceCount)
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
            foreach (var mat in materials)
            {
                mat.Destroy();
            }
            for (int i = 0; i < ProductionCount; i++)
            {
                GraphUtils.TryAcceptAll(outputGrid, DirectoryMaster.Item(ProductionItem));
            }
        }

        public static (PixelWindow, GameSlotInventory, GameGridInventory, GameGridInventory, GameGridInventory, GameSlotInventory) CreateMachineInventoryWindow(bool isDraggable = true, bool isCentered = true, int minWidth = 5)
        {
            PixelWindow window = new PixelWindow(isDraggable, "Auto Loom", WindowStyle.Machinery);
            GridPixelElement mainGrid = new GridPixelElement(5, 2, isCentered);

            GameSlotInventory batterySlot = new GameSlotInventory();
            GameGridInventory moduleGrid = new GameGridInventory(MachineHelper.DEFAULT_MODULE_BAY_CONFIG, MachineHelper.DEFAULT_MODULE_BAY_WIDTH);
            GameGridInventory inputGrid = new GameGridInventory(3, 6);
            GameGridInventory outputGrid = new GameGridInventory(6, 4);
            GameSlotInventory noteSlot = new GameSlotInventory();


            TagElement batteryLabel = new TagElement().SetText("Battery");
            TagElement moduleLabel = new TagElement().SetText("Modules");
            TagElement inputLabel = new TagElement().SetText("Material");
            TagElement outputLabel = new TagElement().SetText("Output");
            TagElement noteLabel = new TagElement().SetText("Note");

            mainGrid.Attach(batteryLabel, 0, 0, minimumHeight: 20, minimumWidth: 20);
            mainGrid.Attach(moduleLabel, 1, 0, minimumHeight: 20, minimumWidth: 20);
            mainGrid.Attach(inputLabel, 2, 0, minimumHeight: 20, minimumWidth: minWidth);
            mainGrid.Attach(outputLabel, 3, 0, minimumHeight: 20, minimumWidth: 5);
            mainGrid.Attach(noteLabel, 4, 0, minimumHeight: 5, minimumWidth: 5);

            mainGrid.Attach(batterySlot, 0, 1, minimumHeight: 50, minimumWidth: 20);
            mainGrid.Attach(moduleGrid, 1, 1, minimumHeight: 50, minimumWidth: 116);
            mainGrid.Attach(inputGrid, 2, 1, minimumHeight: 50, minimumWidth: 100);
            mainGrid.Attach(outputGrid, 3, 1, minimumHeight: 100, minimumWidth: 100);
            mainGrid.Attach(noteSlot, 4, 1, minimumHeight: 5, minimumWidth: 5);

            window.Attach(mainGrid);

            return (window, batterySlot, moduleGrid, inputGrid, outputGrid, noteSlot);
        }

        public static GameItem CreateNote()
        {
            GameItem item = DirectoryMaster.Item("postit", isOwned: true);

            item.name = "AutoLoom - Instructions";
            item.shortDescription = "Place a battery in the power slot and load Kotton or Zilk into the input row. Each synthesis cycle consumes 2 Kotton or 2 Zilk and runs for 1 day, producing 1 Kotton Fabric or 1 Zilk Fabric respectively in the output grid.\n\nUniversal modules can be installed in the module bay to improve power efficiency, production speed, and quality bonuses.";

            item.SetGameItemType(TypeHelper.DOCUMENT);
            item.EnableTag(GeneralHelper.IMPORTANT_TAG);
            item.unitValue = 0;

            return item;
        }

    }
}
