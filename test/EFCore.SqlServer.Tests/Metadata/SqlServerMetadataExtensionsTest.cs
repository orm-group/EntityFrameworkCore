// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Linq;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.SqlServer.Internal;
using Microsoft.EntityFrameworkCore.TestUtilities;
using Xunit;

// ReSharper disable InconsistentNaming
namespace Microsoft.EntityFrameworkCore.Metadata
{
    public class SqlServerMetadataExtensionsTest
    {
        [Fact]
        public void Can_get_and_set_column_name()
        {
            var modelBuilder = GetModelBuilder();

            var property = modelBuilder
                .Entity<Customer>()
                .Property(e => e.Name)
                .Metadata;

            Assert.Equal("Name", property.SqlServer().GetColumnName());
            Assert.Equal("Name", ((IProperty)property).SqlServer().GetColumnName());

            property.Relational().ColumnName = "Eman";

            Assert.Equal("Name", property.Name);
            Assert.Equal("Eman", property.Relational().GetColumnName());
            Assert.Equal("Eman", property.SqlServer().GetColumnName());
            Assert.Equal("Eman", ((IProperty)property).SqlServer().GetColumnName());

            property.SqlServer().ColumnName = "MyNameIs";

            Assert.Equal("Name", property.Name);
            Assert.Equal("MyNameIs", property.Relational().GetColumnName());
            Assert.Equal("MyNameIs", property.SqlServer().GetColumnName());
            Assert.Equal("MyNameIs", ((IProperty)property).SqlServer().GetColumnName());

            property.SqlServer().ColumnName = null;

            Assert.Equal("Name", property.Name);
            Assert.Equal("Name", property.Relational().GetColumnName());
            Assert.Equal("Name", property.SqlServer().GetColumnName());
            Assert.Equal("Name", ((IProperty)property).SqlServer().GetColumnName());
        }

        [Fact]
        public void Can_get_and_set_table_name()
        {
            var modelBuilder = GetModelBuilder();

            var entityType = modelBuilder
                .Entity<Customer>()
                .Metadata;

            Assert.Equal("Customer", entityType.SqlServer().GetTableName());
            Assert.Equal("Customer", ((IEntityType)entityType).SqlServer().GetTableName());

            entityType.Relational().TableName = "Customizer";

            Assert.Equal("Customer", entityType.DisplayName());
            Assert.Equal("Customizer", entityType.Relational().GetTableName());
            Assert.Equal("Customizer", entityType.SqlServer().GetTableName());
            Assert.Equal("Customizer", ((IEntityType)entityType).SqlServer().GetTableName());

            entityType.SqlServer().TableName = "Custardizer";

            Assert.Equal("Customer", entityType.DisplayName());
            Assert.Equal("Custardizer", entityType.Relational().GetTableName());
            Assert.Equal("Custardizer", entityType.SqlServer().GetTableName());
            Assert.Equal("Custardizer", ((IEntityType)entityType).SqlServer().GetTableName());

            entityType.SqlServer().TableName = null;

            Assert.Equal("Customer", entityType.DisplayName());
            Assert.Equal("Customer", entityType.Relational().GetTableName());
            Assert.Equal("Customer", entityType.SqlServer().GetTableName());
            Assert.Equal("Customer", ((IEntityType)entityType).SqlServer().GetTableName());
        }

        [Fact]
        public void Can_get_and_set_schema_name()
        {
            var modelBuilder = GetModelBuilder();

            var entityType = modelBuilder
                .Entity<Customer>()
                .Metadata;

            Assert.Null(entityType.Relational().GetSchema());
            Assert.Null(entityType.SqlServer().GetSchema());
            Assert.Null(((IEntityType)entityType).SqlServer().GetSchema());

            entityType.Relational().Schema = "db0";

            Assert.Equal("db0", entityType.Relational().GetSchema());
            Assert.Equal("db0", entityType.SqlServer().GetSchema());
            Assert.Equal("db0", ((IEntityType)entityType).SqlServer().GetSchema());

            entityType.SqlServer().Schema = "dbOh";

            Assert.Equal("dbOh", entityType.Relational().GetSchema());
            Assert.Equal("dbOh", entityType.SqlServer().GetSchema());
            Assert.Equal("dbOh", ((IEntityType)entityType).SqlServer().GetSchema());

            entityType.SqlServer().Schema = null;

            Assert.Null(entityType.Relational().GetSchema());
            Assert.Null(entityType.SqlServer().GetSchema());
            Assert.Null(((IEntityType)entityType).SqlServer().GetSchema());
        }

        [Fact]
        public void Can_get_and_set_column_type()
        {
            var modelBuilder = GetModelBuilder();

            var property = modelBuilder
                .Entity<Customer>()
                .Property(e => e.Name)
                .Metadata;

            Assert.Null(property.Relational().GetColumnType());
            Assert.Null(property.SqlServer().GetColumnType());
            Assert.Null(((IProperty)property).SqlServer().GetColumnType());

            property.Relational().ColumnType = "nvarchar(max)";

            Assert.Equal("nvarchar(max)", property.Relational().GetColumnType());
            Assert.Equal("nvarchar(max)", property.SqlServer().GetColumnType());
            Assert.Equal("nvarchar(max)", ((IProperty)property).SqlServer().GetColumnType());

            property.SqlServer().ColumnType = "nvarchar(verstappen)";

            Assert.Equal("nvarchar(verstappen)", property.Relational().GetColumnType());
            Assert.Equal("nvarchar(verstappen)", property.SqlServer().GetColumnType());
            Assert.Equal("nvarchar(verstappen)", ((IProperty)property).SqlServer().GetColumnType());

            property.SqlServer().ColumnType = null;

            Assert.Null(property.Relational().GetColumnType());
            Assert.Null(property.SqlServer().GetColumnType());
            Assert.Null(((IProperty)property).SqlServer().GetColumnType());
        }

