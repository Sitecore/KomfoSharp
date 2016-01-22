// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IWithTokenRenewalCalling.cs" company="Sitecore A/S">
//  Copyright (C) 2015 by Sitecore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace KomfoSharp.Sessions.Authenticated.Fluent
{
  /// <summary>
  /// Defines the call to enable the token renewal in the current session builder.
  /// </summary>
  public interface IWithTokenRenewalCalling
  {
    /// <summary>
    /// Enables the token renewal in the current session builder.
    /// </summary>
    /// <returns>The current session builder.</returns>
    IWithTokenRenewalCalled WithTokenRenewal();
  }
}