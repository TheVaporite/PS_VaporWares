using System;
using System.Collections.Generic;
using System.Text;

namespace VaporWares
{
    public class Items
    {

        public static GameItem CreateKottonBolt()
        {
            GameItem item = ItemDirectory.CreateEmptyItem()
                .SetName("Kotton Bolt")
                .SetSpriteAndShapeFromMod("vapor_wares", "kotton_bolt"); //use SetSpriteAndShapeFromMod for sprite from mod. The first parameter is the mod's unique ID, and the second is the name of the sprite in the mod's assets folder (without file extension)
            item.unitValue = 200; //Use unitValue for base price
            item.shortDescription = "A bolt of kotton fabric.";
            item.flavorText = "Made from kotton fibers, this bolt of fabric is used to make simple clothing.";

            //item.SetGameItemType("FABRIC"); // set the item's GameItem Type which is used for buyer/seller and storage
            item.SetGameItemType(TypeHelper.MATERIAL);

            return item;
        }
        public static GameItem CreateZilkBolt()
        {
            GameItem item = ItemDirectory.CreateEmptyItem()
                .SetName("Zilk Bolt")
                .SetSpriteAndShapeFromMod("vapor_wares", "zilk_bolt"); //use SetSpriteAndShapeFromMod for sprite from mod. The first parameter is the mod's unique ID, and the second is the name of the sprite in the mod's assets folder (without file extension)
            item.unitValue = 320; //Use unitValue for base price
            item.shortDescription = "A bolt of Zilk fabric.";
            item.flavorText = "Made from synthetic silk fibers, this bolt of fabric is used to make luxury clothing.";

            //item.SetGameItemType("FABRIC"); // set the item's GameItem Type which is used for buyer/seller and storage
            item.SetGameItemType(TypeHelper.MATERIAL);

            return item;
        }
        public static GameItem CreateZilkFabric()
        {
            GameItem item = ItemDirectory.CreateEmptyItem()
                .SetName("Zilk Fabric")
                .SetSpriteAndShapeFromMod("vapor_wares", "zilk_fabric"); //use SetSpriteAndShapeFromMod for sprite from mod. The first parameter is the mod's unique ID, and the second is the name of the sprite in the mod's assets folder (without file extension)
            item.unitValue = 90; //Use unitValue for base price
            item.shortDescription = "A bolt of Zilk fabric.";
            item.flavorText = "Made from synthetic silk fibers, this bolt of fabric is used to make luxury clothing.";

            //item.SetGameItemType("FABRIC"); // set the item's GameItem Type which is used for buyer/seller and storage
            item.SetGameItemType(TypeHelper.MATERIAL);
            item.SetGameItemType(TypeHelper.FILTER);

            return item;
        }
        public static GameItem CreateZilkFiber()
        {
            GameItem item = ItemDirectory.CreateEmptyItem()
                .SetName("Zilk Fiber")
                .SetSpriteAndShapeFromMod("vapor_wares", "zilk_fiber"); //use SetSpriteAndShapeFromMod for sprite from mod. The first parameter is the mod's unique ID, and the second is the name of the sprite in the mod's assets folder (without file extension)
            item.unitValue = 55; //Use unitValue for base price
            item.shortDescription = "A spool of Zilk fiber.";
            item.flavorText = "A synthetic silk fiber, this spool is ready to be spun into fabric.";

            item.SetGameItemType(TypeHelper.MATERIAL);

            return item;
        }
        public static GameItem CreateTShirt()
        {
            GameItem item = ItemDirectory.CreateEmptyItem()
                .SetName("T-Shirt")
                .SetSpriteAndShapeFromMod("vapor_wares", "tshirt"); //use SetSpriteAndShapeFromMod for sprite from mod. The first parameter is the mod's unique ID, and the second is the name of the sprite in the mod's assets folder (without file extension)
            item.unitValue = 50; //Use unitValue for base price
            item.shortDescription = "A basic kotton T-shirt.";
            item.flavorText = "Made from kotton, this is arguabably the most basic form of clothing on the station.";

            item.SetGameItemType(TypeHelper.HOUSEHOLD_GOOD);
            item.SetGameItemType("CLOTHING"); // set the item's GameItem Type which is used for buyer/seller and storage

            return item;
        }
        public static GameItem CreateTShirtDyed()
        {
            GameItem item = ItemDirectory.CreateEmptyItem()
                .SetName("T-Shirt (Dyed)")
                .SetSpriteAndShapeFromMod("vapor_wares", "tshirt_cyan"); //use SetSpriteAndShapeFromMod for sprite from mod. The first parameter is the mod's unique ID, and the second is the name of the sprite in the mod's assets folder (without file extension)
            item.unitValue = 60; //Use unitValue for base price
            item.shortDescription = "A dyed kotton T-shirt.";
            item.flavorText = "Made from kotton, this is arguabably the most basic form of clothing on the station.";

            item.SetGameItemType(TypeHelper.HOUSEHOLD_GOOD);
            item.SetGameItemType("CLOTHING"); // set the item's GameItem Type which is used for buyer/seller and storage

            return item;
        }