        [Fact]
        public void Can_get_and_set_column_default_expression()
        {
            var modelBuilder = GetModelBuilder();

            var property = modelBuilder
                .Entity<Customer>()
                .Property(e => e.Name)
                .Metadata;

            Assert.Null(property.Relational().GetDefaultValueSql());
            Assert.Null(property.SqlServer().GetDefaultValueSql());
            Assert.Null(((IProperty)property).SqlServer().GetDefaultValueSql());

            property.Relational().DefaultValueSql = "newsequentialid()";

            Assert.Equal("newsequentialid()", property.Relational().GetDefaultValueSql());
            Assert.Equal("newsequentialid()", property.SqlServer().GetDefaultValueSql());
            Assert.Equal("newsequentialid()", ((IProperty)property).SqlServer().GetDefaultValueSql());

            property.SqlServer().DefaultValueSql = "expressyourself()";

            Assert.Equal("expressyourself()", property.Relational().GetDefaultValueSql());
            Assert.Equal("expressyourself()", property.SqlServer().GetDefaultValueSql());
            Assert.Equal("expressyourself()", ((IProperty)property).SqlServer().GetDefaultValueSql());

            property.SqlServer().DefaultValueSql = null;

            Assert.Null(property.Relational().GetDefaultValueSql());
            Assert.Null(property.SqlServer().GetDefaultValueSql());
            Assert.Null(((IProperty)property).SqlServer().GetDefaultValueSql());
        }

        [Fact]
        public void Can_get_and_set_column_computed_expression()
        {
            var modelBuilder = GetModelBuilder();

            var property = modelBuilder
                .Entity<Customer>()
                .Property(e => e.Name)
                .Metadata;

            Assert.Null(property.Relational().GetComputedColumnSql());
            Assert.Null(property.SqlServer().GetComputedColumnSql());
            Assert.Null(((IProperty)property).SqlServer().GetComputedColumnSql());

            property.Relational().ComputedColumnSql = "newsequentialid()";

            Assert.Equal("newsequentialid()", property.Relational().GetComputedColumnSql());
            Assert.Equal("newsequentialid()", property.SqlServer().GetComputedColumnSql());
            Assert.Equal("newsequentialid()", ((IProperty)property).SqlServer().GetComputedColumnSql());

            property.SqlServer().ComputedColumnSql = "expressyourself()";

            Assert.Equal("expressyourself()", property.Relational().GetComputedColumnSql());
            Assert.Equal("expressyourself()", property.SqlServer().GetComputedColumnSql());
            Assert.Equal("expressyourself()", ((IProperty)property).SqlServer().GetComputedColumnSql());

            property.SqlServer().ComputedColumnSql = null;

            Assert.Null(property.Relational().GetComputedColumnSql());
            Assert.Null(property.SqlServer().GetComputedColumnSql());
            Assert.Null(((IProperty)property).SqlServer().GetComputedColumnSql());
        }

        [Fact]
        public void Can_get_and_set_column_default_value()
        {
            var modelBuilder = GetModelBuilder();

            var property = modelBuilder
                .Entity<Customer>()
                .Property(e => e.ByteArray)
                .Metadata;

            Assert.Null(property.Relational().GetDefaultValue());
            Assert.Null(property.SqlServer().GetDefaultValue());
            Assert.Null(((IProperty)property).SqlServer().GetDefaultValue());

            property.Relational().DefaultValue = new byte[] { 69, 70, 32, 82, 79, 67, 75, 83 };

            Assert.Equal(new byte[] { 69, 70, 32, 82, 79, 67, 75, 83 }, property.Relational().GetDefaultValue());
            Assert.Equal(new byte[] { 69, 70, 32, 82, 79, 67, 75, 83 }, property.SqlServer().GetDefaultValue());
            Assert.Equal(new byte[] { 69, 70, 32, 82, 79, 67, 75, 83 }, ((IProperty)property).SqlServer().GetDefaultValue());

            property.SqlServer().DefaultValue = new byte[] { 69, 70, 32, 83, 79, 67, 75, 83 };

            Assert.Equal(new byte[] { 69, 70, 32, 83, 79, 67, 75, 83 }, property.Relational().GetDefaultValue());
            Assert.Equal(new byte[] { 69, 70, 32, 83, 79, 67, 75, 83 }, property.SqlServer().GetDefaultValue());
            Assert.Equal(new byte[] { 69, 70, 32, 83, 79, 67, 75, 83 }, ((IProperty)property).SqlServer().GetDefaultValue());

            property.SqlServer().DefaultValue = null;

            Assert.Null(property.Relational().GetDefaultValue());
            Assert.Null(property.SqlServer().GetDefaultValue());
            Assert.Null(((IProperty)property).SqlServer().GetDefaultValue());
        }

