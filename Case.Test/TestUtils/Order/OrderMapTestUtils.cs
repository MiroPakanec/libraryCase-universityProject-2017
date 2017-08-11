using System;
using System.Runtime.InteropServices;
using DataAccess.EntityMaps;
using DataAccess.EntityMaps.Order;

namespace Case.Test.TestUtils.Order
{
    public class OrderMapTestUtils
    {
        internal static IMap GenerateValidOrderMap()
        {
            return new OrderMap()
            {
                Id = 1,
                Created = DateTime.Now,
                MemberSsn = "099512-1247"
            };
        }
    }
}