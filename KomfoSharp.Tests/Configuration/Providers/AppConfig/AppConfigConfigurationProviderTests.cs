namespace KomfoSharp.Tests.Configuration.Providers.AppConfig
{
  using System;
  using System.Collections.Generic;
  using System.Xml;
  using System.Xml.Linq;
  using FluentAssertions;
  using KomfoSharp.Configuration.Endpoints;
  using KomfoSharp.Configuration.Providers;
  using KomfoSharp.Configuration.Providers.AppConfig;
  using NSubstitute;
  using NUnit.Framework;

  [TestFixture]
  public class AppConfigConfigurationProviderTests
  {
    [Test]
    public void GetConfiguration_KomfoSharpNodeIsAbsent_ThrowsException()
    {
      // Arrange
      var xmlSource = Substitute.For<IXmlSource>();
      var configurationFactory = new AppConfigConfigurationProvider(xmlSource);
      xmlSource.GetXml().Returns((XmlNode)null);

      // Act, Assert
      configurationFactory
        .Invoking((x) => x.GetConfiguration())
        .ShouldThrow<InvalidOperationException>()
        .WithMessage("Could not retrieve the 'komfoSharp' configuration node.");
    }

    [Test]
    public void GetConfiguration_PollingNodeIsAbsent_ThrowsException()
    {
      // Arrange
      var configuration = XDocument.Parse(@"<komfoSharp>
                                              <services>
                                                <endpoints baseUrl='https://connect.komfo.com'>
                                                  <tokens path='/oauth20/tokens' />
                                                  <stream path='/v1/twitter/followers/stream' maxTwitterHandlesPerCall='100' maxResultsPerCall='1000'/>
                                                  <metrics path='/v1/twitter/followers/metrics' maxTwitterHandlesPerCall='100' />
                                                </endpoints>
                                              </services>
                                            </komfoSharp>");

      var xmlSource = Substitute.For<IXmlSource>();
      var configurationFactory = new AppConfigConfigurationProvider(xmlSource);
      xmlSource.GetXml().Returns(ToXmlDocument(configuration).SelectSingleNode("komfoSharp"));

      // Act, Assert
      configurationFactory
        .Invoking((x) => x.GetConfiguration())
        .ShouldThrow<InvalidOperationException>()
        .WithMessage("Could not retrieve the 'komfoSharp/polling' configuration node.");
    }

    [Test]
    public void GetConfiguration_EndpointsNodeIsAbsent_ThrowsException()
    {
      // Arrange
      var configuration = XDocument.Parse(@"<komfoSharp>
                                              <polling defaultTimeInterval='00:00:10' defaultAttemptsCount='6'/>
                                            </komfoSharp>");

      var xmlSource = Substitute.For<IXmlSource>();
      var configurationFactory = new AppConfigConfigurationProvider(xmlSource);
      xmlSource.GetXml().Returns(ToXmlDocument(configuration).SelectSingleNode("komfoSharp"));

      // Act, Assert
      configurationFactory
        .Invoking((x) => x.GetConfiguration())
        .ShouldThrow<InvalidOperationException>()
        .WithMessage("Could not retrieve the 'komfoSharp/services/endpoints' configuration node.");
    }

    [Test]
    public void GetConfiguration_DefaultTimeIntervalAbsent_ThrowsException()
    {
      // Arrange
      var configuration = XDocument.Parse(@"<komfoSharp>
                                              <services>
                                                <endpoints baseUrl='https://connect.komfo.com'>
                                                  <tokens path='/oauth20/tokens' />
                                                  <stream path='/v1/twitter/followers/stream' maxTwitterHandlesPerCall='100' maxResultsPerCall='1000'/>
                                                  <metrics path='/v1/twitter/followers/metrics' maxTwitterHandlesPerCall='100' />
                                                </endpoints>
                                              </services>
                                              <polling defaultAttemptsCount='6'/>
                                            </komfoSharp>");

      var xmlSource = Substitute.For<IXmlSource>();
      var configurationFactory = new AppConfigConfigurationProvider(xmlSource);
      xmlSource.GetXml().Returns(ToXmlDocument(configuration).SelectSingleNode("komfoSharp"));

      // Act, Assert
      configurationFactory
        .Invoking((x) => x.GetConfiguration())
        .ShouldThrow<InvalidOperationException>()
        .WithMessage("The 'defaultTimeInterval' attribute is required for the 'komfoSharp/polling' node.");
    }

    [Test]
    public void GetConfiguration_DefaultTimeIntervalInvalid_ThrowsException()
    {
      // Arrange
      var configuration = XDocument.Parse(@"<komfoSharp>
                                              <services>
                                                <endpoints baseUrl='https://connect.komfo.com'>
                                                  <tokens path='/oauth20/tokens' />
                                                  <stream path='/v1/twitter/followers/stream' maxTwitterHandlesPerCall='100' maxResultsPerCall='1000'/>
                                                  <metrics path='/v1/twitter/followers/metrics' maxTwitterHandlesPerCall='100' />
                                                </endpoints>
                                              </services>
                                              <polling defaultTimeInterval='invalid' defaultAttemptsCount='6'/>
                                            </komfoSharp>");

      var xmlSource = Substitute.For<IXmlSource>();
      var configurationFactory = new AppConfigConfigurationProvider(xmlSource);
      xmlSource.GetXml().Returns(ToXmlDocument(configuration).SelectSingleNode("komfoSharp"));

      // Act, Assert
      configurationFactory
        .Invoking((x) => x.GetConfiguration())
        .ShouldThrow<InvalidOperationException>()
        .WithMessage("Could not parse the 'defaultTimeInterval' attribute. Only positive TimeSpan values are allowed.");
    }

    [Test]
    public void GetConfiguration_DefaultAttemptsCountAbsent_ThrowsException()
    {
      // Arrange
      var configuration = XDocument.Parse(@"<komfoSharp>
                                              <services>
                                                <endpoints baseUrl='https://connect.komfo.com'>
                                                  <tokens path='/oauth20/tokens' />
                                                  <stream path='/v1/twitter/followers/stream' maxTwitterHandlesPerCall='100' maxResultsPerCall='1000'/>
                                                  <metrics path='/v1/twitter/followers/metrics' maxTwitterHandlesPerCall='100' />
                                                </endpoints>
                                              </services>
                                              <polling defaultTimeInterval='00:00:10'/>
                                            </komfoSharp>");

      var xmlSource = Substitute.For<IXmlSource>();
      var configurationFactory = new AppConfigConfigurationProvider(xmlSource);
      xmlSource.GetXml().Returns(ToXmlDocument(configuration).SelectSingleNode("komfoSharp"));

      // Act, Assert
      configurationFactory
        .Invoking((x) => x.GetConfiguration())
        .ShouldThrow<InvalidOperationException>()
        .WithMessage("The 'defaultAttemptsCount' attribute is required for the 'komfoSharp/polling' node.");
    }

    [Test]
    public void GetConfiguration_DefaultAttemptsCountInvalid_ThrowsException()
    {
      // Arrange
      var configuration = XDocument.Parse(@"<komfoSharp>
                                              <services>
                                                <endpoints baseUrl='https://connect.komfo.com'>
                                                  <tokens path='/oauth20/tokens' />
                                                  <stream path='/v1/twitter/followers/stream' maxTwitterHandlesPerCall='100' maxResultsPerCall='1000'/>
                                                  <metrics path='/v1/twitter/followers/metrics' maxTwitterHandlesPerCall='100' />
                                                </endpoints>
                                              </services>
                                              <polling defaultTimeInterval='00:00:10' defaultAttemptsCount='invalid'/>
                                            </komfoSharp>");

      var xmlSource = Substitute.For<IXmlSource>();
      var configurationFactory = new AppConfigConfigurationProvider(xmlSource);
      xmlSource.GetXml().Returns(ToXmlDocument(configuration).SelectSingleNode("komfoSharp"));

      // Act, Assert
      configurationFactory
        .Invoking((x) => x.GetConfiguration())
        .ShouldThrow<InvalidOperationException>()
        .WithMessage("Could not parse the 'defaultAttemptsCount' attribute. Only positive integer values are allowed.");
    }

    [Test]
    public void GetConfiguration_BaseUrlAbsent_ThrowsException()
    {
      // Arrange
      var configuration = XDocument.Parse(@"<komfoSharp>
                                              <services>
                                                <endpoints>
                                                  <tokens path='/oauth20/tokens' />
                                                  <stream path='/v1/twitter/followers/stream' maxTwitterHandlesPerCall='100' maxResultsPerCall='1000'/>
                                                  <metrics path='/v1/twitter/followers/metrics' maxTwitterHandlesPerCall='100' />
                                                </endpoints>
                                              </services>
                                              <polling defaultTimeInterval='00:00:10' defaultAttemptsCount='6'/>
                                            </komfoSharp>");

      var xmlSource = Substitute.For<IXmlSource>();
      var configurationFactory = new AppConfigConfigurationProvider(xmlSource);
      xmlSource.GetXml().Returns(ToXmlDocument(configuration).SelectSingleNode("komfoSharp"));

      // Act, Assert
      configurationFactory
        .Invoking((x) => x.GetConfiguration())
        .ShouldThrow<InvalidOperationException>()
        .WithMessage("The 'baseUrl' attribute is required for the 'komfoSharp/services/endpoints' node.");
    }

    [Test]
    public void GetConfiguration_BaseUrlInvalid_ThrowsException()
    {
      // Arrange
      var configuration = XDocument.Parse(@"<komfoSharp>
                                              <services>
                                                <endpoints baseUrl='invalid'>
                                                  <tokens path='/oauth20/tokens' />
                                                  <stream path='/v1/twitter/followers/stream' maxTwitterHandlesPerCall='100' maxResultsPerCall='1000'/>
                                                  <metrics path='/v1/twitter/followers/metrics' maxTwitterHandlesPerCall='100' />
                                                </endpoints>
                                              </services>
                                              <polling defaultTimeInterval='00:00:10' defaultAttemptsCount='6'/>
                                            </komfoSharp>");

      var xmlSource = Substitute.For<IXmlSource>();
      var configurationFactory = new AppConfigConfigurationProvider(xmlSource);
      xmlSource.GetXml().Returns(ToXmlDocument(configuration).SelectSingleNode("komfoSharp"));

      // Act, Assert
      configurationFactory
        .Invoking((x) => x.GetConfiguration())
        .ShouldThrow<InvalidOperationException>()
        .WithMessage("The 'baseUrl' value should be a valid URL.");
    }

    [Test]
    public void GetConfiguration_ConfigurationIsValid_ReturnsConfiguration()
    {
      // Arrange
      var configuration = XDocument.Parse(@"<komfoSharp>
                                              <services>
                                                <endpoints baseUrl='https://connect.komfo.com/' >
                                                  <tokens path='/oauth20/tokens' />
                                                  <stream path='/v1/twitter/followers/stream' maxTwitterHandlesPerCall='100' maxResultsPerCall='1000'/>
                                                  <metrics path='/v1/twitter/followers/metrics' maxTwitterHandlesPerCall='100' />
                                                  <customAudiences path='/v1/ads/customaudiences' />
                                                  <customAudience path='/v1/ads/customaudiences/{audience_id}' />
                                                  <customAudienceUsers path='/v1/ads/customaudiences/{audience_id}/users' maxEntriesPerCall='5000' />
                                                  <customAudienceStatus path='/v1/ads/customaudiences/{audience_id}/status' />
                                                  <campaigns path='/v1/ads/campaigns' />
                                                  <campaignCustomAudiences path='/v1/ads/campaigns/{campaign_id}/customaudiences' />
                                                </endpoints>
                                              </services>
                                              <polling defaultTimeInterval='00:00:10' defaultAttemptsCount='6'/>
                                            </komfoSharp>");

      var xmlSource = Substitute.For<IXmlSource>();
      var configurationFactory = new AppConfigConfigurationProvider(xmlSource);
      xmlSource.GetXml().Returns(ToXmlDocument(configuration).SelectSingleNode("komfoSharp"));

      // Act
      var result = configurationFactory.GetConfiguration();

      // Assert
      result.PollingConfiguration.DefaultAttemptsCount.Should().Be(6);
      result.PollingConfiguration.DefaultTimeInterval.Should().Be(TimeSpan.FromSeconds(10));

      result.EndpointsConfiguration.Tokens.Path.Should().Be("/oauth20/tokens");

      result.EndpointsConfiguration.Metrics.Path.Should().Be("/v1/twitter/followers/metrics");
      result.EndpointsConfiguration.Metrics.MaxTwitterHandlesPerCall.Should().Be(100);

      result.EndpointsConfiguration.Stream.Path.Should().Be("/v1/twitter/followers/stream");
      result.EndpointsConfiguration.Stream.MaxTwitterHandlesPerCall.Should().Be(100);
      result.EndpointsConfiguration.Stream.MaxResultsPerCall.Should().Be(1000);

      result.EndpointsConfiguration.CustomAudiences.Path.Should().Be("/v1/ads/customaudiences");
      result.EndpointsConfiguration.CustomAudience.Path.Should().Be("/v1/ads/customaudiences/{audience_id}");
      result.EndpointsConfiguration.CustomAudienceUsers.Path.Should().Be("/v1/ads/customaudiences/{audience_id}/users");
      result.EndpointsConfiguration.CustomAudienceUsers.MaxEntriesPerCall.Should().Be(5000);
      result.EndpointsConfiguration.CustomAudienceStatus.Path.Should().Be("/v1/ads/customaudiences/{audience_id}/status");
      result.EndpointsConfiguration.Campaigns.Path.Should().Be("/v1/ads/campaigns");
      result.EndpointsConfiguration.CampaignCustomAudiences.Path.Should().Be("/v1/ads/campaigns/{campaign_id}/customaudiences");
    }

    [Test]
    public void GetConfiguration_PathIsNotSetForTokensEndpoint_ThrowsException()
    {
      // Arrange
      var configuration = XDocument.Parse(@"<komfoSharp>
                                              <services>
                                                <endpoints baseUrl='https://connect.komfo.com' >
                                                  <tokens />
                                                  <stream path='/v1/twitter/followers/stream' maxTwitterHandlesPerCall='100' maxResultsPerCall='1000'/>
                                                  <metrics path='/v1/twitter/followers/metrics' maxTwitterHandlesPerCall='100' />
                                                </endpoints>
                                              </services>
                                              <polling defaultTimeInterval='00:00:10' defaultAttemptsCount='6'/>
                                            </komfoSharp>");

      var xmlSource = Substitute.For<IXmlSource>();
      var configurationFactory = new AppConfigConfigurationProvider(xmlSource);
      xmlSource.GetXml().Returns(ToXmlDocument(configuration).SelectSingleNode("komfoSharp"));

      // Act, Assert
      configurationFactory
        .Invoking((x) => x.GetConfiguration())
        .ShouldThrow<InvalidOperationException>()
        .WithMessage("There were errors during deserializing the 'komfoSharp/services/endpoints/tokens' node into the KomfoSharp.Configuration.Endpoints.TokensEndpoint type. See inner exception for details.");
    }

    [Test]
    public void GetConfiguration_MaxTwitterHandlesPerCallIsNotSetForMetricsEndpoint_ThrowsException()
    {
      // Arrange
      var configuration = XDocument.Parse(@"<komfoSharp>
                                              <services>
                                                <endpoints baseUrl='https://connect.komfo.com'>
                                                  <tokens path='/oauth20/tokens' />
                                                  <stream path='/v1/twitter/followers/stream' maxTwitterHandlesPerCall='100' maxResultsPerCall='1000'/>
                                                  <metrics path='/v1/twitter/followers/metrics' />
                                                </endpoints>
                                              </services>
                                              <polling defaultTimeInterval='00:00:10' defaultAttemptsCount='6'/>
                                            </komfoSharp>");

      var xmlSource = Substitute.For<IXmlSource>();
      var configurationFactory = new AppConfigConfigurationProvider(xmlSource);
      xmlSource.GetXml().Returns(ToXmlDocument(configuration).SelectSingleNode("komfoSharp"));

      // Act, Assert
      configurationFactory
        .Invoking((x) => x.GetConfiguration())
        .ShouldThrow<InvalidOperationException>()
        .WithMessage("There were errors during deserializing the 'komfoSharp/services/endpoints/metrics' node into the KomfoSharp.Configuration.Endpoints.MetricsEndpoint type. See inner exception for details.");
    }

    [Test]
    public void GetConfiguration_MaxTwitterHandlesPerCallIsNotSetForStreamEndpoint_ThrowsException()
    {
      // Arrange
      var configuration = XDocument.Parse(@"<komfoSharp>
                                              <services>
                                                <endpoints baseUrl='https://connect.komfo.com'>
                                                  <tokens path='/oauth20/tokens' />
                                                  <stream path='/v1/twitter/followers/stream' maxResultsPerCall='1000'/>
                                                  <metrics path='/v1/twitter/followers/metrics' maxTwitterHandlesPerCall='100' />
                                                </endpoints>
                                              </services>
                                              <polling defaultTimeInterval='00:00:10' defaultAttemptsCount='6'/>
                                            </komfoSharp>");

      var xmlSource = Substitute.For<IXmlSource>();
      var configurationFactory = new AppConfigConfigurationProvider(xmlSource);
      xmlSource.GetXml().Returns(ToXmlDocument(configuration).SelectSingleNode("komfoSharp"));

      // Act, Assert
      configurationFactory
        .Invoking((x) => x.GetConfiguration())
        .ShouldThrow<InvalidOperationException>()
        .WithMessage("There were errors during deserializing the 'komfoSharp/services/endpoints/stream' node into the KomfoSharp.Configuration.Endpoints.StreamEndpoint type. See inner exception for details.");
    }

    [Test]
    public void GetConfiguration_MaxResultsPerCallIsNotSetForStreamEndpoint_ThrowsException()
    {
      // Arrange
      var configuration = XDocument.Parse(@"<komfoSharp>
                                              <services>
                                                <endpoints baseUrl='https://connect.komfo.com'>
                                                  <tokens path='/oauth20/tokens' />
                                                  <stream path='/v1/twitter/followers/stream' maxTwitterHandlesPerCall='100' />
                                                  <metrics path='/v1/twitter/followers/metrics' maxTwitterHandlesPerCall='100' />
                                                </endpoints>
                                              </services>
                                              <polling defaultTimeInterval='00:00:10' defaultAttemptsCount='6'/>
                                            </komfoSharp>");

      var xmlSource = Substitute.For<IXmlSource>();
      var configurationFactory = new AppConfigConfigurationProvider(xmlSource);
      xmlSource.GetXml().Returns(ToXmlDocument(configuration).SelectSingleNode("komfoSharp"));

      // Act, Assert
      configurationFactory
        .Invoking((x) => x.GetConfiguration())
        .ShouldThrow<InvalidOperationException>()
        .WithMessage("There were errors during deserializing the 'komfoSharp/services/endpoints/stream' node into the KomfoSharp.Configuration.Endpoints.StreamEndpoint type. See inner exception for details.");
    }

    [Test]
    public void GetConfiguration_MaxEntriesPerCallIsNotSetForCustomAudienceUsersEndpoint_ThrowsException()
    {
      // Arrange
      var configuration = XDocument.Parse(@"<komfoSharp>
                                              <services>
                                                <endpoints baseUrl='https://connect.komfo.com'>
                                                  <customAudienceUsers path='/v1/ads/customaudiences/{audience_id}/users' />
                                                </endpoints>
                                              </services>
                                              <polling defaultTimeInterval='00:00:10' defaultAttemptsCount='6'/>
                                            </komfoSharp>");

      var xmlSource = Substitute.For<IXmlSource>();
      var configurationFactory = new AppConfigConfigurationProvider(xmlSource);
      xmlSource.GetXml().Returns(ToXmlDocument(configuration).SelectSingleNode("komfoSharp"));

      // Act, Assert
      configurationFactory
        .Invoking((x) => x.GetConfiguration())
        .ShouldThrow<InvalidOperationException>()
        .WithMessage("There were errors during deserializing the 'komfoSharp/services/endpoints/customAudienceUsers' node into the KomfoSharp.Configuration.Endpoints.CustomAudienceUsersEndpoint type. See inner exception for details.");
    }

    [Test]
    public void GetEndpointUrl_AudienceIdIsPassedToCustomAudienceUsersEndpoint_UrlIsFormattedWithAudienceId()
    {
      // arrange
      var configuration = XDocument.Parse(@"<komfoSharp>
                                              <services>
                                                <endpoints baseUrl='https://connect.komfo.com'>
                                                  <customAudience path='/v1/ads/customaudiences/{audience_id}' />
                                                </endpoints>
                                              </services>
                                              <polling defaultTimeInterval='00:00:10' defaultAttemptsCount='6'/>
                                            </komfoSharp>");

      var xmlSource = Substitute.For<IXmlSource>();
      var configurationFactory = new AppConfigConfigurationProvider(xmlSource);
      xmlSource.GetXml().Returns(ToXmlDocument(configuration).SelectSingleNode("komfoSharp"));

      // act
      var endpointConfiguration = configurationFactory.GetConfiguration().EndpointsConfiguration;
      var url = endpointConfiguration.GetEndpointUrl(endpointConfiguration.CustomAudience, new Dictionary<string, string> { { CustomAudienceEndpoint.Parameters.AudienceId, "1" } });

      // assert
      url.ToString().Should().Be("https://connect.komfo.com/v1/ads/customaudiences/1");
    }

    private static XmlDocument ToXmlDocument(XDocument xDocument)
    {
      var xmlDocument = new XmlDocument();
      using (var xmlReader = xDocument.CreateReader())
      {
        xmlDocument.Load(xmlReader);
      }

      return xmlDocument;
    }
  }
}
