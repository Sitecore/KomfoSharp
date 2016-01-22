// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ICreateCalling.cs" company="Sitecore A/S">
//  Copyright (C) 2015 by Sitecore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace KomfoSharp.Sessions.Fluent
{
  /// <summary>
  /// Defines the call to create an instance.
  /// </summary>
  /// <typeparam name="T">The instance.</typeparam>
  public interface ICreateCalling<out T>
  {
    /// <summary>
    /// Creates an instance.
    /// </summary>
    /// <returns>The instance.</returns>
    T Create();
  }
}