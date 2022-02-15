# Limbo.Integrations.AffaldPlus

.NET integration package for the AffaldPlus web service.

## Installation

Via <a href="https://www.nuget.org/packages/Limbo.Integrations.AffaldPlus/1.0.0" target="_blank">NuGet</a>:

```
dotnet add package Limbo.Integrations.AffaldPlus --version 1.0.0
```

or:

```
Install-Package Limbo.Integrations.AffaldPlus -Version 1.0.0
```

## Examples

For a given municipality (eg. `330` for Slagelse Kommune), you can get suggestions for a search phrase as shown below:

```cshtml
@using Limbo.Integrations.AffaldPlus
@using Limbo.Integrations.AffaldPlus.Models.Suggestions
@using Limbo.Integrations.AffaldPlus.Responses
@{

    // Initialize a new HTTP service
    AffaldPlusHttpService affald = new AffaldPlusHttpService();

    // Get suggestions from the AffaldPlus web service
    AffaldPlusGetSuggestionsResponse response = affald.GetSuggestions(330, "met");

    // Iterate through the suggestions
    <ul>
        @foreach (AffaldPlusSuggestion suggestion in response.Body.Items) {

            <li>
                @suggestion.Name
                <small>(@suggestion.Id)</small>
            </li>

        }
    </ul>

}
```

Each suggestion has an ID, which can then be used for getting the content of a suggestion. Eg. for **Metalskrot** which has the ID `8869`:

```cshtml
@using Limbo.Integrations.AffaldPlus
@using Limbo.Integrations.AffaldPlus.Models.Content
@using Limbo.Integrations.AffaldPlus.Responses
@{

    // Initialize a new HTTP service
    AffaldPlusHttpService affald = new AffaldPlusHttpService();

    // Get the content for "Metalskrot"
    AffaldPlusGetContentResponse response = affald.GetContent(330, 8869);

    // Get the content from the response body
    AffaldPlusContentResult content = response.Body;

    // Print out some information
    <table>
        <tr>
            <th>ID</th>
            <td>@content.Id</td>
        </tr>
        <tr>
            <th>Name</th>
            <td>@content.Name</td>
        </tr>
        <tr>
            <th>Category</th>
            <td>
                <div style="display: flex;">
                    <div style="margin-right: 10px;">
                        <img src="@content.Category.Image" alt="" />
                    </div>
                    <div>
                        <strong>@content.Category.Header</strong><br />
                        @content.Category.Body
                    </div>
                </div>
            </td>
        </tr>
        <tr>
            <th>Home</th>
            <td>
                @if (content.HasHome) {
                    <div style="display: flex;">
                        <div style="margin-right: 10px;">
                            <img src="@content.Home.Image" alt="" />
                        </div>
                        <div>
                            <strong>@content.Home.Header</strong><br />
                            @content.Home.Body
                            <p>
                                <a href="@content.Home.Link" target="_blank" rel="noopener">Read more</a>
                            </p>
                        </div>
                    </div>
                } else {
                    <em>N/A</em>
                }
            </td>
        </tr>
        <tr>
            <th>Recycling Station</th>
            <td>
                @if (content.HasRecyclingStation) {
                    <div style="display: flex;">
                        <div style="margin-right: 10px;">
                            <img src="@content.RecyclingStation.Image" alt="" />
                        </div>
                        <div>
                            <strong>@content.RecyclingStation.Header</strong><br />
                            @content.RecyclingStation.Body
                            <p>
                                <a href="@content.RecyclingStation.Link" target="_blank" rel="noopener">Read more</a>
                            </p>
                        </div>
                    </div>
                } else {
                    <em>N/A</em>
                }
            </td>
        </tr>
    </table>

}
```
