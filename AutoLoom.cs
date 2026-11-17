using System;
using System.Collections.Generic;
using System.Text;

namespace VaporWares
{
    public class AutoLoom
    {
        public const string AUTOLOOM_TAG = "AUTOLOOM_TAG";
        public const int BASE_POWER_USAGE = 4;
        public const int BASE_PROCESSING_SPEED = 10;
        public const int TARGET_PROGRESS = 20; // 2 cycles (days) at base speed of 10
        public static GameItem CreateAutoLoom()
        {
            (PixelWindow itemWindow, GameSlotInventory batterySlot, GameGridInventory moduleGrid, GameGridInventory inputGrid, GameGridInventory outputGrid, GameSlotInventory noteSlot) = CreateMachineInventoryWindow();

            inputGrid.mayInventoryAddItemFunc = (item, _) =>
            {
                if (GeneralHelper.IsItemOwned(item) == false) return false;
                return item.IsGameItemType(TypeHelper.MATERIAL);
            };

            outputGrid.mayInventoryAddItemFunc = (item, _) => item.identifier.Equals("kotton_fabric") || item.identifier.Equals("zilk_fabric");

            noteSlot.mayInventoryAddItemFunc = (item, _) => item.identifier.Equals("postit");
            noteSlot.SetBackgroundFadeSprite("Items/items_tool", "postit");
            

            GameItem machine = ItemDirectory.CreateEmptyItem()
                .SetContentWindow(itemWindow)
                .SetSpriteAndShape("Items/items_tool2", "printer_off");


            machine.name = "Auto Loom";
            machine.shortDescription = "A machine that turns Kotton and Zilk into Fabric and Bolts.";
            machine.flavorText = "";

            machine.unitValue = 500;

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

                GameItem outputTester = DirectoryMaster.Item("tshirt");
                bool outputHasRoom = GraphUtils.CanAccept(outputGrid, outputTester);
                outputTester.Destroy();
                if (outputHasRoom == false) return;

                //GameItem basicMat = GraphUtils.FindChildType<GameItem>(inputGrid, c => c.identifier.Equals("kotton_fiber") || c.identifier.Equals("zilk_fabric"));
                List<GameItem> basicMats = GraphUtils.FindChildrenType<GameItem>(inputGrid, c => c.identifier.Equals("zilk_fiber"));
                if (basicMats == null || basicMats.Count < 3)
                {
                    basicMats = GraphUtils.FindChildrenType<GameItem>(inputGrid, c => c.identifier.Equals("zilk_fabric"));

                    if (basicMats == null || basicMats.Count < 2)
                    {
                        basicMats = GraphUtils.FindChildrenType<GameItem>(inputGrid, c => c.identifier.Equals("kotton_fabric"));

                        if (basicMats == null || basicMats.Count < 2)
                        {
                            basicMats = GraphUtils.FindChildrenType<GameItem>(inputGrid, c => c.identifier.Equals("kotton_fiber"));
                            if(basicMats == null || basicMats.Count < 2)
                            {
                                basicMats = null;
                            }
                        }
                    }
                }

                string state = MachineProgressHelper.GetMachineState(item);

                if (state.Equals(MachineProgressHelper.STATE_READY))
                {
                    if (basicMats == null) return;

                    PowerHelper.DrawPowerSource(batterySlot.childItem, energyUsage);

                    item.ModifyTag(MachineProgressHelper.MACHINE_STATE_TAG, s => s.Enable().SetString(MachineProgressHelper.STATE_WORKING));
                    item.ModifyTag(MachineProgressHelper.MACHINE_PROGRESS_CURRENT_TAG, s => s.Enable().SetInt(0));
                    item.ModifyTag(MachineProgressHelper.MACHINE_PROGRESS_TARGET_TAG, s => s.Enable().SetInt(TARGET_PROGRESS));

                    MachineProgressHelper.ContinueProgressTypeMachine(item);
                    MachineHelper.OnMachineActioned(machine, moduleGrid);

                    if (MachineProgressHelper.IsProgressTypeMachineFinished(item))
                    {
                        FinishSynthesis(item, basicMats, outputGrid);
                    }
                }
                else if (state.Equals(MachineProgressHelper.STATE_WORKING))
                {
                    PowerHelper.DrawPowerSource(batterySlot.childItem, energyUsage);

                    MachineProgressHelper.ContinueProgressTypeMachine(item);
                    MachineHelper.OnMachineActioned(machine, moduleGrid);

                    if (MachineProgressHelper.IsProgressTypeMachineFinished(item))
                    {
                        if (basicMats == null)
                        {
                            MachineProgressHelper.FinishProgressTypeMachine(item);
                            return;
                        }
                        FinishSynthesis(item, basicMats, outputGrid);
                    }
                }
            };

