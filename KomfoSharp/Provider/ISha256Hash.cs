// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ISha256Hash.cs" company="Sitecore A/S">
//  Copyright (C) 2015 by Sitecore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace KomfoSharp.Provider
{
  /// <summary>
  /// Defines methods to compute SHA256 hash.
  /// </summary>
  public interface ISha256Hash
  {
    /// <summary>
    /// Computes the SHA256 hash for the input data.
    /// </summary>
    /// <param name="inputData">The input data.</param>
    /// <returns>
    /// The SHA256 hash.
    /// </returns>
    /// <example>
    ///   <code>
    /// Console.WriteLine(sha256Hash.Compute("user1@domain.com"));
    /// </code>
    /// </example>
    string Compute(string inputData);
  }
}