        [Theory]
        [InlineData(nameof(RelationalPropertyAnnotations.GetDefaultValue()), nameof(RelationalPropertyAnnotations.GetDefaultValueSql()))]
        [InlineData(nameof(RelationalPropertyAnnotations.GetDefaultValue()), nameof(RelationalPropertyAnnotations.GetComputedColumnSql()))]
        [InlineData(nameof(RelationalPropertyAnnotations.GetDefaultValue()), nameof(SqlServerPropertyAnnotations.ValueGenerationStrategy))]
        [InlineData(nameof(RelationalPropertyAnnotations.GetDefaultValueSql()), nameof(RelationalPropertyAnnotations.GetDefaultValue()))]
        [InlineData(nameof(RelationalPropertyAnnotations.GetDefaultValueSql()), nameof(RelationalPropertyAnnotations.GetComputedColumnSql()))]
        [InlineData(nameof(RelationalPropertyAnnotations.GetDefaultValueSql()), nameof(SqlServerPropertyAnnotations.ValueGenerationStrategy))]
        [InlineData(nameof(RelationalPropertyAnnotations.GetComputedColumnSql()), nameof(RelationalPropertyAnnotations.GetDefaultValue()))]
        [InlineData(nameof(RelationalPropertyAnnotations.GetComputedColumnSql()), nameof(RelationalPropertyAnnotations.GetDefaultValueSql()))]
        [InlineData(nameof(RelationalPropertyAnnotations.GetComputedColumnSql()), nameof(SqlServerPropertyAnnotations.ValueGenerationStrategy))]
        [InlineData(nameof(SqlServerPropertyAnnotations.ValueGenerationStrategy), nameof(RelationalPropertyAnnotations.GetDefaultValue()))]
        [InlineData(nameof(SqlServerPropertyAnnotations.ValueGenerationStrategy), nameof(RelationalPropertyAnnotations.GetDefaultValueSql()))]
        [InlineData(nameof(SqlServerPropertyAnnotations.ValueGenerationStrategy), nameof(RelationalPropertyAnnotations.GetComputedColumnSql()))]
        public void Metadata_throws_when_setting_conflicting_serverGenerated_values(string firstConfiguration, string secondConfiguration)
        {
            var modelBuilder = GetModelBuilder();

            var propertyBuilder = modelBuilder
                .Entity<Customer>()
                .Property(e => e.NullableInt);

            ConfigureProperty(propertyBuilder.Metadata, firstConfiguration, "1");

            Assert.Equal(
                RelationalStrings.ConflictingColumnServerGeneration(secondConfiguration, nameof(Customer.NullableInt), firstConfiguration),
                Assert.Throws<InvalidOperationException>(
                    () =>
                        ConfigureProperty(propertyBuilder.Metadata, secondConfiguration, "2")).Message);
        }

        protected virtual void ConfigureProperty(IMutableProperty property, string configuration, string value)
        {
            var propertyAnnotations = property.SqlServer();
            switch (configuration)
            {
                case nameof(RelationalPropertyAnnotations.GetDefaultValue()):
                    propertyAnnotations.DefaultValue = int.Parse(value);
                    break;
                case nameof(RelationalPropertyAnnotations.GetDefaultValueSql()):
                    propertyAnnotations.DefaultValueSql = value;
                    break;
                case nameof(RelationalPropertyAnnotations.GetComputedColumnSql()):
                    propertyAnnotations.ComputedColumnSql = value;
                    break;
                case nameof(SqlServerPropertyAnnotations.ValueGenerationStrategy):
                    propertyAnnotations.ValueGenerationStrategy = SqlServerValueGenerationStrategy.IdentityColumn;
                    break;
                default:
                    throw new NotImplementedException();
            }
        }

        [Fact]
        public void Can_get_and_set_column_key_name()
        {
            var modelBuilder = GetModelBuilder();

            var key = modelBuilder
                .Entity<Customer>()
                .HasKey(e => e.Id)
                .Metadata;

            Assert.Equal("PK_Customer", key.Relational().Name);
            Assert.Equal("PK_Customer", key.SqlServer().GetName());
            Assert.Equal("PK_Customer", ((IKey)key).SqlServer().GetName());

            key.Relational().Name = "PrimaryKey";

            Assert.Equal("PrimaryKey", key.Relational().Name);
            Assert.Equal("PrimaryKey", key.SqlServer().GetName());
            Assert.Equal("PrimaryKey", ((IKey)key).SqlServer().GetName());

            key.SqlServer().Name = "PrimarySchool";

            Assert.Equal("PrimarySchool", key.Relational().Name);
            Assert.Equal("PrimarySchool", key.SqlServer().GetName());
            Assert.Equal("PrimarySchool", ((IKey)key).SqlServer().GetName());

            key.SqlServer().Name = null;

            Assert.Equal("PK_Customer", key.Relational().Name);
            Assert.Equal("PK_Customer", key.SqlServer().GetName());
            Assert.Equal("PK_Customer", ((IKey)key).SqlServer().GetName());
        }

        [Fact]
        public void Can_get_and_set_column_foreign_key_name()
        {
            var modelBuilder = GetModelBuilder();

            modelBuilder
                .Entity<Customer>()
                .HasKey(e => e.Id);

            var foreignKey = modelBuilder
                .Entity<Order>()
                .HasOne<Customer>()
                .WithOne()
                .HasForeignKey<Order>(e => e.CustomerId)
                .Metadata;

            Assert.Equal("FK_Order_Customer_CustomerId", foreignKey.Relational().ConstraintName);
            Assert.Equal("FK_Order_Customer_CustomerId", ((IForeignKey)foreignKey).Relational().ConstraintName);

            foreignKey.Relational().ConstraintName = "FK";

            Assert.Equal("FK", foreignKey.Relational().ConstraintName);
            Assert.Equal("FK", ((IForeignKey)foreignKey).Relational().ConstraintName);

            foreignKey.Relational().ConstraintName = "KFC";

            Assert.Equal("KFC", foreignKey.Relational().ConstraintName);
            Assert.Equal("KFC", ((IForeignKey)foreignKey).Relational().ConstraintName);

            foreignKey.Relational().ConstraintName = null;

            Assert.Equal("FK_Order_Customer_CustomerId", foreignKey.Relational().ConstraintName);
            Assert.Equal("FK_Order_Customer_CustomerId", ((IForeignKey)foreignKey).Relational().ConstraintName);
        }

