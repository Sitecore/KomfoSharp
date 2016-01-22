// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IKomfoProvider.cs" company="Sitecore A/S">
//  Copyright (C) 2015 by Sitecore
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace KomfoSharp.Provider
{
  using System;
  using System.Collections.Generic;
  using System.Threading.Tasks;
  using KomfoSharp.Model;

  /// <summary>
  /// Defines the methods that are used to make calls to Komfo API endpoints.
  /// </summary>
  public interface IKomfoProvider
  {
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
    Task<Token> RetrieveAccessTokenAsync(string clientId, string clientSecret, string[] scopes);

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
    /// <code>
    /// var metrics = await komfoProvider.RetrieveMetricsAsync("&lt;your_access_token&gt;", new[] { "dkoroedova", "bkoroedova" });
    /// foreach (var metric in metrics)
    /// {
    ///   this.ShowMetric(metric);
    /// }
    /// </code>
    /// </example>
    Task<IEnumerable<Metric>> RetrieveMetricsAsync(string accessToken, IEnumerable<string> ids, MetricFields fields = MetricFields.All);

    /// <summary>
    /// Retrieves the tweets asynchronously.
    /// </summary>
    /// <param name="accessToken">The access token.</param>
    /// <param name="ids">The list of Twitter handles (without "@" character).</param>
    /// <param name="since">
    /// UTC time for the earliest tweet. Always matched again the <see cref="Tweet.GatheredTime"/>. 
    /// Must be earlier than <paramref name="until"/>. 
    /// The <paramref name="since"/> can be maximally 30 days in the past. 
    /// Default value is 30 minutes in the past from the current UTC time.</param>
    /// <param name="until">
    /// UTC time for the latest tweet. Always matched again the <see cref="Tweet.GatheredTime"/>. 
    /// Must be later than <paramref name="since"/>. 
    /// Default value is the current time in UTC.</param>
    /// <param name="fields">The fields to be retrieved and filled in the <see cref="Tweet"/>.</param>
    /// <returns>
    /// The tweets list sorted by <see cref="Tweet.GatheredTime"/> from newest to oldest. If no tweets are retrieved an empty list is returned.
    /// </returns>
    /// <example>
    /// <code>
    /// var tweets = await komfoProvider.RetrieveTweetsAsync("&lt;your_access_token&gt;", new[] { "dkoroedova", "bkoroedova" });
    /// foreach (var tweet in tweets)
    /// {
    ///   this.ShowTweet(tweet);
    /// }
    /// </code>
    /// </example>
    Task<IEnumerable<Tweet>> RetrieveTweetsAsync(string accessToken, IEnumerable<string> ids, DateTime? since = null, DateTime? until = null, TweetFields fields = TweetFields.All);

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
    /// <code>
    /// var customAudiences = await komfoProvider.RetrieveCustomAudiencesAsync("&lt;your_access_token&gt;");
    /// foreach (var customAudience in customAudiences)
    /// {
    ///   this.ShowCustomAudience(customAudience);
    /// }
    /// </code>
    /// </example>
    Task<IEnumerable<CustomAudience>> RetrieveCustomAudiencesAsync(string accessToken);

    /// <summary>
    /// Retrieves the custom audience asynchronously.
    /// </summary>
    /// <param name="accessToken">The access token.</param>
    /// <param name="customAudienceId">The custom audience identifier.</param>
    /// <returns>The custom audience.</returns>
    /// <example>
    /// <code>
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
    Task<CustomAudience> RetrieveCustomAudienceAsync(string accessToken, string customAudienceId);

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
    Task<CustomAudience> DeleteCustomAudienceAsync(string accessToken, string customAudienceId);

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
    /// <code>
    /// var customAudienceId = await komfoProvider.CreateCustomAudienceAsync("&lt;your_access_token&gt;", "Sportsmen", "People who like sport");
    /// Console.WriteLine("Custom Audience ID: {0}", customAudienceId);
    /// </code>
    /// </example>
    Task<string> CreateCustomAudienceAsync(string accessToken, string name, string description);

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
    Task<int> AddEmailsToCustomAudienceAsync(string accessToken, string customAudienceId, IEnumerable<string> emails, bool hashWithSha256 = false, bool isHashed = false);

    /// <summary>
    /// Adds phone numbers to the custom audience asynchronously.
    /// </summary>
    /// <param name="accessToken">The access token.</param>
    /// <param name="customAudienceId">The custom audience identifier.</param>
    /// <param name="phoneNumbers">The phone numbers.If <paramref name="hashWithSha256"/> is set to <c>true</c> the phone numbers have to be normalized 
    /// by removing any symbols, letters, and any leading zeros. Maximum 5000 entries could be sent in single call.</param>
    /// <param name="hashWithSha256">if set to <c>true</c> the phone numbers will be hashed by SHA256 before sending to Komfo.</param>
    /// <param name="isHashed">if set to <c>true</c> the phone numbers are hashed already.</param>
    /// <returns>The number of phone numbers requested to be added to the custom audience.</returns>
    /// <example>
    /// <code>
    /// var entriesReceived = await komfoProvider.AddPhoneNumbersToCustomAudienceAsync("&lt;your_access_token&gt;", customAudienceId, new[] { "15559876543", "15559876544", "15559876545" });
    /// Console.WriteLine("Entries received: {0}", entriesReceived);
    /// </code>
    /// </example>
    Task<int> AddPhoneNumbersToCustomAudienceAsync(string accessToken, string customAudienceId, IEnumerable<string> phoneNumbers, bool hashWithSha256 = false, bool isHashed = false);

    /// <summary>
    /// Adds Facebook IDs to the custom audience asynchronously.
    /// </summary>
    /// <param name="accessToken">The access token.</param>
    /// <param name="customAudienceId">The custom audience identifier.</param>
    /// <param name="facebookIds">The Facebook IDs.</param>
    /// <param name="facebookAppIds">The Facebook application IDs (for determining the scope of <paramref name="facebookIds"/>).</param>
    /// <returns>The number of Facebook IDs requested to be added to the custom audience.</returns>
    /// <example>
    /// <code>
    /// var entriesReceived = await komfoProvider.AddFacebookIdsToCustomAudienceAsync(
    ///   "&lt;your_access_token&gt;", customAudienceId, new[] { "145452788993", "882937633", "9921392364" }, new[] { "9923273766736" });
    ///
    ///  Console.WriteLine("Entries received: {0}", entriesReceived);
    /// </code>
    /// </example>
    Task<int> AddFacebookIdsToCustomAudienceAsync(string accessToken, string customAudienceId, IEnumerable<string> facebookIds, IEnumerable<string> facebookAppIds);

    /// <summary>
    /// Removes emails from the custom audience asynchronously.
    /// </summary>
    /// <param name="accessToken">The access token.</param>
    /// <param name="customAudienceId">The custom audience identifier.</param>
    /// <param name="emails">The emails. If <paramref name="hashWithSha256"/> is set to <c>true</c> the emails have to be normalized 
    /// by trimming leading and trailing whitespace and converting all characters to lowercase.
    /// Maximum 5000 entries could be sent in single call.</param>
    /// <param name="hashWithSha256">if set to <c>true</c> the emails will be hashed by SHA256 before sending to Komfo.</param>
    /// <param name="isHashed">if set to <c>true</c> the emails are hashed already.</param>
    /// <returns>The number of emails requested to be removed from the custom audience.</returns>
    /// <example>
    /// <code>
    /// var entriesReceived = await komfoProvider.RemoveEmailsFromCustomAudienceAsync("&lt;your_access_token&gt;", customAudienceId, new[] { "user1@domain.com", "user2@domain.com", "user3@domain.com" });
    /// Console.WriteLine("Entries received: {0}", entriesReceived);
    /// </code>
    /// </example>
    Task<int> RemoveEmailsFromCustomAudienceAsync(string accessToken, string customAudienceId, IEnumerable<string> emails, bool hashWithSha256 = false, bool isHashed = false);

    /// <summary>
    /// Removes phone numbers from the custom audience asynchronously.
    /// </summary>
    /// <param name="accessToken">The access token.</param>
    /// <param name="customAudienceId">The custom audience identifier.</param>
    /// <param name="phoneNumbers">The phone numbers.If <paramref name="hashWithSha256"/> is set to <c>true</c> the phone numbers have to be normalized 
    /// by removing any symbols, letters, and any leading zeros. Maximum 5000 entries could be sent in single call.</param>
    /// <param name="hashWithSha256">if set to <c>true</c> the phone numbers will be hashed by SHA256 before sending to Komfo.</param>
    /// <param name="isHashed">if set to <c>true</c> the phone numbers are hashed already.</param>
    /// <returns>The number of phone numbers requested to be removed from the custom audience.</returns>
    /// <example>
    /// <code>
    /// var entriesReceived = await komfoProvider.RemovePhoneNumbersFromCustomAudienceAsync("&lt;your_access_token&gt;", customAudienceId, new[] { "15559876543", "15559876544", "15559876545" });
    /// Console.WriteLine("Entries received: {0}", entriesReceived);
    /// </code>
    /// </example>
    Task<int> RemovePhoneNumbersFromCustomAudienceAsync(string accessToken, string customAudienceId, IEnumerable<string> phoneNumbers, bool hashWithSha256 = false, bool isHashed = false);

    /// <summary>
    /// Removes Facebook IDs from the custom audience asynchronously.
    /// </summary>
    /// <param name="accessToken">The access token.</param>
    /// <param name="customAudienceId">The custom audience identifier.</param>
    /// <param name="facebookIds">The Facebook IDs.</param>
    /// <param name="facebookAppIds">The Facebook application IDs (for determining the scope of <paramref name="facebookIds"/>).</param>
    /// <returns>The number of Facebook IDs requested to be removed from the custom audience.</returns>
    /// <example>
    /// <code>
    /// var entriesReceived = await komfoProvider.RemoveFacebookIdsFromCustomAudienceAsync(
    ///   "&lt;your_access_token&gt;", customAudienceId, new[] { "145452788993", "882937633", "9921392364" }, new[] { "9923273766736" });
    ///
    /// Console.WriteLine("Entries received: {0}", entriesReceived);
    /// </code>
    /// </example>
    Task<int> RemoveFacebookIdsFromCustomAudienceAsync(string accessToken, string customAudienceId, IEnumerable<string> facebookIds, IEnumerable<string> facebookAppIds);

    /// <summary>
    /// Retrieves the custom audience status asynchronously.
    /// </summary>
    /// <param name="accessToken">The access token.</param>
    /// <param name="customAudienceId">The custom audience identifier.</param>
    /// <returns>The custom audience status.</returns>
    /// <example>
    /// <code>
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
    Task<CustomAudienceStatus> RetrieveCustomAudienceStatusAsync(string accessToken, string customAudienceId);

    #endregion

    #region ads/campaigns

    /// <summary>
    /// Retrieves the campaigns asynchronously.
    /// </summary>
    /// <param name="accessToken">The access token.</param>
    /// <returns>The campaigns list. If no campaigns are retrieved an empty list is returned.</returns>
    /// <example>
    /// <code>
    /// var campaigns = await komfoProvider.RetrieveCampaignsAsync("&lt;your_access_token&gt;");
    /// foreach (var campaign in campaigns)
    /// {
    ///   this.ShowCampaign(campaign);
    /// }
    /// </code>
    /// </example>
    Task<IEnumerable<Campaign>> RetrieveCampaignsAsync(string accessToken);

    /// <summary>
    /// Creates the campaign asynchronously.
    /// </summary>
    /// <param name="accessToken">The access token.</param>
    /// <param name="extCampaignId">The ID of the campaign in the external system (maximum 100 characters).</param>
    /// <param name="extCampaignKey">The URL parameter name used to pass <paramref name="extCampaignId"/>.</param>
    /// <param name="name">The name (maximum 100 characters).</param>
    /// <param name="description">The description (maximum 500 characters).</param>
    /// <returns>The ID of the newly created campaign. The ID is a string with maximum size 200 characters and is case sensitive.</returns>
    /// <example>
    /// <code>
    /// var campaignId = await komfoProvider.CreateCampaignAsync("&lt;your_access_token&gt;", "ABSDFFEHJDU0031239UEE", "ad_camp");
    /// Console.WriteLine("Campaign ID: {0}", campaignId);
    /// </code>
    /// <code>
    /// var campaignId = await komfoProvider.CreateCampaignAsync("&lt;your_access_token&gt;", "ABSDFFEHJDU0031239UEE", "ad_camp", "Summer 2015", "Targeted to coming summer 2015");
    /// Console.WriteLine("Campaign ID: {0}", campaignId);
    /// </code>
    /// </example>
    Task<string> CreateCampaignAsync(string accessToken, string extCampaignId, string extCampaignKey, string name = null, string description = null);

    /// <summary>
    /// Retrieves the custom audiences by campaign asynchronously.
    /// </summary>
    /// <param name="accessToken">The access token.</param>
    /// <param name="campaignId">The campaign identifier.</param>
    /// <returns>The custom audiences list. If no custom audiences are retrieved an empty list is returned.</returns>
    /// <example>
    /// <code>
    /// var customAudiences = await komfoProvider.RetrieveCustomAudiencesAsync("&lt;your_access_token&gt;", campaignId);
    /// foreach (var customAudience in customAudiences)
    /// {
    ///   this.ShowCustomAudience(customAudience);
    /// }
    /// </code>
    /// </example>
    Task<IEnumerable<CustomAudience>> RetrieveCustomAudiencesAsync(string accessToken, string campaignId);

    /// <summary>
    /// Adds the custom audience to the campaign asynchronously.
    /// </summary>
    /// <param name="accessToken">The access token.</param>
    /// <param name="campaignId">The campaign identifier.</param>
    /// <param name="customAudienceId">The custom audience identifier.</param>
    /// <returns>The ID of the custom audience that was requested to be added.</returns>
    /// <example>
    /// <code>
    /// await komfoProvider.AddCustomAudienceToCampaignAsync("&lt;your_access_token&gt;", campaignId, customAudienceId);
    /// </code>
    /// </example>
    Task<string> AddCustomAudienceToCampaignAsync(string accessToken, string campaignId, string customAudienceId);

    /// <summary>
    /// Removes the custom audience from the campaign asynchronously.
    /// </summary>
    /// <param name="accessToken">The access token.</param>
    /// <param name="campaignId">The campaign identifier.</param>
    /// <param name="customAudienceId">The custom audience identifier.</param>
    /// <returns>The ID of the custom audience that was requested to be removed.</returns>
    /// <example>
    /// <code>
    /// await komfoProvider.RemoveCustomAudienceFromCampaignAsync("&lt;your_access_token&gt;", campaignId, customAudienceId);
    /// </code>
    /// </example>
    Task<string> RemoveCustomAudienceFromCampaignAsync(string accessToken, string campaignId, string customAudienceId);

    #endregion
  }
}