            return machine;
        }

        public static void FinishSynthesis(GameItem machine, List<GameItem> basicMats, GameGridInventory outputGrid)
        {
            MachineProgressHelper.FinishProgressTypeMachine(machine);
            bool kotton = basicMats[0].identifier.Equals("kotton_fiber");
            bool kottonfab = basicMats[0].identifier.Equals("kotton_fabric");
            bool zilk = basicMats[0].identifier.Equals("zilk_fiber");
            bool zilkfab = basicMats[0].identifier.Equals("zilk_fabric");
            if (kotton)
            {
                GraphUtils.TryDestroyAll(basicMats, 2);
                GraphUtils.TryAcceptAll(outputGrid, DirectoryMaster.Item("kotton_fabric"));
            }
            else if (kottonfab)
            {
                GraphUtils.TryDestroyAll(basicMats, 2);
                GraphUtils.TryAcceptAll(outputGrid, DirectoryMaster.Item("kotton_bolt"));
            }
            else if (zilk)
            {
                GraphUtils.TryDestroyAll(basicMats, 3);
                GraphUtils.TryAcceptAll(outputGrid, DirectoryMaster.Item("zilk_fabric"));
            }
            else if (zilkfab)
            {
                GraphUtils.TryDestroyAll(basicMats, 2);
                GraphUtils.TryAcceptAll(outputGrid, DirectoryMaster.Item("zilk_bolt"));
            }
        }

        public static (PixelWindow, GameSlotInventory, GameGridInventory, GameGridInventory, GameGridInventory, GameSlotInventory) CreateMachineInventoryWindow(bool isDraggable = true, bool isCentered = true, int minWidth = 5)
        {
            PixelWindow window = new PixelWindow(isDraggable, "Auto Loom", WindowStyle.Machinery);
            GridPixelElement mainGrid = new GridPixelElement(5, 2, isCentered);

            GameSlotInventory batterySlot = new GameSlotInventory();
            GameGridInventory moduleGrid = new GameGridInventory(MachineHelper.DEFAULT_MODULE_BAY_CONFIG, MachineHelper.DEFAULT_MODULE_BAY_WIDTH);
            GameGridInventory inputGrid = new GameGridInventory(6, 4);
            GameGridInventory outputGrid = new GameGridInventory(6, 3);
            GameSlotInventory noteSlot = new GameSlotInventory();

            TagElement batteryLabel = new TagElement().SetText("Battery");
            TagElement moduleLabel = new TagElement().SetText("Modules");
            TagElement inputLabel = new TagElement().SetText("Material");
            TagElement outputLabel = new TagElement().SetText("Fabric Output");
            TagElement noteLabel = new TagElement().SetText("Note");

            mainGrid.Attach(batteryLabel, 0, 0, minimumHeight: 20, minimumWidth: 20);
            mainGrid.Attach(moduleLabel, 1, 0, minimumHeight: 20, minimumWidth: 20);
            mainGrid.Attach(inputLabel, 2, 0, minimumHeight: 20, minimumWidth: minWidth);
            mainGrid.Attach(outputLabel, 3, 0, minimumHeight: 20, minimumWidth: 5);
            mainGrid.Attach(noteLabel, 4, 0, minimumHeight: 5, minimumWidth: 5);

            mainGrid.Attach(batterySlot, 0, 1, minimumHeight: 50, minimumWidth: 20);
            mainGrid.Attach(moduleGrid, 1, 1, minimumHeight: 50, minimumWidth: 20);
            mainGrid.Attach(inputGrid, 2, 1, minimumHeight: 50, minimumWidth: minWidth);
            mainGrid.Attach(outputGrid, 3, 1, minimumHeight: 100, minimumWidth: 5);
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