        [Fact]
        public void Can_get_and_set_index_name()
        {
            var modelBuilder = GetModelBuilder();

            var index = modelBuilder
                .Entity<Customer>()
                .HasIndex(e => e.Id)
                .Metadata;

            Assert.Equal("IX_Customer_Id", index.Relational().GetName());
            Assert.Equal("IX_Customer_Id", ((IIndex)index).Relational().GetName());

            index.Relational().Name = "MyIndex";

            Assert.Equal("MyIndex", index.Relational().GetName());
            Assert.Equal("MyIndex", ((IIndex)index).Relational().GetName());

            index.SqlServer().Name = "DexKnows";

            Assert.Equal("DexKnows", index.Relational().GetName());
            Assert.Equal("DexKnows", ((IIndex)index).Relational().GetName());

            index.SqlServer().Name = null;

            Assert.Equal("IX_Customer_Id", index.Relational().GetName());
            Assert.Equal("IX_Customer_Id", ((IIndex)index).Relational().GetName());
        }

        [Fact]
        public void Can_get_and_set_index_filter()
        {
            var modelBuilder = new ModelBuilder(new ConventionSet());

            var index = modelBuilder
                .Entity<Customer>()
                .HasIndex(e => e.Id)
                .Metadata;

            Assert.Null(index.Relational().GetFilter());
            Assert.Null(index.SqlServer().GetFilter());
            Assert.Null(((IIndex)index).SqlServer().GetFilter());

            index.Relational().Name = "Generic expression";

            Assert.Equal("Generic expression", index.Relational().GetName());
            Assert.Equal("Generic expression", index.SqlServer().GetName());
            Assert.Equal("Generic expression", ((IIndex)index).SqlServer().GetName());

            index.SqlServer().Name = "SqlServer-specific expression";

            Assert.Equal("SqlServer-specific expression", index.Relational().GetName());
            Assert.Equal("SqlServer-specific expression", index.SqlServer().GetName());
            Assert.Equal("SqlServer-specific expression", ((IIndex)index).SqlServer().GetName());

            index.SqlServer().Name = null;

            Assert.Null(index.Relational().GetFilter());
            Assert.Null(index.SqlServer().GetFilter());
            Assert.Null(((IIndex)index).SqlServer().GetFilter());
        }

        [Fact]
        public void Can_get_and_set_index_clustering()
        {
            var modelBuilder = GetModelBuilder();

            var index = modelBuilder
                .Entity<Customer>()
                .HasIndex(e => e.Id)
                .Metadata;

            Assert.Null(index.SqlServer().IsClustered);
            Assert.Null(((IIndex)index).SqlServer().IsClustered);

            index.SqlServer().IsClustered = true;

            Assert.True(index.SqlServer().IsClustered.Value);
            Assert.True(((IIndex)index).SqlServer().IsClustered.Value);

            index.SqlServer().IsClustered = null;

            Assert.Null(index.SqlServer().IsClustered);
            Assert.Null(((IIndex)index).SqlServer().IsClustered);
        }

        [Fact]
        public void Can_get_and_set_key_clustering()
        {
            var modelBuilder = GetModelBuilder();

            var key = modelBuilder
                .Entity<Customer>()
                .HasKey(e => e.Id)
                .Metadata;

            Assert.Null(key.SqlServer().IsClustered);
            Assert.Null(((IKey)key).SqlServer().IsClustered);

            key.SqlServer().IsClustered = true;

            Assert.True(key.SqlServer().IsClustered.Value);
            Assert.True(((IKey)key).SqlServer().IsClustered.Value);

            key.SqlServer().IsClustered = null;

            Assert.Null(key.SqlServer().IsClustered);
            Assert.Null(((IKey)key).SqlServer().IsClustered);
        }

