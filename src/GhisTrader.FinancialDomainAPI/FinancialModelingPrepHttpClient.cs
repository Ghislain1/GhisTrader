// <copyright company="Ghislain One Inc.">
//  Copyright (c) GhislainOne
//  This computer program includes confidential, proprietary
//  information and is a trade secret of GhislainOne. All use,
//  disclosure, or reproduction is prohibited unless authorized in
//  writing by an officer of Ghis. All Rights Reserved.
// </copyright>

namespace GhisTrader.FinancialModelingPrepAPI;

using GhisTrader.FinancialModelingPrepAPI;
using Newtonsoft.Json;

public class FinancialModelingPrepHttpClient
{
    private readonly HttpClient httpClient;
    private readonly string apiKey;

    public FinancialModelingPrepHttpClient(HttpClient httpClient, FinancialModelingPrepAPIKey financialModelingPrepAPIKey)
    {
       this.httpClient = httpClient;
        this.apiKey = financialModelingPrepAPIKey.Key;
    }

    public async Task<T> GetAsync<T>(string uri)
    {
        HttpResponseMessage response = await this.httpClient.GetAsync($"{uri}?apikey={this.apiKey}");
        string jsonResponse = await response.Content.ReadAsStringAsync();

        return JsonConvert.DeserializeObject<T>(jsonResponse);
    }
}