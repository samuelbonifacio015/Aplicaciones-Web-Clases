using ACME.LearningCenterPlatform.API.Publishing.Domain.Model;
using ACME.LearningCenterPlatform.API.Publishing.Domain.Model.Aggregates;
using ACME.LearningCenterPlatform.API.Publishing.Domain.Model.Entities;
using Microsoft.EntityFrameworkCore;

namespace ACME.LearningCenterPlatform.API.Publishing.Infrastructure.Persistence.EFC.Configuration.Extensions;

public static class ModelBuilderExtensions
{
    public static void ApplyPublishingConfiguration(this ModelBuilder modelBuilder)
    {
        //Publishing Context
        // Category Entity Configuration
        modelBuilder.Entity<Category>().HasKey(c => c.Id);
        modelBuilder.Entity<Category>().Property(c => c.Id).IsRequired().ValueGeneratedOnAdd();
        modelBuilder.Entity<Category>().Property(c => c.Name).IsRequired().HasMaxLength(100);
        
        // Tutorial Entity Configuration
        modelBuilder.Entity<Tutorial>().HasKey(t => t.Id);
        modelBuilder.Entity<Tutorial>().Property(t => t.Id).IsRequired().ValueGeneratedOnAdd();
        modelBuilder.Entity<Tutorial>().Property(t => t.Title).IsRequired().HasMaxLength(200);
        modelBuilder.Entity<Tutorial>().Property(t => t.Summary).IsRequired().HasMaxLength(200);
        
        // Asset Entity Configuration with Inheritance
        modelBuilder.Entity<Asset>().HasDiscriminator(a => a.Type);
        modelBuilder.Entity<Asset>().HasKey(a=> a.Id);
        modelBuilder.Entity<Asset>().HasDiscriminator<string>("asset_type")
            .HasValue<Asset>("asset_base")
            .HasValue<ImageAsset>("asset_image")
            .HasValue<VideoAsset>("asset_video")
            .HasValue<ReadableContentAsset>("asset_readable_content");

        modelBuilder.Entity<Asset>().OwnsOne(i => i.AssetIdentifier, ai =>
        {
            ai.WithOwner().HasForeignKey("Id");
            ai.Property(p => p.Identifier).HasColumnName("AssetIdentifier");
        });
        modelBuilder.Entity<ImageAsset>().Property(p => p.ImageUri).IsRequired();
        modelBuilder.Entity<VideoAsset>().Property(p => p.VideoUri).IsRequired();

        modelBuilder.Entity<Tutorial>().HasMany(t => t.Assets);










    }
}