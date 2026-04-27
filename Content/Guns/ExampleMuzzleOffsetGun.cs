using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Toolkit.Content.Guns;

/// <summary>
///     Provides a practical example of a gun that shoots bullets with a muzzle offset.
/// </summary>
public class ExampleMuzzleOffsetGun : ModItem
{
    public override void SetDefaults()
    {
        Item.DamageType = DamageClass.Ranged;
        
        Item.damage = 10;
        Item.knockBack = 1f;
        
        // Marks that the item does not deal any contact damage.
        Item.noMelee = true;

        // Matches the dimensions of the item's texture.
        Item.width = 16;
        Item.height = 16;

        Item.UseSound = SoundID.Item4;

        // Sets the item's use time to 15, which means the item will take 15 frames to be used.
        Item.useTime = 15;
        
        // Sets the item's use animation to 15, which means the item's animation will last for 15 frames.
        Item.useAnimation = 15;
        
        Item.useStyle = ItemUseStyleID.Shoot;

        // Sets the velocity of the projectile that the item shoots, in pixels per frame.
        Item.shootSpeed = 16f;

        // Sets the type of the projectile that the item shoots.
        Item.shoot = ModContent.ProjectileType<ExampleBulletProjectile>();

        // Sets the type of ammo that the projectile item.
        Item.useAmmo = AmmoID.Bullet;
    }

    public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
    {
        // The distance of the muzzle offset, in pixels.
        var distance = 25f;
        
        var offset = velocity.SafeNormalize(Vector2.Zero) * distance;

        if (!Collision.CanHit(position, 0, 0, position + offset, 0, 0))
        {
            return;
        }
        
        position += offset;
    }
}