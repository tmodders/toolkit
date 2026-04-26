using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ExampleMod.Content.Guns;

/// <summary>
///     Provides a practical example of a gun that shoots bullets in an even spread.
/// </summary>
public class ExampleEvenSpreadGunItem : ModItem
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

    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
    {
        // The amount of projectiles to be shot.
        var amount = 5;
        
        // The rotation of each shot, in degrees.
        var rotation = MathHelper.ToRadians(30f);
        
        for (var i = 0; i < amount; i++)
        {
            Projectile.NewProjectile(source, position, velocity.RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (MathF.Max(amount, 1f) - 1f))), type, damage, knockback, player.whoAmI);
        }
        
        // Returning false in Shoot() prevents the original projectile from being shot.
        return false;
    }
}