        public static GameItem CreateTShirtDesignSkull()
        {
            GameItem item = ItemDirectory.CreateEmptyItem()
                .SetName("T-Shirt (Skull)")
                .SetSpriteAndShapeFromMod("vapor_wares", "tshirt_gray_skull"); //use SetSpriteAndShapeFromMod for sprite from mod. The first parameter is the mod's unique ID, and the second is the name of the sprite in the mod's assets folder (without file extension)
            item.unitValue = 75; //Use unitValue for base price
            item.shortDescription = "A kotton T-shirt with a skull on it.";
            item.flavorText = "Skulls, they just work for some people.";

            item.SetGameItemType(TypeHelper.HOUSEHOLD_GOOD);
            item.SetGameItemType("CLOTHING"); // set the item's GameItem Type which is used for buyer/seller and storage

            return item;
        }
        public static GameItem CreateSleevelessShirt()
        {
            GameItem item = ItemDirectory.CreateEmptyItem()
                .SetName("Sleeveless Shirt")
                .SetSpriteAndShapeFromMod("vapor_wares", "sleeveless_shirt"); //use SetSpriteAndShapeFromMod for sprite from mod. The first parameter is the mod's unique ID, and the second is the name of the sprite in the mod's assets folder (without file extension)
            item.unitValue = 45; //Use unitValue for base price
            item.shortDescription = "A basic kotton Shirt without sleeves.";
            item.flavorText = "Made from kotton, this sleeveless shirt works best as an undershirt.";

            item.SetGameItemType(TypeHelper.HOUSEHOLD_GOOD);
            item.SetGameItemType("CLOTHING"); // set the item's GameItem Type which is used for buyer/seller and storage

            return item;
        }
        public static GameItem CreateSleevelessShirtDyed()
        {
            GameItem item = ItemDirectory.CreateEmptyItem()
                .SetName("Sleeveless Shirt (Dyed)")
                .SetSpriteAndShapeFromMod("vapor_wares", "sleeveless_shirt_cyan"); //use SetSpriteAndShapeFromMod for sprite from mod. The first parameter is the mod's unique ID, and the second is the name of the sprite in the mod's assets folder (without file extension)
            item.unitValue = 45; //Use unitValue for base price
            item.shortDescription = "A dyed kotton Shirt without sleeves.";
            item.flavorText = "Made from kotton, this sleeveless shirt works best as an undershirt.";

            item.SetGameItemType(TypeHelper.HOUSEHOLD_GOOD);
            item.SetGameItemType("CLOTHING"); // set the item's GameItem Type which is used for buyer/seller and storage

            return item;
        }
        public static GameItem CreateSleevelessShirtDesignSkull()
        {
            GameItem item = ItemDirectory.CreateEmptyItem()
                .SetName("Sleeveless Shirt (Skull)")
                .SetSpriteAndShapeFromMod("vapor_wares", "sleeveless_gray_skull"); //use SetSpriteAndShapeFromMod for sprite from mod. The first parameter is the mod's unique ID, and the second is the name of the sprite in the mod's assets folder (without file extension)
            item.unitValue = 70; //Use unitValue for base price
            item.shortDescription = "A basic kotton Shirt without sleeves. with a skull on it.";
            item.flavorText = "Skulls, they just work for some people.";

            item.SetGameItemType(TypeHelper.HOUSEHOLD_GOOD);
            item.SetGameItemType("CLOTHING"); // set the item's GameItem Type which is used for buyer/seller and storage

            return item;
        }
        public static GameItem CreateBeanie()
        {
            GameItem item = ItemDirectory.CreateEmptyItem()
                .SetName("Beanie")
                .SetSpriteAndShapeFromMod("vapor_wares", "sleeveless_shirt"); //use SetSpriteAndShapeFromMod for sprite from mod. The first parameter is the mod's unique ID, and the second is the name of the sprite in the mod's assets folder (without file extension)
            item.unitValue = 30; //Use unitValue for base price
            item.shortDescription = "A basic kotton beanie.";
            item.flavorText = "Made from kotton, this beanie keeps folks warm when the power drops out.";

            item.SetGameItemType(TypeHelper.HOUSEHOLD_GOOD);
            item.SetGameItemType("CLOTHING"); // set the item's GameItem Type which is used for buyer/seller and storage

            return item;
        }
        public static GameItem CreateZilkScarf()
        {
            GameItem item = ItemDirectory.CreateEmptyItem()
                .SetName("Zilk Scarf")
                .SetSpriteAndShapeFromMod("vapor_wares", "zilk_fabric"); //use SetSpriteAndShapeFromMod for sprite from mod. The first parameter is the mod's unique ID, and the second is the name of the sprite in the mod's assets folder (without file extension)
            item.unitValue = 100; //Use unitValue for base price
            item.shortDescription = "A scarf make of Zilk.";
            item.flavorText = "Made from Zilk, this scarf is both comfortable and fashionable.";

            item.SetGameItemType(TypeHelper.LUXURY_ITEM);
            item.SetGameItemType(TypeHelper.HOUSEHOLD_GOOD);
            item.SetGameItemType("CLOTHING"); // set the item's GameItem Type which is used for buyer/seller and storage

            return item;
        }
        public static GameItem CreateZilkTShirt()
        {
            GameItem item = ItemDirectory.CreateEmptyItem()
                .SetName("Zilk T-Shirt")
                .SetSpriteAndShapeFromMod("vapor_wares", "zilk_tshirt"); //use SetSpriteAndShapeFromMod for sprite from mod. The first parameter is the mod's unique ID, and the second is the name of the sprite in the mod's assets folder (without file extension)
            item.unitValue = 75; //Use unitValue for base price
            item.shortDescription = "A t-shirt make of Zilk.";
            item.flavorText = "Made from Zilk, this t-shirt is both comfortable and fashionable.";

            item.SetGameItemType(TypeHelper.LUXURY_ITEM);
            item.SetGameItemType(TypeHelper.HOUSEHOLD_GOOD);
            item.SetGameItemType("CLOTHING"); // set the item's GameItem Type which is used for buyer/seller and storage

            return item;
        }