        [Fact]
        public void Can_get_and_set_sequence()
        {
            var modelBuilder = GetModelBuilder();
            var model = modelBuilder.Model;

            Assert.Null(model.Relational().FindSequence("Foo"));
            Assert.Null(model.SqlServer().FindSequence("Foo"));
            Assert.Null(((IModel)model).SqlServer().FindSequence("Foo"));

            var sequence = model.SqlServer().GetOrAddSequence("Foo");

            Assert.Equal("Foo", model.Relational().FindSequence("Foo").Name);
            Assert.Equal("Foo", ((IModel)model).Relational().FindSequence("Foo").Name);
            Assert.Equal("Foo", model.SqlServer().FindSequence("Foo").Name);
            Assert.Equal("Foo", ((IModel)model).SqlServer().FindSequence("Foo").Name);

            Assert.Equal("Foo", sequence.Name);
            Assert.Null(sequence.Schema);
            Assert.Equal(1, sequence.IncrementBy);
            Assert.Equal(1, sequence.StartValue);
            Assert.Null(sequence.MinValue);
            Assert.Null(sequence.MaxValue);
            Assert.Same(typeof(long), sequence.ClrType);

            Assert.NotNull(model.Relational().FindSequence("Foo"));

            var sequence2 = model.SqlServer().FindSequence("Foo");

            sequence.StartValue = 1729;
            sequence.IncrementBy = 11;
            sequence.MinValue = 2001;
            sequence.MaxValue = 2010;
            sequence.ClrType = typeof(int);

            Assert.Equal("Foo", sequence.Name);
            Assert.Null(sequence.Schema);
            Assert.Equal(11, sequence.IncrementBy);
            Assert.Equal(1729, sequence.StartValue);
            Assert.Equal(2001, sequence.MinValue);
            Assert.Equal(2010, sequence.MaxValue);
            Assert.Same(typeof(int), sequence.ClrType);

            Assert.Equal(sequence2.Name, sequence.Name);
            Assert.Equal(sequence2.Schema, sequence.Schema);
            Assert.Equal(sequence2.IncrementBy, sequence.IncrementBy);
            Assert.Equal(sequence2.StartValue, sequence.StartValue);
            Assert.Equal(sequence2.MinValue, sequence.MinValue);
            Assert.Equal(sequence2.MaxValue, sequence.MaxValue);
            Assert.Same(sequence2.ClrType, sequence.ClrType);
        }

        [Fact]
        public void Can_get_and_set_sequence_with_schema_name()
        {
            var modelBuilder = GetModelBuilder();
            var model = modelBuilder.Model;

            Assert.Null(model.Relational().FindSequence("Foo", "Smoo"));
            Assert.Null(model.SqlServer().FindSequence("Foo", "Smoo"));
            Assert.Null(((IModel)model).SqlServer().FindSequence("Foo", "Smoo"));

            var sequence = model.SqlServer().GetOrAddSequence("Foo", "Smoo");

            Assert.Equal("Foo", model.Relational().FindSequence("Foo", "Smoo").Name);
            Assert.Equal("Foo", ((IModel)model).Relational().FindSequence("Foo", "Smoo").Name);
            Assert.Equal("Foo", model.SqlServer().FindSequence("Foo", "Smoo").Name);
            Assert.Equal("Foo", ((IModel)model).SqlServer().FindSequence("Foo", "Smoo").Name);

            Assert.Equal("Foo", sequence.Name);
            Assert.Equal("Smoo", sequence.Schema);
            Assert.Equal(1, sequence.IncrementBy);
            Assert.Equal(1, sequence.StartValue);
            Assert.Null(sequence.MinValue);
            Assert.Null(sequence.MaxValue);
            Assert.Same(typeof(long), sequence.ClrType);

            Assert.NotNull(model.Relational().FindSequence("Foo", "Smoo"));

            var sequence2 = model.SqlServer().FindSequence("Foo", "Smoo");

            sequence.StartValue = 1729;
            sequence.IncrementBy = 11;
            sequence.MinValue = 2001;
            sequence.MaxValue = 2010;
            sequence.ClrType = typeof(int);

            Assert.Equal("Foo", sequence.Name);
            Assert.Equal("Smoo", sequence.Schema);
            Assert.Equal(11, sequence.IncrementBy);
            Assert.Equal(1729, sequence.StartValue);
            Assert.Equal(2001, sequence.MinValue);
            Assert.Equal(2010, sequence.MaxValue);
            Assert.Same(typeof(int), sequence.ClrType);

            Assert.Equal(sequence2.Name, sequence.Name);
            Assert.Equal(sequence2.Schema, sequence.Schema);
            Assert.Equal(sequence2.IncrementBy, sequence.IncrementBy);
            Assert.Equal(sequence2.StartValue, sequence.StartValue);
            Assert.Equal(sequence2.MinValue, sequence.MinValue);
            Assert.Equal(sequence2.MaxValue, sequence.MaxValue);
            Assert.Same(sequence2.ClrType, sequence.ClrType);
        }

        [Fact]
        public void Can_get_multiple_sequences()
        {
            var modelBuilder = GetModelBuilder();
            var model = modelBuilder.Model;

            model.Relational().GetOrAddSequence("Fibonacci");
            model.SqlServer().GetOrAddSequence("Golomb");

            var sequences = model.SqlServer().Sequences;

            Assert.Equal(2, sequences.Count);
            Assert.Contains(sequences, s => s.Name == "Fibonacci");
            Assert.Contains(sequences, s => s.Name == "Golomb");
        }

        [Fact]
        public void Can_get_multiple_sequences_when_overridden()
        {
            var modelBuilder = GetModelBuilder();
            var model = modelBuilder.Model;

            model.Relational().GetOrAddSequence("Fibonacci").StartValue = 1;
            model.SqlServer().GetOrAddSequence("Fibonacci").StartValue = 3;
            model.SqlServer().GetOrAddSequence("Golomb");

            var sequences = model.SqlServer().Sequences;

            Assert.Equal(2, sequences.Count);
            Assert.Contains(sequences, s => s.Name == "Golomb");

            var sequence = sequences.FirstOrDefault(s => s.Name == "Fibonacci");
            Assert.NotNull(sequence);
            Assert.Equal(3, sequence.StartValue);
        }

