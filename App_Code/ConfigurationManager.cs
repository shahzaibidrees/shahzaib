using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Win32;
using System.Configuration;

/// <summary>
///     The configuration manager handles the storing and retrieval of configuration values
///     the default implementation is against the registry.
/// </summary>
public static class BVisionConfigurationManager
{
    #region Connection
    //Retrieve the configuration string for Conferencely
    public static string GetConnectionString()
    {
        return GetConnectionString("Vcard");
    }

    public static string GetConnectionString(string connectionStringName)
    {
        return ConfigurationManager.ConnectionStrings[connectionStringName].ConnectionString;
    }
    #endregion Connection

}