        public static GameItem CreateTailorChipTShirt()
        {
            GameItem item = ItemDirectory.CreateEmptyItem()
                .SetName("Chip (Kotton TShirt)")
                .SetSpriteAndShapeFromMod("vapor_wares", "tailor_chip_basic"); //use SetSpriteAndShapeFromMod for sprite from mod. The first parameter is the mod's unique ID, and the second is the name of the sprite in the mod's assets folder (without file extension)
            item.unitValue = 80; //Use unitValue for base price
            item.shortDescription = "A chip for making Kotton t-shirts.";
            item.flavorText = "<b>Resources:</b>\nThree(3) Kotton Fabric\n<b>Produces:</b>\nFour(4) t-shirts\n<b>Progress:</b>\n45";

            item.SetGameItemType(TypeHelper.PRINTER_MODULE);
            
            return item;
        }
        public static GameItem CreateTailorChipMassTShirt()
        {
            GameItem item = ItemDirectory.CreateEmptyItem()
                .SetName("Chip (Mass Kotton TShirt)")
                .SetSpriteAndShapeFromMod("vapor_wares", "tailor_chip_basic"); //use SetSpriteAndShapeFromMod for sprite from mod. The first parameter is the mod's unique ID, and the second is the name of the sprite in the mod's assets folder (without file extension)
            item.unitValue = 80; //Use unitValue for base price
            item.shortDescription = "A chip for mass producing Kotton t-shirts.";
            item.flavorText = "<b>Resources:</b>\nOne(1) Kotton Bolt\n<b>Produces:</b>\nFour(4) t-shirts\n<b>Progress:</b>\n30";

            item.SetGameItemType(TypeHelper.PRINTER_MODULE);

            return item;
        }
        public static GameItem CreateTailorChipSleevelessShirt()
        {
            GameItem item = ItemDirectory.CreateEmptyItem()
                .SetName("Chip (Kotton Sleeveless Shirt)")
                .SetSpriteAndShapeFromMod("vapor_wares", "tailor_chip_basic"); //use SetSpriteAndShapeFromMod for sprite from mod. The first parameter is the mod's unique ID, and the second is the name of the sprite in the mod's assets folder (without file extension)
            item.unitValue = 70; //Use unitValue for base price
            item.shortDescription = "A chip for making Kotton sleeveless shirts.";
            item.flavorText = "Produces two(2) sleeveless shirts for one(1) Kotton Fabric.";

            item.SetGameItemType(TypeHelper.PRINTER_MODULE);

            return item;
        }
        public static GameItem CreateTailorChipBeanie()
        {
            GameItem item = ItemDirectory.CreateEmptyItem()
                .SetName("Chip (Beanie)")
                .SetSpriteAndShapeFromMod("vapor_wares", "tailor_chip_basic"); //use SetSpriteAndShapeFromMod for sprite from mod. The first parameter is the mod's unique ID, and the second is the name of the sprite in the mod's assets folder (without file extension)
            item.unitValue = 75; //Use unitValue for base price
            item.shortDescription = "A chip for making Kotton beanies.";
            item.flavorText = "Produces three(3) beanies for one(1) Kotton Fabric.";

            item.SetGameItemType(TypeHelper.PRINTER_MODULE);

            return item;
        }
        public static GameItem CreateTailorChipZilkTShirt()
        {
            GameItem item = ItemDirectory.CreateEmptyItem()
                .SetName("Chip (Zilk TShirt)")
                .SetSpriteAndShapeFromMod("vapor_wares", "tailor_chip_luxury"); //use SetSpriteAndShapeFromMod for sprite from mod. The first parameter is the mod's unique ID, and the second is the name of the sprite in the mod's assets folder (without file extension)
            item.unitValue = 140; //Use unitValue for base price
            item.shortDescription = "A chip for a Zilk T-shirt.";
            item.flavorText = "Produces one(1) Zilk T-shirt for one(1) Zilk Fabric.";

            item.SetGameItemType(TypeHelper.PRINTER_MODULE);

            return item;
        }
        public static GameItem CreateTailorChipZilkScarf()
        {
            GameItem item = ItemDirectory.CreateEmptyItem()
                .SetName("Chip (Zilk Scarf)")
                .SetSpriteAndShapeFromMod("vapor_wares", "tailor_chip_luxury"); //use SetSpriteAndShapeFromMod for sprite from mod. The first parameter is the mod's unique ID, and the second is the name of the sprite in the mod's assets folder (without file extension)
            item.unitValue = 125; //Use unitValue for base price
            item.shortDescription = "A chip for making Zilk Scarves.";
            item.flavorText = "Produces two(2) Zilk scarves for one(1) Zilk Fabric..";

            item.SetGameItemType(TypeHelper.PRINTER_MODULE);

            return item;
        }