        [Fact]
        public void Can_get_and_set_value_generation_on_model()
        {
            var modelBuilder = GetModelBuilder();
            var model = modelBuilder.Model;

            Assert.Equal(SqlServerValueGenerationStrategy.IdentityColumn, model.SqlServer().ValueGenerationStrategy);

            model.SqlServer().ValueGenerationStrategy = SqlServerValueGenerationStrategy.SequenceHiLo;

            Assert.Equal(SqlServerValueGenerationStrategy.SequenceHiLo, model.SqlServer().ValueGenerationStrategy);

            model.SqlServer().ValueGenerationStrategy = null;

            Assert.Null(model.SqlServer().ValueGenerationStrategy);
        }

        [Fact]
        public void Can_get_and_set_default_sequence_name_on_model()
        {
            var modelBuilder = GetModelBuilder();
            var model = modelBuilder.Model;

            Assert.Null(model.SqlServer().HiLoSequenceName);
            Assert.Null(((IModel)model).SqlServer().HiLoSequenceName);

            model.SqlServer().HiLoSequenceName = "Tasty.Snook";

            Assert.Equal("Tasty.Snook", model.SqlServer().HiLoSequenceName);
            Assert.Equal("Tasty.Snook", ((IModel)model).SqlServer().HiLoSequenceName);

            model.SqlServer().HiLoSequenceName = null;

            Assert.Null(model.SqlServer().HiLoSequenceName);
            Assert.Null(((IModel)model).SqlServer().HiLoSequenceName);
        }

        [Fact]
        public void Can_get_and_set_default_sequence_schema_on_model()
        {
            var modelBuilder = GetModelBuilder();
            var model = modelBuilder.Model;

            Assert.Null(model.SqlServer().HiLoSequenceSchema);
            Assert.Null(((IModel)model).SqlServer().HiLoSequenceSchema);

            model.SqlServer().HiLoSequenceSchema = "Tasty.Snook";

            Assert.Equal("Tasty.Snook", model.SqlServer().HiLoSequenceSchema);
            Assert.Equal("Tasty.Snook", ((IModel)model).SqlServer().HiLoSequenceSchema);

            model.SqlServer().HiLoSequenceSchema = null;

            Assert.Null(model.SqlServer().HiLoSequenceSchema);
            Assert.Null(((IModel)model).SqlServer().HiLoSequenceSchema);
        }

        [Fact]
        public void Can_get_and_set_value_generation_on_property()
        {
            var modelBuilder = GetModelBuilder();
            modelBuilder.Model.SqlServer().ValueGenerationStrategy = null;

            var property = modelBuilder
                .Entity<Customer>()
                .Property(e => e.Id)
                .Metadata;

            Assert.Null(property.SqlServer().ValueGenerationStrategy);
            Assert.Equal(ValueGenerated.OnAdd, property.ValueGenerated);

            property.SqlServer().ValueGenerationStrategy = SqlServerValueGenerationStrategy.SequenceHiLo;

            Assert.Equal(SqlServerValueGenerationStrategy.SequenceHiLo, property.SqlServer().ValueGenerationStrategy);
            Assert.Equal(SqlServerValueGenerationStrategy.SequenceHiLo, ((IProperty)property).SqlServer().ValueGenerationStrategy);
            Assert.Equal(ValueGenerated.OnAdd, property.ValueGenerated);

            property.SqlServer().ValueGenerationStrategy = null;

            Assert.Null(property.SqlServer().ValueGenerationStrategy);
            Assert.Equal(ValueGenerated.OnAdd, property.ValueGenerated);
        }

        [Fact]
        public void Can_get_and_set_value_generation_on_nullable_property()
        {
            var modelBuilder = GetModelBuilder();

            var property = modelBuilder
                .Entity<Customer>()
                .Property(e => e.NullableInt)
                .Metadata;

            Assert.Null(property.SqlServer().ValueGenerationStrategy);

            property.SqlServer().ValueGenerationStrategy = SqlServerValueGenerationStrategy.SequenceHiLo;

            Assert.Equal(SqlServerValueGenerationStrategy.SequenceHiLo, property.SqlServer().ValueGenerationStrategy);
            Assert.Equal(SqlServerValueGenerationStrategy.SequenceHiLo, ((IProperty)property).SqlServer().ValueGenerationStrategy);

            property.SqlServer().ValueGenerationStrategy = null;

            Assert.Null(property.SqlServer().ValueGenerationStrategy);
        }

        [Fact]
        public void Throws_setting_sequence_generation_for_invalid_type()
        {
            var modelBuilder = GetModelBuilder();

            var property = modelBuilder
                .Entity<Customer>()
                .Property(e => e.Name)
                .Metadata;

            Assert.Equal(
                SqlServerStrings.SequenceBadType("Name", nameof(Customer), "string"),
                Assert.Throws<ArgumentException>(
                    () => property.SqlServer().ValueGenerationStrategy = SqlServerValueGenerationStrategy.SequenceHiLo).Message);
        }

        [Fact]
        public void Throws_setting_identity_generation_for_invalid_type()
        {
            var modelBuilder = GetModelBuilder();

            var property = modelBuilder
                .Entity<Customer>()
                .Property(e => e.Name)
                .Metadata;

            Assert.Equal(
                SqlServerStrings.IdentityBadType("Name", nameof(Customer), "string"),
                Assert.Throws<ArgumentException>(
                    () => property.SqlServer().ValueGenerationStrategy = SqlServerValueGenerationStrategy.IdentityColumn).Message);
        }

