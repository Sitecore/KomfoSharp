// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IClientIdCalling.cs" company="Sitecore A/S">
//  Copyright (C) 2015 by Sitecore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace KomfoSharp.Sessions.Fluent
{
  /// <summary>
  /// Defines the call to specify a client ID.
  /// </summary>
  /// <typeparam name="T">The result of the call.</typeparam>
  public interface IClientIdCalling<out T>
  {
    /// <summary>
    /// Specifies the client ID.
    /// </summary>
    /// <param name="clientId">The client identifier.</param>
    /// <returns>The result of the call.</returns>
    T ClientId(string clientId);
  }
}