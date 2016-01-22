// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Sha256Hash.cs" company="Sitecore A/S">
//  Copyright (C) 2015 by Sitecore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace KomfoSharp.Provider
{
  using System.Globalization;
  using System.Security.Cryptography;
  using System.Text;

  /// <summary>
  /// Provides methods to compute SHA256 hash.
  /// </summary>
  public class Sha256Hash : ISha256Hash
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="Sha256Hash"/> class.
    /// </summary>
    public Sha256Hash()
    {
      this.Sha256 = SHA256.Create();
    }

    /// <summary>
    /// Gets the SHA256.
    /// </summary>
    /// <value>
    /// The SHA256.
    /// </value>
    protected SHA256 Sha256 { get; private set; }

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
    public string Compute(string inputData)
    {
      var hashString = new StringBuilder(string.Empty);

      foreach (var x in this.Sha256.ComputeHash(Encoding.UTF8.GetBytes(inputData)))
      {
        hashString.AppendFormat(CultureInfo.InvariantCulture, "{0:x2}", x);
      }

      return hashString.ToString();
    }
  }
}