        [Fact]
        public void Can_get_and_set_sequence_name_on_property()
        {
            var modelBuilder = GetModelBuilder();

            var property = modelBuilder
                .Entity<Customer>()
                .Property(e => e.Id)
                .Metadata;

            Assert.Null(property.SqlServer().HiLoSequenceName);
            Assert.Null(((IProperty)property).SqlServer().HiLoSequenceName);

            property.SqlServer().HiLoSequenceName = "Snook";

            Assert.Equal("Snook", property.SqlServer().HiLoSequenceName);
            Assert.Equal("Snook", ((IProperty)property).SqlServer().HiLoSequenceName);

            property.SqlServer().HiLoSequenceName = null;

            Assert.Null(property.SqlServer().HiLoSequenceName);
            Assert.Null(((IProperty)property).SqlServer().HiLoSequenceName);
        }

        [Fact]
        public void Can_get_and_set_sequence_schema_on_property()
        {
            var modelBuilder = GetModelBuilder();

            var property = modelBuilder
                .Entity<Customer>()
                .Property(e => e.Id)
                .Metadata;

            Assert.Null(property.SqlServer().HiLoSequenceSchema);
            Assert.Null(((IProperty)property).SqlServer().HiLoSequenceSchema);

            property.SqlServer().HiLoSequenceSchema = "Tasty";

            Assert.Equal("Tasty", property.SqlServer().HiLoSequenceSchema);
            Assert.Equal("Tasty", ((IProperty)property).SqlServer().HiLoSequenceSchema);

            property.SqlServer().HiLoSequenceSchema = null;

            Assert.Null(property.SqlServer().HiLoSequenceSchema);
            Assert.Null(((IProperty)property).SqlServer().HiLoSequenceSchema);
        }

        [Fact]
        public void TryGetSequence_returns_null_if_property_is_not_configured_for_sequence_value_generation()
        {
            var modelBuilder = GetModelBuilder();

            var property = modelBuilder
                .Entity<Customer>()
                .Property(e => e.Id)
                .Metadata;

            modelBuilder.Model.SqlServer().GetOrAddSequence("DaneelOlivaw");

            Assert.Null(property.SqlServer().FindHiLoSequence());
            Assert.Null(((IProperty)property).SqlServer().FindHiLoSequence());

            property.SqlServer().HiLoSequenceName = "DaneelOlivaw";

            Assert.Null(property.SqlServer().FindHiLoSequence());
            Assert.Null(((IProperty)property).SqlServer().FindHiLoSequence());

            modelBuilder.Model.SqlServer().ValueGenerationStrategy = SqlServerValueGenerationStrategy.IdentityColumn;

            Assert.Null(property.SqlServer().FindHiLoSequence());
            Assert.Null(((IProperty)property).SqlServer().FindHiLoSequence());

            modelBuilder.Model.SqlServer().ValueGenerationStrategy = null;
            property.SqlServer().ValueGenerationStrategy = SqlServerValueGenerationStrategy.IdentityColumn;

            Assert.Null(property.SqlServer().FindHiLoSequence());
            Assert.Null(((IProperty)property).SqlServer().FindHiLoSequence());
        }

        [Fact]
        public void TryGetSequence_returns_sequence_property_is_marked_for_sequence_generation()
        {
            var modelBuilder = GetModelBuilder();

            var property = modelBuilder
                .Entity<Customer>()
                .Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .Metadata;

            modelBuilder.Model.SqlServer().GetOrAddSequence("DaneelOlivaw");
            property.SqlServer().HiLoSequenceName = "DaneelOlivaw";
            property.SqlServer().ValueGenerationStrategy = SqlServerValueGenerationStrategy.SequenceHiLo;

            Assert.Equal("DaneelOlivaw", property.SqlServer().FindHiLoSequence().Name);
            Assert.Equal("DaneelOlivaw", ((IProperty)property).SqlServer().FindHiLoSequence().Name);
        }

        [Fact]
        public void TryGetSequence_returns_sequence_property_is_marked_for_default_generation_and_model_is_marked_for_sequence_generation()
        {
            var modelBuilder = GetModelBuilder();

            var property = modelBuilder
                .Entity<Customer>()
                .Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .Metadata;

            modelBuilder.Model.SqlServer().GetOrAddSequence("DaneelOlivaw");
            modelBuilder.Model.SqlServer().ValueGenerationStrategy = SqlServerValueGenerationStrategy.SequenceHiLo;
            property.SqlServer().HiLoSequenceName = "DaneelOlivaw";

            Assert.Equal("DaneelOlivaw", property.SqlServer().FindHiLoSequence().Name);
            Assert.Equal("DaneelOlivaw", ((IProperty)property).SqlServer().FindHiLoSequence().Name);
        }

        [Fact]
        public void TryGetSequence_returns_sequence_property_is_marked_for_sequence_generation_and_model_has_name()
        {
            var modelBuilder = GetModelBuilder();

            var property = modelBuilder
                .Entity<Customer>()
                .Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .Metadata;

            modelBuilder.Model.SqlServer().GetOrAddSequence("DaneelOlivaw");
            modelBuilder.Model.SqlServer().HiLoSequenceName = "DaneelOlivaw";
            property.SqlServer().ValueGenerationStrategy = SqlServerValueGenerationStrategy.SequenceHiLo;

            Assert.Equal("DaneelOlivaw", property.SqlServer().FindHiLoSequence().Name);
            Assert.Equal("DaneelOlivaw", ((IProperty)property).SqlServer().FindHiLoSequence().Name);
        }

