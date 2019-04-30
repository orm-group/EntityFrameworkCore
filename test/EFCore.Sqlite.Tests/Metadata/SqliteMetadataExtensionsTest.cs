// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Xunit;

namespace Microsoft.EntityFrameworkCore.Metadata
{
    public class SqliteMetadataExtensionsTest
    {
        [Fact]
        public void Can_get_and_set_column_name()
        {
            var modelBuilder = new ModelBuilder(new ConventionSet());

            var property = modelBuilder
                .Entity<Customer>()
                .Property(e => e.Name)
                .Metadata;

            Assert.Equal("Name", property.Relational().GetColumnName());
            Assert.Equal("Name", ((IProperty)property).Relational().GetColumnName());

            property.Relational().ColumnName = "Eman";

            Assert.Equal("Eman", property.Relational().GetColumnName());
            Assert.Equal("Eman", ((IProperty)property).Relational().GetColumnName());

            property.Relational().ColumnName = "MyNameIs";

            Assert.Equal("MyNameIs", property.Relational().GetColumnName());
            Assert.Equal("MyNameIs", ((IProperty)property).Relational().GetColumnName());

            property.Relational().ColumnName = null;

            Assert.Equal("Name", property.Relational().GetColumnName());
            Assert.Equal("Name", ((IProperty)property).Relational().GetColumnName());
        }

        [Fact]
        public void Can_get_and_set_table_name()
        {
            var modelBuilder = new ModelBuilder(new ConventionSet());

            var entityType = modelBuilder
                .Entity<Customer>()
                .Metadata;

            Assert.Equal("Customer", entityType.Relational().GetTableName());
            Assert.Equal("Customer", ((IEntityType)entityType).Relational().GetTableName());

            entityType.Relational().TableName = "Customizer";

            Assert.Equal("Customizer", entityType.Relational().GetTableName());
            Assert.Equal("Customizer", ((IEntityType)entityType).Relational().GetTableName());

            entityType.Relational().TableName = "Custardizer";

            Assert.Equal("Custardizer", entityType.Relational().GetTableName());
            Assert.Equal("Custardizer", ((IEntityType)entityType).Relational().GetTableName());

            entityType.Relational().TableName = null;

            Assert.Equal("Customer", entityType.Relational().GetTableName());
            Assert.Equal("Customer", ((IEntityType)entityType).Relational().GetTableName());
        }

        [Fact]
        public void Can_get_schema_name()
        {
            var modelBuilder = new ModelBuilder(new ConventionSet());

            var entityType = modelBuilder
                .Entity<Customer>()
                .Metadata;

            Assert.Null(entityType.Relational().GetSchema());
            Assert.Null(((IEntityType)entityType).Relational().GetSchema());

            entityType.Relational().Schema = "db0";

            Assert.Equal("db0", entityType.Relational().GetSchema());
            Assert.Equal("db0", ((IEntityType)entityType).Relational().GetSchema());
        }

        [Fact]
        public void Can_get_and_set_column_type()
        {
            var modelBuilder = new ModelBuilder(new ConventionSet());

            var property = modelBuilder
                .Entity<Customer>()
                .Property(e => e.Name)
                .Metadata;

            Assert.Null(property.Relational().GetColumnType());
            Assert.Null(((IProperty)property).Relational().GetColumnType());

            property.Relational().ColumnType = "nvarchar(max)";

            Assert.Equal("nvarchar(max)", property.Relational().GetColumnType());
            Assert.Equal("nvarchar(max)", ((IProperty)property).Relational().GetColumnType());

            property.Relational().ColumnType = "nvarchar(verstappen)";

            Assert.Equal("nvarchar(verstappen)", property.Relational().GetColumnType());
            Assert.Equal("nvarchar(verstappen)", ((IProperty)property).Relational().GetColumnType());

            property.Relational().ColumnType = null;

            Assert.Null(property.Relational().GetColumnType());
            Assert.Null(((IProperty)property).Relational().GetColumnType());
        }

