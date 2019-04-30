// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Xunit;

// ReSharper disable InconsistentNaming
namespace Microsoft.EntityFrameworkCore
{
    public class ScaffoldingMetadataExtensionsTest
    {
        [Fact]
        public void It_sets_gets_entity_type_errors()
        {
            IMutableModel model = new Model();

            model.Scaffolding().GetEntityTypeErrors().Add("ET", "FAIL!");
            Assert.Equal("FAIL!", model.Scaffolding().GetEntityTypeErrors()["ET"]);

            model.Scaffolding().SetEntityTypeErrors(new Dictionary<string, string>());
            Assert.Empty(model.Scaffolding().GetEntityTypeErrors().Values);

            model.Scaffolding().GetEntityTypeErrors()["ET"] = "FAIL 2!";
            model.Scaffolding().GetEntityTypeErrors().Clear();
            Assert.Empty(model.Scaffolding().GetEntityTypeErrors().Values);
        }

        [Fact]
        public void It_sets_DbSet_name()
        {
            IMutableModel model = new Model();
            var entity = model.AddEntityType("Blog");
            entity.Scaffolding().SetDbSetName("Blogs");

            Assert.Equal("Blogs", entity.Scaffolding().GetDbSetName());
        }

        [Fact]
        public void It_sets_gets_database_name()
        {
            var model = new Model();
            var extensions = model.Scaffolding();

            Assert.Null(extensions.GetDatabaseName());

            extensions.SetDatabaseName("Northwind");

            Assert.Equal("Northwind", extensions.GetDatabaseName());

            extensions.SetDatabaseName(null);

            Assert.Null(extensions.GetDatabaseName());
        }
    }
}