        [Fact]
        public void
            TryGetSequence_returns_sequence_property_is_marked_for_default_generation_and_model_is_marked_for_sequence_generation_and_model_has_name()
        {
            var modelBuilder = GetModelBuilder();

            var property = modelBuilder
                .Entity<Customer>()
                .Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .Metadata;

            modelBuilder.Model.SqlServer().GetOrAddSequence("DaneelOlivaw");
            modelBuilder.Model.SqlServer().ValueGenerationStrategy = SqlServerValueGenerationStrategy.SequenceHiLo;
            modelBuilder.Model.SqlServer().HiLoSequenceName = "DaneelOlivaw";

            Assert.Equal("DaneelOlivaw", property.SqlServer().FindHiLoSequence().Name);
            Assert.Equal("DaneelOlivaw", ((IProperty)property).SqlServer().FindHiLoSequence().Name);
        }

        [Fact]
        public void TryGetSequence_with_schema_returns_sequence_property_is_marked_for_sequence_generation()
        {
            var modelBuilder = GetModelBuilder();

            var property = modelBuilder
                .Entity<Customer>()
                .Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .Metadata;

            modelBuilder.Model.SqlServer().GetOrAddSequence("DaneelOlivaw", "R");
            property.SqlServer().HiLoSequenceName = "DaneelOlivaw";
            property.SqlServer().HiLoSequenceSchema = "R";
            property.SqlServer().ValueGenerationStrategy = SqlServerValueGenerationStrategy.SequenceHiLo;

            Assert.Equal("DaneelOlivaw", property.SqlServer().FindHiLoSequence().Name);
            Assert.Equal("DaneelOlivaw", ((IProperty)property).SqlServer().FindHiLoSequence().Name);
            Assert.Equal("R", property.SqlServer().FindHiLoSequence().Schema);
            Assert.Equal("R", ((IProperty)property).SqlServer().FindHiLoSequence().Schema);
        }

        [Fact]
        public void TryGetSequence_with_schema_returns_sequence_model_is_marked_for_sequence_generation()
        {
            var modelBuilder = GetModelBuilder();

            var property = modelBuilder
                .Entity<Customer>()
                .Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .Metadata;

            modelBuilder.Model.SqlServer().GetOrAddSequence("DaneelOlivaw", "R");
            modelBuilder.Model.SqlServer().ValueGenerationStrategy = SqlServerValueGenerationStrategy.SequenceHiLo;
            property.SqlServer().HiLoSequenceName = "DaneelOlivaw";
            property.SqlServer().HiLoSequenceSchema = "R";

            Assert.Equal("DaneelOlivaw", property.SqlServer().FindHiLoSequence().Name);
            Assert.Equal("DaneelOlivaw", ((IProperty)property).SqlServer().FindHiLoSequence().Name);
            Assert.Equal("R", property.SqlServer().FindHiLoSequence().Schema);
            Assert.Equal("R", ((IProperty)property).SqlServer().FindHiLoSequence().Schema);
        }

        [Fact]
        public void TryGetSequence_with_schema_returns_sequence_property_is_marked_for_sequence_generation_and_model_has_name()
        {
            var modelBuilder = GetModelBuilder();

            var property = modelBuilder
                .Entity<Customer>()
                .Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .Metadata;

            modelBuilder.Model.SqlServer().GetOrAddSequence("DaneelOlivaw", "R");
            modelBuilder.Model.SqlServer().HiLoSequenceName = "DaneelOlivaw";
            modelBuilder.Model.SqlServer().HiLoSequenceSchema = "R";
            property.SqlServer().ValueGenerationStrategy = SqlServerValueGenerationStrategy.SequenceHiLo;

            Assert.Equal("DaneelOlivaw", property.SqlServer().FindHiLoSequence().Name);
            Assert.Equal("DaneelOlivaw", ((IProperty)property).SqlServer().FindHiLoSequence().Name);
            Assert.Equal("R", property.SqlServer().FindHiLoSequence().Schema);
            Assert.Equal("R", ((IProperty)property).SqlServer().FindHiLoSequence().Schema);
        }

        [Fact]
        public void TryGetSequence_with_schema_returns_sequence_model_is_marked_for_sequence_generation_and_model_has_name()
        {
            var modelBuilder = GetModelBuilder();

            var property = modelBuilder
                .Entity<Customer>()
                .Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .Metadata;

            modelBuilder.Model.SqlServer().GetOrAddSequence("DaneelOlivaw", "R");
            modelBuilder.Model.SqlServer().ValueGenerationStrategy = SqlServerValueGenerationStrategy.SequenceHiLo;
            modelBuilder.Model.SqlServer().HiLoSequenceName = "DaneelOlivaw";
            modelBuilder.Model.SqlServer().HiLoSequenceSchema = "R";

            Assert.Equal("DaneelOlivaw", property.SqlServer().FindHiLoSequence().Name);
            Assert.Equal("DaneelOlivaw", ((IProperty)property).SqlServer().FindHiLoSequence().Name);
            Assert.Equal("R", property.SqlServer().FindHiLoSequence().Schema);
            Assert.Equal("R", ((IProperty)property).SqlServer().FindHiLoSequence().Schema);
        }

        private static ModelBuilder GetModelBuilder() => SqlServerTestHelpers.Instance.CreateConventionBuilder();

        private class Customer
        {
            public int Id { get; set; }
            public int? NullableInt { get; set; }
            public string Name { get; set; }
            public byte Byte { get; set; }
            public byte? NullableByte { get; set; }
            public byte[] ByteArray { get; set; }
        }

        private class Order
        {
            public int OrderId { get; set; }
            public int CustomerId { get; set; }
        }
    }
}
