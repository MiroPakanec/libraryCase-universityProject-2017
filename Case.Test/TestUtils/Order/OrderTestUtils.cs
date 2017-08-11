using System;

namespace Case.Test.TestUtils.Order
{
    public class OrderTestUtils
    {
        public static Core.Entity.Order GenerateValidOrder()
        {
            return new Core.Entity.Order()
            {
                Id = 1,
                Created = DateTime.Now,
                MemberId = "099512 - 1247"
            };
        }

        public static Core.Entity.Order GenerateInvalidOrder()
        {
            return new Core.Entity.Order()
            {
                Id = -1,
                Created = DateTime.MinValue,
                MemberId = "5"
            };
        }
    }
}