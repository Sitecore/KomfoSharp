// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IAddCalling.cs" company="Sitecore A/S">
//  Copyright (C) 2015 by Sitecore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace KomfoSharp.Sessions.Authenticated.Requests.Fluent
{
  using System;
  using KomfoSharp.Sessions.Fluent;

  /// <summary>
  /// Defines the call to add an entity.
  /// </summary>
  /// <typeparam name="T">The result of the call.</typeparam>
  /// <typeparam name="TEntityBuilder">The type of the entity builder.</typeparam>
  /// <typeparam name="TEntity">The type of the entity.</typeparam>
  public interface IAddCalling<out T, out TEntityBuilder, in TEntity>
  {
    /// <summary>
    /// Adds the specified entity.
    /// </summary>
    /// <param name="entitySetupFunc">The entity setup function.</param>
    /// <returns>The result of the call.</returns>
    T Add(Func<TEntityBuilder, ICreateCalling<TEntity>> entitySetupFunc);
  }
}