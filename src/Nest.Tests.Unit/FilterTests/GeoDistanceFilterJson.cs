﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

using Nest.Tests.MockData.Domain;

namespace Nest.Tests.Unit.FilterTests
{
	[TestFixture]
	public class GeoDistanceFilterJson
	{
		[Test]
		public void GeoDistanceFilter()
		{
			var s = new SearchDescriptor<ElasticSearchProject>()
				.From(0)
				.Size(10)
				.Filter(filter => filter
					.Cache(true)
					.Name("my_geo_filter")
					.GeoDistance(f=>f.Origin, d=>d
						.Location(X: 40, Y: -70)
						.Distance(12, GeoUnit.km)
						.Optimize(GeoOptimizeBBox.memory)
					)
				);

			var json = ElasticClient.Serialize(s);
			var expected = @"{ from: 0, size: 10, 
				filter : {
					geo_distance: {
						distance: 12.0,
						distance_type: ""km"",
						optimize_bbox: ""memory"",
						origin: ""40, -70"",
						_cache: true,
						_name: ""my_geo_filter""
					}
				}
			}";
			Assert.True(json.JsonEquals(expected), json);
		}
		
	}
}
