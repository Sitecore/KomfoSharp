// --------------------------------------------------------------------------------------------------------------------
// <copyright file="KomfoSharpSectionHandler.cs" company="Sitecore A/S">
//  Copyright (C) 2015 by Sitecore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace KomfoSharp.Configuration.Providers.AppConfig
{
  using System.Configuration;
  using System.Xml;
  using KomfoSharp.Diagnostics;

  /// <summary>
  /// The "komfoSharp" section handler.
  /// </summary>
  public class KomfoSharpSectionHandler : IConfigurationSectionHandler
  {
    /// <summary>
    /// The "komfoSharp" section node.
    /// </summary>
    private XmlNode komfoSharpSection;

    /// <summary>
    /// Creates a configuration section handler.
    /// </summary>
    /// <param name="parent">Parent object.</param>
    /// <param name="configContext">Configuration context object.</param>
    /// <param name="section">Section XML node.</param>
    /// <returns>
    /// The created section handler object.
    /// </returns>
    public object Create(object parent, object configContext, XmlNode section)
    {
      this.komfoSharpSection = section;
      return this;
    }

    /// <summary>
    /// Gets the node.
    /// </summary>
    /// <param name="xPath">The x path.</param>
    /// <returns>The <see cref="XmlNode"/> instance.</returns>
    public XmlNode GetNode(string xPath)
    {
      Assert.ArgumentNotNull(xPath, "xPath");

      return this.komfoSharpSection.SelectSingleNode(xPath);
    }

    /// <summary>
    /// Gets the root node.
    /// </summary>
    /// <returns>The <see cref="XmlNode"/> instance.</returns>
    public XmlNode GetRootNode()
    {
      return this.komfoSharpSection;
    }
  }
}
