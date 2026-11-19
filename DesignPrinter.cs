using System;
using System.Collections.Generic;
using System.Text;

namespace VaporWares
{
    public class DesignPrinter
    {
        public const string DESIGNPRINTER_TAG = "DESIGNPRINTER_TAG";
        public const int BASE_POWER_USAGE = 6;
        public const int BASE_PROCESSING_SPEED = 25;
        public static GameItem CreateDesignPrinter()
        {
            (PixelWindow itemWindow, GameSlotInventory batterySlot, GameGridInventory moduleGrid, GameSlotInventory clothingSlot, GameGridInventory inputGrid, GameSlotInventory chipSlot, GameGridInventory outputGrid, GameSlotInventory noteSlot) = CreateMachineInventoryWindow();

            inputGrid.mayInventoryAddItemFunc = (item, _) =>
            {
                if (GeneralHelper.IsItemOwned(item) == false) return false;
                return item.identifier.Contains("vaporwares_dye_");
            };

            clothingSlot.mayInventoryAddItemFunc = (item, _) =>
            {
                if (GeneralHelper.IsItemOwned(item) == false) return false;
                return item.identifier.Contains("vaporwares_clothing_");
            };

            chipSlot.mayInventoryAddItemFunc = (item, _) =>
            {
                if (GeneralHelper.IsItemOwned(item) == false) return false;
                return item.identifier.Contains("vaporwares_design_");
            };

            outputGrid.mayInventoryAddItemFunc = (item, _) => item.identifier.Contains("vaporwares_clothing_") && item.identifier.Contains("design");

            noteSlot.mayInventoryAddItemFunc = (item, _) => item.identifier.Equals("postit");
            noteSlot.SetBackgroundFadeSprite("Items/items_tool", "postit");
            

            GameItem machine = ItemDirectory.CreateEmptyItem()
                .SetContentWindow(itemWindow)
                .SetSpriteAndShape("Items/items_tool2", "printer_off");


            machine.name = "Design Printer";
            machine.shortDescription = "A machine that prints graphic designs on clothing.";
            machine.flavorText = "";

            machine.unitValue = 300;

            machine.SetGameItemType(TypeHelper.MACHINE);
            machine.EnableTag(ContainerHelper.CONTAINER_TAG);
            machine.EnableTag("manufacture");
            machine.EnableTag(MachineHelper.STANDARD_MACHINE_TAG);
            machine.EnableTag(DESIGNPRINTER_TAG);

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

                GameItem outputTester = DirectoryMaster.Item("vaporwares_clothing_tshirt_design_skull");
                bool outputHasRoom = GraphUtils.CanAccept(outputGrid, outputTester);
                outputTester.Destroy();
                if (outputHasRoom == false) return;

                var Clothing = clothingSlot.childItem;
                if (Clothing == null) return;

                var Slotted = chipSlot.childItem.identifier;
                if (Slotted == null) return;

                int ResCyanAmount = 0;
                int ResMagentaAmount = 0;
                int ResYellowAmount = 0;
                int ResBlackAmount = 0;
                string ProductionItem = "vaporwares_clothing_tshirt_design_skull";
                int ProductionCount = 1;
                int TargetProgress = 40;
                //Determine which process to work, go most valuable first.
                switch (Slotted)
                {
                    //If this can be moved to the chips, that'd be prefered.
                    case "vaporwares_design_skull":
                        {
                            ResBlackAmount = 40;
                            if (Clothing.identifier.Equals("vaporwares_clothing_tshirt"))
                            {
                                ProductionItem = "vaporwares_clothing_tshirt_design_skull";
                            }
                            else if (Clothing.identifier.Equals("vaporwares_clothing_sleeveless"))
                            {
                                ProductionItem = "vaporwares_clothing_sleeveless_design_skull";
                            }
                            TargetProgress = 20;
                            break;
                        }
                    default:
                        {
                            //No chip, just dye the shirt!.
                            return;
                        }
                }


                List<GameItem> Materials = new List<GameItem>();
                foreach (var child in inputGrid.childItems)
                {
                    //Check CMYK values!
                    //if (child.identifier.Equals(ResourceConsumed))
                    //{
                    //    Materials.Add(child);
                    //    if (Materials.Count >= ResourceCount)
                    //    {
                    //        break;
                    //    }
                    //}
                }


                string state = MachineProgressHelper.GetMachineState(item);

                if (state.Equals(MachineProgressHelper.STATE_READY))
                {
                    if (Clothing == null) return;

                    PowerHelper.DrawPowerSource(batterySlot.childItem, energyUsage);

                    item.ModifyTag(MachineProgressHelper.MACHINE_STATE_TAG, s => s.Enable().SetString(MachineProgressHelper.STATE_WORKING));
                    item.ModifyTag(MachineProgressHelper.MACHINE_PROGRESS_CURRENT_TAG, s => s.Enable().SetInt(0));
                    item.ModifyTag(MachineProgressHelper.MACHINE_PROGRESS_TARGET_TAG, s => s.Enable().SetInt(TargetProgress));
                    item.SetSpriteAndShape("Items/items_tool2", "printer_on");

                    MachineProgressHelper.ContinueProgressTypeMachine(item);
                    MachineHelper.OnMachineActioned(machine, moduleGrid);
                    moduleGrid.LockInv();
                    inputGrid.LockInv();
                    clothingSlot.LockInv();
                    chipSlot.LockInv();

                    if (MachineProgressHelper.IsProgressTypeMachineFinished(item))
                    {
                        FinishSynthesis(item, Clothing, Materials, outputGrid, ProductionItem);
                        moduleGrid.UnlockInv();
                        inputGrid.UnlockInv();
                        clothingSlot.UnlockInv();
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
                        moduleGrid.UnlockInv();
                        inputGrid.UnlockInv();
                        clothingSlot.UnlockInv();
                        chipSlot.UnlockInv();
                        if (Clothing == null)
                        {
                            MachineProgressHelper.FinishProgressTypeMachine(item);
                            return;
                        }
                        FinishSynthesis(item, Clothing, Materials, outputGrid, ProductionItem);
                    }
                }
            };

