﻿// Copyright (c) Microsoft. All rights reserved.

using PineconeIntegrationTests.Support;
using VectorDataSpecificationTests.Filter;
using VectorDataSpecificationTests.Support;
using VectorDataSpecificationTests.Xunit;
using Xunit;

namespace PineconeIntegrationTests.Filter;

public class PineconeBasicFilterTests(PineconeBasicFilterTests.Fixture fixture)
    : BasicFilterTests<string>(fixture), IClassFixture<PineconeBasicFilterTests.Fixture>
{
    // Specialized Pinecone syntax for NOT over Contains ($nin)
    [ConditionalFact]
    public virtual Task Not_over_Contains()
        => this.TestFilterAsync(r => !new[] { 8, 10 }.Contains(r.Int));

    #region Null checking

    // Pinecone currently doesn't support null checking ({ "Foo" : null }) in vector search pre-filters
    public override Task Equal_with_null_reference_type()
        => Assert.ThrowsAsync<NotSupportedException>(() => base.Equal_with_null_reference_type());

    public override Task Equal_with_null_captured()
        => Assert.ThrowsAsync<NotSupportedException>(() => base.Equal_with_null_captured());

    public override Task NotEqual_with_null_reference_type()
        => Assert.ThrowsAsync<NotSupportedException>(() => base.NotEqual_with_null_reference_type());

    public override Task NotEqual_with_null_captured()
        => Assert.ThrowsAsync<NotSupportedException>(() => base.NotEqual_with_null_captured());

    #endregion

    #region Not

    // Pinecone currently doesn't support NOT in vector search pre-filters
    // (https://www.mongodb.com/docs/atlas/atlas-vector-search/vector-search-stage/#atlas-vector-search-pre-filter)
    public override Task Not_over_And()
        => Assert.ThrowsAsync<NotSupportedException>(() => base.Not_over_And());

    public override Task Not_over_Or()
        => Assert.ThrowsAsync<NotSupportedException>(() => base.Not_over_Or());

    #endregion

    public override Task Contains_over_field_string_array()
        => Assert.ThrowsAsync<NotSupportedException>(() => base.Contains_over_field_string_array());

    public override Task Contains_over_field_string_List()
        => Assert.ThrowsAsync<NotSupportedException>(() => base.Contains_over_field_string_List());

    // AnyTagEqualTo not (currently) supported on Pinecone
    [Obsolete("Legacy filter support")]
    public override Task Legacy_AnyTagEqualTo_array()
        => Assert.ThrowsAsync<NotSupportedException>(() => base.Legacy_AnyTagEqualTo_array());

    [Obsolete("Legacy filter support")]
    public override Task Legacy_AnyTagEqualTo_List()
        => Assert.ThrowsAsync<NotSupportedException>(() => base.Legacy_AnyTagEqualTo_List());

    public new class Fixture : BasicFilterTests<string>.Fixture
    {
        public override TestStore TestStore => PineconeTestStore.Instance;

        // https://docs.pinecone.io/troubleshooting/restrictions-on-index-names
        protected override string CollectionName => "filter-tests";
    }
}
