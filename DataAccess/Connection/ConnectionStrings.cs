using System;
using System.Configuration;

namespace DataAccess.Connection
{
    internal class ConnectionStrings
    {
        internal string Local
        {
            get
            {
                var settings = ConfigurationManager.ConnectionStrings["Local"];

                if (settings == null)
                {
                    throw new Exception("Unable to find connection string.");
                }

                return settings.ConnectionString;
            }
        }

        internal string Azure
        {
            get
            {
                var settings = ConfigurationManager.ConnectionStrings["Azure"];

                if (settings == null)
                {
                    throw new Exception("Unable to find connection string.");
                }

                return settings.ConnectionString;
            }
        }

        internal string Kraka
        {
            get
            {
                var settings = ConfigurationManager.ConnectionStrings["Kraka"];

                if (settings == null)
                {
                    throw new Exception("Unable to find connection string.");
                }

                return settings.ConnectionString;
            }
        }
    }
}
