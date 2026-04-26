using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExampleMod.Content.Guns;

/// <summary>
///     Provides a practical example of a gun that converts musket bullets into example bullets.
/// </summary>
public class ExampleConversionGunItem : ModItem
{
    public override void SetDefaults()
    {
        Item.DamageType = DamageClass.Ranged;
        
        // Indicates the item does not deal contact damage.
        Item.noMelee = true;

        // Matches the dimensions of the item's texture.
        Item.width = 16;
        Item.height = 16;

        Item.UseSound = SoundID.Item4;

        // Indicates the item takes 15 frames to be used.
        Item.useTime = 15;
        
        // Indicates the animation of the item lasts 15 frames.
        Item.useAnimation = 15;
        
        Item.useStyle = ItemUseStyleID.Shoot;

        // Indicates the velocity of the projectile that the item shoots, in pixels per frame.
        Item.shootSpeed = 16f;

        // Indicates the type of the projectile that the item shoots.
        Item.shoot = ModContent.ProjectileType<ExampleBulletProjectile>();

        // Indicates the type of ammo that the projectile uses.
        Item.useAmmo = AmmoID.Bullet;
    }

    public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
    {
        if (type != ProjectileID.Bullet)
        {
            return;
        }
        
        // Overrides the type of the projectile shot by the item.
        type = ModContent.ProjectileType<ExampleBulletProjectile>();
    }
}