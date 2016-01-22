// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IXmlSource.cs" company="Sitecore A/S">
//  Copyright (C) 2015 by Sitecore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace KomfoSharp.Configuration.Providers
{
  using System.Xml;

  /// <summary>
  /// Defines abstraction for xml source.
  /// </summary>
  public interface IXmlSource
  {
    /// <summary>
    /// Gets the XML.
    /// </summary>
    /// <returns>The <see cref="XmlNode"/> instance.</returns>
    XmlNode GetXml();
  }
}
