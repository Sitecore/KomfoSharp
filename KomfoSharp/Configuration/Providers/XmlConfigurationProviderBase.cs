// --------------------------------------------------------------------------------------------------------------------
// <copyright file="XmlConfigurationProviderBase.cs" company="Sitecore A/S">
//  Copyright (C) 2015 by Sitecore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace KomfoSharp.Configuration.Providers
{
  using System;
  using System.Collections.Generic;
  using System.ComponentModel.DataAnnotations;
  using System.Linq;
  using System.Reflection;
  using System.Xml;
  using System.Xml.Serialization;
  using KomfoSharp.Configuration.Endpoints;
  using KomfoSharp.Diagnostics;

  /// <summary>
  /// Base class for XML-based configuration providers. Converts XML to configuration POCO classes.
  /// </summary>
  public abstract class XmlConfigurationProviderBase : IConfigurationProvider
  {
    /// <summary>
    /// The end point classes
    /// </summary>
    private static readonly IReadOnlyCollection<Type> EndPointClasses;

    /// <summary>
    /// The XML source
    /// </summary>
    private readonly IXmlSource xmlSource;

    /// <summary>
    /// The configuration
    /// </summary>
    private Configuration configuration;

    /// <summary>
    /// Initializes the <see cref="XmlConfigurationProviderBase"/> class.
    /// </summary>
    static XmlConfigurationProviderBase()
    {
      EndPointClasses = Assembly.GetExecutingAssembly().GetTypes().Where(type => typeof(EndpointBase).IsAssignableFrom(type)).ToList().AsReadOnly();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="XmlConfigurationProviderBase"/> class.
    /// </summary>
    /// <param name="xmlSource">The XML source.</param>
    protected XmlConfigurationProviderBase(IXmlSource xmlSource)
    {
      Assert.ArgumentNotNull(xmlSource, "xmlSource");
      this.xmlSource = xmlSource;
    }

    /// <summary>
    /// Gets the configuration.
    /// </summary>
    /// <returns>
    /// A <see cref="Configuration" /> instance.
    /// </returns>
    public virtual Configuration GetConfiguration()
    {
      if (this.configuration != null)
      {
        return this.configuration;
      }

      var rootNode = this.xmlSource.GetXml();
      Assert.NotNull(rootNode, "Could not retrieve the 'komfoSharp' configuration node.");

      var endpointsNode = rootNode.SelectSingleNode("services/endpoints");
      Assert.NotNull(endpointsNode, "Could not retrieve the 'komfoSharp/services/endpoints' configuration node.");

      var pollingNode = rootNode.SelectSingleNode("polling");
      Assert.NotNull(pollingNode, "Could not retrieve the 'komfoSharp/polling' configuration node.");

      var endpointsConfiguration = this.GetEndpointsConfiguration(endpointsNode);
      var pollingConfiguration = this.GetPollingConfiguration(pollingNode);

      this.configuration = new Configuration
      {
        EndpointsConfiguration = endpointsConfiguration,
        PollingConfiguration = pollingConfiguration
      };

      return this.configuration;
    }

    /// <summary>
    /// Gets the endpoints configuration.
    /// </summary>
    /// <param name="endpointsNode">The endpoints node.</param>
    /// <returns></returns>
    /// <exception cref="System.InvalidOperationException">
    /// The 'baseUrl' value should be a valid URL.
    /// </exception>
    protected virtual EndpointsConfiguration GetEndpointsConfiguration(XmlNode endpointsNode)
    {
      var baseUrlNode = endpointsNode.Attributes["baseUrl"];
      Assert.NotNull(baseUrlNode, "The 'baseUrl' attribute is required for the 'komfoSharp/services/endpoints' node.");
      Assert.NotNullOrEmpty(baseUrlNode.Value, "The 'baseUrl' value should be a valid URL.");

      Uri baseUrl;
      if (!Uri.TryCreate(baseUrlNode.Value, UriKind.Absolute, out baseUrl) || !(baseUrl.Scheme == Uri.UriSchemeHttp || baseUrl.Scheme == Uri.UriSchemeHttps))
      {
        throw new InvalidOperationException("The 'baseUrl' value should be a valid URL.");
      }

      var endpoints = new List<EndpointBase>();

      foreach (XmlNode node in endpointsNode.ChildNodes)
      {
        var endpoint = this.DeserializeEndpoint(node);
        endpoints.Add(endpoint);
      }

      var endpointsConfiguration = new EndpointsConfiguration(endpoints, new Uri(baseUrlNode.Value));
      return endpointsConfiguration;
    }

    /// <summary>
    /// Deserializes the endpoint.
    /// </summary>
    /// <param name="node">The node.</param>
    /// <returns>The appropriate <see cref="EndpointBase"/> instance.</returns>
    /// <exception cref="System.InvalidOperationException"></exception>
    protected virtual EndpointBase DeserializeEndpoint(XmlNode node)
    {
      var type = EndPointClasses.FirstOrDefault(c => c.Name.StartsWith(node.Name, StringComparison.OrdinalIgnoreCase));
      Assert.NotNull(type, string.Format("Could not find a class representation of the {0} endpoint.", node.Name));

      EndpointBase endpoint;

      try
      {
        var xmlSerializer = new XmlSerializer(type, new XmlRootAttribute(node.Name));
        endpoint = (EndpointBase)xmlSerializer.Deserialize(new XmlNodeReader(node));
        var context = new ValidationContext(endpoint, null, null);
        Validator.ValidateObject(endpoint, context, true);
      }
      catch (Exception exception)
      {
        throw new InvalidOperationException(string.Format("There were errors during deserializing the 'komfoSharp/services/endpoints/{0}' node into the {1} type. See inner exception for details.", node.Name, type.FullName), exception);
      }

      return endpoint;
    }

    /// <summary>
    /// Gets the polling configuration.
    /// </summary>
    /// <param name="pollingNode">The polling node.</param>
    /// <returns></returns>
    /// <exception cref="System.InvalidOperationException">
    /// Could not parse the 'defaultAttemptsCount' attribute. Only integer values are allowed.
    /// or
    /// Could not parse the 'defaultTimeInterval' attribute. Only TimeSpan values are allowed.
    /// </exception>
    protected virtual PollingConfiguration GetPollingConfiguration(XmlNode pollingNode)
    {
      Assert.NotNull(pollingNode.Attributes["defaultAttemptsCount"], "The 'defaultAttemptsCount' attribute is required for the 'komfoSharp/polling' node.");
      Assert.NotNull(pollingNode.Attributes["defaultTimeInterval"], "The 'defaultTimeInterval' attribute is required for the 'komfoSharp/polling' node.");

      int defaultAttemptsCount;
      TimeSpan defaultTimeInterval;

      if (!int.TryParse(pollingNode.Attributes["defaultAttemptsCount"].Value, out defaultAttemptsCount) || defaultAttemptsCount <= 0)
      {
        throw new InvalidOperationException("Could not parse the 'defaultAttemptsCount' attribute. Only positive integer values are allowed.");
      }

      if (!TimeSpan.TryParse(pollingNode.Attributes["defaultTimeInterval"].Value, out defaultTimeInterval) || defaultTimeInterval <= TimeSpan.Zero)
      {
        throw new InvalidOperationException("Could not parse the 'defaultTimeInterval' attribute. Only positive TimeSpan values are allowed.");
      }

      var pollingConfiguration = new PollingConfiguration
      {
        DefaultAttemptsCount = defaultAttemptsCount,
        DefaultTimeInterval = defaultTimeInterval
      };

      return pollingConfiguration;
    }
  }
}
