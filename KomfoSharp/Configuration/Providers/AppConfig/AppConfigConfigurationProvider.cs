// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AppConfigConfigurationProvider.cs" company="Sitecore A/S">
//  Copyright (C) 2015 by Sitecore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace KomfoSharp.Configuration.Providers.AppConfig
{
  using System.Configuration;
  using System.Xml;
  using KomfoSharp.Diagnostics;

  /// <summary>
  /// Configuration provider, which reads configuration from config file of an application.
  /// </summary>
  public class AppConfigConfigurationProvider : XmlConfigurationProviderBase
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="AppConfigConfigurationProvider"/> class.
    /// </summary>
    public AppConfigConfigurationProvider() : this(new AppConfigXmlSource())
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="AppConfigConfigurationProvider"/> class.
    /// </summary>
    /// <param name="xmlSource">The XML source.</param>
    public AppConfigConfigurationProvider(IXmlSource xmlSource) : base(xmlSource)
    {
    }

    /// <summary>
    /// Gets XML from application configuration file.
    /// </summary>
    private class AppConfigXmlSource : IXmlSource
    {
      /// <summary>
      /// Gets the XML.
      /// </summary>
      /// <returns>
      /// The <see cref="XmlNode" /> instance, which represents the "komfoSharp" XML node.
      /// </returns>
      public XmlNode GetXml()
      {
        var sectionHandler = (KomfoSharpSectionHandler)ConfigurationManager.GetSection("komfoSharp");
        Assert.NotNull(sectionHandler, "Could not retrieve the \"komfoSharp\" section.");

        return sectionHandler.GetRootNode();
      }
    }
  }
}
