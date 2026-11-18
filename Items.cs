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
    }
}
