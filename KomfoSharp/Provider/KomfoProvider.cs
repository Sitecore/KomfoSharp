// --------------------------------------------------------------------------------------------------------------------
// <copyright file="KomfoProvider.cs" company="Sitecore A/S">
//  Copyright (C) 2015 by Sitecore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace KomfoSharp.Provider
{
  using System;
  using System.Collections.Generic;
  using System.Collections.Specialized;
  using System.Globalization;
  using System.IO;
  using System.Linq;
  using System.Net;
  using System.Net.Http;
  using System.Threading.Tasks;
  using System.Web;
  using KomfoSharp.Provider.Extensions;
  using KomfoSharp.Configuration;
  using KomfoSharp.Configuration.Endpoints;
  using KomfoSharp.Configuration.Providers;
  using KomfoSharp.Configuration.Providers.AppConfig;
  using KomfoSharp.Diagnostics;
  using KomfoSharp.Model;
  using Newtonsoft.Json;
  using Newtonsoft.Json.Converters;
  using Newtonsoft.Json.Linq;

  /// <summary>
  /// Provides the methods that are used to make calls to Komfo API endpoints.
  /// </summary>
  public class KomfoProvider : IKomfoProvider
  {
    /// <summary>
    /// The endpoints configuration
    /// </summary>
    private readonly EndpointsConfiguration endpointsConfiguration;

    /// <summary>
    /// The SHA256 hash
    /// </summary>
    private ISha256Hash sha256Hash;

    /// <summary>
    /// Initializes a new instance of the <see cref="KomfoProvider"/> class.
    /// </summary>
    public KomfoProvider()
      : this(new AppConfigConfigurationProvider())
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="KomfoProvider" /> class.
    /// </summary>
    /// <param name="configurationProvider">The configuration provider.</param>
    public KomfoProvider(IConfigurationProvider configurationProvider)
    {
      this.endpointsConfiguration = configurationProvider.GetConfiguration().EndpointsConfiguration;
    }

    /// <summary>
    /// Gets the SHA256 hash.
    /// </summary>
    /// <value>
    /// The SHA256 hash.
    /// </value>
    protected virtual ISha256Hash Sha256Hash
    {
      get
      {
        return this.sha256Hash ?? (this.sha256Hash = new Sha256Hash());
      }
    }

    #region oauth20/tokens

    /// <summary>
    /// Retrieves the access token asynchronously.
    /// </summary>
    /// <param name="clientId">The Komfo client identifier.</param>
    /// <param name="clientSecret">The Komfo client secret.</param>
    /// <param name="scopes">The scopes.</param>
    /// <returns>
    /// The <see cref="Token" />.
    /// </returns>
    /// <example>
    ///   <code>
    /// var token = await komfoProvider.RetrieveAccessTokenAsync("&lt;your_client_Id&gt;", "&lt;your_client_secret&gt;", new [] { "twitter_followers" });
    /// Console.WriteLine("Access Token: {0}, expires in: {1} days", token.AccessToken, token.ExpiresIn.TotalDays);
    /// </code>
    /// </example>
    public virtual async Task<Token> RetrieveAccessTokenAsync(string clientId, string clientSecret, string[] scopes)
    {
      Assert.ArgumentNotNull(clientId, "clientId");
      Assert.ArgumentNotNull(clientSecret, "clientSecret");

      var postContent = new FormUrlEncodedContent(new[]
        {
            new KeyValuePair<string, string>("grant_type", "client_credentials"), 
            new KeyValuePair<string, string>("scope", string.Join(" ", scopes)),
            new KeyValuePair<string, string>("client_id", clientId),
            new KeyValuePair<string, string>("client_secret", clientSecret)
        });

      using (var httpClient = new HttpClient())
      {
        var response = await httpClient.PostAsync(this.endpointsConfiguration.GetEndpointUrl(this.endpointsConfiguration.Tokens), postContent).ConfigureAwait(false);

        var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

        if (!response.IsSuccessStatusCode)
        {
          throw this.CreateException(content, response.StatusCode, null);
        }

        return JsonConvert.DeserializeObject<Token>(content);
      }
    }

    #endregion

    #region twitter/followers

    /// <summary>
    /// Retrieves the metrics asynchronously.
    /// </summary>
    /// <param name="accessToken">The access token.</param>
    /// <param name="ids">The list of Twitter handles (without "@" character).</param>
    /// <param name="fields">The fields to be retrieved and filled in the <see cref="Metric" />.</param>
    /// <returns>
    /// The metrics list. If no metrics are retrieved an empty list is returned.
    /// </returns>
    /// <example>
    ///   <code>
    /// var metrics = await komfoProvider.RetrieveMetricsAsync("&lt;your_access_token&gt;", new[] { "dkoroedova", "bkoroedova" });
    /// foreach (var metric in metrics)
    /// {
    ///   this.ShowMetric(metric);
    /// }
    ///   </code>
    /// </example>
    public virtual async Task<IEnumerable<Metric>> RetrieveMetricsAsync(string accessToken, IEnumerable<string> ids, MetricFields fields = MetricFields.All)
    {
      Assert.ArgumentNotNull(accessToken, "accessToken");
      Assert.ArgumentNotNull(ids, "ids");
      Assert.ArgumentCondition(fields != MetricFields.None, "fields", "At least one field should be specified.");

      var idsList = ids as IList<string> ?? ids.ToList();
      Assert.ArgumentCondition(idsList.Any(), "ids", "There should be at least one Twitter handle.");

      var queryString = HttpUtility.ParseQueryString(string.Empty);

      queryString["ids"] = string.Join(",", idsList);
        
      if (fields != MetricFields.All)
      {
        queryString["fields"] = this.ParseFlagsEnum(fields);
      }

      var responseContent = await this.SendJsonRequestAsync(HttpMethod.Get, this.endpointsConfiguration.Metrics, accessToken, null, queryString);

      return JsonConvert.DeserializeAnonymousType(responseContent, new { Data = new List<Metric>() }).Data;
    }

    /// <summary>
    /// Retrieves the tweets asynchronously.
    /// </summary>
    /// <param name="accessToken">The access token.</param>
    /// <param name="ids">The list of Twitter handles (without "@" character).</param>
    /// <param name="since">UTC time for the earliest tweet. Always matched again the <see cref="Tweet.GatheredTime" />.
    /// Must be earlier than <paramref name="until" />.
    /// The <paramref name="since" /> can be maximally 30 days in the past.
    /// Default value is 30 minutes in the past from the current UTC time.</param>
    /// <param name="until">UTC time for the latest tweet. Always matched again the <see cref="Tweet.GatheredTime" />.
    /// Must be later than <paramref name="since" />.
    /// Default value is the current time in UTC.</param>
    /// <param name="fields">The fields to be retrieved and filled in the <see cref="Tweet" />.</param>
    /// <returns>
    /// The tweets list sorted by <see cref="Tweet.GatheredTime" /> from newest to oldest. If no tweets are retrieved an empty list is returned.
    /// </returns>
    /// <example>
    ///   <code>
    /// var tweets = await komfoProvider.RetrieveTweetsAsync("&lt;your_access_token&gt;", new[] { "dkoroedova", "bkoroedova" });
    /// foreach (var tweet in tweets)
    /// {
    ///   this.ShowTweet(tweet);
    /// }
    ///   </code>
    /// </example>
    public virtual async Task<IEnumerable<Tweet>> RetrieveTweetsAsync(string accessToken, IEnumerable<string> ids, DateTime? since = null, DateTime? until = null, TweetFields fields = TweetFields.All)
    {
      Assert.ArgumentNotNull(accessToken, "accessToken");
      Assert.ArgumentNotNull(ids, "ids");
      Assert.ArgumentCondition(fields != TweetFields.None, "fields", "At least one field should be specified.");
      if (since.HasValue)
      {
        Assert.ArgumentCondition(since.Value.Kind == DateTimeKind.Utc, "since", "DateTime should be in the UTC format.");
      }

      if (until.HasValue)
      {
        Assert.ArgumentCondition(until.Value.Kind == DateTimeKind.Utc, "until", "DateTime should be in the UTC format.");
      }

      var idsList = ids as IList<string> ?? ids.ToList();
      Assert.ArgumentCondition(idsList.Any(), "ids", "There should be at least one Twitter handle.");

      var queryString = HttpUtility.ParseQueryString(string.Empty);
      
      queryString["ids"] = string.Join(",", idsList);

      if (fields != TweetFields.All)
      {
        queryString["fields"] = this.ParseFlagsEnum(fields);
      }

      if (since.HasValue)
      {
        queryString["since"] = since.Value.ToUtcIso();
      }

      if (until.HasValue)
      {
        queryString["until"] = until.Value.ToUtcIso();
      }

      var responseContent = await this.SendJsonRequestAsync(HttpMethod.Get, this.endpointsConfiguration.Stream, accessToken, null, queryString);

      return JsonConvert.DeserializeAnonymousType(responseContent, new { Data = new List<Tweet>() }).Data;
    }

    #endregion

    #region ads/customaudiences

    /// <summary>
    /// Retrieves the custom audiences asynchronously.
    /// </summary>
    /// <param name="accessToken">The access token.</param>
    /// <returns>
    /// The custom audiences list. If no custom audiences are retrieved an empty list is returned.
    /// </returns>
    /// <example>
    ///   <code>
    /// var customAudiences = await komfoProvider.RetrieveCustomAudiencesAsync("&lt;your_access_token&gt;");
    /// foreach (var customAudience in customAudiences)
    /// {
    ///   this.ShowCustomAudience(customAudience);
    /// }
    /// </code>
    /// </example>
    public virtual async Task<IEnumerable<CustomAudience>> RetrieveCustomAudiencesAsync(string accessToken)
    {
      Assert.ArgumentNotNull(accessToken, "accessToken");

      var responseContent = await this.SendJsonRequestAsync(HttpMethod.Get, this.endpointsConfiguration.CustomAudiences, accessToken);

      return JsonConvert.DeserializeAnonymousType(responseContent, new { Data = new List<CustomAudience>() }).Data;
    }

    /// <summary>
    /// Retrieves the custom audience asynchronously.
    /// </summary>
    /// <param name="accessToken">The access token.</param>
    /// <param name="customAudienceId">The custom audience identifier.</param>
    /// <returns>
    /// The custom audience.
    /// </returns>
    /// <example>
    ///   <code>
    /// try
    /// {
    ///   var customAudience = await komfoProvider.RetrieveCustomAudienceAsync("&lt;your_access_token&gt;", customAudienceId);
    ///   this.ShowCustomAudience(customAudience);
    /// }
    /// catch (KomfoProviderException ex)
    /// {
    ///   if (ex.KomfoStatusCode == KomfoStatusCode.AudienceIdIsNotValidCustomAudience)
    ///   {
    ///     Console.WriteLine("The custom audience doesn't exist.");
    ///   }
    /// }
    /// </code>
    /// </example>
    public virtual async Task<CustomAudience> RetrieveCustomAudienceAsync(string accessToken, string customAudienceId)
    {
      Assert.ArgumentNotNull(accessToken, "accessToken");
      Assert.ArgumentNotNull(customAudienceId, "campaignId");

      var responseContent = await this.SendJsonRequestAsync(
        HttpMethod.Get, 
        this.endpointsConfiguration.CustomAudience,
        accessToken,
        new Dictionary<string, string> { { CustomAudienceEndpoint.Parameters.AudienceId, customAudienceId } });

      return JsonConvert.DeserializeAnonymousType(responseContent, new { data = (CustomAudience)null }).data;
    }

    /// <summary>
    /// Deletes the custom audience asynchronously.
    /// </summary>
    /// <param name="accessToken">The access token.</param>
    /// <param name="customAudienceId">The custom audience identifier.</param>
    /// <returns>The custom audience which was removed.</returns>
    /// <example>
    /// <code>
    /// try
    /// {
    ///   var customAudience = await komfoProvider.DeleteCustomAudienceAsync("&lt;your_access_token&gt;", customAudienceId);
    ///   this.ShowCustomAudience(customAudience);
    /// }
    /// catch (KomfoProviderException ex)
    /// {
    ///   if (ex.KomfoStatusCode == KomfoStatusCode.AudienceIdIsNotValidCustomAudience)
    ///   {
    ///     Console.WriteLine("The custom audience doesn't exist.");
    ///   }
    ///
    ///   if (ex.KomfoStatusCode == KomfoStatusCode.CustomAudienceParticipateInActiveAdvertisements)
    ///   {
    ///     Console.WriteLine("Cannot delete the custom audience. The custom audience is participating in active advertisements.");
    ///   }
    /// }
    /// </code>
    /// </example>
    public async Task<CustomAudience> DeleteCustomAudienceAsync(string accessToken, string customAudienceId)
    {
      Assert.ArgumentNotNull(accessToken, "accessToken");
      Assert.ArgumentNotNull(customAudienceId, "campaignId");

      var responseContent = await this.SendJsonRequestAsync(
        HttpMethod.Delete, 
        this.endpointsConfiguration.CustomAudience,
        accessToken,
        new Dictionary<string, string> { { CustomAudienceEndpoint.Parameters.AudienceId, customAudienceId } });

      return JsonConvert.DeserializeAnonymousType(responseContent, new { data = (CustomAudience)null }).data;
    }

    /// <summary>
    /// Creates the custom audience asynchronously.
    /// </summary>
    /// <param name="accessToken">The access token.</param>
    /// <param name="name">The name (up to 100 characters).</param>
    /// <param name="description">The description (up to 500 characters).</param>
    /// <returns>
    /// The ID of the newly created empty custom audience. The ID is a string of digits and letters and is case sensitive.
    /// Maximum size 200 characters.
    /// </returns>
    /// <example>
    ///   <code>
    /// var customAudienceId = await komfoProvider.CreateCustomAudienceAsync("&lt;your_access_token&gt;", "Sportsmen", "People who like sport");
    /// Console.WriteLine("Custom Audience ID: {0}", customAudienceId);
    /// </code>
    /// </example>
    public virtual async Task<string> CreateCustomAudienceAsync(string accessToken, string name, string description)
    {
      Assert.ArgumentNotNull(accessToken, "accessToken");
      Assert.ArgumentNotNull(name, "name");
      Assert.ArgumentNotNull(description, "description");

      var responseContent = await this.SendJsonRequestAsync(
        HttpMethod.Post, 
        this.endpointsConfiguration.CustomAudiences,
        accessToken,
        null,
        null,
        JsonConvert.SerializeObject(new
        {
          name,
          description
        }));

      return JObject.Parse(responseContent)["data"]["id"].Value<string>();
    }

    /// <summary>
    /// Adds emails to the custom audience asynchronously.
    /// </summary>
    /// <param name="accessToken">The access token.</param>
    /// <param name="customAudienceId">The custom audience identifier.</param>
    /// <param name="emails">The emails. If <paramref name="hashWithSha256" /> is set to <c>true</c> the emails have to be normalized
    /// by trimming leading and trailing whitespace and converting all characters to lowercase.
    /// Maximum 5000 entries could be sent in single call.</param>
    /// <param name="hashWithSha256">if set to <c>true</c> the emails will be hashed by SHA256 before sending to Komfo.</param>
    /// <param name="isHashed">if set to <c>true</c> the emails are hashed already.</param>
    /// <returns>
    /// The number of emails requested to be added to the custom audience.
    /// </returns>
    /// <example>
    ///   <code>
    /// var entriesReceived = await komfoProvider.AddEmailsToCustomAudienceAsync("&lt;your_access_token&gt;", customAudienceId, new[] { "user1@domain.com", "user2@domain.com", "user3@domain.com" });
    /// Console.WriteLine("Entries received: {0}", entriesReceived);
    /// </code>
    /// </example>
    public virtual async Task<int> AddEmailsToCustomAudienceAsync(string accessToken, string customAudienceId, IEnumerable<string> emails, bool hashWithSha256 = false, bool isHashed = false)
    {
      return await this.UpdateUsersInCustomAudienceAsync(
        HttpMethod.Put, 
        accessToken,
        customAudienceId,
        hashWithSha256 || isHashed ? CustomAudienceUserIdentificationType.EmailSha256 : CustomAudienceUserIdentificationType.Email,
        emails,
        null,
        isHashed);
    }

    /// <summary>
    /// Adds phone numbers to the custom audience asynchronously.
    /// </summary>
    /// <param name="accessToken">The access token.</param>
    /// <param name="customAudienceId">The custom audience identifier.</param>
    /// <param name="phoneNumbers">The phone numbers.If <paramref name="hashWithSha256" /> is set to <c>true</c> the phone numbers have to be normalized
    /// by removing any symbols, letters, and any leading zeros. Maximum 5000 entries could be sent in single call.</param>
    /// <param name="hashWithSha256">if set to <c>true</c> the phone numbers will be hashed by SHA256 before sending to Komfo.</param>
    /// <param name="isHashed">if set to <c>true</c> the phone numbers are hashed already.</param>
    /// <returns>
    /// The number of phone numbers requested to be added to the custom audience.
    /// </returns>
    /// <example>
    ///   <code>
    /// var entriesReceived = await komfoProvider.AddPhoneNumbersToCustomAudienceAsync("&lt;your_access_token&gt;", customAudienceId, new[] { "15559876543", "15559876544", "15559876545" });
    /// Console.WriteLine("Entries received: {0}", entriesReceived);
    /// </code>
    /// </example>
    public virtual async Task<int> AddPhoneNumbersToCustomAudienceAsync(string accessToken, string customAudienceId, IEnumerable<string> phoneNumbers, bool hashWithSha256 = false, bool isHashed = false)
    {
      return await this.UpdateUsersInCustomAudienceAsync(
        HttpMethod.Put, 
        accessToken,
        customAudienceId,
        hashWithSha256 || isHashed ? CustomAudienceUserIdentificationType.PhoneSha256 : CustomAudienceUserIdentificationType.Phone,
        phoneNumbers,
        null,
        isHashed);
    }

    /// <summary>
    /// Adds Facebook IDs to the custom audience asynchronously.
    /// </summary>
    /// <param name="accessToken">The access token.</param>
    /// <param name="customAudienceId">The custom audience identifier.</param>
    /// <param name="facebookIds">The Facebook IDs.</param>
    /// <param name="facebookAppIds">The Facebook application IDs (for determining the scope of <paramref name="facebookIds" />).</param>
    /// <returns>
    /// The number of Facebook IDs requested to be added to the custom audience.
    /// </returns>
    /// <example>
    ///   <code>
    /// var entriesReceived = await komfoProvider.AddFacebookIdsToCustomAudienceAsync(
    ///   "&lt;your_access_token&gt;", customAudienceId, new[] { "145452788993", "882937633", "9921392364" }, new[] { "9923273766736" });
    /// Console.WriteLine("Entries received: {0}", entriesReceived);
    /// </code>
    /// </example>
    public virtual async Task<int> AddFacebookIdsToCustomAudienceAsync(string accessToken, string customAudienceId, IEnumerable<string> facebookIds, IEnumerable<string> facebookAppIds)
    {
      return await this.UpdateUsersInCustomAudienceAsync(
        HttpMethod.Put, 
        accessToken,
        customAudienceId,
        CustomAudienceUserIdentificationType.FacebookUid,
        facebookIds,
        facebookAppIds);
    }

    /// <summary>
    /// Removes emails from the custom audience asynchronously.
    /// </summary>
    /// <param name="accessToken">The access token.</param>
    /// <param name="customAudienceId">The custom audience identifier.</param>
    /// <param name="emails">The emails. If <paramref name="hashWithSha256" /> is set to <c>true</c> the emails have to be normalized
    /// by trimming leading and trailing whitespace and converting all characters to lowercase.
    /// Maximum 5000 entries could be sent in single call.</param>
    /// <param name="hashWithSha256">if set to <c>true</c> the emails will be hashed by SHA256 before sending to Komfo.</param>
    /// <param name="isHashed">if set to <c>true</c> the emails are hashed already.</param>
    /// <returns>
    /// The number of emails requested to be removed from the custom audience.
    /// </returns>
    /// <example>
    ///   <code>
    /// var entriesReceived = await komfoProvider.RemoveEmailsFromCustomAudienceAsync("&lt;your_access_token&gt;", customAudienceId, new[] { "user1@domain.com", "user2@domain.com", "user3@domain.com" });
    /// Console.WriteLine("Entries received: {0}", entriesReceived);
    /// </code>
    /// </example>
    public virtual async Task<int> RemoveEmailsFromCustomAudienceAsync(string accessToken, string customAudienceId, IEnumerable<string> emails, bool hashWithSha256 = false, bool isHashed = false)
    {
      return await this.UpdateUsersInCustomAudienceAsync(
        HttpMethod.Delete, 
        accessToken,
        customAudienceId,
        hashWithSha256 || isHashed ? CustomAudienceUserIdentificationType.EmailSha256 : CustomAudienceUserIdentificationType.Email,
        emails,
        null,
        isHashed);
    }

    /// <summary>
    /// Removes phone numbers from the custom audience asynchronously.
    /// </summary>
    /// <param name="accessToken">The access token.</param>
    /// <param name="customAudienceId">The custom audience identifier.</param>
    /// <param name="phoneNumbers">The phone numbers.If <paramref name="hashWithSha256" /> is set to <c>true</c> the phone numbers have to be normalized
    /// by removing any symbols, letters, and any leading zeros. Maximum 5000 entries could be sent in single call.</param>
    /// <param name="hashWithSha256">if set to <c>true</c> the phone numbers will be hashed by SHA256 before sending to Komfo.</param>
    /// <param name="isHashed">if set to <c>true</c> the phone numbers are hashed already.</param>
    /// <returns>
    /// The number of phone numbers requested to be removed from the custom audience.
    /// </returns>
    /// <example>
    ///   <code>
    /// var entriesReceived = await komfoProvider.RemovePhoneNumbersFromCustomAudienceAsync("&lt;your_access_token&gt;", customAudienceId, new[] { "15559876543", "15559876544", "15559876545" });
    /// Console.WriteLine("Entries received: {0}", entriesReceived);
    /// </code>
    /// </example>
    public virtual async Task<int> RemovePhoneNumbersFromCustomAudienceAsync(string accessToken, string customAudienceId, IEnumerable<string> phoneNumbers, bool hashWithSha256 = false, bool isHashed = false)
    {
      return await this.UpdateUsersInCustomAudienceAsync(
        HttpMethod.Delete, 
        accessToken,
        customAudienceId,
        hashWithSha256 || isHashed ? CustomAudienceUserIdentificationType.PhoneSha256 : CustomAudienceUserIdentificationType.Phone,
        phoneNumbers,
        null,
        isHashed);
    }

    /// <summary>
    /// Removes Facebook IDs from the custom audience asynchronously.
    /// </summary>
    /// <param name="accessToken">The access token.</param>
    /// <param name="customAudienceId">The custom audience identifier.</param>
    /// <param name="facebookIds">The Facebook IDs.</param>
    /// <param name="facebookAppIds">The Facebook application IDs (for determining the scope of <paramref name="facebookIds" />).</param>
    /// <returns>
    /// The number of Facebook IDs requested to be removed from the custom audience.
    /// </returns>
    /// <example>
    ///   <code>
    /// var entriesReceived = await komfoProvider.RemoveFacebookIdsFromCustomAudienceAsync(
    ///   "&lt;your_access_token&gt;", customAudienceId, new[] { "145452788993", "882937633", "9921392364" }, new[] { "9923273766736" });
    /// Console.WriteLine("Entries received: {0}", entriesReceived);
    /// </code>
    /// </example>
    public virtual async Task<int> RemoveFacebookIdsFromCustomAudienceAsync(string accessToken, string customAudienceId, IEnumerable<string> facebookIds, IEnumerable<string> facebookAppIds)
    {
      return await this.UpdateUsersInCustomAudienceAsync(
        HttpMethod.Delete, 
        accessToken,
        customAudienceId,
        CustomAudienceUserIdentificationType.FacebookUid,
        facebookIds,
        facebookAppIds);
    }

    /// <summary>
    /// Retrieves the custom audience status asynchronously.
    /// </summary>
    /// <param name="accessToken">The access token.</param>
    /// <param name="customAudienceId">The custom audience identifier.</param>
    /// <returns>
    /// The custom audience status.
    /// </returns>
    /// <example>
    ///   <code>
    /// try
    /// {
    ///   var customAudienceStatus = await komfoProvider.RetrieveCustomAudienceStatusAsync("&lt;your_access_token&gt;", customAudienceId);
    ///   this.ShowCustomAudienceStatus(customAudienceStatus);
    /// }
    /// catch (KomfoProviderException ex)
    /// {
    ///   if (ex.KomfoStatusCode == KomfoStatusCode.AudienceIdIsNotValidCustomAudience)
    ///   {
    ///     Console.WriteLine("The custom audience doesn't exist or you don't have access to it.");
    ///   }
    /// }
    /// </code>
    /// </example>
    public virtual async Task<CustomAudienceStatus> RetrieveCustomAudienceStatusAsync(string accessToken, string customAudienceId)
    {
      Assert.ArgumentNotNull(accessToken, "accessToken");
      Assert.ArgumentNotNull(customAudienceId, "campaignId");

      var responseContent = await this.SendJsonRequestAsync(
        HttpMethod.Get, 
        this.endpointsConfiguration.CustomAudienceStatus,
        accessToken,
        new Dictionary<string, string> { { CustomAudienceStatusEndpoint.Parameters.AudienceId, customAudienceId } });

      return JsonConvert.DeserializeAnonymousType(responseContent, new { data = (CustomAudienceStatus)null }).data;
    }

    #endregion

    #region ads/campaigns

    /// <summary>
    /// Retrieves the campaigns asynchronously.
    /// </summary>
    /// <param name="accessToken">The access token.</param>
    /// <returns>
    /// The campaigns list. If no campaigns are retrieved an empty list is returned.
    /// </returns>
    /// <example>
    ///   <code>
    /// var campaigns = await komfoProvider.RetrieveCampaignsAsync("&lt;your_access_token&gt;");
    /// foreach (var campaign in campaigns)
    /// {
    ///   this.ShowCampaign(campaign);
    /// }
    /// </code>
    /// </example>
    public virtual async Task<IEnumerable<Campaign>> RetrieveCampaignsAsync(string accessToken)
    {
      Assert.ArgumentNotNull(accessToken, "accessToken");

      var responseContent = await this.SendJsonRequestAsync(HttpMethod.Get, this.endpointsConfiguration.Campaigns, accessToken);

      return JsonConvert.DeserializeAnonymousType(responseContent, new { Data = new List<Campaign>() }).Data;
    }

    /// <summary>
    /// Creates the campaign asynchronously.
    /// </summary>
    /// <param name="accessToken">The access token.</param>
    /// <param name="extCampaignId">The ID of the campaign in the external system (maximum 100 characters).</param>
    /// <param name="extCampaignKey">The URL parameter name used to pass <paramref name="extCampaignId" />.</param>
    /// <param name="name">The name (maximum 100 characters).</param>
    /// <param name="description">The description (maximum 500 characters).</param>
    /// <returns>
    /// The ID of the newly created campaign. The ID is a string with maximum size 200 characters and is case sensitive.
    /// </returns>
    /// <example>
    ///   <code>
    /// var campaignId = await komfoProvider.CreateCampaignAsync("&lt;your_access_token&gt;", "ABSDFFEHJDU0031239UEE", "ad_camp");
    /// Console.WriteLine("Campaign ID: {0}", campaignId);
    /// </code>
    ///   <code>
    /// var campaignId = await komfoProvider.CreateCampaignAsync("&lt;your_access_token&gt;", "ABSDFFEHJDU0031239UEE", "ad_camp", "Summer 2015", "Targeted to coming summer 2015");
    /// Console.WriteLine("Campaign ID: {0}", campaignId);
    /// </code>
    /// </example>
    public virtual async Task<string> CreateCampaignAsync(string accessToken, string extCampaignId, string extCampaignKey, string name = null, string description = null)
    {
      Assert.ArgumentNotNull(accessToken, "accessToken");
      Assert.ArgumentNotNull(extCampaignId, "extCampaignId");
      Assert.ArgumentNotNull(extCampaignKey, "extCampaignKey");
      
      var responseContent = await this.SendJsonRequestAsync(
        HttpMethod.Post, 
        this.endpointsConfiguration.Campaigns,
        accessToken,
        null,
        null,
        JsonConvert.SerializeObject(new
        {
          ext_campaign_id = extCampaignId,
          ext_campaign_key = extCampaignKey,
          name,
          description
        }, Formatting.None, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.Ignore }));

      return JObject.Parse(responseContent)["data"]["id"].Value<string>();
    }

    /// <summary>
    /// Retrieves the custom audiences by campaign asynchronously.
    /// </summary>
    /// <param name="accessToken">The access token.</param>
    /// <param name="campaignId">The campaign identifier.</param>
    /// <returns>
    /// The custom audiences list. If no custom audiences are retrieved an empty list is returned.
    /// </returns>
    /// <example>
    ///   <code>
    /// var customAudiences = await komfoProvider.RetrieveCustomAudiencesAsync("&lt;your_access_token&gt;", campaignId);
    /// foreach (var customAudience in customAudiences)
    /// {
    ///   this.ShowCustomAudience(customAudience);
    /// }
    /// </code>
    /// </example>
    public virtual async Task<IEnumerable<CustomAudience>> RetrieveCustomAudiencesAsync(string accessToken, string campaignId)
    {
      Assert.ArgumentNotNull(accessToken, "accessToken");
      Assert.ArgumentNotNull(campaignId, "campaignId");

      var responseContent = await this.SendJsonRequestAsync(
        HttpMethod.Get,
        this.endpointsConfiguration.CampaignCustomAudiences,
        accessToken,
        new Dictionary<string, string> { { CampaignCustomAudiencesEndpoint.Parameters.CampaignId, campaignId } });

      return JsonConvert.DeserializeAnonymousType(responseContent, new { Data = new List<CustomAudience>() }).Data;
    }

    /// <summary>
    /// Adds the custom audience to the campaign asynchronously.
    /// </summary>
    /// <param name="accessToken">The access token.</param>
    /// <param name="campaignId">The campaign identifier.</param>
    /// <param name="customAudienceId">The custom audience identifier.</param>
    /// <returns>
    /// The ID of the custom audience that was requested to be added.
    /// </returns>
    /// <example>
    ///   <code>
    /// await komfoProvider.AddCustomAudienceToCampaignAsync("&lt;your_access_token&gt;", campaignId, customAudienceId);
    /// </code>
    /// </example>
    public virtual async Task<string> AddCustomAudienceToCampaignAsync(string accessToken, string campaignId, string customAudienceId)
    {
      return await this.UpdateCustomAudienceInCampaignAsync(HttpMethod.Put, accessToken, campaignId, customAudienceId);
    }

    /// <summary>
    /// Removes the custom audience from the campaign asynchronously.
    /// </summary>
    /// <param name="accessToken">The access token.</param>
    /// <param name="campaignId">The campaign identifier.</param>
    /// <param name="customAudienceId">The custom audience identifier.</param>
    /// <returns>
    /// The ID of the custom audience that was requested to be removed.
    /// </returns>
    /// <example>
    ///   <code>
    /// await komfoProvider.RemoveCustomAudienceFromCampaignAsync("&lt;your_access_token&gt;", campaignId, customAudienceId);
    /// </code>
    /// </example>
    public virtual async Task<string> RemoveCustomAudienceFromCampaignAsync(string accessToken, string campaignId, string customAudienceId)
    {
      return await this.UpdateCustomAudienceInCampaignAsync(HttpMethod.Delete, accessToken, campaignId, customAudienceId);
    }

    #endregion 

    #region ads helpers

    /// <summary>
    /// Updates the users in the custom audience asynchronously.
    /// </summary>
    /// <param name="httpMethod">The HTTP method.</param>
    /// <param name="accessToken">The access token.</param>
    /// <param name="customAudienceId">The custom audience identifier.</param>
    /// <param name="schema">The schema.</param>
    /// <param name="data">The data.</param>
    /// <param name="appIds">The application ids.</param>
    /// <param name="isHashed">if set to <c>true</c> the data is hashed already.</param>
    /// <returns>
    /// The number of entries requested to be added/removed from the custom audience.
    /// </returns>
    protected virtual async Task<int> UpdateUsersInCustomAudienceAsync(
      HttpMethod httpMethod,
      string accessToken, 
      string customAudienceId, 
      CustomAudienceUserIdentificationType schema, 
      IEnumerable<string> data, 
      IEnumerable<string> appIds = null,
      bool isHashed = false)
    {
      Assert.ArgumentNotNull(accessToken, "accessToken");
      Assert.ArgumentNotNull(customAudienceId, "customAudienceId");
      Assert.ArgumentNotNull(schema, "schema");
      Assert.ArgumentNotNull(data, "data");
      
      if (schema == CustomAudienceUserIdentificationType.FacebookUid)
      {
        Assert.ArgumentNotNull(appIds, "appIds");
      }

      var dataList = data as IList<string> ?? data.ToList();
      Assert.ArgumentCondition(dataList.Any(), "data", "There should be at least one data entry.");

      var responseContent = await this.SendJsonRequestAsync(
        httpMethod,
        this.endpointsConfiguration.CustomAudienceUsers,
        accessToken,
        new Dictionary<string, string> { { CustomAudienceUsersEndpoint.Parameters.AudienceId, customAudienceId } },
        null,
        JsonConvert.SerializeObject(new
        {
          payload = new
          {
            schema = schema.IdentificationType,
            data = schema.IsSha256Version && (!isHashed)
              ? dataList.Select(this.Sha256Hash.Compute).ToList()
              : dataList,
            app_ids = appIds
          }
        }, Formatting.None, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.Ignore }));

      return JObject.Parse(responseContent)["entries_received"].Value<int>();
    }

    /// <summary>
    /// Updates the custom audience in the campaign asynchronously.
    /// </summary>
    /// <param name="httpMethod">The HTTP method.</param>
    /// <param name="accessToken">The access token.</param>
    /// <param name="campaignId">The campaign identifier.</param>
    /// <param name="customAudienceId">The custom audience identifier.</param>
    /// <returns>The ID of the custom audience that was requested to be added/removed.</returns>
    protected virtual async Task<string> UpdateCustomAudienceInCampaignAsync(HttpMethod httpMethod, string accessToken, string campaignId, string customAudienceId)
    {
      Assert.ArgumentNotNull(accessToken, "accessToken");
      Assert.ArgumentNotNull(campaignId, "campaignId");
      Assert.ArgumentNotNull(customAudienceId, "customAudienceId");

      var responseContent = await this.SendJsonRequestAsync(
        httpMethod,
        this.endpointsConfiguration.CampaignCustomAudiences,
        accessToken,
        new Dictionary<string, string> { { CampaignCustomAudiencesEndpoint.Parameters.CampaignId, campaignId } },
        null,
        JsonConvert.SerializeObject(new { custom_audience_id = customAudienceId }));

      return JObject.Parse(responseContent)["data"]["id"].Value<string>();
    }

    #endregion

    #region JSON request/response

    /// <summary>
    /// Sends the JSON request asynchronously.
    /// </summary>
    /// <param name="httpMethod">The HTTP method.</param>
    /// <param name="endpoint">The endpoint configuration.</param>
    /// <param name="accessToken">The access token.</param>
    /// <param name="endpointParameters">The endpoint parameters.</param>
    /// <param name="queryString">The query string.</param>
    /// <param name="requestContent">Content of the request.</param>
    /// <returns>The response content.</returns>
    protected virtual async Task<string> SendJsonRequestAsync(
      HttpMethod httpMethod, 
      EndpointBase endpoint, 
      string accessToken, 
      IDictionary<string, string> endpointParameters = null, 
      NameValueCollection queryString = null, 
      string requestContent = null)
    {
      var uri = this.endpointsConfiguration.GetEndpointUrl(endpoint, endpointParameters);

      if (queryString != null)
      {
        uri = new UriBuilder(uri)
        {
          Query = queryString.ToString()
        }.Uri;
      }

      return await this.SendJsonRequestAsync(httpMethod, uri, accessToken, requestContent);
    }

    /// <summary>
    /// Sends the JSON request asynchronously.
    /// </summary>
    /// <param name="httpMethod">The HTTP method.</param>
    /// <param name="uri">The URI.</param>
    /// <param name="accessToken">The access token.</param>
    /// <param name="requestContent">Content of the request.</param>
    /// <returns>The response content.</returns>
    protected virtual async Task<string> SendJsonRequestAsync(HttpMethod httpMethod, Uri uri, string accessToken, string requestContent = null)
    {
      var httpWebRequest = WebRequest.CreateHttp(uri);
      httpWebRequest.ContentType = "application/json";
      httpWebRequest.Method = httpMethod.Method;
      httpWebRequest.Headers.Add("Authorization", string.Format(CultureInfo.InvariantCulture, "Bearer {0}", accessToken));

      if (!string.IsNullOrEmpty(requestContent))
      {
        using (var streamWriter = new StreamWriter(await httpWebRequest.GetRequestStreamAsync()))
        {
          await streamWriter.WriteAsync(requestContent);
        }
      }

      HttpWebResponse httpResponse;
      Exception responseException = null;

      try
      {
        httpResponse = (HttpWebResponse)await httpWebRequest.GetResponseAsync();
      }
      catch (WebException ex)
      {
        httpResponse = ex.Response as HttpWebResponse;

        if (ex.Response == null)
        {
          throw;
        }

        responseException = ex;
      }

      return await this.ProcessResponseAsync(httpResponse, responseException);
    }

    /// <summary>
    /// Processes the response asynchronously.
    /// </summary>
    /// <param name="httpResponse">The HTTP response.</param>
    /// <param name="responseException">The response exception.</param>
    /// <returns>
    /// The response content.
    /// </returns>
    protected virtual async Task<string> ProcessResponseAsync(HttpWebResponse httpResponse, Exception responseException)
    {
      var responseContent = string.Empty;
      var responseStream = httpResponse.GetResponseStream();

      if (responseStream != null)
      {
        using (var streamReader = new StreamReader(responseStream))
        {
          responseContent = await streamReader.ReadToEndAsync();
        }
      }

      if (!(httpResponse.StatusCode >= HttpStatusCode.OK && httpResponse.StatusCode <= (HttpStatusCode)299))
      {
        throw this.CreateException(responseContent, httpResponse.StatusCode, responseException);
      }

      return responseContent;
    }

    /// <summary>
    /// Creates the exception.
    /// </summary>
    /// <param name="exceptionContent">Content of the exception.</param>
    /// <param name="httpStatusCode">The HTTP status code.</param>
    /// <param name="responseException">The response exception.</param>
    /// <returns>
    /// The exception.
    /// </returns>
    protected virtual Exception CreateException(string exceptionContent, HttpStatusCode httpStatusCode, Exception responseException)
    {
      string message;
      KomfoStatusCode komfoStatusCode;

      try
      {
        var exception = JObject.Parse(exceptionContent);
        message = exception["error"]["message"].Value<string>();
        komfoStatusCode = (KomfoStatusCode)exception["error"]["code"].Value<int>();
      }
      catch (Exception exception)
      {
        if (responseException != null)
        {
          return responseException;
        }

        return new Exception(
          string.Format(CultureInfo.InvariantCulture, "An error has occurred but it could not be parsed properly: {0}. HTTP status code: {1}", exceptionContent, httpStatusCode), 
          exception);
      }

      return new KomfoProviderException(message, komfoStatusCode, httpStatusCode);
    }

    #endregion

    #region Parsers

    /// <summary>
    /// Parses the flags enum into a comma-separated string.
    /// </summary>
    /// <typeparam name="T">The enum marked with <see cref="FlagsAttribute"/>.</typeparam>
    /// <param name="flagsEnum">The flags enum.</param>
    /// <returns>Parsed enum.</returns>
    protected virtual string ParseFlagsEnum<T>(T flagsEnum)
    {
      return JsonConvert.SerializeObject(flagsEnum, new StringEnumConverter())
      .Trim('"') // Remove quotes added by JSON.NET
      .Replace(" ", string.Empty); // Remove spaces added by JSON.NET
    }

    #endregion
  }
}
