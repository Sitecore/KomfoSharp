// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IClientSecretCalling.cs" company="Sitecore A/S">
//  Copyright (C) 2015 by Sitecore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace KomfoSharp.Sessions.Fluent
{
  /// <summary>
  /// Defines the call to specify a client secret.
  /// </summary>
  /// <typeparam name="T">The result of the call.</typeparam>
  public interface IClientSecretCalling<out T>
  {
    /// <summary>
    /// Specifies the client secret.
    /// </summary>
    /// <param name="clientSecret">The client secret.</param>
    /// <returns>The result of the call.</returns>
    T ClientSecret(string clientSecret);
  }
}