            return machine;
        }

        public static void FinishSynthesis(GameItem machine, GameItem clothing, List<GameItem> materials, GameGridInventory outputGrid, string ProductionItem)
        {
            MachineProgressHelper.FinishProgressTypeMachine(machine);
            clothing.Destroy();
            //Drink up the required inks?
            GraphUtils.TryAcceptAll(outputGrid, DirectoryMaster.Item(ProductionItem));
        }

        public static (PixelWindow, GameSlotInventory, GameGridInventory, GameSlotInventory, GameGridInventory, GameSlotInventory, GameGridInventory, GameSlotInventory) CreateMachineInventoryWindow(bool isDraggable = true, bool isCentered = true, int minWidth = 5)
        {
            PixelWindow window = new PixelWindow(isDraggable, "Design Printer", WindowStyle.Machinery);
            GridPixelElement mainGrid = new GridPixelElement(7, 2, isCentered);

            GameSlotInventory batterySlot = new GameSlotInventory();
            GameGridInventory moduleGrid = new GameGridInventory(MachineHelper.DEFAULT_MODULE_BAY_CONFIG, MachineHelper.DEFAULT_MODULE_BAY_WIDTH);
            GameSlotInventory clothingSlot = new GameSlotInventory(3, 3);
            GameGridInventory inputGrid = new GameGridInventory(2, 4);
            GameSlotInventory chipSlot = new GameSlotInventory(1, 2);
            GameGridInventory outputGrid = new GameGridInventory(6, 6);
            GameSlotInventory noteSlot = new GameSlotInventory();


            TagElement batteryLabel = new TagElement().SetText("Battery");
            TagElement moduleLabel = new TagElement().SetText("Modules");
            TagElement clothingLabel = new TagElement().SetText("Clothing");
            TagElement inputLabel = new TagElement().SetText("Dyes");
            TagElement chipLabel = new TagElement().SetText("Design\nChip");
            TagElement outputLabel = new TagElement().SetText("Output");
            TagElement noteLabel = new TagElement().SetText("Note");

            mainGrid.Attach(batteryLabel, 0, 0, minimumHeight: 20, minimumWidth: 20);
            mainGrid.Attach(moduleLabel, 1, 0, minimumHeight: 20, minimumWidth: 20);
            mainGrid.Attach(clothingLabel, 2, 0, minimumHeight: 20, minimumWidth: minWidth);
            mainGrid.Attach(inputLabel, 3, 0, minimumHeight: 20, minimumWidth: minWidth);
            mainGrid.Attach(chipLabel, 4, 0, minimumHeight: 20, minimumWidth: minWidth);
            mainGrid.Attach(outputLabel, 5, 0, minimumHeight: 20, minimumWidth: 5);
            mainGrid.Attach(noteLabel, 6, 0, minimumHeight: 5, minimumWidth: 5);

            mainGrid.Attach(batterySlot, 0, 1, minimumHeight: 50, minimumWidth: 20);
            mainGrid.Attach(moduleGrid, 1, 1, minimumHeight: 50, minimumWidth: 116);
            mainGrid.Attach(clothingSlot, 2, 1, minimumHeight: 50, minimumWidth: 50);
            mainGrid.Attach(inputGrid, 3, 1, minimumHeight: 50, minimumWidth: 40);
            mainGrid.Attach(chipSlot, 4, 1, minimumHeight: 50, minimumWidth: 40);
            mainGrid.Attach(outputGrid, 5, 1, minimumHeight: 100, minimumWidth: 100);
            mainGrid.Attach(noteSlot, 6, 1, minimumHeight: 5, minimumWidth: 5);

            window.Attach(mainGrid);

            return (window, batterySlot, moduleGrid, clothingSlot, inputGrid, chipSlot, outputGrid, noteSlot);
        }

        public static GameItem CreateNote()
        {
            GameItem item = DirectoryMaster.Item("postit", isOwned: true);

            item.name = "Design Printer - Instructions";
            item.shortDescription = "Place a battery in the power slot, undyed clothing into the clothing row, and dyes into the input row. Each print cycle runs for 1 day, dying the clothing with various colors and designs.\n\nUniversal modules can be installed in the module bay to improve power efficiency, production speed, and quality bonuses.";

            item.SetGameItemType(TypeHelper.DOCUMENT);
            item.EnableTag(GeneralHelper.IMPORTANT_TAG);
            item.unitValue = 0;

            return item;
        }

    }
}