        public static GameItem CreateDesignChipSkull()
        {
            GameItem item = ItemDirectory.CreateEmptyItem()
                .SetName("Design Chip (Skull)")
                .SetSpriteAndShapeFromMod("vapor_wares", "tailor_chip_design"); //use SetSpriteAndShapeFromMod for sprite from mod. The first parameter is the mod's unique ID, and the second is the name of the sprite in the mod's assets folder (without file extension)
            item.unitValue = 90; //Use unitValue for base price
            item.shortDescription = "A design chip of a skull pattern.";
            item.flavorText = "Requires:\nCyan: 0\nMagenta: 0\nYellow: 0\nBlack: 40\nProgress:\n20.";

            item.SetGameItemType(TypeHelper.PRINTER_MODULE);

            return item;
        }


        public static GameItem CreateDyeCyan()
        {
            GameItem item = ItemDirectory.CreateEmptyItem()
                .SetName("Dye (Cyan)")
                .SetSpriteAndShapeFromMod("vapor_wares", "dye_cyan"); //use SetSpriteAndShapeFromMod for sprite from mod. The first parameter is the mod's unique ID, and the second is the name of the sprite in the mod's assets folder (without file extension)
            item.unitValue = 60; //Use unitValue for base price
            item.shortDescription = "A bottle of cyan dye.";
            item.flavorText = "Used to dye clothing, or used in an AutoDesign to print designs on clothing.";

            item.onItemTargetItemFunc = (itemA, itemB) =>
            {
                //Apply dye to clothing?
                switch(itemB.identifier)
                {
                    case "vaporwares_clothing_tshirt":
                        {
                            itemB.Destroy();
                            itemB.parentInventory.TryAcceptAll(ModHelper.SpawnItem("vaporwares_clothing_tshirt_dyed"));
                            break;
                        }
                    default:
                        {
                            break;
                        }
                };
            };
            item.onItemTargetThisFunc = (itemA, itemB) =>
            {
                //Apply dye to clothing?
                switch (itemB.identifier)
                {
                    case "vaporwares_clothing_tshirt":
                        {
                            itemB.Destroy();
                            itemB.parentInventory.TryAcceptAll(ModHelper.SpawnItem("vaporwares_clothing_tshirt_dyed"));
                            break;
                        }
                    default:
                        {
                            break;
                        }
                }
                ;
            };
            item.canActivateSlotItemFunc = (_, _, _) =>
            {
                return true;
            };

            item.canUseOutsideCombat = true;
            item.canItemTargetThisFunc = (itemA, itemB) =>
            {
                if (itemB.identifier.Contains("vaporwares_clothing_"))
                {
                    return true;
                }
                return true;
            };

            item.canItemTargetItemFunc = (itemA, itemB) =>
            {
                if (itemB.identifier.Contains("vaporwares_clothing_"))
                {
                    return true;
                }
                return true;
            };

            return item;
        }
        public static GameItem CreateDyeMagenta()
        {
            GameItem item = ItemDirectory.CreateEmptyItem()
                .SetName("Dye (Magenta)")
                .SetSpriteAndShapeFromMod("vapor_wares", "dye_magenta"); //use SetSpriteAndShapeFromMod for sprite from mod. The first parameter is the mod's unique ID, and the second is the name of the sprite in the mod's assets folder (without file extension)
            item.unitValue = 60; //Use unitValue for base price
            item.shortDescription = "A bottle of magenta dye.";
            item.flavorText = "Used to dye clothing, or used in an AutoDesign to print designs on clothing.";

            item.onItemTargetItemFunc = (itemA, itemB) =>
            {
                //Apply dye to clothing?
            };

            return item;
        }
        public static GameItem CreateDyeYellow()
        {
            GameItem item = ItemDirectory.CreateEmptyItem()
                .SetName("Dye (Yellow)")
                .SetSpriteAndShapeFromMod("vapor_wares", "dye_yellow"); //use SetSpriteAndShapeFromMod for sprite from mod. The first parameter is the mod's unique ID, and the second is the name of the sprite in the mod's assets folder (without file extension)
            item.unitValue = 60; //Use unitValue for base price
            item.shortDescription = "A bottle of yellow dye.";
            item.flavorText = "Used to dye clothing, or used in an AutoDesign to print designs on clothing.";

            item.onItemTargetItemFunc = (itemA, itemB) =>
            {
                //Apply dye to clothing?
            };

            return item;
        }
        public static GameItem CreateDyeBlack()
        {
            GameItem item = ItemDirectory.CreateEmptyItem()
                .SetName("Dye (Black)")
                .SetSpriteAndShapeFromMod("vapor_wares", "dye_black"); //use SetSpriteAndShapeFromMod for sprite from mod. The first parameter is the mod's unique ID, and the second is the name of the sprite in the mod's assets folder (without file extension)
            item.unitValue = 60; //Use unitValue for base price
            item.shortDescription = "A bottle of black dye.";
            item.flavorText = "Used to dye clothing, or used in an AutoDesign to print designs on clothing.";

            item.onItemTargetItemFunc = (itemA, itemB) =>
            {
                //Apply dye to clothing?
            };

            return item;
        }
        public static GameItem CreateDyeCartridge()
        {
            GameItem item = ItemDirectory.CreateEmptyItem()
                .SetName("Dye Cartridge")
                .SetSpriteAndShapeFromMod("vapor_wares", "dye_cartridge"); //use SetSpriteAndShapeFromMod for sprite from mod. The first parameter is the mod's unique ID, and the second is the name of the sprite in the mod's assets folder (without file extension)
            item.unitValue = 200; //Use unitValue for base price
            item.shortDescription = "A cartridge of dyes.";
            item.flavorText = "Used in an AutoDesign to print designs on clothing.";

            return item;
        }
    }
}