        [Fact]
        public void Can_get_and_set_column_default_expression()
        {
            var modelBuilder = new ModelBuilder(new ConventionSet());

            var property = modelBuilder
                .Entity<Customer>()
                .Property(e => e.Name)
                .Metadata;

            Assert.Null(property.Relational().GetDefaultValueSql());
            Assert.Null(((IProperty)property).Relational().GetDefaultValueSql());

            property.Relational().DefaultValueSql = "newsequentialid()";

            Assert.Equal("newsequentialid()", property.Relational().GetDefaultValueSql());
            Assert.Equal("newsequentialid()", ((IProperty)property).Relational().GetDefaultValueSql());

            property.Relational().DefaultValueSql = "expressyourself()";

            Assert.Equal("expressyourself()", property.Relational().GetDefaultValueSql());
            Assert.Equal("expressyourself()", ((IProperty)property).Relational().GetDefaultValueSql());

            property.Relational().DefaultValueSql = null;

            Assert.Null(property.Relational().GetDefaultValueSql());
            Assert.Null(((IProperty)property).Relational().GetDefaultValueSql());
        }

        [Fact]
        public void Can_get_and_set_column_key_name()
        {
            var modelBuilder = new ModelBuilder(new ConventionSet());

            var key = modelBuilder
                .Entity<Customer>()
                .HasKey(e => e.Id)
                .Metadata;

            Assert.Equal("PK_Customer", key.Relational().Name);
            Assert.Equal("PK_Customer", ((IKey)key).Relational().Name);

            key.Relational().Name = "PrimaryKey";

            Assert.Equal("PrimaryKey", key.Relational().Name);
            Assert.Equal("PrimaryKey", ((IKey)key).Relational().Name);

            key.Relational().Name = "PrimarySchool";

            Assert.Equal("PrimarySchool", key.Relational().Name);
            Assert.Equal("PrimarySchool", ((IKey)key).Relational().Name);

            key.Relational().Name = null;

            Assert.Equal("PK_Customer", key.Relational().Name);
            Assert.Equal("PK_Customer", ((IKey)key).Relational().Name);
        }

        [Fact]
        public void Can_get_and_set_column_foreign_key_name()
        {
            var modelBuilder = new ModelBuilder(new ConventionSet());

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
            var modelBuilder = new ModelBuilder(new ConventionSet());

            var index = modelBuilder
                .Entity<Customer>()
                .HasIndex(e => e.Id)
                .Metadata;

            Assert.Equal("IX_Customer_Id", index.Relational().GetName());
            Assert.Equal("IX_Customer_Id", ((IIndex)index).Relational().GetName());

            index.Relational().Name = "MyIndex";

            Assert.Equal("MyIndex", index.Relational().GetName());
            Assert.Equal("MyIndex", ((IIndex)index).Relational().GetName());

            index.Relational().Name = "DexKnows";

            Assert.Equal("DexKnows", index.Relational().GetName());
            Assert.Equal("DexKnows", ((IIndex)index).Relational().GetName());

            index.Relational().Name = null;

            Assert.Equal("IX_Customer_Id", index.Relational().GetName());
            Assert.Equal("IX_Customer_Id", ((IIndex)index).Relational().GetName());
        }

        [Fact]
        public void Can_get_and_set_sequence()
        {
            var modelBuilder = new ModelBuilder(new ConventionSet());
            var model = modelBuilder.Model;

            Assert.Null(model.Relational().FindSequence("Foo"));
            Assert.Null(model.Relational().FindSequence("Foo"));
            Assert.Null(((IModel)model).Relational().FindSequence("Foo"));

            var sequence = model.Relational().GetOrAddSequence("Foo");

            Assert.Equal("Foo", model.Relational().FindSequence("Foo").Name);
            Assert.Equal("Foo", model.Relational().FindSequence("Foo").Name);
            Assert.Equal("Foo", ((IModel)model).Relational().FindSequence("Foo").Name);

            Assert.Equal("Foo", sequence.Name);
            Assert.Null(sequence.Schema);
            Assert.Equal(1, sequence.IncrementBy);
            Assert.Equal(1, sequence.StartValue);
            Assert.Null(sequence.MinValue);
            Assert.Null(sequence.MaxValue);
            Assert.Same(typeof(long), sequence.ClrType);

            var sequence2 = model.Relational().FindSequence("Foo");

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
        public void Can_get_multiple_sequences()
        {
            var modelBuilder = new ModelBuilder(new ConventionSet());
            var model = modelBuilder.Model;

            model.Relational().GetOrAddSequence("Fibonacci");
            model.Relational().GetOrAddSequence("Golomb");

            var sequences = model.Relational().Sequences;

            Assert.Equal(2, sequences.Count);
            Assert.Contains(sequences, s => s.Name == "Fibonacci");
            Assert.Contains(sequences, s => s.Name == "Golomb");
        }

        private class Customer
        {
            public int Id { get; set; }
            public string Name { get; set; }
        }

        private class Order
        {
            public int CustomerId { get; set; }
        }
    }
}
