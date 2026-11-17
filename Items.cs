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
            item.unitValue = 110; //Use unitValue for base price
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
            item.unitValue = 180; //Use unitValue for base price
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
            item.unitValue = 85; //Use unitValue for base price
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
            item.unitValue = 25; //Use unitValue for base price
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
            item.flavorText = "Made from kotton, this is arguabably the most basic form of clothing on the station. Often worn as an undershirt";

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
            item.flavorText = "Made from kotton, this sleeveless shirt works best as an ";
            
            item.SetGameItemType("CLOTHING"); // set the item's GameItem Type which is used for buyer/seller and storage

            return item;
        }
    }
}
