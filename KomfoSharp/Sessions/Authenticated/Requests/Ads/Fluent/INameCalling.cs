// --------------------------------------------------------------------------------------------------------------------
// <copyright file="INameCalling.cs" company="Sitecore A/S">
//  Copyright (C) 2015 by Sitecore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace KomfoSharp.Sessions.Authenticated.Requests.Ads.Fluent
{
  /// <summary>
  /// Defines the call to specify the name.
  /// </summary>
  /// <typeparam name="T">The result of the call.</typeparam>
  public interface INameCalling<out T>
  {
    /// <summary>
    /// Specifies the name.
    /// </summary>
    /// <param name="name">The name.</param>
    /// <returns>The result of the call.</returns>
    T Name(string name);
  }
}