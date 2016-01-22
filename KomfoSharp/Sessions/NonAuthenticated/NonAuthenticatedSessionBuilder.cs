// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NonAuthenticatedSessionBuilder.cs" company="Sitecore A/S">
//  Copyright (C) 2015 by Sitecore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace KomfoSharp.Sessions.NonAuthenticated
{
  using KomfoSharp.Configuration.Providers;
  using KomfoSharp.Provider;

  /// <summary>
  /// Defines the methods that are used to build the non-authenticated session.
  /// </summary>
  public class NonAuthenticatedSessionBuilder : INonAuthenticatedSessionBuilder
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="NonAuthenticatedSessionBuilder" /> class.
    /// </summary>
    /// <param name="configurationProvider">The configuration provider.</param>
    /// <param name="komfoProvider">The Komfo provider.</param>
    public NonAuthenticatedSessionBuilder(IConfigurationProvider configurationProvider, IKomfoProvider komfoProvider)
    {
      this.ConfigurationProvider = configurationProvider;
      this.KomfoProvider = komfoProvider;
    }

    /// <summary>
    /// Gets the Komfo provider.
    /// </summary>
    /// <value>
    /// The Komfo provider.
    /// </value>
    protected IKomfoProvider KomfoProvider { get; private set; }

    /// <summary>
    /// Gets the configuration provider.
    /// </summary>
    /// <value>
    /// The configuration provider.
    /// </value>
    protected IConfigurationProvider ConfigurationProvider { get; private set; }

    /// <summary>
    /// Creates the non-authenticated session.
    /// </summary>
    /// <returns>
    /// The <see cref="INonAuthenticatedSession" />.
    /// </returns>
    public INonAuthenticatedSession Create()
    {
      return new NonAuthenticatedSession(this.ConfigurationProvider, this.KomfoProvider);
    }
  }
}