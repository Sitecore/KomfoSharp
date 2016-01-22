// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IConfigurationProvider.cs" company="Sitecore A/S">
//  Copyright (C) 2015 by Sitecore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace KomfoSharp.Configuration.Providers
{
  /// <summary>
  /// Responsible for retrieving configuration.
  /// </summary>
  public interface IConfigurationProvider
  {
    /// <summary>
    /// Gets the configuration.
    /// </summary>
    /// <returns>A <see cref="Configuration"/> instance.</returns>
    Configuration GetConfiguration();
  }
}
