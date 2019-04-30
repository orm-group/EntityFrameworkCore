// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Linq;
using Microsoft.EntityFrameworkCore.Sqlite.Metadata.Internal;
using Xunit;

// ReSharper disable InconsistentNaming
namespace Microsoft.EntityFrameworkCore.Metadata.Internal
{
    public class SqliteInternalMetadataBuilderExtensionsTest
    {
        private InternalModelBuilder CreateBuilder()
            => new InternalModelBuilder(new Model());

        [Fact]
        public void Can_access_model()
        {
            var builder = CreateBuilder();

            builder.Sqlite(ConfigurationSource.Convention).GetOrAddSequence("Mine").IncrementBy = 77;

            Assert.Equal(77, builder.Metadata.Relational().FindSequence("Mine").IncrementBy);

            Assert.Equal(
                1, builder.Metadata.GetAnnotations().Count(
                    a => a.Name.StartsWith(RelationalAnnotationNames.Prefix, StringComparison.Ordinal)));
        }

        [Fact]
        public void Can_access_entity_type()
        {
            var typeBuilder = CreateBuilder().Entity(typeof(Splot), ConfigurationSource.Convention);

            Assert.True(typeBuilder.Sqlite(ConfigurationSource.Convention).ToTable("Splew"));
            Assert.Equal("Splew", typeBuilder.Metadata.Relational().GetTableName());

            Assert.True(typeBuilder.Sqlite(ConfigurationSource.DataAnnotation).ToTable("Splow"));
            Assert.Equal("Splow", typeBuilder.Metadata.Relational().GetTableName());

            Assert.False(typeBuilder.Sqlite(ConfigurationSource.Convention).ToTable("Splod"));
            Assert.Equal("Splow", typeBuilder.Metadata.Relational().GetTableName());

            Assert.Equal(
                1, typeBuilder.Metadata.GetAnnotations().Count(
                    a => a.Name.StartsWith(RelationalAnnotationNames.Prefix, StringComparison.Ordinal)));
        }

        [Fact]
        public void Can_access_property()
        {
            var propertyBuilder = CreateBuilder()
                .Entity(typeof(Splot), ConfigurationSource.Convention)
                .Property(typeof(int), "Id", ConfigurationSource.Convention);

            Assert.True(propertyBuilder.Sqlite(ConfigurationSource.Convention).HasColumnName("Splew"));
            Assert.Equal("Splew", propertyBuilder.Metadata.Relational().GetColumnName());

            Assert.True(propertyBuilder.Sqlite(ConfigurationSource.DataAnnotation).HasColumnName("Splow"));
            Assert.Equal("Splow", propertyBuilder.Metadata.Relational().GetColumnName());

            Assert.False(propertyBuilder.Sqlite(ConfigurationSource.Convention).HasColumnName("Splod"));
            Assert.Equal("Splow", propertyBuilder.Metadata.Relational().GetColumnName());

            Assert.Equal(
                1, propertyBuilder.Metadata.GetAnnotations().Count(
                    a => a.Name.StartsWith(RelationalAnnotationNames.Prefix, StringComparison.Ordinal)));
        }

        [Fact]
        public void Can_access_key()
        {
            var modelBuilder = CreateBuilder();
            var entityTypeBuilder = modelBuilder.Entity(typeof(Splot), ConfigurationSource.Convention);
            var idProperty = entityTypeBuilder.Property(typeof(string), "Id", ConfigurationSource.Convention).Metadata;
            var keyBuilder = entityTypeBuilder.HasKey(new[] { idProperty.Name }, ConfigurationSource.Convention);

            Assert.True(keyBuilder.Sqlite(ConfigurationSource.Convention).HasName("Splew"));
            Assert.Equal("Splew", keyBuilder.Metadata.Relational().Name);

            Assert.True(keyBuilder.Sqlite(ConfigurationSource.DataAnnotation).HasName("Splow"));
            Assert.Equal("Splow", keyBuilder.Metadata.Relational().Name);

            Assert.False(keyBuilder.Sqlite(ConfigurationSource.Convention).HasName("Splod"));
            Assert.Equal("Splow", keyBuilder.Metadata.Relational().Name);

            Assert.Equal(
                1, keyBuilder.Metadata.GetAnnotations().Count(
                    a => a.Name.StartsWith(RelationalAnnotationNames.Prefix, StringComparison.Ordinal)));
        }

        [Fact]
        public void Can_access_index()
        {
            var modelBuilder = CreateBuilder();
            var entityTypeBuilder = modelBuilder.Entity(typeof(Splot), ConfigurationSource.Convention);
            entityTypeBuilder.Property(typeof(int), "Id", ConfigurationSource.Convention);
            var indexBuilder = entityTypeBuilder.HasIndex(new[] { "Id" }, ConfigurationSource.Convention);

            indexBuilder.Sqlite(ConfigurationSource.Convention).HasName("Splew");
            Assert.Equal("Splew", indexBuilder.Metadata.Relational().GetName());

            indexBuilder.Sqlite(ConfigurationSource.DataAnnotation).HasName("Splow");
            Assert.Equal("Splow", indexBuilder.Metadata.Relational().GetName());

            indexBuilder.Sqlite(ConfigurationSource.Convention).HasName("Splod");
            Assert.Equal("Splow", indexBuilder.Metadata.Relational().GetName());

            Assert.Equal(
                1, indexBuilder.Metadata.GetAnnotations().Count(
                    a => a.Name.StartsWith(RelationalAnnotationNames.Prefix, StringComparison.Ordinal)));
        }

        [Fact]
        public void Can_access_relationship()
        {
            var modelBuilder = CreateBuilder();
            var entityTypeBuilder = modelBuilder.Entity(typeof(Splot), ConfigurationSource.Convention);
            entityTypeBuilder.Property(typeof(int), "Id", ConfigurationSource.Convention);
            var relationshipBuilder = entityTypeBuilder.HasRelationship("Splot", new[] { "Id" }, ConfigurationSource.Convention);

            Assert.True(relationshipBuilder.Sqlite(ConfigurationSource.Convention).HasConstraintName("Splew"));
            Assert.Equal("Splew", relationshipBuilder.Metadata.Relational().ConstraintName);

            Assert.True(relationshipBuilder.Sqlite(ConfigurationSource.DataAnnotation).HasConstraintName("Splow"));
            Assert.Equal("Splow", relationshipBuilder.Metadata.Relational().ConstraintName);

            Assert.False(relationshipBuilder.Sqlite(ConfigurationSource.Convention).HasConstraintName("Splod"));
            Assert.Equal("Splow", relationshipBuilder.Metadata.Relational().ConstraintName);

            Assert.Equal(
                1, relationshipBuilder.Metadata.GetAnnotations().Count(
                    a => a.Name.StartsWith(RelationalAnnotationNames.Prefix, StringComparison.Ordinal)));
        }

        private class Splot
        {
        }
    }
}
